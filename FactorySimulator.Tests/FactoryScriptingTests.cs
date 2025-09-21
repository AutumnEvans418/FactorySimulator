using CSScriptLib;
using FactorySimulator.Factories;
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
    }
}
