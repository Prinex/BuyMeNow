namespace BuyMeNow.Models;

public class ItemInteractionHistory
{
    [PrimaryKey, AutoIncrement, NotNull, Unique]
    public int interactionID { get; set; }

    [Column("itemid"), ForeignKey(typeof(Item)), NotNull]
    public int ItemID { get; set; }

    [Column("userID"), ForeignKey(typeof(Account)), NotNull]
    public int UserID { get; set; }

    [Column("rating"), NotNull]
    public double Rating { get; set; }

    [Column("quantity"), NotNull]
    public int Quantity { get; set; }

    [ManyToOne]
    public Item Itm { get; set; }
}

