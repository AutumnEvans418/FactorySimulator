using CSScriptLib;
using FactorySimulator.Factories;
using FactorySimulator.GameWorld;
using FactorySimulator.Scripting;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySimulator.Tests
{
    public interface ICalc
    {
        int Sum(int a, int b);
        int Div(int a, int b);
    }

    
    public class FactoryScriptingTests
    {
        FactoryScriptEngine scriptEngine;
        public FactoryScriptingTests()
        {
            scriptEngine = new FactoryScriptEngine(); 
        }

        [Fact]
        public void ProductTest()
        {
            ICalc script = CSScript.Evaluator
                       .LoadMethod<ICalc>(@"public int Sum(int a, int b)
                                            {
                                                return a + b;
                                            }
                                            public int Div(int a, int b)
                                            {
                                                return a/b;
                                            }");
            script.Div(15, 3).Should().Be(5);
        }

        [Fact]
        public void EvaluatorShouldCreateFactory()
        {
            var factory = CSScript.Evaluator.Eval("using FactorySimulator.Factories; using FactorySimulator.GameWorld; return new Factory(new World());") as Factory;

            factory.Should().BeOfType<Factory>();
        }

        [Fact]
        public void ScriptEngine_Execute_ShouldModifyFactory()
        {
            var factory = new Factory(new World());

            factory = scriptEngine.Execute(factory, "f.Miner(0).Smelter().Storage()");
            factory.Buildings.Should().HaveCount(3);
        }

        [Fact]
        public void ScriptEngine_MergeList_ShouldModifyFactory()
        {
            var factory = new Factory(new World());

            factory = scriptEngine.Execute(factory, "[f.Miner(0)].Merge().Smelter().Storage()");
            factory.Buildings.Should().HaveCount(4);
        }
    }
}
