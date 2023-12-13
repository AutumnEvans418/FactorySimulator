using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Gpt
{

    public class Building : IBuilding
    {
        public string Name { get; set; }
        public Dictionary<string, int> InputResources { get; set; }
        public Dictionary<string, int> OutputResources { get; set; }
        public IBuilding? OutputConveyor { get; set; } // New property for output conveyor
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public IGame Game { get; }
        public Building(string name, Dictionary<string, int> input, List<Recipe> recipes, IGame game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<string, int>();
            Recipes = recipes;
            Game = game;
            game.AddBuilding(this);
        }

        public Building(string name, Dictionary<string, int> input, IGame game)
        {
            Name = name;
            InputResources = input;
            OutputResources = new Dictionary<string, int>();
            Game = game;
            game.AddBuilding(this);
        }

        public void SetOutputConveyor(IBuilding outputBuilding)
        {
            OutputConveyor = outputBuilding;
        }

        public void ProcessResources(Recipe recipe)
        {
            bool canProduce = InputResources.Any(i => i.Key == recipe.InputResource && i.Value >= recipe.InputQuantity);

            if (canProduce)
            {
                InputResources[recipe.InputResource] -= recipe.InputQuantity;
                OutputResources.CreateOrAdd(recipe.OutputResource, recipe.OutputQuantity);

                // Pass resources to the connected building via the output conveyor
                if (OutputConveyor != null)
                {
                    foreach (var output in OutputResources)
                    {
                        OutputConveyor.InputResources.CreateOrAdd(output.Key, output.Value);
                    }
                }
            }
        }
    }
}

