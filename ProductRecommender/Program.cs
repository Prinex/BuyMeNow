namespace ProductRecommender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Load sample data
            var sampleData = new CollaborativeFilteringModel.ModelInput()
            {
                UserID = 272F,
                ItemID = 94748F,
            };
            float userId = 272F;
            
            // load more samples of data
            List<CollaborativeFilteringModel.ModelInput> collaborativeFilteringModels= new List<CollaborativeFilteringModel.ModelInput>();
            for (int i =  0; i < 3; i++)
            {
                CollaborativeFilteringModel.ModelInput m = new CollaborativeFilteringModel.ModelInput();
                float item = float.Parse(Console.ReadLine());
                
                m.UserID = userId;
                m.ItemID = item;
                collaborativeFilteringModels.Add(m);
            
            }
            Console.WriteLine($"Recommendations for user {userId}:");
            foreach (var i in collaborativeFilteringModels)
            {
                var result = CollaborativeFilteringModel.Predict(i);
                
                Console.WriteLine(result.ItemID.ToString());
            }
        }
    }
}