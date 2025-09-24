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

        public static Storage SmelterMerge(this Miner miner) => miner.Split(s => s.Smelter(), s => s.Smelter()).Merge().Storage();

        public static Merge IronRodLine(this Building building)
        {
            static void split(Split sm) => sm.Constructor(RecipeList.IronRod);
            return building.Smelter().Split(split, split).Merge();
        }

        public static Building IronRodFactory(this Miner building)
        {
            return building.Split(
                s => s.IronRodLine(),
                s => s.IronRodLine()).Merge();
        }
    }
}
