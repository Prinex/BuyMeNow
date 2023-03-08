namespace BuyMeNow.ViewModels.Main;

[QueryProperty(nameof(ShoppingListDetail), "ShoppingListDetail")]
[QueryProperty(nameof(ItemListDetail), "ItemListDetail")]
public partial class AddItemInteractionHistoryPageViewModel : BaseViewModel
{
    private readonly IItemService _itemService;

    private readonly IItemInteractionHistoryService _historyService;

    [ObservableProperty]
    private Item _itemListDetail = new Item();

    [ObservableProperty]
    private ShoppingList _shoppingListDetail = new ShoppingList();

    public AddItemInteractionHistoryPageViewModel(IItemService itemService, IItemInteractionHistoryService historyService)
    {
        _itemService = itemService;
        _historyService = historyService;
    }

    [RelayCommand]
    public async Task AddInteractionHistory()
     {
        if (ItemListDetail.Rating == 0 && ItemListDetail.Quantity == 0)
        {
            await Shell.Current.DisplayAlert("Field error", "Cannot give 0 value for rating and quantity.", "OK");
            return;
        }
        else if (ItemListDetail.Rating == 0)
        {
            await Shell.Current.DisplayAlert("Field error", "The item rating should between on a scale from 1-5 stars.", "OK");
            return;
        }
        else if (ItemListDetail.Quantity == 0) 
        {
            await Shell.Current.DisplayAlert("Field error", "The qunatity should be greater than 0.", "OK");
            return;
        }
        else
        {
            // NOTE TOMORROW MORNING FINISH THIS: FIND A WAY TO INSERT AN ITEM WITHOUT ANY PROBLEMS
            bool responseItemListElement = false;
            bool responseInteractionItemHistory = false;
            var itemInteraction = await _historyService.GetItemInteraction(ShoppingListDetail.UserID, ItemListDetail.ItemID);
            
            responseItemListElement = await _itemService.UpdateItem(ItemListDetail);
            if (itemInteraction.IsExistent == true)
            {
                itemInteraction.Rating = ItemListDetail.Rating;
                itemInteraction.Quantity = ItemListDetail.Quantity;
                responseInteractionItemHistory = await _historyService.UpdateItemInteraction(itemInteraction);
            }
            else
            {
                responseInteractionItemHistory = await _historyService.AddItemInteraction(new ItemInteractionHistory
                {
                    UserID = ShoppingListDetail.UserID,
                    StoreName = ItemListDetail.StoreName,
                    ItemTitle = ItemListDetail.Title,
                    Rating = ItemListDetail.Rating,
                    Quantity = ItemListDetail.Quantity
                });
            }
            if (responseInteractionItemHistory == true)
                await Shell.Current.DisplayAlert("Item reviewed", $"Your item review been saved in the history section.", "OK");
            else
                await Shell.Current.DisplayAlert("Error", "Something went wrong while trying to save the item review in the shopping list.", "OK");
        }
    }

}
