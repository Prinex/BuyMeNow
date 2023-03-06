namespace BuyMeNow.Models;

public class Store
{
    [Column("name"), PrimaryKey, NotNull, Unique]
    public string Name { get; set; }

    [Column("storeID"), NotNull, Unique]
    public int StoreID { get; set; }

    [Column("longitude"), NotNull, Unique]
    public double Longitude { get; set; }

    [Column("latitude"), NotNull, Unique]
    public double Latitude { get; set; }
}

