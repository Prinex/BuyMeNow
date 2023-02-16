namespace BuyMeNow.ViewModels.Startup;

public partial class SignupPageViewModel : BaseViewModel
{
    [ObservableProperty]
    string username;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string postcode;

    [RelayCommand]
    public async void Signin()
    {
        await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
    }

    [RelayCommand]
    public async void Signup()
    {
        await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
    }
}

