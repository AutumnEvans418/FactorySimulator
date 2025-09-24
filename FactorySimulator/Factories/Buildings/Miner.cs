using FactorySimulator.Factories.Buildings.Base;
using FactorySimulator.Factories.Items;
using FactorySimulator.GameWorld;

namespace FactorySimulator.Factories.Buildings
{
    public class Miner : Building, INodeProcessor
    {
        internal Miner(MaterialNode input, Action<Building> game) : base("Miner", input.ToInput(), game)
        {
            Recipes.Add(RecipeList.IronOre);

            SetRecipe(Recipes.First(r => r.Input.First().Item == input.Item));
            Node = input.Id;
        }

        public int Node { get; set; }
    }
}

