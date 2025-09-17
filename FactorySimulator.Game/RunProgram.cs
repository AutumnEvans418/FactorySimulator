using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;
using NLua;

namespace ConsoleApp1.Gpt
{
    public static class RunProgram
    {

        public static void Run()
        {
            var count = 0;
            using var lua = new Lua();
            var recipes = new Recipe.List();
            var game = new Game();

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

