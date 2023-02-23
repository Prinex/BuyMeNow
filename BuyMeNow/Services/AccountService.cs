namespace BuyMeNow.Services;

public class AccountService : IAccountService
{
    public SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn == null)
        {
            conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await conn.CreateTableAsync<Account>();
            // each service will create its table in the dbs
            //await conn.CreateTableAsync<Store>();
            //await conn.CreateTableAsync<Item>();
            //await conn.CreateTableAsync<ItemInteractionHistory>();
            //await conn.CreateTableAsync<ShoppingLists>();
            //await conn.CreateTableAsync<ShoppingList>();
        }
    }

    public async Task<Account> GetAccount(string username)
    {
        await Init();
        var result = await conn.Table<Account>().Where(i => i.Username == username).FirstOrDefaultAsync();

        return result ?? new Account() { IsExistent = false };
    }

    public async Task<bool> AddAccount(Account model)
    {
        await Init();
        await conn.InsertAsync(model);
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateAccount(Account model)
    {
        await Init();
        await conn.UpdateAsync(model);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAccount(Account model)
    {
        await Init();
        await conn.DeleteAsync(model);
        return await Task.FromResult(true);
    }
}
