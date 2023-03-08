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

    public async Task<List<ItemInteractionHistory>> GetItemInteractionsList(int id)
    {
        await Init();
        var itemList = await conn.Table<ItemInteractionHistory>().Where(i => i.UserID == id).ToListAsync();
        return itemList;
    }

    public async Task<ItemInteractionHistory> GetItemInteraction(int userID, int itemInteractionID)
    {
        await Init();
        var result = await conn.Table<ItemInteractionHistory>().Where(i => i.UserID == userID && i.InteractionID == itemInteractionID).FirstOrDefaultAsync();
        return result ?? new ItemInteractionHistory() { IsExistent = false };
    }

    public async Task<bool> AddItemInteraction(ItemInteractionHistory model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }

    public async Task<bool> UpdateItemInteraction(ItemInteractionHistory model)
    {
        await Init();
        var query = await conn.UpdateAsync(model);
        return query > 0;
    }

    public async Task<bool> DeleteItemInteraction(ItemInteractionHistory model)
    {
        await Init();
        var query = await conn.DeleteAsync(model);
        return query > 0;
    }
}

