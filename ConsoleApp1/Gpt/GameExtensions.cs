namespace ConsoleApp1.Gpt
{
    public static class GameExtensions
    {
        public static void CreateOrAdd(this Dictionary<string, int> dict, string key, int add)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] += add;
            }
            else
            {
                dict.Add(key, add);
            }
        }

        public static IMiner Miner(this IGame game, int node)
        {
            var miner = new Miner(game.Node(node), game);
            return miner;
        }

        public static ISmelter Smelter(this IMiner miner)
        {
            var smelter = new Smelter(miner.Game);
            miner.SetOutputConveyor(smelter);
            return smelter;
        }
    }

    public class Miner : Building, IMiner
    {
        public Miner(Dictionary<string, int> input, IGame game) : base("Miner", input, game)
        {
            Recipes.Add(new Recipe("IronOre", "IronOre", 1, 1, 60));
        }
    }

    public class Smelter : Building, ISmelter
    {
        public Smelter(IGame game) : base("Smelter", new Dictionary<string, int>(), game)
        {
            Recipes.Add(new Recipe("IronOre", "IronIngot", 1, 1, 30));
            Recipes.Add(new Recipe("CopperOre", "CopperIngot", 1, 1, 30));
        }
    }

    public interface IBuilding
    {
        void SetOutputConveyor(IBuilding outputBuilding);
        Dictionary<string, int> InputResources { get; set; }
    }

    public interface ISmelter : IBuilding
    {

    }

    public interface IMiner : IBuilding
    {
        IGame Game { get; }
    }

    public interface IGame
    {
        Dictionary<string, int> Node(int node);
        void AddBuilding(Building building);
    }
}

