using FactorySimulator.Factories;
using FactorySimulator.Factories.Buildings.Base;
using FactorySimulator.GameWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySimulator.Display
{

    public class GameMermaid
    {
        private readonly Game game;

        public GameMermaid(Game game)
        {
            this.game = game;
        }

        public string DisplayFactory()
        {
            var builder = new StringBuilder();
            builder.AppendLine("graph LR");
            foreach (var node in game.world.Nodes)
            {
                builder.AppendLine($"N{node.Id}[{node.Item}]");
            }

            foreach (var building in game.factory.Buildings)
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

            foreach (var building in game.factory.Buildings)
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
