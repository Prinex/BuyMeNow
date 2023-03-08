namespace BuyMeNow.ViewModels.Main;

[QueryProperty(nameof(ShoppingListDetail), "ShoppingListDetail")]
public partial class AddUpdateShoppingListPageViewModel : BaseViewModel
{
    private readonly IShoppingListService _shoppingListService;

    [ObservableProperty]
    private ShoppingList _shoppingListDetail = new ShoppingList();

    public AddUpdateShoppingListPageViewModel(IShoppingListService shoppingListService)
    {
        _shoppingListService = shoppingListService;
    }

    [RelayCommand]
    public async void AddUpdateShoppingList()
    {
        if (string.IsNullOrEmpty(ShoppingListDetail.ShoppingListName))
        {
            await Shell.Current.DisplayAlert("Field error", "Cannot create a list with an empty field.", "OK");
            return;
        }    
        else
        {
            bool response = false;
            if (ShoppingListDetail.ShoppingListID > 0)
            {
                response = await _shoppingListService.UpdateShoppingList(ShoppingListDetail);
            }
            else
            {
                response = await _shoppingListService.AddShoppingList(new ShoppingList
                {
                    UserID = App.UserDetails.UserID,
                    ShoppingListName = ShoppingListDetail.ShoppingListName
                });
            }
            if (response == true)
                await Shell.Current.DisplayAlert("List saved", "You shopping list has been saved successfully.", "OK");
            else
                await Shell.Current.DisplayAlert("Error", "Something went wrong while trying to save the shopping list.", "OK");
               
            
        }

    }
}

