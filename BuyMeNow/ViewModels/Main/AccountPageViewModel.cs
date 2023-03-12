namespace BuyMeNow.ViewModels.Main;


public partial class AccountPageViewModel : BaseViewModel
{
    private readonly IAccountService _accountService;

    [ObservableProperty]
    string username;

    [ObservableProperty]
    string postcode;
    
    public AccountPageViewModel(IAccountService accountService)
    {
        // initialize the database service for account
        _accountService = accountService;

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
            var deleteAccount = await _accountService.GetAccount(App.UserDetails.Username);
            var delResponse = await _accountService.DeleteAccount(deleteAccount);

            if (delResponse == true)
            {
                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    Preferences.Remove(nameof(App.UserDetails));
                await Shell.Current.DisplayAlert("Account deleted", "Your account has been successfully deleted.", "OK");
                await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
            }
        }
    }
}

