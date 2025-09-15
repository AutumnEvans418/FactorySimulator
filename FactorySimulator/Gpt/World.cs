using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt
{
    public class World
    {
        public World()
        {
            Nodes =
            [
               new MaterialNode{
                    Item=ItemName.IronOre,
                    Quantity=int.MaxValue,
                    Speed = 1
               }
            ];
        }
        internal MaterialNode[] Nodes { get; set; }
        internal MaterialNode Node(int node)
        {
            return Nodes[node];
        }
    }
}

