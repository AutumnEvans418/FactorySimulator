using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt;

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
    }
}
