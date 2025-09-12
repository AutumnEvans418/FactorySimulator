using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Smelter : Building
    {
        public Smelter(Factory game) : base("Smelter", game)
        {
            Recipes.Add(new Recipe(ItemName.IronOre, ItemName.IronIngot, 1, 1, 30));
            Recipes.Add(new Recipe(ItemName.CopperOre, ItemName.CopperIngot, 1, 1, 30));
        }
    }
}

