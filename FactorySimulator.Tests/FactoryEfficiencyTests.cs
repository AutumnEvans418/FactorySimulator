using FactorySimulator.Factories;
using FactorySimulator.Factories.Buildings;
using FactorySimulator.GameWorld;
using FluentAssertions;

namespace FactorySimulator.Tests
{
    public class FactoryEfficiencyTests
    {
        private readonly Factory factory;
        private readonly Action<Building> onCreated;
        public FactoryEfficiencyTests()
        {
            var world = new World();
            factory = new Factory(world);
            onCreated = a => { };
        }

        [Fact]
        public void Miner_ShouldBe1()
        {
            factory.Miner(0).GetEfficiency().Should().Be(1);
        }

        [Fact]
        public void MinerOverfill_ShouldBe50()
        {
            var miner = factory.Miner(0);
            miner.Smelter();

            miner.GetEfficiency().Should().Be(0.5f);
        }

        [Fact]
        public void Smelter_ShouldBe1()
        {
            factory.Miner(0).Smelter().GetEfficiency().Should().Be(1);
        }

        [Fact]
        public void SmelterSplit_ShouldBe1()
        {
            factory.Miner(0).Split(
                s => s.Smelter(), 
                s => s.Smelter(), 
                s => s.Smelter()).First().GetEfficiency().Should().BeApproximately(.66f,.01f);

        }
    }
}