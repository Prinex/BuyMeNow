namespace BuyMeNow.ViewModels.Startup;

public partial class LoadingPageViewModel : BaseViewModel
{
    public LoadingPageViewModel()
    {
        CheckUserLoginDetails();
    }
    // using preferences to save user credentials between sessions
    private async void CheckUserLoginDetails()
    {
        // here check for some session info, like user id
        bool rememberUser = Preferences.Get("RememberUser", false);

        if (rememberUser == true) 
        {
            // navigate to home page
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");
            // when opening the app again the saved session data needs to be appended to the UserDetails
            // so the user does not have to re-do the sign in steps
            Account userInfo = new Account();
            if (userDetailsStr != null)
            {
                userInfo = JsonConvert.DeserializeObject<Account>(userDetailsStr);
                App.UserDetails = userInfo;
            }
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        else
        {
            // navigate to login page
            await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
        }
    }
}

