namespace BuyMeNow.Models;

public class ShoppingLists
{
    [PrimaryKey, AutoIncrement, NotNull, Unique]
    public int shoppingListsID { get; set; }

    [ForeignKey(typeof(Account)), NotNull]
    public int userID { get; set; }

    [NotNull]
    public string shoppingListName { get; set; }
}

