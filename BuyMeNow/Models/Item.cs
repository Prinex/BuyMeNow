namespace BuyMeNow.Models;

public class Item
{
    [Column("itemID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int ItemID { get; set; }

    [Column("storeID"), ForeignKey(typeof(Store)), NotNull]
    public int StoreID { get; set; }

    [Column("title"), NotNull, Unique]
    public string Title { get; set; }

    [Column("price"), NotNull]
    public double Price { get; set; }
}

