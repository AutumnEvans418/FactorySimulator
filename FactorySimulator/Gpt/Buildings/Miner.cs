using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Miner : Building
    {
        public Miner(MaterialNode input, Factory game) : base("Miner", input.ToInput(), game)
        {
            input.Building = this;
            Recipes.Add(new Recipe(ItemName.IronOre, ItemName.IronOre, 1, 1, 60));

            Recipe(Recipes.First(r => r.Input.First().Item == input.Item));
        }
    }
}

