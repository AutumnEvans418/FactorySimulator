using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Merge : Building
    {
        public Merge(Factory game) : base("Merger", game)
        {
            
        }
    }

    public class Split : Building
    {
        public Split(Factory game) : base("Splitter", game)
        {
            Recipes.Add(new Recipe(ItemName.Any, ItemName.Any, 1, 1, 120));
        }

        int tick;

        public override void CopyTo(Building building)
        {
            if(building is Split split)
            {
                split.tick = this.tick;
            }
            base.CopyTo(building);
        }

        internal override void ProcessResources(Recipe recipe)
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

