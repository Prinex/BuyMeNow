namespace BuyMeNow.Services;
public interface IExistentItemService
{
    Task<ExistentItem> GetItem(string name);
    Task<ExistentItem> GetItemByID(int id);
    Task<List<ExistentItem>> GetItems();
    Task<bool> AddItem(ExistentItem model);
    void AddItems();

}

