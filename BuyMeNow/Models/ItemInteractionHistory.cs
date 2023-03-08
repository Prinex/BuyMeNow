namespace BuyMeNow.Models;

public class ItemInteractionHistory
{
    private bool isExistent = true;

    public bool IsExistent { get => isExistent; set => isExistent = value; }

    [Column("interactionID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int InteractionID { get; set; }

    [Column("userID"), ForeignKey(typeof(Account)), NotNull]
    public int UserID { get; set; }

    [Column("storeName"), ForeignKey(typeof(Store)), NotNull]
    public string StoreName { get; set; }

    [Column("itemTitle"), NotNull]
    public string ItemTitle { get; set; }

    [Column("rating"), NotNull]
    public double Rating { get; set; }

    [Column("quantity"), NotNull]
    public int Quantity { get; set; }

    [ManyToOne]
    public Item Itm { get; set; }
}

