namespace BuyMeNow.Models;

public class ShoppingLists
{
    [Column("shoppingListID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int ShoppingListsID { get; set; }

    [Column("userID"), ForeignKey(typeof(Account)), NotNull]
    public int UserID { get; set; }

    [Column("shoppingListName"), NotNull]
    public string ShoppingListName { get; set; }
}

