namespace ConsoleApp1.Gpt.Buildings
{
    public class Miner : Building, IMiner
    {
        public Miner(Dictionary<string, int> input, IGame game) : base("Miner", input, game)
        {
            Recipes.Add(new Recipe("IronOre", "IronOre", 1, 1, 60));
        }
    }
}

