namespace BuyMeNow.Services;

public interface IItemService
{
    Task<List<Item>> GetItemList();
    Task<bool> AddAccount(Item model);
    Task<bool> UpdateAccount(Item model);
    Task<bool> DeleteAccount(Item model);
}

