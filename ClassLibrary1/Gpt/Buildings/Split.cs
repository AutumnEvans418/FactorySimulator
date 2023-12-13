using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Split : Building
    {
        public Split(Factory game) : base("Splitter", game)
        {
            Recipes.Add(new Recipe(ItemName.Any, ItemName.Any, 1, 1, 120));
        }

        int tick;

        internal override void ProcessResources(Recipe recipe)
        {
            bool canProduce = InputResources.Any(i => i.Value >= recipe.InputQuantity);



            if (canProduce)
            {
                foreach (var item in InputResources)
                {
                    InputResources[item.Key]--;
                    OutputResources.CreateOrAdd(item.Key, 1);
                }

                ProcessConveyors();
            }
            base.ProcessResources(recipe);
        }

        private void ProcessConveyors()
        {
            if (!OutputConveyors.Any())
            {
                return;
            }
            tick++;

            var conveyors = OutputConveyors[tick % OutputConveyors.Count];

            foreach (var output in OutputResources)
            {
                conveyors.InputResources.CreateOrAdd(output.Key, output.Value);
                OutputResources[output.Key] = 0;
            }
        }
    }
}

