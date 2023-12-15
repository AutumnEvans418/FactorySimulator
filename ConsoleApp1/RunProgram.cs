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
            var recipes = new RecipeList();
            var game = new Game();
            game.StartGame(f =>
            {
                count++;

                lua["r"] = recipes;
                lua["f"] = f;
                lua.DoString(@"f:Miner(0):Smelter():Constructor(r.IronRod)
split = f:Miner(0):Split()
l1 = split:Smelter()
split:Smelter():Merge(l1):Constructor(r.IronPlate)");

                //var miner = f.Miner(0).Split();
                
                //var screws = miner.Smelter().Constructor(RecipeList.IronRod).Constructor(RecipeList.Screw);
                //miner.Smelter().Constructor(RecipeList.IronPlate).Merge(screws).Assembler(RecipeList.ReinforcedPlate);


            });
        }
    } 
}

