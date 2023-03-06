namespace BuyMeNow.Services;

public class ItemInteractionHistoryService : IItemInteractionHistoryService
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

    public async Task<List<ItemInteractionHistory>> GetItemInteractionsList()
    {
        await Init();
        var itemList = await conn.Table<ItemInteractionHistory>().ToListAsync();
        return itemList;
    }

    public async Task<bool> AddItemInteraction(ItemInteractionHistory model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }

    public async Task<bool> DeleteItemInteraction(ItemInteractionHistory model)
    {
        await Init();
        var query = await conn.DeleteAsync(model);
        return query > 0;
    }
}

