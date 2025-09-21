using FactorySimulator.Factories.Items;

namespace FactorySimulator.GameWorld
{
    public class MaterialNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ItemName Item { get; set; }
        public int Quantity { get; set; }
        public float Speed { get; set; } = 1;

        public Dictionary<ItemName, int> ToInput()
        {
            return new Dictionary<ItemName, int> { { Item, Quantity } };
        }
    }
}

