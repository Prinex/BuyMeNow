namespace BuyMeNow.ViewModels.Startup;


public partial class SignupPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string usernameR;
    
    [ObservableProperty]
    private string passwordR;
    
    [ObservableProperty]
    private string postcodeR;
    
    //[ObservableProperty]
    //private BuyMeNow.Models.Account accountCredentials = new BuyMeNow.Models.Account();
   
    private readonly IAccountService _accountService;

    public SignupPageViewModel(IAccountService accountService) 
    {
        _accountService = accountService; 
    }

    [RelayCommand]
    public async void Signup()
    {
        bool response = false;
        if (string.IsNullOrEmpty(UsernameR) || string.IsNullOrEmpty(PasswordR))
            await Shell.Current.DisplayAlert("Registration error", "Not all fields are provided. Provide your username and password in order to register.", "OK");
        else if (string.IsNullOrEmpty(PostcodeR))
            await Shell.Current.DisplayAlert("Registration error", "The postcode is used for the geolocation feature of stores. Please provide it for the best experience.", "OK");
        // add an account on creation to the database
        else if (!string.IsNullOrEmpty(UsernameR) && !string.IsNullOrEmpty(PasswordR) && !string.IsNullOrEmpty(PostcodeR))
        {
            try
            {
                // hash and salt the password (salting is done automatically by the HashPasword function
                response = await _accountService.AddAccount(new Account
                {
                    Username = UsernameR,
                    Password = BCrypt.Net.BCrypt.HashPassword(PasswordR),
                    Postcode = PostcodeR
                });
            }
            catch
            {
                // general catch clause to
                // catch the username when it is not unique/already taken
                await Shell.Current.DisplayAlert($"Registration issue for: {UsernameR}", "Username is already taken.", "OK");
            }
            finally
            {
                if (response == true)
                    await Shell.Current.DisplayAlert("Account created successfully", "You can now sign in.", "OK");
            }
        }    
    }

    [RelayCommand]
    public async void Signin()
    {
        await Shell.Current.GoToAsync($"//{nameof(SigninPage)}");
    }
}

