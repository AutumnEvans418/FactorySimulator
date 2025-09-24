using FactorySimulator.Factories;
using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public class Split : Building
    {
        internal Split(Action<Building> game, Action<Split>[] actions) : base("Splitter", game)
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
                split.tick = tick;
            }
            base.CopyTo(building);
        }

        internal override EfficiencyResult GetEfficiency(Dictionary<int, EfficiencyResult> cache, HashSet<int> visiting)
        {
            if (cache.TryGetValue(Id, out var cached))
                return cached;

            if (!visiting.Add(Id))
                return EfficiencyResult.Full;

            EfficiencyResult eff;
            if (InputConveyors.Count == 0)
            {
                eff = EfficiencyResult.Full; // nothing to split
            }
            else
            {
                // just pass through the input’s efficiency
                eff = InputConveyors[0].GetEfficiency(cache, visiting);
            }

            cache[Id] = eff;
            visiting.Remove(Id);
            return eff;
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

