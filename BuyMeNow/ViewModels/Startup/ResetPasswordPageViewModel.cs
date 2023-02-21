namespace BuyMeNow.ViewModels.Startup;

public partial class ResetPasswordPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string usernameF;

    [ObservableProperty]
    private string passwordF;

    private readonly IAccountService _accountService;

    public ResetPasswordPageViewModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [RelayCommand]
    public async void Reset()
    {
        var newUserDetails = new Account();
        bool response = false;
        if (string.IsNullOrEmpty(UsernameF)) 
            await Shell.Current.DisplayAlert("Reset error", "Please, provide your username in order to reset your password.", "OK");
        else if (string.IsNullOrEmpty(PasswordF)) 
            await Shell.Current.DisplayAlert("Reset error", "Please, provide your new password in order to reset it.", "OK");
        else if (!string.IsNullOrEmpty(UsernameF) && !string.IsNullOrEmpty(PasswordF)) 
        {
            newUserDetails = await _accountService.GetAccount(UsernameF);
            if (newUserDetails.IsExistent == false)
            {
                // catch invalid/inexistent username NullReferenceException (can't do it with try-catch)
                await Shell.Current.DisplayAlert($"Authentication issue for: {UsernameF}", $"Username does not exist", "OK");
            }
            if (newUserDetails.IsExistent != false) 
            {
                // hash the new password and update it in the database
                newUserDetails.Password = BCrypt.Net.BCrypt.HashPassword(PasswordF);
                response = await _accountService.UpdateAccount(newUserDetails);

                if (response == true) 
                {
                    await Shell.Current.DisplayAlert("Reset update", "Your password has been changed successfully. You can now try to login in again", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Reset error", "Something went wrong while trying to update the password", "OK");
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

