using Microsoft.CodeAnalysis;

namespace ClassLibrary1.Gpt.Item
{
    public record struct RecipeItem(ItemName Item, int Quantity);

    public struct Recipe
    {
        public RecipeItem[] Input { get; }
        public RecipeItem[] Output { get; }
        /// <summary>
        /// The input production rate
        /// </summary>
        public int ProductionRate { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputResource"></param>
        /// <param name="outputResource"></param>
        /// <param name="inputQuantity"></param>
        /// <param name="outputQuantity"></param>
        /// <param name="rate">This is the input rate, so if it consumes x amount a minute, that's what goes here</param>
        public Recipe(ItemName inputResource, ItemName outputResource, int inputQuantity, int outputQuantity, int rate)
        {
            Input = [new RecipeItem(inputResource, inputQuantity)];
            Output = [new RecipeItem(outputResource, outputQuantity)];
            ProductionRate = rate;
        }

        public Recipe(RecipeItem[] input, RecipeItem[] output, int rate)
        {
            Input = input;
            Output = output;
            ProductionRate = rate;
        }
    }
}

