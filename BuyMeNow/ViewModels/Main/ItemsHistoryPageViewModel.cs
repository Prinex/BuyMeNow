namespace BuyMeNow.ViewModels.Main;

public partial class ItemsHistoryPageViewModel : BaseViewModel
{
    private readonly IItemInteractionHistoryService _interactionHistoryService;

    public ObservableCollection<ItemInteractionHistory> Items { get; set; } = new ObservableCollection<ItemInteractionHistory>();

    public ItemsHistoryPageViewModel(IItemInteractionHistoryService interactionHistoryService)
    {
        _interactionHistoryService = interactionHistoryService;
    }

    [RelayCommand]
    public async void GetItemInteractionHistory()
    {
        Items.Clear();

        var itemsInteractionHistory = await _interactionHistoryService.GetItemInteractionsList(App.UserDetails.UserID);

        if (itemsInteractionHistory?.Count > 0)
        {
            itemsInteractionHistory = itemsInteractionHistory.OrderBy(i => i.InteractionID).ToList();
            foreach (var item in itemsInteractionHistory)
            {
                Items.Add(item);
            }
        }
    }

    [RelayCommand]
    public async void ClearHistory()
    {
        var confirmation = await Shell.Current.DisplayActionSheet("Are you sure you want to clear the history?", "No", null, "Yes");
        var itemsInteractionHistory = await _interactionHistoryService.GetItemInteractionsList(App.UserDetails.UserID);
        
        if (confirmation == "Yes")
        {
            
            for (int i = 0; i < itemsInteractionHistory.Count; i++)
            {
                bool delResponse = await _interactionHistoryService.DeleteItemInteraction(itemsInteractionHistory[i]);
                if (delResponse == false)
                {
                    await Shell.Current.DisplayAlert("Deletion error", "Something went wrong while trying to delete the item interaction history.", "OK");
                }
            }

        }
    }
}

