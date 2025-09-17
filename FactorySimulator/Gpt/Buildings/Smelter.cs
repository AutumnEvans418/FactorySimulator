using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Smelter : Building
    {
        public Smelter(Factory game) : base("Smelter", game)
        {
            Recipes.Add(Recipe.List.IronIngot);
            Recipes.Add(Recipe.List.CopperIngot);
        }
    }
}

