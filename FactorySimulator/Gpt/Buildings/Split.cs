using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Split : Building
    {
        public Split(Factory game, Action<Split>[] actions) : base("Splitter", game)
        {
            foreach (var action in actions)
            {
                action(this);
            }
        }

        int tick;

        public override Recipe? GetRecipe()
        {
            return InputConveyors.FirstOrDefault()?.GetRecipe();
        }

        public override void CopyTo(Building building)
        {
            if(building is Split split)
            {
                split.tick = this.tick;
            }
            base.CopyTo(building);
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
            return base.ProcessResources();
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

