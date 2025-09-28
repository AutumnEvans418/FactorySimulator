using FactorySimulator.Factories;
using FactorySimulator.Factories.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FactorySimulator.Factories.Buildings
{
    public record EfficiencyResult(float Value, string? Reason)
    {
        public static EfficiencyResult Full => new EfficiencyResult(1, null);
    }

    public partial class Building
    {
        public int Id { get; set; }
        internal string Name { get; set; }
        internal Dictionary<ItemName, int> InputResources { get; set; }
        internal Dictionary<ItemName, int> OutputResources { get; set; }
        internal List<Building> OutputConveyors { get; set; } = new List<Building>();
        internal List<Building> InputConveyors { get; set; } = new List<Building>();
        internal Action<Building> OnBuildingCreated { get; }
        private Recipe? SelectedRecipe { get; set; }
        protected List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public Building(string name, Dictionary<ItemName, int> input, List<Recipe> recipes, Action<Building> game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<ItemName, int>();
            Recipes = recipes;
            OnBuildingCreated = game;
            game(this);
        }

        public Building(string name, Dictionary<ItemName, int> input, Action<Building> game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<ItemName, int>();
            OnBuildingCreated = game;
            game(this);
        }

        public Building(string name, Action<Building> game)
        {
            Name = name;
            InputResources = new Dictionary<ItemName, int>();
            OutputResources = new Dictionary<ItemName, int>();
            OnBuildingCreated = game;
            game(this);
        }

        internal void AddOutputConveyor(Building outputBuilding)
        {
            OutputConveyors.Add(outputBuilding);
            outputBuilding.AddInputConveyor(this);
        }

        internal void AddInputConveyor(Building building)
        {
            InputConveyors.Add(building);
        }

        public Building SetRecipe(Recipe recipe)
        {
            SelectedRecipe = recipe;
            return this;
        }

        bool CanProduce(Recipe recipe)
        {
            return recipe.Input.All(ri => InputResources.Any(i => i.Key == ri.Item && i.Value >= ri.Quantity));
        }

        public virtual Recipe? GetRecipe()
        {
            return SelectedRecipe ?? Recipes.FirstOrDefault(r => r.Input.All(i => InputConveyors.Any(ic => ic.GetRecipe()?.Output.Any(io => io.Item == i.Item) == true)));
        }

        public float Rate()
        {
            var rates = InputConveyors.Select(c => c.Rate()).ToList();

            return GetRecipe()?.TicksRate ?? 0;
        }

        internal virtual Building ProcessResources()
        {
            var recipe1 = GetRecipe();
            if (recipe1 == null)
            {
                return this;
            }
            var recipe = recipe1.Value;

            var canProduce = CanProduce(recipe);

            if (canProduce)
            {
                foreach (var item in recipe.Input)
                {
                    InputResources[item.Item] -= item.Quantity;
                }

                foreach (var item in recipe.Output)
                {
                    OutputResources.CreateOrAdd(item.Item, item.Quantity);
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

        public virtual void CopyTo(Building building)
        {
            building.OutputResources = OutputResources;
            building.InputResources = InputResources;
        }


        internal T Create<T>(T create) where T : Building
        {
            var building = create;
            AddOutputConveyor(building);
            return building;
        }

        public override string ToString()
        {
            var inputs = string.Join(", ", InputResources.Select(i => $"{i.Value} {i.Key}s"));
            var outputs = string.Join(", ", OutputResources.Select(i => $"{i.Value} {i.Key}s"));
            return $"{inputs} -|{Name}|-> {outputs}";
        }

        public List<Building> GetEndOutputConveyors()
        {
            if (OutputConveyors.Count == 0)
                return [this];

            return OutputConveyors.SelectMany(o => o.GetEndOutputConveyors()).Distinct().ToList();
        }

        // public entrypoint - keeps your existing API
        internal virtual EfficiencyResult GetEfficiency()
        {
            var cache = new Dictionary<int, EfficiencyResult>();         // memo for this evaluation
            var visiting = new HashSet<int>();                // recursion stack guard
            return GetEfficiency(cache, visiting);
        }

        // internal worker with memoization & cycle detection
        internal virtual EfficiencyResult GetEfficiency(Dictionary<int, EfficiencyResult> cache, HashSet<int> visiting)
        {
            // cheap path: cached value
            if (cache.TryGetValue(Id, out var cached))
                return cached;

            // cycle detection: if we're already evaluating this node, return a conservative non-blocking value
            // (returning 1 here prevents infinite recursion; we can treat it as "assume full" for the purpose of
            // breaking cycles — this is the simplest safe choice for typical factory graphs)
            if (!visiting.Add(Id))
                return EfficiencyResult.Full;

            var recipe = GetRecipe();
            var eff = EfficiencyResult.Full;
            if (recipe != null)
            {
                var inputEff = GetInputEfficiency(recipe.Value, cache, visiting);
                var outputEff = GetOutputEfficiency(recipe.Value, cache, visiting);
                eff = inputEff.Value < outputEff.Value ? inputEff : outputEff;
            }

            cache[Id] = eff;          // memoize
            visiting.Remove(Id);      // pop stack
            return eff;
        }

        private EfficiencyResult GetOutputEfficiency(Recipe recipe, Dictionary<int, EfficiencyResult> cache, HashSet<int> visiting)
        {
            if (OutputConveyors.Count == 0)
                return new EfficiencyResult(1, null); // sink doesn't limit production

            float producedRate = recipe.Speed(recipe.Output[0]);
            float totalCapacity = 0f;

            var myOutputItem = recipe.Output[0].Item;
            string? limitingReason = null;

            foreach (var consumer in OutputConveyors)
            {
                var consumerRecipe = consumer.GetRecipe();
                if (consumerRecipe == null)
                    continue;

                // does consumer actually take this item?
                if (myOutputItem != ItemName.Any && consumerRecipe.Value.Input.All(i => i.Item != ItemName.Any && i.Item != myOutputItem))
                    continue;

                var consumerInput = consumerRecipe.Value.Input.First(i => i.Item == ItemName.Any || i.Item == myOutputItem || myOutputItem == ItemName.Any);
                var need = consumerRecipe.Value.Speed(consumerInput);
                var consumerEff = consumer.GetEfficiency(cache, visiting);

                float share = 1f;

                if (consumer is Merge)
                {
                    share = 1f / consumer.InputConveyors.Count;
                }

                totalCapacity += need * consumerEff.Value * share;

                if (consumerEff.Value < 1f)
                    limitingReason = $"Downstream {consumer.GetType().Name} limited ({consumerEff.Reason})";
            }

            // Miner is limited only if consumers together can't absorb all its output
            return totalCapacity >= producedRate
                ? EfficiencyResult.Full
                : new EfficiencyResult(totalCapacity / producedRate, string.IsNullOrEmpty(limitingReason)
            ? $"Outputs cannot absorb full rate: {totalCapacity}/{producedRate}"
            : limitingReason);
        }


        private EfficiencyResult GetInputEfficiency(Recipe? recipe, Dictionary<int, EfficiencyResult> cache, HashSet<int> visiting)
        {
            if (recipe == null)
                return EfficiencyResult.Full;

            if (InputConveyors.Count == 0)
            {
                return EfficiencyResult.Full;
            }

            // For each required ingredient, compute total supply from all input conveyors that produce that item.
            // The final input efficiency is the minimum supply fraction across all required ingredients.
            EfficiencyResult overallMin = EfficiencyResult.Full;
            foreach (var req in recipe.Value.Input)
            {
                var needed = recipe.Value.Speed(req); // how much of this item we need per minute
                float totalSupply = 0f;

                foreach (var supplier in InputConveyors)
                {
                    var supplierRecipe = supplier.GetRecipe();
                    if (supplierRecipe == null)
                        continue;

                    // does supplier produce the item we need?
                    if (req.Item != ItemName.Any && supplierRecipe.Value.Output.All(o => o.Item != req.Item))
                        continue;

                    var supplierOutput = supplierRecipe.Value.Output.First(o => o.Item == req.Item || req.Item == ItemName.Any);
                    // supplier's raw output speed for that item
                    var supplierRawSpeed = supplierRecipe.Value.Speed(supplierOutput);
                    // supplier's effective contribution depends on its efficiency
                    var supplierEff = supplier.GetEfficiency(cache, visiting);

                    float share = 1f;
                    if (supplier is Split split)
                    {
                        share = 1f / split.OutputConveyors.Count;
                    }

                    totalSupply += supplierRawSpeed * supplierEff.Value * share;
                }

                var ratio = needed == 0 ?
                    EfficiencyResult.Full :
                    (totalSupply >= needed ?
                        EfficiencyResult.Full :
                        new EfficiencyResult(totalSupply / needed, $"Input {req.Item} underfed: {totalSupply}/{needed}"));


                overallMin = overallMin.Value < ratio.Value ? overallMin : ratio;
            }

            return overallMin;
        }
    }
}

