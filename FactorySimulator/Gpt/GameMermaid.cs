using ConsoleApp1.Gpt;
using ConsoleApp1.Gpt.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySimulator.Gpt
{

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
                if (building is INodeProcessor np)
                {
                    builder.AppendLine($"N{np.Node} --> N{building.Id}");
                }
                var recipe = building.GetRecipe();
                var outputs = string.Join(",", recipe?.Output.Select(o => $"{o.Item} {recipe?.Speed(o)}ipm") ?? []);
                var inputs = string.Join(",", recipe?.Input.Select(o => $"{o.Item} {recipe?.Speed(o)}ipm") ?? []);
                builder.AppendLine($"N{building.Id}[\"{building.Name} ({inputs})-->({outputs})\"]");
            }

            foreach (var building in factory.Buildings)
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
