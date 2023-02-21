namespace BuyMeNow.Models;

public class ShoppingList
{
    [Column("entryID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int EntryID { get; set; }

    [Column("shoppingListID"), ForeignKey(typeof(ShoppingLists)), NotNull]
    public int ShoppingListID { get; set; }

    [Column("itemID"), ForeignKey(typeof(Item)), NotNull]
    public int ItemID { get; set; }

    [OneToMany]
    public List<Item> Itm { get; set; }
}
