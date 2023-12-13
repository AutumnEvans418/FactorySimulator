using ConsoleApp1.Gpt.Buildings;

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
            miner.AddOutputConveyor(smelter);
            return smelter;
        }
    }
}

