using CSScriptLib;
using FactorySimulator.Factories;
using FactorySimulator.Factories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactorySimulator.Scripting
{
    public class r : RecipeList
    {

    }
    internal class FactoryScriptEngine
    {
        

        public Factory Execute(Factory factory, string script)
        {
            
            var parsedScript = Regex.Replace(script, @"\[(.*)\]", m => $"new []{{{m.Groups[1].Value}}}");
            var code = $$"""
                using FactorySimulator.Factories;
                using FactorySimulator.GameWorld;
                using FactorySimulator.Scripting;
                public Factory Execute(Factory f)
                {
                    {{parsedScript}};
                    return f;
                }
                """;

            var f = CSScript.Evaluator.LoadMethod<IScriptEngine>(code);
            return f.Execute(factory);
        }
    }
}
