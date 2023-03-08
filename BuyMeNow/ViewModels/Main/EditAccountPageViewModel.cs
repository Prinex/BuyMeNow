namespace BuyMeNow.ViewModels.Main;

public partial class EditAccountPageViewModel : BaseViewModel
{
    private readonly IAccountService _accountService;

    [ObservableProperty]
    private Account changeUserDetails = new Account();

    public EditAccountPageViewModel(IAccountService accountService)
    {
        // initialize the database service for account
        _accountService = accountService;

        // the current details are the ones from the base model
        if (App.UserDetails != null)
        {
            ChangeUserDetails.UserID = App.UserDetails.UserID;    
            ChangeUserDetails.Username = App.UserDetails.Username;
            ChangeUserDetails.Password = App.UserDetails.Password; 
            ChangeUserDetails.Postcode = App.UserDetails.Postcode;
        }
    }

    [RelayCommand]
    public async Task Update()
    {
        var currentUserDetails = new Account();
        bool response = false;

        // get the account details by username to check to see if there are differences for updating
        currentUserDetails = await _accountService.GetAccount(App.UserDetails.Username);

        if (string.IsNullOrEmpty(ChangeUserDetails.Username) || string.IsNullOrEmpty(ChangeUserDetails.Password) || string.IsNullOrEmpty(ChangeUserDetails.Postcode))
            await Shell.Current.DisplayAlert("Updating error", "Cannot update account with empty fields.", "OK");
        else if (!string.IsNullOrEmpty(ChangeUserDetails.Username) && !string.IsNullOrEmpty(ChangeUserDetails.Password) && !string.IsNullOrEmpty(ChangeUserDetails.Postcode))
        {   
            if (currentUserDetails.Username == ChangeUserDetails.Username && BCrypt.Net.BCrypt.Verify(ChangeUserDetails.Password, currentUserDetails.Password) == true
                && currentUserDetails.Postcode == ChangeUserDetails.Postcode)
                await Shell.Current.DisplayAlert("Updating error", "There are no changes being made.", "OK");
            else
            {
                currentUserDetails.Username = ChangeUserDetails.Username;
                currentUserDetails.Password = BCrypt.Net.BCrypt.HashPassword(ChangeUserDetails.Password);
                currentUserDetails.Postcode = ChangeUserDetails.Postcode;
                response = await _accountService.UpdateAccount(currentUserDetails);
                currentUserDetails.Password = ChangeUserDetails.Password;

                if (response == true)
                {
                    await Shell.Current.DisplayAlert("Succesfull update", "Your account details have been updated.", "OK");
                   
                    // Update session data
                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                        Preferences.Remove(nameof(App.UserDetails));
              
                    string userDetailsStr = JsonConvert.SerializeObject(currentUserDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailsStr);

                    // saving "data session" with preferences: user id and a preference whether the
                    // user wants to be remembered next time when opening the app
                    App.UserDetails = currentUserDetails;
                }
                else
                    await Shell.Current.DisplayAlert("Update error", "Something went wrong while trying to update your account details.", "OK");
            }
        }  
    }
}

