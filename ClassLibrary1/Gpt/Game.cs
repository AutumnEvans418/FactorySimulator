using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public class Game
    {
        internal Dictionary<string, int>[] Nodes { get; set; }
        public Game()
        {
            Nodes = new Dictionary<string, int>[]
            {
               new Dictionary<string, int>{{"IronOre", int.MaxValue} }
            };

        }

        private List<Building> buildings = new List<Building>();

        internal void AddBuilding(Building building)
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
                        var when = 120 / recipe.ProductionRate;

                        if (ticks % when == 0)
                        {
                            building.ProcessResources(recipe);
                        }
                    }
                }
                DisplayBuildingChain();
                Thread.Sleep(500);
            }
        }

        internal void DisplayItems()
        {
            var totalItem = new Dictionary<string, int>();

            foreach (var building in buildings)
            {
                var resources = building.OutputResources.AsEnumerable();
                if (Nodes.All(n => n != building.InputResources))
                {
                    resources = resources.Union(building.InputResources);
                }
                foreach (var item in resources)
                {
                    totalItem.CreateOrAdd(item.Key, item.Value);
                }
            }
            foreach (var item in totalItem)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        internal string Output(IDictionary<string, int> building) => building.Aggregate(string.Empty, (p, f) => p + $"{f.Key} ({f.Value})");

        internal void DisplayBuildingChain()
        {
            void DisplayBuilding(Building building)
            {
                Console.Write(building.Name);
                Console.Write("->");

                if (building.OutputConveyors.Any())
                {
                    var (left, top) = Console.GetCursorPosition();
                    for (int i = 0; i < building.OutputConveyors.Count; i++)
                    {
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(left, top + i);
                        var conveyor = building.OutputConveyors[i];
                        Console.Write(Output(conveyor.InputResources) + "->");
                        DisplayBuilding(conveyor);
                    }
                }
                else
                {
                    Console.WriteLine(Output(building.OutputResources));
                }
            }
            Console.SetCursorPosition(0, 0);
            foreach (var building in buildings.Where(b => b.InputConveyors.Count == 0))
            {
                DisplayBuilding(building);
            }
            Console.WriteLine();
            DisplayItems();
        }

        internal Dictionary<string, int> Node(int node)
        {
            return Nodes[node];
        }
    }
}

