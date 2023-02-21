namespace BuyMeNow.Models;

public class ShoppingLists
{
    [Column("shoppingListID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int ShoppingListsID { get; set; }

    [Column("userID"), ForeignKey(typeof(Account)), NotNull]
    public int UserID { get; set; }

    [NotNull]
    public string ShoppingListName { get; set; }
}

