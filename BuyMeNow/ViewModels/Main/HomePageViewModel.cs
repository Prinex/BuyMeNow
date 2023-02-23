namespace BuyMeNow.ViewModels.Main;

public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string username;

    public HomePageViewModel()
    {
        if (App.UserDetails != null)
        {
            Username = App.UserDetails.Username;
        }
    }  
}

