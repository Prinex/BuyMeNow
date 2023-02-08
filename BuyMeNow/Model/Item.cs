namespace BuyMeNow.Model;

class Item
{
    private int itemID;
    private string title;
    private double unit_price;
    private string store;
    private double storeLongitude;
    private double storeLatitude;

    public Item(int itemID) { this.itemID = itemID; }

    public Item(int itemID, string title, double unit_price, string store, double storeLongitude, double storeLatitude)
    {
        this.itemID = itemID;
        this.title = title;
        this.unit_price = unit_price;
        this.store = store;
        this.storeLongitude = storeLongitude;
        this.storeLatitude = storeLatitude;
    }

    public int ItemID 
    { 
        get => itemID; 
        set => itemID = value;
    }

    public string Title
    { 
        get => title;
        set => title = value; 
    }

    public double UnitPrice
    {
        get => unit_price;
        set => unit_price = value;
    }

    public string Store
    {
        get => store; 
        set => store = value;
    }

    public double StoreLongitude { get => storeLongitude; }

    public double StoreLatitude { get => storeLatitude; }
}

