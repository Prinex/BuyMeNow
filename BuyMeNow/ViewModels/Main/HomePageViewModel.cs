namespace BuyMeNow.ViewModels.Main;



public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string username;

    public HomePageViewModel()
    {
        if (App.UserDetails != null)
        {
            // bind only the username on the homepage
            username = App.UserDetails.Username;
        }
    }

    public void UpdateUsername()
    {
        if (App.UserDetails != null)
            Username = App.UserDetails.Username;
    }
}

