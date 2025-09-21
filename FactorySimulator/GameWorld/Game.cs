using FactorySimulator.Factories;

namespace FactorySimulator.GameWorld
{
    public class Game
    {
        public Game()
        {
            world = new World();
            factory = new(world.Node);
        }

        public Game(Factory factory)
        {
            world = new World();
            factory.GetNode = world.Node;
            this.factory = factory;
        }

        internal Factory factory;
        internal World world;
        public Action? OnUpdate { get; set; }
        public int UpdateSpeedMilliseconds { get; set; } = 500;

        internal void SwapFactor(Factory newFactory)
        {
            for (int i = 0; i < newFactory.Buildings.Count; i++)
            {
                if (i >= factory.Buildings.Count)
                {
                    continue;
                }

                var oldBuilding = factory.Buildings[i];
                var newBuilding = newFactory.Buildings[i];

                if (oldBuilding.Name != newBuilding.Name)
                {
                    continue;
                }

                oldBuilding.CopyTo(newBuilding);
            }
            var ticks = factory.Ticks;
            factory = newFactory;
            factory.Ticks = ticks;
        }

        public void StartGame(Action<Factory> action)
        {
            while (true)
            {
                Update(action);
                factory.ProcessResources();
                OnUpdate?.Invoke();
                Thread.Sleep(UpdateSpeedMilliseconds);
            }
        }

        private void Update(Action<Factory> action)
        {
            try
            {
                var f = new Factory(world.Node);
                action(f);
                SwapFactor(f);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

