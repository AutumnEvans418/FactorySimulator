using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt;
using ConsoleApp1.Gpt.Buildings;

namespace FactorySimulator.Gpt
{
    public class DefaultFactories
    {
        public static Factory BasicScrewFactory(World game)
        {
            var factory = new Factory(game);
            factory.Miner(0).Smelter().Constructor(Recipe.List.IronRod).Constructor(Recipe.List.Screw);
            return factory;
        }

        public static Factory SplitSmelters(World game)
        {
            var factory = new Factory(game);
            factory.Miner(0).Split(
                s => s.Smelter(), 
                s => s.Smelter());
            return factory;
        }

        public static Building IronRodFactory(Building building)
        {
            return building.Split(
                s => s.Smelter().Split(
                    sm => sm.Constructor(Recipe.List.IronRod),
                    sm => sm.Constructor(Recipe.List.IronRod)
                    ).Merge(),
                s => s.Smelter().Split(
                    sm => sm.Constructor(Recipe.List.IronRod),
                    sm => sm.Constructor(Recipe.List.IronRod)
                    ).Merge()).Merge();
        }
    }
}
