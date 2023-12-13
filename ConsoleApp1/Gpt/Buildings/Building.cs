using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Gpt.Buildings
{

    public class Building : IBuilding
    {
        public string Name { get; set; }
        public Dictionary<string, int> InputResources { get; set; }
        public Dictionary<string, int> OutputResources { get; set; }
        public List<IBuilding> OutputConveyors { get; set; } = new List<IBuilding>();
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

        public void AddOutputConveyor(IBuilding outputBuilding)
        {
            OutputConveyors.Add(outputBuilding);
        }

        public void ProcessResources(Recipe recipe)
        {
            bool canProduce = InputResources.Any(i => i.Key == recipe.InputResource && i.Value >= recipe.InputQuantity);

            if (canProduce)
            {
                InputResources[recipe.InputResource] -= recipe.InputQuantity;
                OutputResources.CreateOrAdd(recipe.OutputResource, recipe.OutputQuantity);

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
}

