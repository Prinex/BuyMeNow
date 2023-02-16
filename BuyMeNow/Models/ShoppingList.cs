namespace BuyMeNow.Models;

public class ShoppingList
{
    [PrimaryKey, AutoIncrement, NotNull, Unique]
    public int entryID { get; set; }

    [ForeignKey(typeof(ShoppingLists)), NotNull]
    public int shoppingListID { get; set; }

    [ForeignKey(typeof(Item)), NotNull]
    public int itemID { get; set; }

    [OneToMany]
    public List<Item> Itm { get; set; }
}
