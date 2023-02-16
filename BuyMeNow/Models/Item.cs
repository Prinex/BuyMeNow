namespace BuyMeNow.Models;

public class Item
{
    private string title;
    private double price;
    private int storeID;

    public Item() { }

    public Item(string title, double _price, int _storeID, double storeLongitude, double storeLatitude)
    {
        this.title = title;
        this.price = _price;
        this.storeID = _storeID;
    }

    [Column("itemID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int ItemID { get; set; }

    [Column("storeID"), ForeignKey(typeof(Store)), NotNull]
    public int StoreID { get => storeID; set => storeID = value; }

    [Column("title"), NotNull, Unique]
    public string Title { get => title; set => title = value; }

    [Column("price"), NotNull]
    public double Price { get => price; set => price = value; }
}

