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
        string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

        if (string.IsNullOrEmpty(userDetailsStr)) 
        {
            // navigate to login page
            await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
        }
        else
        {
            // navigate to home page
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");

        }
    }
}

