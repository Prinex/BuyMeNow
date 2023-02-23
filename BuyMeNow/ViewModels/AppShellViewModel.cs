namespace BuyMeNow.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{ 
    [RelayCommand]
    public async void SignOut()
    {
        if (Preferences.ContainsKey("RememberUser"))
        {
            Preferences.Remove("RememberUser");
        }
        await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
    }
}
