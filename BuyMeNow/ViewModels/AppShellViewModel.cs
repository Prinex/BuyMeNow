namespace BuyMeNow.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{ 
    [RelayCommand]
    public async void SignOut()
    {
        if (Preferences.ContainsKey(nameof(App.UserDetails)))
        {
            Preferences.Remove(nameof(App.UserDetails));
        }
        await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
    }
}
