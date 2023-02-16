namespace BuyMeNow.ViewModels.Startup;

public partial class LoadingPageViewModel : BaseViewModel
{
    public LoadingPageViewModel()
    {
        CheckUserLoginDetails();
    }

    private async void CheckUserLoginDetails()
    {
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

