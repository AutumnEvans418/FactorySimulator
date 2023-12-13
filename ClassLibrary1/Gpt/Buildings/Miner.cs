namespace ConsoleApp1.Gpt.Buildings
{
    public class Miner : Building
    {
        public Miner(Dictionary<string, int> input, Game game) : base("Miner", input, game)
        {
            Recipes.Add(new Recipe("IronOre", "IronOre", 1, 1, 60));
        }
    }

    public class Split : Building
    {
        public Split(Game game) : base("Splitter", new Dictionary<string, int>(), game)
        {
            Recipes.Add(new Recipe("All", "All", 1, 1, 120));
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

