using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public class Game : IGame
    {
        public Dictionary<string, int>[] Nodes { get; set; }
        public Game()
        {
            Nodes = new Dictionary<string, int>[]
            {
               new Dictionary<string, int>{{"IronOre", int.MaxValue} }
            };

        }

        private List<Building> buildings = new List<Building>();

        public void AddBuilding(Building building)
        {
            buildings.Add(building);
        }

        public void StartGame()
        {
            var ticks = 0;

            while (true)
            {
                ticks++;
                // Start processing resources for each building
                foreach (var building in buildings)
                {
                    foreach (var recipe in building.Recipes)
                    {
                        var when = 60 / recipe.ProductionRate;

                        if (ticks % when == 0)
                        {
                            building.ProcessResources(recipe);
                        }
                    }
                }
                DisplayBuildingChain();
                Thread.Sleep(1000);
            }
        }

        public void DisplayBuildingChain()
        {
            var totalItem = new Dictionary<string, int>();
            Console.SetCursorPosition(0, 0);
            foreach (var building in buildings)
            {
                var resources = building.OutputResources.AsEnumerable();
                if(Nodes.All(n => n != building.InputResources))
                {
                    resources = resources.Union(building.InputResources);
                }
                foreach (var item in resources)
                {
                    totalItem.CreateOrAdd(item.Key, item.Value);
                }
                Console.Write(building.Name);
                Console.Write("->");

                if (building.OutputConveyors.Any())
                {
                    foreach (var outputResource in building.OutputResources)
                    {
                        foreach (var conveyor in building.OutputConveyors)
                        {
                            Console.Write(outputResource.Key + " (" + (conveyor.InputResources.ContainsKey(outputResource.Key) ? conveyor.InputResources[outputResource.Key].ToString() : "0") + ")" + "->");
                        }
                    }
                }
                else
                {
                    foreach (var output in building.OutputResources)
                    {
                        Console.Write($"{output.Key} ({output.Value})");
                    }    
                    Console.WriteLine();
                }
            }
            Console.WriteLine();

            foreach (var item in totalItem)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        public Dictionary<string, int> Node(int node)
        {
            return Nodes[node];
        }
    }
}

