namespace BuyMeNow.Services;

public interface IItemInteractionHistoryService 
{
    Task<List<ItemInteractionHistory>> GetItemInteractionsList(int id);
    Task<ItemInteractionHistory> GetItemInteraction(int userID, int itemID);
    Task<bool> AddItemInteraction(ItemInteractionHistory model);
    Task<bool> UpdateItemInteraction(ItemInteractionHistory model);
    Task<bool> DeleteItemInteraction(ItemInteractionHistory model);
}

