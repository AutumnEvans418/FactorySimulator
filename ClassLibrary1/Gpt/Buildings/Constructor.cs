using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Constructor : Building
    {
        public Constructor(Factory game, Recipe recipe) : base("Constructor", game)
        {
            Recipes.Add(recipe);
        }
    }
}

