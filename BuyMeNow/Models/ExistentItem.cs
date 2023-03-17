namespace BuyMeNow.Models;
public class ExistentItem
{
    private bool isExistent = true;
    public bool IsExistent { get => isExistent; set => isExistent = value; }

    [Column("itemID"), PrimaryKey, NotNull, Unique]
    public int ItemID { get; set; }

    [Column("title"), NotNull]
    public string Title { get; set; }

    [Column("price"), NotNull]
    public double Price { get; set; }

    [Column("storeName"), NotNull]
    public string StoreName { get; set; }
}

