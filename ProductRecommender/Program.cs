namespace ProductRecommender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Load sample data
            var sampleData = new CollaborativeFilteringModel.ModelInput()
            {
                UserID = 260F,
                ItemID = 6016F,
                Quantity = 6F,
            };
            float userId = 260F;
            
            // load more samples of data
            List<CollaborativeFilteringModel.ModelInput> collaborativeFilteringModels= new List<CollaborativeFilteringModel.ModelInput>();
            for (int i =  0; i < 1; i++)
            {
                CollaborativeFilteringModel.ModelInput m = new CollaborativeFilteringModel.ModelInput();
                float item = float.Parse(Console.ReadLine());
                int quantity = Convert.ToInt32(Console.ReadLine());
                
                m.UserID = userId;
                m.ItemID = item;
                m.Quantity = quantity;
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