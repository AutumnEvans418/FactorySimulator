using FactorySimulator.Factories.Buildings.Base;
using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Refinery : Building, INodeProcessor
    {
        internal Refinery(Action<Building> game, Recipe recipe) : base("Refinery", game)
        {
            Recipes.Add(recipe);
            SetRecipe(recipe);
        }

        public int Node { get; set; }

    }
}

