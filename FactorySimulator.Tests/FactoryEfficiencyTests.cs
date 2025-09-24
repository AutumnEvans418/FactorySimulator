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
            factory.Miner(0).GetEfficiency().Value.Should().Be(1);
        }

        [Fact]
        public void MinerOverfill_ShouldBe50()
        {
            var miner = factory.Miner(0);
            miner.Smelter();

            var eff = miner.GetEfficiency();
            eff.Value.Should().Be(0.5f);
            eff.Reason.Should().Be("Outputs cannot absorb full rate: 30/60");
        }

        [Fact]
        public void Smelter_ShouldBe1()
        {
            factory.Miner(0).Smelter().GetEfficiency().Value.Should().Be(1);
        }

        [Fact]
        public void SmelterSplit_ShouldBe1()
        {
            var ef = factory.Miner(0).Split(
                s => s.Smelter(),
                s => s.Smelter(),
                s => s.Smelter()).First().GetEfficiency();

            ef.Value.Should().BeApproximately(.66f, .01f);
            ef.Reason.Should().Be("Input IronOre underfed: 20/30");
        }

        [Fact]
        public void SmelterSplit_Miner_ShouldBe1()
        {
            var miner = factory.Miner(0);

            miner.Split(
                s => s.Smelter(),
                s => s.Smelter(),
                s => s.Smelter());

            miner.GetEfficiency().Value.Should().Be(1);
        }
    }
}