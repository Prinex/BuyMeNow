namespace BuyMeNow.ViewModels.Main;

[QueryProperty(nameof(ShoppingListDetail), "ShoppingListDetail")]
public partial class ShoppingListPageViewModel : BaseViewModel
{
    private readonly IShoppingListService _shoppingListService;

    private readonly IItemService _itemListService;

    [ObservableProperty]
    private ShoppingList _shoppingListDetail = new ShoppingList();

    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

    public ShoppingListPageViewModel(IShoppingListService shoppingListService, IItemService itemService)
    {
        _shoppingListService = shoppingListService;
        _itemListService = itemService;
    }

    public async Task GetShoppingList()
    {
        Items.Clear();
    
        var itemsList = await _itemListService.GetItemList(ShoppingListDetail.ShoppingListID);
    
        if (itemsList?.Count > 0)
        {
            itemsList = itemsList.OrderBy(i => i.ShoppingListID).ToList();
            foreach (var list in itemsList)
            {
                Items.Add(list);
            }
        }
    }

    [RelayCommand]
    public async Task AddShoppingListItem()
    {
        var navParam = new Dictionary<string, object>();
        navParam.Add("ShoppingListDetail", ShoppingListDetail);
        await Shell.Current.GoToAsync($"/{nameof(AddItemPage)}", navParam); 
    }

    [RelayCommand]
    public async Task DecideAction(Item itemListModel)
    {
        var response = await Shell.Current.DisplayActionSheet("Select option", "OK", null, "Edit", "Delete", "Review");
        
        if (response == "Edit")
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ShoppingListDetail", ShoppingListDetail);
            navParam.Add("ItemListDetail", itemListModel);
            await Shell.Current.GoToAsync($"/{nameof(AddItemPage)}", navParam);
        }
        else if (response == "Delete")
        {
            var confirmation = await Shell.Current.DisplayActionSheet("Are you sure?", "No", null, "Yes");

            if (confirmation == "Yes")
            {
                var delResponse = await _itemListService.DeleteItem(itemListModel);
                if (delResponse == true)
                {
                    await GetShoppingList();
                }
            }
        }
        else if (response == "Review")
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ShoppingListDetail", ShoppingListDetail);
            navParam.Add("ItemListDetail", itemListModel);
            await Shell.Current.GoToAsync($"/{nameof(AddItemInteractionHistoryPage)}", navParam);
        }
    }
}



