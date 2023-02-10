using SQLite;

namespace BuyMeNow.Model;

public class Account
{
    
    private int userID;
    private string username;
    private string password;
    private string postcode;
    private List<ItemInteractionHistory> interactions;
    private List<ShoppingList> shoppingList;

    public Account(int userID, string username, string password, string postcode)
    {
        this.userID = userID;
        this.username = username;
        this.password = password;
        this.postcode = postcode;
    }

    [PrimaryKey, Unique, NotNull, Column("userID")]
    public int UserID { get => userID; set => userID = value; }
    
    [MaxLength(255), Unique, NotNull]
    public string Username { get => username; set => username = value; }

    [MaxLength(255), Unique, NotNull]
    public string Password { get => password; set => username = value; }

    [MaxLength(255), NotNull]
    public string Postcode { get => postcode; set => postcode = value; }

    [Ignore]
    public List<ItemInteractionHistory> Interactions { get => interactions; set => interactions = value; }

    public List<ShoppingList> _shoppingList { get => shoppingList; set => shoppingList = value; }
}

