using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public class GameConsole
    {
        private readonly Game game;

        public GameConsole(Game game)
        {
            this.game = game;
            game.OnUpdate = DisplayBuildingChain;
        }
        public static void ClearVisibleRegion()
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            for (int y = Console.WindowTop; y < Console.WindowTop + Console.WindowHeight; y++)
            {
                Console.SetCursorPosition(Console.WindowLeft, y);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(cursorLeft, cursorTop);
        }
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
                var (left, top) = Console.GetCursorPosition();
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(left, top);
                Console.WriteLine(Output(building.OutputResources));
            }
        }

        internal void DisplayBuildingChain()
        {
            ClearVisibleRegion();

            Console.SetCursorPosition(0, 0);
            foreach (var building in game.factory.Buildings.Where(b => b.InputConveyors.Count == 0))
            {
                DisplayBuilding(building);
            }
            Console.WriteLine();
            DisplayItems();
        }

        internal void DisplayItems()
        {
            var totalItem = new Dictionary<ItemName, int>();

            foreach (var building in game.factory.Buildings)
            {
                var resources = building.OutputResources.AsEnumerable();
                resources = resources.Union(building.InputResources);
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

    }
}

