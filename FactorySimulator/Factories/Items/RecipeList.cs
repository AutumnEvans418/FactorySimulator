namespace FactorySimulator.Factories.Items
{
    public class RecipeList
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
}

