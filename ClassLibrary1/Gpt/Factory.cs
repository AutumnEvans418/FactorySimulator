using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public class Factory
    {
        private readonly World game;

        public Factory(World game)
        {
            this.game = game;
        }
        public List<Building> Buildings { get; set; } = new List<Building>();
        internal void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        internal Dictionary<ItemName, int> Node(int node) => game.Node(node);
    }
}

