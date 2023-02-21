namespace BuyMeNow.Services;

public interface IAccountService
{
    // Account authorization and authentication services
    Task<Account> GetAccount(string username);
    Task<bool> AddAccount(Account model);
    Task<bool> UpdateAccount(Account model);
    Task<bool> DeleteAccount(Account model);
}

