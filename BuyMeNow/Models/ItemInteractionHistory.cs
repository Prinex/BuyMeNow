namespace BuyMeNow.Models;

public class ItemInteractionHistory
{
    private int itemID;
    private int userID;
    private double rating;
    private int quantity;
    private Item item;

    public ItemInteractionHistory() { }

    public ItemInteractionHistory(int userID, int itemID, int quantity, double rating)
    {
        this.userID = userID;
        this.itemID = itemID;
        this.rating = rating;
        this.quantity = quantity;
    }

    [PrimaryKey, AutoIncrement, NotNull, Unique]
    public int interactionID { get; set; }

    [Column("itemid"), ForeignKey(typeof(Item))]
    public int ItemID { get => itemID; set => itemID = value; }

    [Column("userID"), ForeignKey(typeof(Account))]
    public int UserID { get => userID; set => userID = value; }

    [Column("rating"), NotNull]
    public double Rating { get => rating; set => rating = value; }

    [Column("quantity"), NotNull]
    public int Quantity { get => quantity; set => quantity = value; }

    [ManyToOne]
    public Item Itm { get => item;set => item = value; }
}

