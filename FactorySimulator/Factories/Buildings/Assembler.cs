using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Assembler : Building
    {
        internal Assembler(Action<Building> game, Recipe recipe) : base("Assembler", game)
        {
            Recipes.Add(recipe);
            SetRecipe(recipe);
        }
    }
}

