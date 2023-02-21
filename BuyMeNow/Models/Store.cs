namespace BuyMeNow.Models;

public class Store
{
    [Column("storeID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int StoreID { get; set; }

    [Column("name"), NotNull]
    public string Name { get; set; }

    [Column("longitude"), NotNull, Unique]
    public double Longitude { get; set; }

    [Column("latitude"), NotNull, Unique]
    public double Latitude { get; set; }
}

