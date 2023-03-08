namespace BuyMeNow.ViewModels.Startup;

public partial class ResetPasswordPageViewModel : BaseViewModel
{
    private readonly IAccountService _accountService;

    [ObservableProperty]
    private Account accountDetails = new Account();

    public ResetPasswordPageViewModel(IAccountService accountService)
    {
        _accountService = accountService;
        if (App.UserDetails != null)
        {
            // UserDetails with the help of preferences, the "session data"
            AccountDetails.Username = App.UserDetails.Username;
            AccountDetails.Password = App.UserDetails.Password;
            AccountDetails.Postcode = App.UserDetails.Postcode;
        }
    }

    [RelayCommand]
    public async void Reset()
    {
        var newUserDetails = new Account();
        bool response = false;
        if (string.IsNullOrEmpty(AccountDetails.Username)) 
            await Shell.Current.DisplayAlert("Reset error", "Please, provide your username in order to reset your password.", "OK");
        else if (string.IsNullOrEmpty(AccountDetails.Password)) 
            await Shell.Current.DisplayAlert("Reset error", "Please, provide your new password in order to reset it.", "OK");
        else if (!string.IsNullOrEmpty(AccountDetails.Username) && !string.IsNullOrEmpty(AccountDetails.Password)) 
        {
            newUserDetails = await _accountService.GetAccount(AccountDetails.Username);
            if (newUserDetails.IsExistent == false)
            {
                // catch invalid/inexistent username NullReferenceException (can't do it with try-catch)
                await Shell.Current.DisplayAlert($"Authentication issue for: {AccountDetails.Username}", $"Username does not exist.", "OK");
            }
            if (newUserDetails.IsExistent != false) 
            {
                // hash the new password and update it in the database
                newUserDetails.Password = BCrypt.Net.BCrypt.HashPassword(AccountDetails.Password);
                response = await _accountService.UpdateAccount(newUserDetails);

                if (response == true) 
                {
                    await Shell.Current.DisplayAlert("Reset update", "Your password has been changed successfully. You can now try to login in again.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Reset error", "Something went wrong while trying to update the password.", "OK");
                }
            }
        }
    }

    [RelayCommand]
    public async void Back()
    {
        await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
    }
}

