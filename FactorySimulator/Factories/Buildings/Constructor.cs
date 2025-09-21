using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Constructor : Building
    {
        internal Constructor(Action<Building> game, Recipe recipe) : base("Constructor", game)
        {
            Recipes.Add(recipe);
            SetRecipe(recipe);
        }
    }
}

