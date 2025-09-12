using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Miner : Building
    {
        public Miner(Dictionary<ItemName, int> input, Factory game) : base("Miner", input, game)
        {
            Recipes.Add(new Recipe(ItemName.IronOre, ItemName.IronOre, 1, 1, 60));
        }
    }
}

