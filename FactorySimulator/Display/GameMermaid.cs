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
            var str = new StringBuilder();
            str.AppendLine("graph TD");
            foreach (var node in game.World.Nodes)
            {
                str.AppendLine($"N{node.Id}[\"{node.Item} Node {node.Id}\"]");
            }

            foreach (var building in game.Factory.Buildings)
            {
                if (building is INodeProcessor np)
                {
                    str.AppendLine($"N{np.Node} --> {building.Name}{building.Id}");
                }
                var recipe = building.GetRecipe();

                var inputs = string.Join(",", recipe?.Input.Select(o => $"{o.Item} {recipe?.Speed(o)}ipm") ?? []);

                str.Append($"{building.Name}{building.Id}[\"{building.Name} {building.Id}<br>({inputs})");

                if (recipe?.Output != null)
                {
                    var outputs = string.Join(",", recipe?.Output.Select(o => $"{o.Item} {recipe?.Speed(o)}ipm") ?? []);
                    str.Append($"-->({outputs})");
                }

                str.AppendLine($"<br>{building.GetEfficiency()*100f}%\"]");
            }

            foreach (var building in game.Factory.Buildings)
            {
                foreach (var output in building.OutputConveyors)
                {
                    str.AppendLine($"{building.Name}{building.Id} --> {output.Name}{output.Id}");
                }
            }

            return str.ToString();
        }
    }
}
