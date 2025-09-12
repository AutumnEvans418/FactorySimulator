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
                lua.DoString(@"
for i = 1,5 do
    f:Miner(0):Smelter():Constructor(r.IronRod):Constructor(r.Screw)
end");

                //var miner = f.Miner(0).Split();
                
                //var screws = miner.Smelter().Constructor(RecipeList.IronRod).Constructor(RecipeList.Screw);
                //miner.Smelter().Constructor(RecipeList.IronPlate).Merge(screws).Assembler(RecipeList.ReinforcedPlate);


            });
        }
    } 
}

