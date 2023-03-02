namespace BuyMeNow.ViewModels.Startup;

public partial class SigninPageViewModel : BaseViewModel
{
    [ObservableProperty]
    public bool rememberUser = false;

    private readonly IAccountService _accountService;

    [ObservableProperty]
    private Account accountDetails = new Account();

    public SigninPageViewModel(IAccountService accountService)
    {
        _accountService = accountService;

        if (App.UserDetails != null)
        {
            // UserDetails with the help of preferences, the "session data"
            // when logging out and logging back previous user information will be displayed
            AccountDetails.Username = App.UserDetails.Username;
            AccountDetails.Password = App.UserDetails.Password;
        }
    }

    [RelayCommand]
    public async void Signin()
    {
        var userDetails = new Account();
        // here the most important thing is checking the user credentials. if they exists or not in the database, and their validity
        if (string.IsNullOrEmpty(AccountDetails.Password) || string.IsNullOrEmpty(AccountDetails.Password))
            await Shell.Current.DisplayAlert("Authentication error", "Not all fields are provided. Provide your username and password in order to login.", "OK");
        if (!string.IsNullOrEmpty(AccountDetails.Username) && !string.IsNullOrEmpty(AccountDetails.Password))
        {
            // query the database to get the user credentials according to the username
            userDetails = await _accountService.GetAccount(AccountDetails.Username);
            
            if (userDetails.IsExistent == false)
            {
                // catch invalid/inexistent username NullReferenceException (can't do it with try-catch)
                await Shell.Current.DisplayAlert($"Authentication issue for: {AccountDetails.Username}", $"Username does not exist.", "OK");
            }
            // verify the if user exists and the password 
            if (userDetails.IsExistent != false)
            {
                if (BCrypt.Net.BCrypt.Verify(AccountDetails.Password, userDetails.Password) == true)
                {
                    // remove previous "session data"
                    if (Preferences.ContainsKey(nameof(RememberUser)) || Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(RememberUser));
                        Preferences.Remove(nameof(App.UserDetails));
                    }
                    // if the user does want to keep inserting the credentials next time
                    if (RememberUser == true)
                        Preferences.Set(nameof(RememberUser), RememberUser);

                    // store the password unhashed in the session data
                    userDetails.Password = AccountDetails.Password;
                    // saving "data session" with preferences: user id and a preference whether the
                    // user wants to be remembered next time when opening the app
                    string userDetailsStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailsStr);
                    // update data session 
                    App.UserDetails = userDetails;

                    // go to home page
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                    await Shell.Current.DisplayAlert("Authentication error:", "The password does not match. Try again.", "OK");
            }
        }
    }

    [RelayCommand]
    public async void ResetPassword()
    {
        await Shell.Current.GoToAsync($"//{nameof(ResetPasswordPage)}");
    }

    [RelayCommand]
    public async void Signup()
    {
        await Shell.Current.GoToAsync($"//{nameof(SignupPage)}");
    }

}

