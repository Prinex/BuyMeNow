using Microsoft.ML.Data;

namespace CustomProductRecommender;

public class ItemRating
{
    [LoadColumn(0)]
    public float userID;
    [LoadColumn(1)]
    public float itemID;
    [LoadColumn(2)]
    public float Label;
    [LoadColumn(3)]
    public float quantity;
}

