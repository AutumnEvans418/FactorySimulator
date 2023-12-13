namespace ConsoleApp1.Gpt
{
    public static class RunProgram
    {

        public static void Run()
        {
            // Creating resources
            //var ironOre = new Resource { Name = "IronOre", Quantity = 0 };
            //var ironIngot = new Resource { Name = "IronIngot", Quantity = 0 };
            //var ironRod = new Resource { Name = "IronRod", Quantity = 0 };
            //var ironPlate = new Resource { Name = "IronPlate", Quantity = 0 };

            // Creating buildings
            //var miner = new Building("Miner", new Dictionary<string, int>() { { "IronOre", 60 } }, new List<Recipe>
            //{
            //    new Recipe("IronOre", "IronOre", 1, 1, 60)
            //});
            //var miner2 = new Building("Miner", new Dictionary<string, int> { { "IronOre", 60 } }, new List<Recipe>
            //{
            //    new Recipe("IronOre", "IronOre", 1, 1, 60)
            //});
            
            //var smelter = new Building("Smelter", new Dictionary<string, int> { { "IronOre", 1 } }, new List<Recipe>
            //{
            //    new Recipe("IronOre", "IronIngot", 1, 1, 30)
            //});

            //var smelter2 = new Building("Smelter", new Dictionary<string, int> { { "IronOre", 1 } }, new List<Recipe>
            //{
            //    new Recipe("IronOre", "IronIngot", 1, 1, 30)
            //});
            
            //var constructor = new Building("Constructor", new Dictionary<string, int> { { "IronIngot", 1 } }, new List<Recipe>
            //{
            //    new Recipe("IronIngot", "IronRod", 1, 1, 15),
            //    new Recipe("IronIngot", "IronPlate", 3, 2, 30),
            //});

            //var assembler = new Building("Assembler", new Dictionary<string, int>(), new List<Recipe>());

            // Connect buildings
            //miner.SetOutputConveyor(smelter);
            //smelter.SetOutputConveyor(constructor);
            //constructor.SetOutputConveyor(assembler);

           // miner2.SetOutputConveyor(smelter2);

            // Creating game instance and adding buildings
            var game = new Game();

            var splitter = game.Miner(0).Split();

            splitter.Smelter();
            splitter.Smelter();
            splitter.Smelter();
            splitter.Smelter();

            //game.Miner(0).Smelter();

            //game.AddBuilding(miner);


            //game.AddBuilding(smelter);
            //game.AddBuilding(constructor);
            //game.AddBuilding(assembler);

            //game.AddBuilding(miner2);
            //game.AddBuilding(smelter2);

            // Starting the game
            game.StartGame();

        }
    }
}

