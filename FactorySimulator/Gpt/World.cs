using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt
{
    public class World
    {
        public World()
        {
            Nodes =
            [
               new Dictionary<ItemName, int> { { ItemName.IronOre, int.MaxValue } }
            ];
        }
        internal Dictionary<ItemName, int>[] Nodes { get; set; }
        internal Dictionary<ItemName, int> Node(int node)
        {
            return Nodes[node];
        }
    }
}

