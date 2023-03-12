namespace BuyMeNow.ViewModels.Main;

public partial class ShoppingListsPageViewModel : BaseViewModel
{
    private readonly IShoppingListService _shoppingListService;

    private readonly IItemService _itemListService;

    public static List<ShoppingList> ShoppingListsDB { get; private set; } = new List<ShoppingList>();

    public ObservableCollection<ShoppingList> Lists { get; set; } = new ObservableCollection<ShoppingList>();

    public ShoppingListsPageViewModel(IShoppingListService shoppingListService, IItemService itemListService)
    {
        _shoppingListService = shoppingListService;
        _itemListService = itemListService;
    }

    public async Task GetShoppingLists()
    {
        Lists.Clear();

        var shoppingLists = await _shoppingListService.GetShoppingLists(App.UserDetails.UserID);
        
        if (shoppingLists?.Count > 0) 
        {
            shoppingLists = shoppingLists.OrderBy(i => i.UserID).ToList();
            foreach (var list in shoppingLists) 
            {
                Lists.Add(list);
            }
            ShoppingListsDB.Clear();
            ShoppingListsDB.AddRange(shoppingLists);
        }
    }

    [RelayCommand]
    public async Task CreateShoppingList()
    {
        await Shell.Current.GoToAsync($"/{nameof(AddUpdateShoppingListPage)}");
    }

    [RelayCommand]
    public async Task DecideAction(ShoppingList shoppingListModel)
    {
        var response = await Shell.Current.DisplayActionSheet("Select option", "OK", null, "Add Item", "Edit", "Delete");
        if (response == "Add Item")
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ShoppingListDetail", shoppingListModel);
            await Shell.Current.GoToAsync($"/{nameof(ShoppingListPage)}", navParam);
        }
        if (response == "Edit")
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ShoppingListDetail", shoppingListModel);
            await Shell.Current.GoToAsync($"/{nameof(AddUpdateShoppingListPage)}", navParam);
        }
        else if (response == "Delete")
        {
            var confirmation = await Shell.Current.DisplayActionSheet("Are you sure?", "No", null, "Yes");

            // delete the items from the shopping list
            if (confirmation == "Yes")
            {
                var delItems = await _itemListService.GetItemList(shoppingListModel.ShoppingListID);

                for (int i = 0; i < delItems.Count; i++)
                {
                    var delItem = await _itemListService.DeleteItem(delItems[i]);
                    if (delItem == false)
                    {
                        await Shell.Current.DisplayAlert("Deleting error", $"Something went wrong while trying to delete the items from {shoppingListModel.ShoppingListName} list.", "OK");
                    }
                }
                // delete the shopping list
                var delResponse = await _shoppingListService.DeleteShoppingList(shoppingListModel);
                if (delResponse == true)
                {
                    await GetShoppingLists();
                }
            }
        }
    }
}

