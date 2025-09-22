using FactorySimulator.Factories.Buildings;
using FactorySimulator.Factories.Items;
using FactorySimulator.GameWorld;

namespace FactorySimulator.Factories
{
    public static class DefaultFactories
    {
        public static Factory BasicScrewFactory(IWorld game)
        {
            var factory = new Factory(game);
            factory.Miner(0).Smelter().Constructor(RecipeList.IronRod).Constructor(RecipeList.Screw);
            return factory;
        }

        public static Factory SplitSmelters(IWorld game)
        {
            var factory = new Factory(game);
            factory.Miner(0).Split(
                s => s.Smelter(), 
                s => s.Smelter());
            return factory;
        }

        public static Building IronRodFactory(this Building building)
        {
            return building.Split(
                s => s.Smelter().Split(
                    sm => sm.Constructor(RecipeList.IronRod),
                    sm => sm.Constructor(RecipeList.IronRod)
                    ).Merge(),
                s => s.Smelter().Split(
                    sm => sm.Constructor(RecipeList.IronRod),
                    sm => sm.Constructor(RecipeList.IronRod)
                    ).Merge()).Merge();
        }
    }
}
