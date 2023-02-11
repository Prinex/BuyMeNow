namespace BuyMeNow.Model;

public class ShoppingList
{
    private int itemID;
    private int userID;
    private Item item;

    public int ItemID { get => itemID; set => itemID = value; }

    public int UserID { get => userID; set => userID = value; }

    public Item Itm { get => item; set => item = value; }
}
