namespace BuyMeNow.Services;

public interface IShoppingListService
{
    Task<List<ShoppingList>> GetShoppingLists(int id);
    Task<bool> AddShoppingList(ShoppingList model);
    Task<bool> UpdateShoppingList(ShoppingList model);
    Task<bool> DeleteShoppingList(ShoppingList model);
}

