namespace ConsoleApp1.Gpt
{
    public class Recipe
    {
        public string InputResource { get; }
        public string OutputResource { get; }
        public int InputQuantity { get; }
        public int OutputQuantity { get; }
        public int ProductionRate { get; set; }

        public Recipe(string inputResource, string outputResource, int inputQuantity, int outputQuantity, int rate)
        {
            InputResource = inputResource;
            OutputResource = outputResource;
            InputQuantity = inputQuantity;
            OutputQuantity = outputQuantity;
            ProductionRate = rate;
        }
    }
}

