using FactorySimulator.Display;
using FactorySimulator.Factories;
using FactorySimulator.Factories.Items;
using FactorySimulator.GameWorld;
using NLua;

namespace FactorySimulator.Game
{
    public static class RunProgram
    {

        public static void Run()
        {
            var count = 0;
            using var lua = new Lua();
            var recipes = new RecipeList();
            var world = new World();
            var game = new GameWorld.Game(() => new Factory(world), world);

            var console = new GameConsole(game);

            game.StartGame(f =>
            {
                count++;

                lua["r"] = recipes;
                lua["f"] = f;
                lua.DoString(@"f:Miner(0):Smelter():Constructor(r.IronRod):Constructor(r.Screw)");
            });
        }
    } 
}

