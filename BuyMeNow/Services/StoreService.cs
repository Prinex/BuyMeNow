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
            await AddStore(new Store { Name = "Tesco", StoreID = 0, Longitude = 52.44488188, Latitude = -1.491646779 });
            await AddStore(new Store { Name = "ASDA", StoreID = 1, Longitude = 52.43177624, Latitude = -1.518266864 });
            await AddStore(new Store { Name = "LIDL", StoreID = 2, Longitude = 52.40764634, Latitude = -1.476378793 });
            await AddStore(new Store { Name = "Sainsbury's", StoreID = 3, Longitude = 52.4097898, Latitude = -1.508790224 });
            await AddStore(new Store { Name = "ALDI", StoreID = 4, Longitude = 52.39912069, Latitude = -1.556676213 });
        }
    }
}

