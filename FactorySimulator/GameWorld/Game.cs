using FactorySimulator.Factories;

namespace FactorySimulator.GameWorld
{
    public class Game
    {
        public Game(Func<IFactory> factoryFactory, IWorld world)
        {
            Factory = factoryFactory();
            FactoryFactory = factoryFactory;
            World = world;
        }

        public IFactory Factory { get; set; }
        public Func<IFactory> FactoryFactory { get; }
        public IWorld World { get; set; }
        public Action? OnUpdate { get; set; }
        public int UpdateSpeedMilliseconds { get; set; } = 500;

        public void StartGame(Action<IFactory> action)
        {
            while (true)
            {
                Update(action);
                Thread.Sleep(UpdateSpeedMilliseconds);
            }
        }

        private void SwapFactory(IFactory newFactory)
        {
            for (int i = 0; i < newFactory.Buildings.Count; i++)
            {
                if (i >= Factory.Buildings.Count)
                {
                    continue;
                }

                var oldBuilding = Factory.Buildings[i];
                var newBuilding = newFactory.Buildings[i];

                if (oldBuilding.Name != newBuilding.Name)
                {
                    continue;
                }

                oldBuilding.CopyTo(newBuilding);
            }
            var ticks = Factory.Ticks;
            Factory = newFactory;
            Factory.Ticks = ticks;
        }

        public void Update(Action<IFactory> action)
        {
            try
            {
                var f = FactoryFactory();
                action(f);
                SwapFactory(f);
                Factory.ProcessResources();
                OnUpdate?.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

