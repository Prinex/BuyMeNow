namespace BuyMeNow.Models;

public class Account
{
    private bool isExistent = true;

    public bool IsExistent { get => isExistent; set => isExistent = value; }

    [Column("userID"), PrimaryKey, NotNull, Unique]
    public int UserID { get; set; }
    
    [Column("username"), NotNull, Unique]
    public string Username { get; set; }

    [Column("password"), NotNull, Unique]
    public string Password { get; set; }

    [Column("postcode"), NotNull]
    public string Postcode { get; set; }

    [OneToMany]
    public List<ItemInteractionHistory> Interactions { get; set; }
    
    [OneToMany]
    public List<ShoppingList> ShoppingListsUser { get; set; }
}

