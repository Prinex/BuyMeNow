namespace BuyMeNow.ViewModels.Main;

[QueryProperty(nameof(ShoppingListDetail), "ShoppingListDetail")]
[QueryProperty(nameof(ItemListDetail), "ItemListDetail")]
public partial class AddItemPageViewModel : BaseViewModel
{
    private readonly IItemService _itemService;

    private readonly IStoreService _storeService;

    [ObservableProperty]
    private Item _itemListDetail = new Item();

    [ObservableProperty]
    private ShoppingList _shoppingListDetail = new ShoppingList();

    public AddItemPageViewModel(IItemService itemService, IStoreService storeService)
    {
        _itemService = itemService;
        _storeService = storeService;
    }

    [RelayCommand]
    public async void AddUpdateShoppingList()
    {
        _storeService.AddStores();

        var getStore = await _storeService.GetStore(ItemListDetail.StoreName); 
        if (string.IsNullOrEmpty(ItemListDetail.Title) || string.IsNullOrEmpty(ItemListDetail.StoreName) || ItemListDetail.Price == 0)
        {
            await Shell.Current.DisplayAlert("Field error", "Cannot create an item with no name, store name or price.", "OK");
            return;
        }
        else if (!string.IsNullOrEmpty(ItemListDetail.Title) && !string.IsNullOrEmpty(ItemListDetail.StoreName) && ItemListDetail.Price > 0 && getStore.IsExistent == false)
        {
            await Shell.Current.DisplayAlert("Field error", $"{ItemListDetail.StoreName} is currently unavailable.", "OK");
            return;
        }
        else
        {
            bool response = false;
            if (ItemListDetail.ItemID > 0)
            {
                response = await _itemService.UpdateItem(ItemListDetail);
            }
            else
            {
                response = await _itemService.AddItem(new Item
                {
                    ShoppingListID = ShoppingListDetail.ShoppingListID,
                    StoreName = getStore.Name,
                    Title = ItemListDetail.Title,
                    Price = ItemListDetail.Price,
                });
            }
            if (response == true)
                await Shell.Current.DisplayAlert("Item saved", $"Your item has been saved to {ShoppingListDetail.ShoppingListName} list.", "OK");
            else
                await Shell.Current.DisplayAlert("Error", "Something went wrong while trying to save the item in the shopping list.", "OK");
        }
    
    }
}

