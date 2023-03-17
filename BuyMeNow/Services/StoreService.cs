namespace BuyMeNow.Services;

public class StoreService : IStoreService
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

    public async Task<Store> GetStore(string name)
    {
        await Init();
        var result = await conn.Table<Store>().Where(i => i.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        return result ?? new Store() { IsExistent = false };
    }

    public async Task<Store> GetStoreByID(int id)
    {
        await Init();
        var result = await conn.Table<Store>().Where(i => i.StoreID == id).FirstOrDefaultAsync();
        return result ?? new Store() { IsExistent = false };
    }

    public async Task<List<Store>> GetStores()
    {
        await Init();
        var stores = await conn.Table<Store>().ToListAsync();
        return stores;
    }

    public async Task<bool> AddStore(Store model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }

    public async void AddStores()
    {
        await Init();
        var stores = await GetStores();

        if (stores.Count == 0)
        {
            await AddStore(new Store { Name = "Tesco", StoreID = 0, Latitude = 52.443873871451565, Longitude = -1.4927899566792684 });
            await AddStore(new Store { Name = "ASDA", StoreID = 1, Latitude = 52.43020727057027, Longitude = -1.517752386096698 });
            await AddStore(new Store { Name = "LIDL", StoreID = 2, Latitude = 52.406172200269125, Longitude = -1.4769864433370885 });
            await AddStore(new Store { Name = "Sainsbury's", StoreID = 3, Latitude = 52.41022497548822, Longitude = -1.5086918148408384 });
            await AddStore(new Store { Name = "ALDI", StoreID = 4, Latitude = 52.42314266644427, Longitude = -1.5219652726451098 });
        }
    }
}