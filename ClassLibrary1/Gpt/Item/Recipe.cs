namespace ClassLibrary1.Gpt.Item
{
    public struct Recipe
    {
        public ItemName InputResource { get; }
        public ItemName OutputResource { get; }
        public int InputQuantity { get; }
        public int OutputQuantity { get; }
        public int ProductionRate { get; }

        public Recipe(ItemName inputResource, ItemName outputResource, int inputQuantity, int outputQuantity, int rate)
        {
            InputResource = inputResource;
            OutputResource = outputResource;
            InputQuantity = inputQuantity;
            OutputQuantity = outputQuantity;
            ProductionRate = rate;
        }
    }
}

