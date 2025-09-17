using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Refinery : Building
    {
        public Refinery(Factory game, Recipe recipe) : base("Refinery", game)
        {
            Recipes.Add(recipe);
            SetRecipe(recipe);
        }
    }
}

