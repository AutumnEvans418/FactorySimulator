using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{

    public class Factory
    {
        public readonly World game;

        public Factory(World game)
        {
            this.game = game;
        }
        public List<Building> Buildings { get; set; } = new List<Building>();
        internal void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        public Miner Miner(int node)
        {
            var nodeMaterial = game.Node(node);

            //if (nodeMaterial.Building is Miner m)
            //{
            //    return m;
            //}

            var miner = new Miner(nodeMaterial, this);
            return miner;
        }
    }
}

