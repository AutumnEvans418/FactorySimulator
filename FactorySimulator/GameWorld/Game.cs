using FactorySimulator.Factories;

namespace FactorySimulator.GameWorld
{
    public class Game
    {
        public Game(Func<IFactory> factoryFactory, IWorld getNode)
        {
            Factory = factoryFactory();
            FactoryFactory = factoryFactory;
            World = getNode;
        }

        public IFactory Factory { get; set; }
        public Func<IFactory> FactoryFactory { get; }
        public IWorld World { get; set; }
        public Action? OnUpdate { get; set; }
        public int UpdateSpeedMilliseconds { get; set; } = 500;

        internal void SwapFactory(IFactory newFactory)
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

        public void StartGame(Action<IFactory> action)
        {
            while (true)
            {
                Update(action);
                Factory.ProcessResources();
                OnUpdate?.Invoke();
                Thread.Sleep(UpdateSpeedMilliseconds);
            }
        }

        private void Update(Action<IFactory> action)
        {
            try
            {
                var f = FactoryFactory();
                action(f);
                SwapFactory(f);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

