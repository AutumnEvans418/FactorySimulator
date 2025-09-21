using FactorySimulator.Factories;
using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Merge : Building
    {
        internal Merge(Action<Building> game) : base("Merger", game)
        {
            Recipes.Add(RecipeList.Any);
        }

        internal override Building ProcessResources()
        {
            bool canProduce = InputResources.Any(i => i.Value > 0);

            if (canProduce)
            {
                foreach (var item in InputResources)
                {
                    InputResources[item.Key]--;
                    OutputResources.CreateOrAdd(item.Key, 1);
                }

                ProcessConveyors();
            }
            return this;
        }

        private void ProcessConveyors()
        {
            foreach (var conveyors in OutputConveyors)
            {
                foreach (var output in OutputResources)
                {
                    conveyors.InputResources.CreateOrAdd(output.Key, output.Value);
                    OutputResources[output.Key] = 0;
                }
            }
        }
    }
}

