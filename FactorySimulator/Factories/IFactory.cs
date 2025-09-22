using FactorySimulator.Factories.Buildings;

namespace FactorySimulator.Factories
{
    public interface IFactory
    {
        List<Building> Buildings { get; set; }
        int Ticks { get; set; }

        Miner Miner(int node);
        void ProcessResources();
    }
}