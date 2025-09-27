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

        public void Test<T>(float value, string? error = null) where T : Building
        {
            var miner = factory.Buildings.OfType<T>().First();
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


        [Fact]
        public void MinerMergeStorage_ShouldBe1()
        {
            factory.Miner(0).Merge().Storage();

            Test<Miner>(1);
            Test<Merge>(1);
            Test<Storage>(1);
        }

        [Fact]
        public void MinerMinerMergeStorage_ShouldBe2()
        {
            var list = new[] { factory.Miner(0), factory.Miner(1) }.Merge().Storage();

            Test<Merge>(1);
            Test<Storage>(1);
            Test<Miner>(0.5f);

        }

        public static IEnumerable<object[]> DynamicTests()
        {
            yield return new object[] { typeof(Merge), "[f.Miner(0),f.Miner(1)].Merge().Storage()", 1 };
        }

        [Theory]
        [MemberData(nameof(DynamicTests))]
        public void DynamicEffTests<T>(string code, float value, string? error = null) where T : Building
        {
            var engine = new FactoryScriptEngine();

            engine.Execute(factory, code);

            Test<T>(value, error);
        }
       
    }
}