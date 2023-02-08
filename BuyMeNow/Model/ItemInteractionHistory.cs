namespace BuyMeNow.Model;

class ItemInteractionHistory
{
    private int userID;
    private int itemID;
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

    public int UserID
    {
        get => userID;
        set => userID = value; 
    }

    public int ItemID
    {
        get => itemID;
        set => itemID = value;
    }

    public double Rating
    {
        get => rating;
        set => rating = value;
    }

    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    public Item Itm
    {
        get => item;
        set => item = value;
    }
}

