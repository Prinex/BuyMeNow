using CustomProductRecommender;
using Microsoft.ML;
using Microsoft.ML.Trainers;

// reference: https://learn.microsoft.com/en-us/dotnet/machine-learning/tutorials/movie-recommendation

public static class Program
{
    static void Main(string[] args)
    {
        // ML context for the model used always at the top
        MLContext mlContext = new MLContext();
        // load the data
        (IDataView trainingDataView, IDataView testDataView) = LoadData(mlContext);

        // train the model 
        ITransformer model = BuildAndTrainModel(mlContext, trainingDataView);

        // evaluate the model
        EvaluateModel(mlContext, testDataView, model);

        // prediction
        SinglePrediction(mlContext, model);

        // save the model
        SaveModel(mlContext, trainingDataView.Schema, model);
    }

    // method to load the data for the model
    static (IDataView training, IDataView test) LoadData(MLContext mlContext)
    {
        var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "interactions_train.csv");
        var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "interactions_test.csv");

        IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ItemRating>(trainingDataPath, hasHeader: true, separatorChar: ',');
        IDataView testDataView = mlContext.Data.LoadFromTextFile<ItemRating>(testDataPath, hasHeader: true, separatorChar: ',');

        return (trainingDataView, testDataView);
    }

    // training the model method
    static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
    {
        IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIDEncoded", inputColumnName: "userID")
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "itemIDEncoded", inputColumnName: "itemID"));

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = "userIDEncoded",
            MatrixRowIndexColumnName = "itemIDEncoded",
            LabelColumnName = "Label",
            NumberOfIterations = 1300,
            ApproximationRank = 44,
            Quiet = true,
        };

        var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));



        Console.WriteLine("=============== Training the model ===============");
        ITransformer model = trainerEstimator.Fit(trainingDataView);

        return model;
    }

    static void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
    {
        Console.WriteLine("=============== Evaluating the model ===============");
        var prediction = model.Transform(testDataView);
        var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");

        Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
        Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
        Console.WriteLine("Absolute-loss: " + metrics.MeanAbsoluteError.ToString());
        Console.WriteLine("Squared-loss: " + metrics.MeanSquaredError.ToString());
        Console.WriteLine("Absolute-loss: " + metrics.RootMeanSquaredError.ToString());
    }

    // prediction function
    static void SinglePrediction(MLContext mlContext, ITransformer model)
    {
        Console.WriteLine("=============== Making a prediction ===============");
        var predictionEngine = mlContext.Model.CreatePredictionEngine<ItemRating, ItemRatingPrediction>(model);

        var testInput = new ItemRating { userID = 289, itemID = 97203 };

        var movieRatingPrediction = predictionEngine.Predict(testInput);

        if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
        {
            Console.WriteLine("Item " + testInput.itemID + " is recommended for user " + testInput.userID);
        }
        else
        {
            Console.WriteLine("Item " + testInput.itemID + " is not recommended for user " + testInput.userID);
        }
    }

    // method to save the model
    static void SaveModel(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
    {
        var modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "MovieRecommenderModel.zip");

        Console.WriteLine("=============== Saving the model to a file ===============");
        mlContext.Model.Save(model, trainingDataViewSchema, modelPath);
    }
}
