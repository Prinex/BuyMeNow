namespace BuyMeNow.Services;

public interface IItemService
{
    Task<List<Item>> GetItemList(int id);
    Task<bool> AddItem(Item model);
    Task<bool> UpdateItem(Item model);
    Task<bool> DeleteItem(Item model);
}

