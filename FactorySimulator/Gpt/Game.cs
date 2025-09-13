namespace ConsoleApp1.Gpt
{
    public class Game
    {
        public Game()
        {
            world = new World();
            factory = new(world);
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
            factory = newFactory;
        }

        public void StartGame(Action<Factory> action)
        {
            var ticks = 0;

            while (true)
            {
                ticks++;
                Update(action);
                // Start processing resources for each building
                foreach (var building in factory.Buildings)
                {
                    var recipe = building.GetRecipe();

                    var when = 120 / recipe?.RatePerMinute;

                    if (ticks % when == 0)
                    {
                        building.ProcessResources();
                    }
                }
                OnUpdate?.Invoke();
                Thread.Sleep(UpdateSpeedMilliseconds);
            }
        }

        private void Update(Action<Factory> action)
        {
            try
            {
                var f = new Factory(this.world);
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

