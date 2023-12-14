using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public class World
    {
        public World()
        {
            Nodes =
            [
               new Dictionary<ItemName, int> { { ItemName.IronOre, int.MaxValue } }
            ];
        }
        internal Dictionary<ItemName, int>[] Nodes { get; set; }
        internal Dictionary<ItemName, int> Node(int node)
        {
            return Nodes[node];
        }
    }

    public class Game
    {
        public Game()
        {
            world = new World();
            factory = new(world);
        }

        private Factory factory;
        private World world;
        
        internal void SwapFactor(Factory newFactory)
        {
            for (int i = 0; i < newFactory.Buildings.Count; i++)
            {
                if (i >= factory.Buildings.Count)
                {
                    continue;
                }

                var oldBuilding = factory.Buildings[i];
                var newBuilding = newFactory.Buildings[i];

                if (oldBuilding.Name != newBuilding.Name)
                {
                    continue;
                }

                oldBuilding.CopyTo(newBuilding);
            }
            factory = newFactory;
        }

        public void StartGame(Action<Factory> action)
        {
            var ticks = 0;

            while (true)
            {
                ticks++;
                var f = new Factory(this.world);
                action(f);
                SwapFactor(f);
                // Start processing resources for each building
                foreach (var building in factory.Buildings)
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
            var totalItem = new Dictionary<ItemName, int>();

            foreach (var building in factory.Buildings)
            {
                var resources = building.OutputResources.AsEnumerable();
                if (world.Nodes.All(n => n != building.InputResources))
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

        internal string Output(IDictionary<ItemName, int> building) => building.Aggregate(string.Empty, (p, f) => p + $"{f.Key} ({f.Value})");

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
            foreach (var building in factory.Buildings.Where(b => b.InputConveyors.Count == 0))
            {
                DisplayBuilding(building);
            }
            Console.WriteLine();
            DisplayItems();
        }

       
    }
}

