namespace BuyMeNow.Services;

public class ExistentItemService : IExistentItemService
{
    public SQLiteAsyncConnection conn;
    private async Task Init()
    {
        if (conn == null)
        {
            conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await conn.CreateTableAsync<ExistentItem>();
            await conn.ExecuteAsync("PRAGMA foreign_keys=ON;");
        }
    }

    public async Task<ExistentItem> GetItem(string name)
    {
        await Init();
        var items = await GetItems();

        ExistentItem result = null;
        bool equality = false;
        var nameSubstrings = name.ToLower().Split().ToList();
        
        for (int i = 0; i < items.Count; i++)
        {
            var ithSubstring = items[i].Title.ToLower().Split().ToList();
            for (int n = 0; n < nameSubstrings.Count; n++)
            {
                bool doesExist = ithSubstring.Any(s => s.Contains(nameSubstrings[n]));
                if (doesExist == true)
                    equality = true;
                else
                {
                    equality = false;
                    break;
                }
            }
            if (equality == true)
            {
                result = items[i];
                break;
            }
        }

        //var result = await conn.Table<ExistentItem>().Where(t => t.Title.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        return result ?? new ExistentItem() { IsExistent = false };
    }

    public async Task<ExistentItem> GetItemByID(int id)
    {
        await Init();
        var result = await conn.Table<ExistentItem>().Where(i => i.ItemID == id).FirstOrDefaultAsync();
        return result ?? new ExistentItem() { IsExistent = false };
    }

    public async Task<List<ExistentItem>> GetItems()
    {
        await Init();
        var items = await conn.Table<ExistentItem>().ToListAsync();
        return items;
    }

    public async Task<bool> AddItem(ExistentItem model)
    {
        await Init();
        var query = await conn.InsertAsync(model);
        return query > 0;
    }
    public async void AddItems()
    {
        await Init();
        var items = await GetItems();

        // add the existent items used for the recommendation system
        if (items.Count == 0)
        {
            // import the csv file and then add each row into the db
            string filename = "itemsDB.csv";
            using var stream = await FileSystem.OpenAppPackageFileAsync(filename);
            using var reader = new StreamReader(stream);
            
            //string[] lines = System.IO.File.ReadAllLines(path);
            while (!reader.EndOfStream) 
            {
                string line = reader.ReadLine();
                string[] columns = line.Split(",");
                
                ExistentItem item = new ExistentItem
                {
                    ItemID = Convert.ToInt32(columns[0]),
                    Title = columns[1],
                    Price = Convert.ToDouble(columns[2]),
                    StoreName = columns[3],
                };
                await AddItem(item);
            }
        }
    }
}

