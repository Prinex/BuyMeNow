using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMeNow.Services;

public class ItemService : IItemService
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

    public async Task<List<Item>> GetItemList()
    {
        await Init();
        var itemList = await conn.Table<Item>().ToListAsync();
        return itemList;
    }
    public async Task<bool> AddAccount(Item model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }

    public async Task<bool> UpdateAccount(Item model)
    {
        await Init();
        var query = await conn.UpdateAsync(model);
        return query > 0;
    }

    public async Task<bool> DeleteAccount(Item model)
    {
        await Init();
        var query = await conn.DeleteAsync(model);
        return query > 0;
    }
}

