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

        internal virtual float GetEfficiency()
        {
            var recipe = GetRecipe();
            if (recipe == null)
                return 1;

            return MathF.Min(GetInputEfficiency(recipe.Value), GetOutputEfficiency(recipe.Value));
        }

        private float GetOutputEfficiency(Recipe recipe)
        {
            if (OutputConveyors.Count == 0)
                return 1f; // sinks (no outputs) never limit production

            float producedRate = recipe.Speed(recipe.Output[0]);

            // distribute production equally if multiple outputs
            float share = producedRate / OutputConveyors.Count;

            // check each output conveyor's ability to consume
            var efficiencies = new List<float>();
            foreach (var output in OutputConveyors)
            {
                var outputRecipe = output.GetRecipe();
                if (outputRecipe == null)
                {
                    efficiencies.Add(0f); // consumer has no recipe, can’t consume
                    continue;
                }

                float need = outputRecipe.Value.Speed(outputRecipe.Value.Input[0]);
                float inputCapacity = need * output.GetEfficiency();

                // how much of my "share" this conveyor can take
                float eff = Math.Min(1f, inputCapacity / share);
                efficiencies.Add(eff);
            }

            // If any consumer is limiting, take the minimum
            return efficiencies.Min();
        }

        private float GetInputEfficiency(Recipe recipe)
        {
            if (InputConveyors.Count == 0)
                return 1f;

            var recipeNeed = recipe.Speed(recipe.Input[0]);

            var input = InputConveyors[0];

            var inputRecipe = input.GetRecipe();

            if (inputRecipe == null)
                return 0;

            var inputSpeed = inputRecipe.Value.Speed(inputRecipe.Value.Output[0]) * input.GetEfficiency();

            if (inputSpeed > recipeNeed)
                return 1;

            return inputSpeed / recipeNeed;
        }
    }
}

