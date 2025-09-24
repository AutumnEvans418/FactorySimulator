using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Storage : Building
    {
        internal Storage(Action<Building> game) : base(nameof(Storage), game)
        {
            SetRecipe(RecipeList.Any);
        }
    }
}

