namespace BuyMeNow.Services;

public class AccountService : IAccountService
{
    public SQLiteAsyncConnection conn;

    private async Task Init()
    {
        if (conn == null)
        {
            conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await conn.CreateTableAsync<Account>();
            await conn.CreateTableAsync<Store>();
            await conn.CreateTableAsync<Item>();
            await conn.CreateTableAsync<ItemInteractionHistory>();
            await conn.CreateTableAsync<ShoppingLists>();
            await conn.CreateTableAsync<ShoppingList>();
            await conn.ExecuteAsync("PRAGMA foreign_keys=ON;");
        }
    }

    public async Task<Account> GetAccount(string username)
    {
        // get an account by id or by username
        await Init();
        var result = await conn.Table<Account>().Where(i => i.Username == username).FirstOrDefaultAsync();
        return result ?? new Account() { IsExistent = false };
    }

    public async Task<bool> AddAccount(Account model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }

    public async Task<bool> UpdateAccount(Account model)
    {
        await Init();
        var query = await conn.UpdateAsync(model);
        return query > 0;
    }

    public async Task<bool> DeleteAccount(Account model)
    {
        // if any issues here, might need to delete all
        // entries from the table for each user
        await Init();
        var qAccount = await conn.DeleteAsync(model);
        return qAccount > 0;
    }
}
