using Geocoding;
using Geocoding.Microsoft;
using Microsoft.Maui.Devices.Sensors;

namespace BuyMeNow.ViewModels.Main;

public partial class RecommendationsPageViewModel : BaseViewModel
{
    // you need history services here and account
    // need to implement predict command - function
    // need a recommend button, which in the back end is actually prediction - recommendation of items based on the history
    // a clear command for the recommended items
    // save the recommendations in preferences
    // for the recommendation just associate the name of the product with the name of the existent one from ExistentItems table
    private readonly IItemInteractionHistoryService _interactionHistoryService;

    private readonly IStoreService _storeService;

    private readonly IExistentItemService _existentItemService;

    // service used for checking internet connectivity 
    IConnectivity _connectivity;
    IGeolocation _geolocation;
    IMap _map;
    public ObservableCollection<ExistentItem> ExistentItems { get; set; } = new ObservableCollection<ExistentItem>();

    public RecommendationsPageViewModel(IItemInteractionHistoryService interactionHistoryService, IExistentItemService existentItemService, 
        IConnectivity connectivity, IGeolocation geolocation, IStoreService storeService, IMap map)
    {
        _interactionHistoryService = interactionHistoryService;
        _existentItemService = existentItemService;
        _connectivity = connectivity;
        _geolocation = geolocation;
        _storeService = storeService;
        _map = map;
    }

    [RelayCommand]
    public async Task AddRecommendations()
    {
        // add existent items here; only once will be added
        _existentItemService.AddItems();
        ExistentItems.Clear();
        
        // based on the history of the user recommend other items
        // so retrieve the history of the user according to the user id
        var itemsInteractionHistory = await _interactionHistoryService.GetItemInteractionsList(App.UserDetails.UserID);

        if (itemsInteractionHistory.Count == 0)
        {
            await Shell.Current.DisplayAlert("No interactions", "Your history is empty, add items and reviews to these so you can get recommendations", "OK");
            return;
        }

        // making recommendations
        
        // first need to have model inputs according to the history of the user
        // so retrieve all history items of the user and append it the the model input for the recommendation system
        List<CollaborativeFilteringModel.ModelInput> collaborativeFilteringModels = new List<CollaborativeFilteringModel.ModelInput>();

        
        
        for (int i = 0; i < itemsInteractionHistory.Count; i++) 
        {
            // get the associtated item id from the existent items 
            var actualItem = await _existentItemService.GetItem(itemsInteractionHistory[i].ItemTitle);
            // create model input for with each item from the history
            if (actualItem.IsExistent is not false)
            {
                var itemHistory = new CollaborativeFilteringModel.ModelInput()
                {
                    UserID = App.UserDetails.UserID,
                    ItemID = actualItem.ItemID,
                    Quantity = itemsInteractionHistory[i].Quantity,
                };
                // append the model input for the ml to a list
                collaborativeFilteringModels.Add(itemHistory);
            }
            else
            {
                await Shell.Current.DisplayAlert("Recommendation error", "Item has little interaction or nothing at all.", "OK");
                return;
            }
        }

        // Prediction part: predict, make a recommendation for each item from the history using the list of model inputs defined above
        // and search the recommended item in the items dataset and add it to the observable collection to display its name/title 
        // knowing that items dataset does not contain titles of items only their IDs
        // the observable collection is saved in preferences and will be retrieved for next session when user logs back

        // first check if there are recommendations in preferences and delete them
        //if (Preferences.ContainsKey("Recommendations") == true)
        //   Preferences.Remove("Recommendations");

        List<ExistentItem> recommendations = new List<ExistentItem>();
        foreach (var i in collaborativeFilteringModels)
        {
            // make prediction
            var recommendedItem = CollaborativeFilteringModel.Predict(i);
            
            // search for the item in the items table (ExistentItem - to be specific according to the database) by using item ID
            var existentItem = await _existentItemService.GetItemByID(Convert.ToInt32(recommendedItem.ItemID));
            recommendations.Add(existentItem);
        }
        // add the recommendations to the preferences
        // serialize the recommendations list
        // this will be done for each user : can make use of dictionaries, i.e, each user in a dictionary will have a range of recommendations        
        string serializedRecommendations = JsonConvert.SerializeObject(recommendations);
        // save  the recommendations for the user using the user id: App.Userdetails.UserID being the current logged user
        Preferences.Set(App.UserDetails.UserID.ToString(), serializedRecommendations);
        await GetRecommendations();
    }

