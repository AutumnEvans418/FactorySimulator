using FactorySimulator.Factories;
using FactorySimulator.Factories.Buildings;
using FactorySimulator.Factories.Items;
using FactorySimulator.GameWorld;
using FluentAssertions;

namespace FactorySimulator.Tests
{

    public class FactoryTests
    {
        private readonly Factory factory;
        private readonly Action<Building> onCreated;
        public FactoryTests()
        {
            var world = new World();
            factory = new Factory(world);
            onCreated = a => { };
        }

        [Fact]
        public void SplitSmelters_ShouldCreateTwoSmelters()
        {
            var factory = DefaultFactories.SplitSmelters(new World());
            factory.Buildings.Should().HaveCount(4);
            factory.Buildings.OfType<Split>().Should().HaveCount(1);
            factory.Buildings.OfType<Miner>().Should().HaveCount(1);
            factory.Buildings.OfType<Smelter>().Should().HaveCount(2);

            factory.ProcessResources();
            var smelters = factory.Buildings.OfType<Smelter>().ToList();
            smelters[0].InputResources.Should().HaveCount(0);

            smelters[1].InputResources.Should().HaveCount(1);
            smelters[1].InputResources.First().Value.Should().Be(1);

            factory.ProcessResources();

            smelters[0].InputResources.Should().HaveCount(1);
            smelters[0].InputResources.First().Value.Should().Be(0);
            smelters[0].OutputResources.First().Value.Should().Be(1);

            smelters[1].InputResources.Should().HaveCount(1);
            smelters[1].InputResources.First().Value.Should().Be(0);
            smelters[1].OutputResources.First().Value.Should().Be(1);

        }

        [Fact]
        public void IronRod_ShouldBeCrafted15PerMin()
        {
            var recipe = RecipeList.IronRod;

            recipe.Input.Should().HaveCount(1);
            recipe.Speed(recipe.Input[0]).Should().Be(15);
            recipe.Speed(recipe.Output[0]).Should().Be(15);
        }

        [Fact]
        public void IronPlate_ShouldBeCrafted20PerMin()
        {
            var recipe = RecipeList.IronPlate;
            recipe.Input.Should().HaveCount(1);
            recipe.Speed(recipe.Input[0]).Should().Be(30);
            recipe.Speed(recipe.Output[0]).Should().Be(20);
        }

        [Fact]
        public void IronOre_ShouldBeMined60PerMin()
        {
            var recipe = RecipeList.IronOre;
            recipe.Input.Should().HaveCount(1);
            recipe.Speed(recipe.Input[0]).Should().Be(60);
            recipe.Speed(recipe.Output[0]).Should().Be(60);
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
            factory.Miner(0).Rate().Should().Be(1);
        }

        [Fact]
        public void Factory_Smelter_Speed_ShouldBe()
        {
            factory.Miner(0).Smelter().GetRecipe().Should().BeEquivalentTo(RecipeList.IronIngot);

            factory.Miner(0).Smelter().Rate().Should().Be(2);
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
            var constructor = new Constructor(onCreated, RecipeList.Screw);
            constructor.InputResources.CreateOrAdd(ItemName.IronRod, 1);

            constructor.OutputResources.Should().HaveCount(0);
            
            constructor.ProcessResources();

            constructor.OutputResources.First().Value.Should().Be(4);
        }

        [Fact]
        public void Assembler_Should_CreateOneReinforcedPlate()
        {
            var assembler = new Assembler(onCreated, RecipeList.ReinforcedPlate);

            assembler.InputResources.CreateOrAdd(ItemName.IronPlate, 6);
            assembler.InputResources.CreateOrAdd(ItemName.Screw, 12);

            assembler.ProcessResources();

            assembler.OutputResources.First().Value.Should().Be(1);
        }

        [Fact]
        public void Merger_Should_MoveAny()
        {
            var merger = new Merge(onCreated);
            merger.InputResources.CreateOrAdd(ItemName.IronOre, 1);
            merger.InputResources.CreateOrAdd(ItemName.CopperOre, 1);

            merger.ProcessResources();

            merger.OutputResources.Should().HaveCount(2);
        }

        [Fact]
        public void Refinery_Should_ProduceTwoOututs()
        {
            var refinery = new Refinery(onCreated, RecipeList.Plastic);
            refinery.InputResources.CreateOrAdd(ItemName.CrudeOil, 3);
            refinery.ProcessResources();

            refinery.OutputResources.Should().HaveCount(2);
        }
    }
}