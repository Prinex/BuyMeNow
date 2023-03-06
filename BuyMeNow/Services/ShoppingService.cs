namespace BuyMeNow.Services;

public class ShoppingService : IShoppingListService
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
            await conn.CreateTableAsync<ShoppingList>();
            await conn.ExecuteAsync("PRAGMA foreign_keys=ON;");
        }
    }

    public async Task<List<ShoppingList>> GetShoppingLists()
    {
        await Init();
        var shoppingLists = await conn.Table<ShoppingList>().ToListAsync();
        return shoppingLists;
    }

    public async Task<bool> AddShoppingList(ShoppingList model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }

    public async Task<bool> UpdateShoppingList(ShoppingList model)
    {
        await Init();
        var query = await conn.UpdateAsync(model);
        return query > 0;
    }

    public async Task<bool> DeleteShoppingList(ShoppingList model)
    {
        await Init();
        var query = await conn.DeleteAsync(model);
        return query > 0;
    }
}