    public async Task GetRecommendations()
    { 
        // get the recommendations and add them to the observable collection then which will be displayed
        ExistentItems.Clear();

        // get the recommendations
        string serializedRecommendations = Preferences.Get(App.UserDetails.UserID.ToString(), null);
        List<ExistentItem> recommendations = new List<ExistentItem>();

        if (serializedRecommendations != null)
            recommendations = JsonConvert.DeserializeObject<List<ExistentItem>>(serializedRecommendations);

        if (recommendations?.Count > 0)
        {
            foreach (var r in recommendations)
            {
                ExistentItems.Add(r);
            }
        }
    }

    [RelayCommand]
    public async Task ClearRecommendations()
    {
        if (Preferences.ContainsKey(App.UserDetails.UserID.ToString()) == true)
        {
            var decide = await Shell.Current.DisplayActionSheet("Warning", "OK", null, "Yes");
            if (decide == "Yes")
            {
                Preferences.Remove(App.UserDetails.UserID.ToString());
                await GetRecommendations();
            }
        }
        else
            await Shell.Current.DisplayAlert("Warning", "There are no recommendations to clear out.", "OK");
    }

    [RelayCommand]
    public async Task DecideAction(ExistentItem model)
    {
        var response = await Shell.Current.DisplayActionSheet("Locate item", "OK", null, "Get Distance", "Locate");

        // based on this example of converting a postcode to longitude & latitude : https://github.com/chadly/Geocoding.net/blob/master/README.md -  
        // convert postcode of the user to latitude longitude and calculate the distance between the user and the store
        // this uses bing api maps
        string bingKey = "AlIJtQ8qFdmEJKOKUOINwh--7niJ9xKzj1gJIdQwLZxWFQ0Dhj0tBZQJnjhz4a3d";
        IGeocoder geocoder = new BingMapsGeocoder(bingKey);

        IEnumerable<Address> address = geocoder.Geocode(App.UserDetails.Postcode);

        double latitudeUser = address.First().Coordinates.Latitude;
        double longitudeUser = address.First().Coordinates.Longitude;

        if (response == "Get Distance") 
        {
            try
            {
                // check for internet connectivity
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Warning", "There is no internet connection.", "OK");
                    return;
                }
                
                // get longitude latitude of store
                var storeCoordinates = await _storeService.GetStore(model.StoreName);

                // initiate all coordinates for calculation process using defined method from System.Device.Location
                // CALCULATING THE DISTANCE FROM USER TO STORE
                double distance = GeoCalculator.GetDistance(latitudeUser, longitudeUser, storeCoordinates.Latitude, storeCoordinates.Longitude, distanceUnit: DistanceUnit.Miles);
                
                await Shell.Current.DisplayAlert("Walking distance", $"Walking distance to {model.StoreName} is {distance} mile(s) away.", "OK");
            }
            catch (Exception ex) 
            {
                await Shell.Current.DisplayAlert("Error", $"The store cannot be located: {ex}.", "OK");
                return;
            }
        }
        else if (response == "Locate")
        {
            try
            {
                var storeCoordinates = await _storeService.GetStore(model.StoreName);
                // open link to maps with the predefined latitude-longitude coordinates 
                var urlGoogleMapsLocation = $"https://www.google.com/maps/dir/?api=1&origin={latitudeUser},{longitudeUser}&destination={storeCoordinates.Latitude}, {storeCoordinates.Longitude}";
                await Launcher.OpenAsync(new Uri(urlGoogleMapsLocation));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Maps cannot be opened: {ex}.", "OK");
                return;
            }
        }
    }
}

