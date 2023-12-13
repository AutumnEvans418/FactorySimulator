using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Gpt.Buildings
{

    public class Building
    {
        internal string Name { get; set; }
        internal Dictionary<string, int> InputResources { get; set; }
        internal Dictionary<string, int> OutputResources { get; set; }
        internal List<Building> OutputConveyors { get; set; } = new List<Building>();
        internal List<Building> InputConveyors { get; set; } = new List<Building>();
        internal List<Recipe> Recipes { get; set; } = new List<Recipe>();
        internal Factory Game { get; }
        public Building(string name, Dictionary<string, int> input, List<Recipe> recipes, Factory game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<string, int>();
            Recipes = recipes;
            Game = game;
            game.AddBuilding(this);
        }

        public Building(string name, Dictionary<string, int> input, Factory game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<string, int>();
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

        internal virtual void ProcessResources(Recipe recipe)
        {
            bool canProduce = InputResources.Any(i => i.Key == recipe.InputResource && i.Value >= recipe.InputQuantity);

            if (canProduce)
            {
                InputResources[recipe.InputResource] -= recipe.InputQuantity;
                OutputResources.CreateOrAdd(recipe.OutputResource, recipe.OutputQuantity);

                ProcessConveyors();
            }
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

