using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt;
using ConsoleApp1.Gpt.Buildings;
using FluentAssertions;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Miner_Should_Generate1FromNode()
        {
            var factory = new Factory(new World());

            var miner = factory.Miner(0);

            miner.InputResources.Count.Should().Be(1);
            miner.InputResources.First().Value.Should().Be(int.MaxValue);

            miner.OutputResources.Count.Should().Be(0);

            miner.ProcessResources(miner.Recipes.First());

            miner.InputResources.First().Value.Should().Be(int.MaxValue - 1);
            miner.OutputResources.First().Value.Should().Be(1);
        }

        [Fact]
        public void Constructor_Should_CreateScrews()
        {
            var factory = new Factory(new World());
            var constructor = new Constructor(factory, RecipeList.Screw);
            constructor.InputResources.CreateOrAdd(ItemName.IronRod, 1);

            constructor.OutputResources.Should().HaveCount(0);
            
            constructor.ProcessResources(constructor.Recipes.First());

            constructor.OutputResources.First().Value.Should().Be(4);
        }

        [Fact]
        public void Assembler_Should_CreateOneReinforcedPlate()
        {
            var assembler = new Assembler(new Factory(new World()), RecipeList.ReinforcedPlate);

            assembler.InputResources.CreateOrAdd(ItemName.IronPlate, 6);
            assembler.InputResources.CreateOrAdd(ItemName.Screw, 12);

            assembler.ProcessResources(assembler.Recipes.First());

            assembler.OutputResources.First().Value.Should().Be(1);
        }

        [Fact]
        public void Merger_Should_MoveAny()
        {
            var merger = new Merge(new Factory(new World()));
            merger.InputResources.CreateOrAdd(ItemName.IronOre, 1);
            merger.InputResources.CreateOrAdd(ItemName.CopperOre, 1);

            merger.ProcessResources(merger.Recipes.First());

            merger.OutputResources.Should().HaveCount(2);
        }
    }
}