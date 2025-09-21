using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Smelter : Building
    {
        internal Smelter(Action<Building> game) : base("Smelter", game)
        {
            Recipes.Add(RecipeList.IronIngot);
            Recipes.Add(RecipeList.CopperIngot);
        }
    }
}

