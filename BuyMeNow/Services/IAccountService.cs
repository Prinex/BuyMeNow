namespace BuyMeNow.Services;

public interface IAccountService
{
    // Account authorization and authentication services
    Task<Account> GetAccount(string username);
    Task<int> AddAccount(Account model);
    Task<int> UpdateAccount(Account model);
    Task<int> DeleteAccount(Account model);
}

