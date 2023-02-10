namespace BuyMeNow.Services;

public interface ISigninService
{
    Task<Account> Login(string username, string password);
}

