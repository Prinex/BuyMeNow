namespace BuyMeNow.Models;

public class ShoppingList
{
    [Column("shoppingListID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int ShoppingListID { get; set; }

    [Column("userID"), ForeignKey(typeof(Account)), NotNull]
    public int UserID { get; set; }

    [Column("shoppingListName"), NotNull]
    public string ShoppingListName { get; set; }
}

