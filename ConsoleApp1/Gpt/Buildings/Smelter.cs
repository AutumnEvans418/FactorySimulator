namespace ConsoleApp1.Gpt.Buildings
{
    public class Smelter : Building, ISmelter
    {
        public Smelter(IGame game) : base("Smelter", new Dictionary<string, int>(), game)
        {
            Recipes.Add(new Recipe("IronOre", "IronIngot", 1, 1, 30));
            Recipes.Add(new Recipe("CopperOre", "CopperIngot", 1, 1, 30));
        }
    }
}

