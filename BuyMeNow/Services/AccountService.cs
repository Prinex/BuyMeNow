namespace BuyMeNow.Services;

public class AccountService : IAccountService
{
    public SQLiteAsyncConnection conn;

    public async Task Init()
    {
        if (conn is not null)
            return;

        conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var accountTable = await conn.CreateTableAsync<Account>();
        var storeTable = await conn.CreateTableAsync<Store>();
        var itemTable = await conn.CreateTableAsync<Item>();
        var interactionTable = await conn.CreateTableAsync<ItemInteractionHistory>();
        var shoppingListsTable = await conn.CreateTableAsync<ShoppingLists>();
        var shoppingListTable = await conn.CreateTableAsync<ShoppingList>();
    }

    public async Task<Account> GetAccount(string username)
    {
        await Init();
        return await conn.Table<Account>().Where(i => i.Username == username).FirstOrDefaultAsync();
    }

    public async Task<int> AddAccount(Account model)
    {
        await Init();
        return await conn.InsertAsync(model);
    }

    public async Task<int> UpdateAccount(Account model)
    {
        await Init();
        return await conn.UpdateAsync(model);
    }

    public async Task<int> DeleteAccount(Account model)
    {
        await Init();
        return await conn.DeleteAsync(model);
    }
}
