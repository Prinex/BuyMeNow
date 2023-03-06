namespace BuyMeNow.Services;

public interface IItemInteractionHistoryService 
{
    Task<List<ItemInteractionHistory>> GetItemInteractionsList();
    Task<bool> AddItemInteraction(ItemInteractionHistory model);
    Task<bool> DeleteItemInteraction(ItemInteractionHistory model);
}

