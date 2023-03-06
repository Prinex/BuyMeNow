namespace BuyMeNow.Models;

public class Item
{
    [Column("itemID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int ItemID { get; set; }

    [Column("shoppingListID"), ForeignKey(typeof(ShoppingList)), NotNull]
    public int ShoppingListID { get; set; }

    [Column("storeName"), ForeignKey(typeof(Store)), NotNull]
    public string StoreName { get; set; }

    [Column("title"), NotNull]
    public string Title { get; set; }

    [Column("price"), NotNull]
    public double Price { get; set; }

    [Column("rating")]
    public double Rating { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }
}

