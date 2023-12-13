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

        public static Miner Miner(this Factory game, int node)
        {
            var miner = new Miner(game.Node(node), game);
            return miner;
        }

        public static Smelter Smelter(this Building miner)
        {
            var smelter = new Smelter(miner.Game);
            miner.AddOutputConveyor(smelter);
            return smelter;
        }

        public static Split Split(this Building building)
        {
            var split = new Split(building.Game);
            building.AddOutputConveyor(split);
            return split;
        }
    }
}

