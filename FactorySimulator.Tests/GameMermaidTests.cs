using FactorySimulator.Display;
using FactorySimulator.Factories;
using FactorySimulator.GameWorld;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySimulator.Tests
{
    public class GameMermaidTests
    {
        [Fact]
        public void Factory_Should_ShowDiagram()
        {
            var world = new World();

            var game = new Game(() => DefaultFactories.BasicScrewFactory(world), world);

            var mermaid = new GameMermaid(game);

            mermaid.DisplayFactory().Should().Contain("graph");
        }

        [Fact]
        public void Factory_Spit_Should_ShowDiagram()
        {
            var world = new World();

            var game = new Game(() => DefaultFactories.SplitSmelters(world), world);

            var mermaid = new GameMermaid(game);

            mermaid.DisplayFactory().Should().Contain("graph");
        }

        [Fact]
        public void Factory_Merge_Should_ShowDiagram()
        {
            var world = new World();

            var fact = new Factory(world);
            fact.Miner(0).IronRodFactory();

            var game = new Game(() => fact, world);

            var mermaid = new GameMermaid(game);

            mermaid.DisplayFactory().Should().Contain("graph");
        }

        [Fact]
        public void Factory_Storage_Should_ShowDiagram()
        {

            var world = new World();

            var fact = new Factory(world);
            fact.Miner(0).SmelterMerge();

            var game = new Game(() => fact, world);

            var mermaid = new GameMermaid(game);

            mermaid.DisplayFactory().Should().Contain("graph");
        }
    }
}
