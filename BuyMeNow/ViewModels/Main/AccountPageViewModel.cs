namespace BuyMeNow.ViewModels.Main;


public partial class AccountPageViewModel : BaseViewModel
{
    private readonly IAccountService _accountService;

    private readonly IItemService _itemSerivce;

    private readonly IItemInteractionHistoryService _interactionHistoryService;

    private readonly IShoppingListService _shoppingListService;

    [ObservableProperty]
    string username;

    [ObservableProperty]
    string postcode;
    
    public AccountPageViewModel(IAccountService accountService, IItemService itemService, IItemInteractionHistoryService itemInteractionHistoryService, IShoppingListService shoppingListService)
    {
        // initialize the database service for account
        _accountService = accountService;
        _itemSerivce = itemService;
        _interactionHistoryService = itemInteractionHistoryService;
        _shoppingListService= shoppingListService;

        // the current details are the ones from the base model
        if (App.UserDetails != null)
        {
            // won't display the password here, but when editing since the password 
            // should not be seen at this point
            Username = App.UserDetails.Username;
            Postcode = App.UserDetails.Postcode;
        }
    }

    public void UpdateAccountDetails()
    {
        // update the credentials with in this page with the help of session data
        if (App.UserDetails != null)
        {
            Username = App.UserDetails.Username;
            Postcode = App.UserDetails.Postcode;
        }
    }

    // passing account model when navigating to EditAccountPage
    [RelayCommand]
    public async Task Edit(Account accountModel)
    { 
        await Shell.Current.GoToAsync(nameof(EditAccountPage));
    }

    [RelayCommand]
    public async Task Delete()
    {
        var response = await AppShell.Current.DisplayActionSheet("Are you sure you want to delete your account?", "Cancel", null, "Delete");

        if (response == "Delete") 
        {
            // Delete any session data
            var items = await _itemSerivce.GetItemList(App.UserDetails.UserID);
            var history = await _interactionHistoryService.GetItemInteractionsList(App.UserDetails.UserID);
            var lists = await _shoppingListService.GetShoppingLists(App.UserDetails.UserID);

            for (int i = 0; i < items.Count; i++) 
            {
                var delItem = await _itemSerivce.DeleteItem(items[i]);
                if (delItem == false)
                    await Shell.Current.DisplayAlert("Error", "Something went wrong while trying to delete your account.", "OK");
            }

            for (int i = 0; i < history.Count; i++)
            {
                var delItem = await _interactionHistoryService.DeleteItemInteraction(history[i]);
                if (delItem == false)
                    await Shell.Current.DisplayAlert("Error", "Something went wrong while trying to delete your account.", "OK");
            }

            for (int i = 0; i < lists.Count; i++)
            {
                var delItem = await _shoppingListService.DeleteShoppingList(lists[i]);
                if (delItem == false)
                    await Shell.Current.DisplayAlert("Error", "Something went wrong while trying to delete your account.", "OK");
            }

            var deleteAccount = await _accountService.GetAccount(App.UserDetails.Username);
            var delResponse = await _accountService.DeleteAccount(deleteAccount);

            if (delResponse == true)
            {
                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    Preferences.Remove(nameof(App.UserDetails));
                if (Preferences.ContainsKey(App.UserDetails.UserID.ToString()) == true)
                    Preferences.Remove(App.UserDetails.UserID.ToString());

                await Shell.Current.DisplayAlert("Account deleted", "Your account has been successfully deleted.", "OK");
                await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
            }
        }
    }
}

