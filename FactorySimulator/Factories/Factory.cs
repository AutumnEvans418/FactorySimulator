using FactorySimulator.Factories.Buildings;
using FactorySimulator.GameWorld;

namespace FactorySimulator.Factories
{

    public class Factory
    {
        public int Ticks { get; set; }
        public Factory(Func<int, MaterialNode> getNode)
        {
            GetNode = getNode;
        }
        public List<Building> Buildings { get; set; } = new List<Building>();
        internal Func<int, MaterialNode> GetNode { get; set; }

        internal void AddBuilding(Building building)
        {
            Buildings.Add(building);
        }

        public Miner Miner(int node)
        {
            var nodeMaterial = GetNode(node);

            var existingMiner = Buildings.OfType<Miner>().FirstOrDefault(m => m.Node == nodeMaterial.Id);
            if (existingMiner != null)
            {
                return existingMiner;
            }

            var miner = new Miner(nodeMaterial, AddBuilding);
            return miner;
        }

        public void ProcessResources()
        {
            Ticks++;

            // Start processing resources for each building
            foreach (var building in Buildings)
            {
                var recipe = building.GetRecipe();

                var when = recipe?.TicksRate;

                if (Ticks % when == 0)
                {
                    building.ProcessResources();
                }
            }


        }
    }
}

