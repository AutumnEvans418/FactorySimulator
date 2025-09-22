using FactorySimulator.Factories.Items;

namespace FactorySimulator.GameWorld
{

    public class World : IWorld
    {
        public World()
        {
            Nodes =
            [
               new MaterialNode {
                    Item=ItemName.IronOre,
                    Quantity=int.MaxValue,
                    Speed = 1
               }
            ];
        }
        public MaterialNode[] Nodes { get; }
    }
}

