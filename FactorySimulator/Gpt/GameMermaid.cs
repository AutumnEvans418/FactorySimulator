using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySimulator.Gpt
{
    public class DefaultFactories
    {
        public static Factory BasicScrewFactory(World game)
        {
            var factory = new Factory(game);
            factory.Miner(0).Smelter().Constructor(RecipeList.IronRod).Constructor(RecipeList.Screw);
            return factory;
        }
    }

    public class GameMermaid
    {
        public GameMermaid(Game game)
        {
            game.OnUpdate = () =>
            {
                
            };
        }

        public static string DisplayFactory(Factory factory)
        {
            var builder = new StringBuilder();
            builder.AppendLine("graph LR");
            foreach (var node in factory.game.Nodes)
            {
                builder.AppendLine($"N{node.Id}[{node.Item}]");
            }

            foreach (var building in factory.Buildings)
            {
                builder.AppendLine($"N{building.Id}[\"{building.Name} ({string.Join(",", building.GetRecipe()?.Output.Select(o => o.Item) ?? [])})\"]");
            }

            foreach(var building in factory.Buildings)
            {
                foreach (var output in building.OutputConveyors)
                {
                    builder.AppendLine($"N{building.Id} --> N{output.Id}");
                }
            }

            return builder.ToString();
        }
    }
}
