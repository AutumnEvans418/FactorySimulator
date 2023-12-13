using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public class Factory
    {
        private readonly Game game;

        public Factory(Game game)
        {
            this.game = game;
        }
        public List<Building> Buildings { get; set; } = new List<Building>();
        internal void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        internal Dictionary<string, int> Node(int node) => game.Node(node);
    }
}

