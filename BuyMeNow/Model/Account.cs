namespace BuyMeNow.Model;

class Account
{
    private int userID;
    private string email;
    private string username;
    private string password;
    private string postcode;
    private List<ItemInteractionHistory> interactions;
    private List<ShoppingList> shoppingList;

    public Account(string password, string email = null, string username = null) 
    {
        this.email = email;
        this.username = username;
        this.password = password;
    }

    public Account(string email, string username, string password, string postcode)
    {
        this.email = email;
        this.username = username;
        this.password = password;
        this.postcode = postcode;
    }

    public int UserID
    {
        get => userID;
        set => userID = value;
    }
    
    public string Email
    {
        get => email;
        set { email = value; }
    }

    public string Username
    {
        get => username;
        set => username = value;
    }

    public string Password
    {
        get => password;
        set => password = value;
    }

    public string Postcode
    {
        get => postcode;
        set => postcode = value;
    }
    
    public List<ItemInteractionHistory> Interactions
    { 
        get => interactions;
        set => interactions = value; 
    }

    public List<ShoppingList> _shoppingList
    {
        get => shoppingList;
        set => shoppingList = value;
    }
}

