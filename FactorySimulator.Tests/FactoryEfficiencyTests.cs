using FactorySimulator.Factories;
using FactorySimulator.Factories.Buildings;
using FactorySimulator.GameWorld;
using FactorySimulator.Scripting;
using FluentAssertions;

namespace FactorySimulator.Tests
{
    public class FactoryEfficiencyTests
    {
        private readonly Factory factory;
        public FactoryEfficiencyTests()
        {
            var world = new World();
            factory = new Factory(world);
        }

        private void Test<T>(float value, string? error = null) where T : Building
        {
            Test(typeof(T), value, error);
        }

        private void Test(Type t, float value, string? error = null)
        {
            var miner = factory.Buildings.First(b => b.GetType() == t);
            var eff = miner.GetEfficiency();
            eff.Reason.Should().Be(error);
            eff.Value.Should().Be(value);
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

        [Fact]
        public void Factory_IronRodFactory_MergerEff_Should_Be100()
        {

            var world = new World();

            var fact = new Factory(world);
            fact.Miner(1).IronRodFactory();

            var game = new Game(() => fact, world);

            fact.Buildings.Last().Name.Should().Be("Merger");
            fact.Buildings.Last().GetEfficiency().Value.Should().Be(1);
        }


        [Fact]
        public void Storage_ShouldBe1()
        {
            var store = factory.Miner(0).Smelter().Storage();
            Test<Storage>(.5f, "Input Any underfed: 30/60");
        }

        [Fact]
        public void MinerStorage_ShouldBe1()
        {
            var store = factory.Miner(0).Storage();
            Test<Storage>(1);
        }

        [Fact]
        public void StorageMiner_ShouldBe1()
        {
            factory.Miner(0).Storage();
            Test<Miner>(1);
        }

        [Fact]
        public void MinerMerge_ShouldBe1()
        {
            factory.Miner(0).Merge();

            Test<Miner>(1);
            Test<Merge>(1);
        }

        [Theory]
        [InlineData(DefaultFactoryScripts.MinersMergeStorage, typeof(Merge), 1)]
        [InlineData(DefaultFactoryScripts.MinersMergeStorage, typeof(Storage), 1)]
        [InlineData(DefaultFactoryScripts.MinersMergeStorage, typeof(Miner), 0.5f, "Outputs cannot absorb full rate: 30/60")]

        [InlineData(DefaultFactoryScripts.MinerMergeStorage, typeof(Merge), 1)]
        [InlineData(DefaultFactoryScripts.MinerMergeStorage, typeof(Storage), 1)]
        [InlineData(DefaultFactoryScripts.MinerMergeStorage, typeof(Miner), 1)]

        [InlineData(DefaultFactoryScripts.MinerSplitSmelter, typeof(Miner), 1)]
        [InlineData(DefaultFactoryScripts.MinerSplitSmelter, typeof(Smelter), 1)]

        [InlineData(DefaultFactoryScripts.MinerSplitSmelterMergeStorage, typeof(Miner), 1)]
        [InlineData(DefaultFactoryScripts.MinerSplitSmelterMergeStorage, typeof(Smelter), 1)]
        [InlineData(DefaultFactoryScripts.MinerSplitSmelterMergeStorage, typeof(Storage), 1)]

        [InlineData(DefaultFactoryScripts.MinerSplitSmelterConstructor, typeof(Miner), 1)]
        [InlineData(DefaultFactoryScripts.MinerSplitSmelterConstructor, typeof(Smelter), 1)]
        [InlineData(DefaultFactoryScripts.MinerSplitSmelterConstructor, typeof(Constructor), 1)]

        [InlineData(DefaultFactoryScripts.MinerSmelterConstructorPlate, typeof(Miner), 0.5f, "Outputs cannot absorb full rate: 30/60")]
        [InlineData(DefaultFactoryScripts.MinerSmelterConstructorPlate, typeof(Smelter), 1)]
        [InlineData(DefaultFactoryScripts.MinerSmelterConstructorPlate, typeof(Constructor), 1)]

        [InlineData(DefaultFactoryScripts.MinerSmelterConstructorRod, typeof(Miner), 0.25f, "Downstream Smelter limited (Outputs cannot absorb full rate: 15/30)")]
        [InlineData(DefaultFactoryScripts.MinerSmelterConstructorRod, typeof(Smelter), 0.5f, "Outputs cannot absorb full rate: 15/30")]
        [InlineData(DefaultFactoryScripts.MinerSmelterConstructorRod, typeof(Constructor), 1)]
        public void DynamicEffTests(string code, Type t, float value, string? error = null)
        {
            var engine = new FactoryScriptEngine();

            engine.Execute(factory, code);

            Test(t, value, error);
        }
       
    }
}