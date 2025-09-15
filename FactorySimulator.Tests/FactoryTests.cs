using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt;
using ConsoleApp1.Gpt.Buildings;
using FluentAssertions;

namespace TestProject1
{
    public class FactoryTests
    {
        Factory factory;
        public FactoryTests()
        {
            factory = new Factory(new World());
        }

        [Fact]
        public void CreateMinerOnSameNode_ShouldBeSame()
        {
            var miner1 = factory.Miner(0);
            var miner2 = factory.Miner(0);
            miner1.Id.Should().Be(miner2.Id);
        }

        [Fact]
        public void Factory_Miner_Speed_ShouldBe()
        {
            factory.Miner(0).Rate().Should().Be(60);
        }

        [Fact]
        public void Factory_Smelter_Speed_ShouldBe()
        {
            factory.Miner(0).Smelter().GetRecipe().Should().BeEquivalentTo(RecipeList.IronIngot);

            factory.Miner(0).Smelter().Rate().Should().Be(30);
        }

        [Fact]
        public void Miner_Should_Generate1FromNode()
        {
            var miner = factory.Miner(0);

            miner.InputResources.Count.Should().Be(1);
            miner.InputResources.First().Value.Should().Be(int.MaxValue);

            miner.OutputResources.Count.Should().Be(0);

            miner.ProcessResources();

            miner.InputResources.First().Value.Should().Be(int.MaxValue - 1);
            miner.OutputResources.First().Value.Should().Be(1);
        }

        [Fact]
        public void Constructor_Should_CreateScrews()
        {
            var constructor = new Constructor(factory, RecipeList.Screw);
            constructor.InputResources.CreateOrAdd(ItemName.IronRod, 1);

            constructor.OutputResources.Should().HaveCount(0);
            
            constructor.ProcessResources();

            constructor.OutputResources.First().Value.Should().Be(4);
        }

        [Fact]
        public void Assembler_Should_CreateOneReinforcedPlate()
        {
            var assembler = new Assembler(factory, RecipeList.ReinforcedPlate);

            assembler.InputResources.CreateOrAdd(ItemName.IronPlate, 6);
            assembler.InputResources.CreateOrAdd(ItemName.Screw, 12);

            assembler.ProcessResources();

            assembler.OutputResources.First().Value.Should().Be(1);
        }

        [Fact]
        public void Merger_Should_MoveAny()
        {
            var merger = new Merge(factory);
            merger.InputResources.CreateOrAdd(ItemName.IronOre, 1);
            merger.InputResources.CreateOrAdd(ItemName.CopperOre, 1);

            merger.ProcessResources();

            merger.OutputResources.Should().HaveCount(2);
        }

        [Fact]
        public void Refinery_Should_ProduceTwoOututs()
        {
            var refinery = new Refinery(factory, RecipeList.Plastic);
            refinery.InputResources.CreateOrAdd(ItemName.CrudeOil, 3);
            refinery.ProcessResources();

            refinery.OutputResources.Should().HaveCount(2);
        }
    }
}