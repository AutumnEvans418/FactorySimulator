using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{

    public class Factory
    {
        public readonly World game;
        public int Ticks { get; set; }
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

            var existingMiner = Buildings.OfType<Miner>().FirstOrDefault(m => m.Node == nodeMaterial.Id);
            if (existingMiner != null)
            {
                return existingMiner;
            }

            var miner = new Miner(nodeMaterial, this);
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

