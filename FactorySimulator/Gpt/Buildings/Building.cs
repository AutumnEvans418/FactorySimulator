using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Building
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        internal string Name { get; set; }
        internal Dictionary<ItemName, int> InputResources { get; set; }
        internal Dictionary<ItemName, int> OutputResources { get; set; }
        internal List<Building> OutputConveyors { get; set; } = new List<Building>();
        internal List<Building> InputConveyors { get; set; } = new List<Building>();
        internal List<Recipe> Recipes { get; set; } = new List<Recipe>();
        internal Factory Game { get; }
        private Recipe? SelectedRecipe { get; set; }

        public Building(string name, Dictionary<ItemName, int> input, List<Recipe> recipes, Factory game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<ItemName, int>();
            Recipes = recipes;
            Game = game;
            game.AddBuilding(this);
        }

        public Building(string name, Dictionary<ItemName, int> input, Factory game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<ItemName, int>();
            Game = game;
            game.AddBuilding(this);
        }

        public Building(string name, Factory game)
        {
            Name = name;
            InputResources = new Dictionary<ItemName, int>();
            OutputResources = new Dictionary<ItemName, int>();
            Game = game;
            game.AddBuilding(this);
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

        public Recipe? GetRecipe()
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
            building.OutputResources = this.OutputResources;
            building.InputResources = this.InputResources;
        }

        public Merge Merge(Building secondary)
        {
            var c = Create(new Merge(this.Game));
            secondary.AddOutputConveyor(c);
            return c;
        }

        public Assembler Assembler(Recipe recipe)
            => Create(new Assembler(this.Game, recipe));

        public Smelter Smelter()
            => Create(new Smelter(Game));

        public Split Split(params Action<Split>[] actions)
            => Create(new Split(this.Game, actions));

        public Constructor Constructor(Recipe recipe)
            => Create(new Constructor(this.Game, recipe));

        private T Create<T>(T create) where T : Building
        {
            var building = create;
            this.AddOutputConveyor(building);
            return building;
        }
    }
}

