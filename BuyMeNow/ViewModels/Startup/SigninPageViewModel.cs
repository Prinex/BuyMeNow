namespace BuyMeNow.ViewModels.Startup;

public partial class SigninPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string usernameL;

    [ObservableProperty]
    private string passwordL;

    [ObservableProperty]
    public bool rememberUser = false;

    //[ObservableProperty]
    //private BuyMeNow.Models.Account accountCredentials = new BuyMeNow.Models.Account();

    private readonly IAccountService _accountService;

    public SigninPageViewModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [RelayCommand]
    public async void Signin()
    {
        var userDetails = new Account();
        // here the most important thing is checking the user credentials. if they exists or not in the database, and their validity
        if (string.IsNullOrEmpty(UsernameL) || string.IsNullOrEmpty(PasswordL))
            await Shell.Current.DisplayAlert("Authentication error", "Not all fields are provided. Provide your username and password in order to login.", "OK");
        if (!string.IsNullOrEmpty(UsernameL) && !string.IsNullOrEmpty(PasswordL))
        {
            // query the database to get the user credentials according to the username
            userDetails = await _accountService.GetAccount(UsernameL);
            if (userDetails.IsExistent == false)
            {
                // catch invalid/inexistent username NullReferenceException (can't do it with try-catch)
                await Shell.Current.DisplayAlert($"Authentication issue for: {UsernameL}", $"Username does not exist.", "OK");
            }
            // verify the if user exists and the password 
            if (userDetails.IsExistent != false)
            {
                if (BCrypt.Net.BCrypt.Verify(PasswordL, userDetails.Password) == true)
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

                    // saving "data session" with preferences: user id and a preference whether the
                    // user wants to be remembered next time when opening the app
                    string userDetailsStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailsStr);
                    // only one instance of the account
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

