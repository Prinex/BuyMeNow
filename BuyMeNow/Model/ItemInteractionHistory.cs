using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyMeNow.Model;

public class ItemInteractionHistory
{
    private int itemID;
    private int userID;
    private double rating;
    private int quantity;
    private Item item;

    public ItemInteractionHistory(int userID, int itemID, int quantity, double rating)
    {
        this.userID = userID;
        this.itemID = itemID;
        this.rating = rating;
        this.quantity = quantity;
    }

    //[ForeignKey()]
    public int ItemID { get => itemID; set => itemID = value; }

    public int UserID { get => userID; set => userID = value; }

    public double Rating { get => rating; set => rating = value; }

    public int Quantity { get => quantity; set => quantity = value; }

    public Item Itm { get => item;set => item = value; }
}

