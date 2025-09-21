using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FactorySimulator.Factories;
using FactorySimulator.Factories.Items;

namespace FactorySimulator.Factories.Buildings
{
    public partial class Building
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
    }
}

