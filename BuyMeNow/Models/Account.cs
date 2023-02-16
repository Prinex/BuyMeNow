namespace BuyMeNow.Models;

public class Account
{   
    private string username;
    private string password;
    private string postcode;
    private List<ItemInteractionHistory> interactions;
    private List<ShoppingList> shoppingList;

    public Account() { }

    // for sign in
    public Account(string username, string password) 
    {
        this.username = username;
        this.password = password;
    }

    // for sign up
    public Account(string username, string password, string postcode)
    {
        this.username = username;
        this.password = password;
        this.postcode = postcode;
    }

    [Column("userID"), PrimaryKey, AutoIncrement, NotNull, Unique]
    public int UserID { get; set; }
    
    [Column("username"), NotNull, Unique]
    public string Username { get => username; set => username = value; }

    [Column("password"), NotNull, Unique]
    public string Password { get => password; set => username = value; }

    [Column("postcode"), NotNull]
    public string Postcode { get => postcode; set => postcode = value; }

    [OneToMany]
    public List<ItemInteractionHistory> Interactions { get => interactions; set => interactions = value; }
    
    [OneToMany]
    public List<ShoppingList> _shoppingList { get => shoppingList; set => shoppingList = value; }
}

