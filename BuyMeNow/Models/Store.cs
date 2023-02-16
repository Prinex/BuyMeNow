namespace BuyMeNow.Models;

public class Store
{
    [PrimaryKey, AutoIncrement, NotNull, Unique]
    public int storeID { get; set; }

    [NotNull]
    public string store { get; set; }

    [NotNull, Unique]
    public double latitude { get; set; }

    [NotNull, Unique]
    public double longitude { get; set; }
}

