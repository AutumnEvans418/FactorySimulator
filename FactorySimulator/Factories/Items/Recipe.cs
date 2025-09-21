using Microsoft.CodeAnalysis;

namespace FactorySimulator.Factories.Items
{
    public struct Recipe
    {
        public RecipeStack[] Input { get; }
        public RecipeStack[] Output { get; }
        /// <summary>
        /// The input production rate
        /// </summary>
        public float TicksRate { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputResource"></param>
        /// <param name="outputResource"></param>
        /// <param name="inputQuantity"></param>
        /// <param name="outputQuantity"></param>
        /// <param name="rate">This is the input rate, so if it consumes x amount a minute, that's what goes here</param>
        internal Recipe(ItemName inputResource, ItemName outputResource, int inputQuantity, int outputQuantity, int rate)
        {
            Input = [new RecipeStack(inputResource, inputQuantity)];
            Output = [new RecipeStack(outputResource, outputQuantity)];
            TicksRate = rate;
        }

        internal Recipe(RecipeStack[] input, RecipeStack[] output, int rate)
        {
            Input = input;
            Output = output;
            TicksRate = rate;
        }

        public float Speed(RecipeStack recipeItem)
        {
            return 60 / TicksRate * recipeItem.Quantity;
        }

        public override string ToString()
        {
            var inputs = string.Join(", ", Input.Select(i => $"{i.Quantity} {i.Item}s"));
            var outputs = string.Join(", ", Output.Select(i => $"{i.Quantity} {i.Item}s"));
            
            return $"{inputs} => {outputs} @ {TicksRate} ticks";
        }
    }
}

