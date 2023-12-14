using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public static class RunProgram
    {

        public static void Run()
        {
            var count = 0;

            var game = new Game();
            game.StartGame(f =>
            {
                count++;

                var miner = f.Miner(0).Split();
                
                var screws = miner.Smelter().Constructor(RecipeList.IronRod).Constructor(RecipeList.Screw);
                miner.Smelter().Constructor(RecipeList.IronPlate).Merge(screws).Assembler(RecipeList.ReinforcedPlate);


            });
        }
    } 
}

