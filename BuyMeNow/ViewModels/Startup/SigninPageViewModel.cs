using System.Text.Json.Nodes;

namespace BuyMeNow.ViewModels.Startup;

public partial class SigninPageViewModel : BaseViewModel
{
    [ObservableProperty]
    public string username;

    [ObservableProperty]
    private string password;

    [RelayCommand]
    public async void Signin()
    {
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
        {
            var userDetails = new Account(Username, Password, "CV1 3KE");

            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }

            string userDetailsStr = JsonConvert.SerializeObject(userDetails);
            Preferences.Set(nameof(App.UserDetails), userDetailsStr);
            App.UserDetails = userDetails;

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

    [RelayCommand]
    public async void Signup()
    {
        await Shell.Current.GoToAsync($"//{nameof(SignupPage)}");
    }

}

