using System.Security;
using System.Windows.Input;

namespace BuyMeNow.ViewModel;

public partial class SigninPageViewModel : BaseViewModel
{
    [ObservableProperty]
    string username;

    [ObservableProperty]
    private  SecureString password;

    [RelayCommand]
    public async void Login()
    {
    }
    
}

