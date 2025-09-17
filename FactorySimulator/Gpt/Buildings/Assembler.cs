using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Assembler : Building
    {
        public Assembler(Factory game, Recipe recipe) : base("Assembler", game)
        {
            Recipes.Add(recipe);
            SetRecipe(recipe);
        }
    }
}

