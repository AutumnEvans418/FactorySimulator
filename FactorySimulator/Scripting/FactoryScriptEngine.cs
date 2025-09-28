using CSScriptLib;
using FactorySimulator.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FactorySimulator.Scripting
{
    public static class DefaultFactoryScripts
    {
        public const string MinersMergeStorage = "[f.Miner(0),f.Miner(1)].Merge().Storage()";
        public const string MinerMergeStorage = "f.Miner(0).Merge().Storage()";
    }

    public interface IScriptEngine
    {
        Factory Execute(Factory factory);
    }
    internal class FactoryScriptEngine
    {
        

        public Factory Execute(Factory factory, string script)
        {

            var parsedScript = Regex.Replace(script, @"\[(.*)\]", m => $"new []{{{m.Groups[1].Value}}}");
            

            var code = $$"""
                using FactorySimulator.Factories;
                using FactorySimulator.GameWorld;
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
