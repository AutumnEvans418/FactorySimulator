using Microsoft.CodeAnalysis;

namespace ClassLibrary1.Gpt.Item
{
    public record struct RecipeItem(ItemName Item, int Quantity);

    public struct Recipe
    {
        public class List
        {
            public static Recipe IronRod { get; } = new(ItemName.IronIngot, ItemName.IronRod, 1, 1, 4);
            public static Recipe IronOre { get; } = new(ItemName.IronOre, ItemName.IronOre, 1, 1, 1);
            public static Recipe IronPlate { get; } = new(ItemName.IronIngot, ItemName.IronPlate, 3, 2, 6);
            public static Recipe Screw { get; } = new(ItemName.IronRod, ItemName.Screw, 1, 4, 10);
            public static Recipe ReinforcedPlate { get; }
                = new(
                    [new(ItemName.Screw, 12), new(ItemName.IronPlate, 6)],
                    [new(ItemName.ReinforcedPlate, 1)], 5);

            public static Recipe Any { get; } = new(ItemName.Any, ItemName.Any, 1, 1, 1);

            public static Recipe Plastic { get; } = new([new(ItemName.CrudeOil, 3)], [new(ItemName.Plastic, 2), new(ItemName.HeavyOilResidue, 1)], 10);
            public static Recipe Rubber { get; } = new([new(ItemName.CrudeOil, 3)], [new(ItemName.Rubber, 2), new(ItemName.HeavyOilResidue, 2)], 10);

            public static Recipe IronIngot { get; } = new Recipe(ItemName.IronOre, ItemName.IronIngot, 1, 1, 2);
            public static Recipe CopperIngot { get; } = new Recipe(ItemName.CopperOre, ItemName.CopperIngot, 1, 1, 30);

        }

        public RecipeItem[] Input { get; }
        public RecipeItem[] Output { get; }
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
        private Recipe(ItemName inputResource, ItemName outputResource, int inputQuantity, int outputQuantity, int rate)
        {
            Input = [new RecipeItem(inputResource, inputQuantity)];
            Output = [new RecipeItem(outputResource, outputQuantity)];
            TicksRate = rate;
        }

        private Recipe(RecipeItem[] input, RecipeItem[] output, int rate)
        {
            Input = input;
            Output = output;
            TicksRate = rate;
        }

        public float Speed(RecipeItem recipeItem)
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

