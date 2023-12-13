using ClassLibrary1.Gpt.Item;
using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public static class GameExtensions
    {
        public static void CreateOrAdd(this Dictionary<ItemName, int> dict, ItemName key, int add)
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
            => Create(miner, new Smelter(miner.Game));

        public static Split Split(this Building building)
            => Create(building, new Split(building.Game));

        public static Constructor Constructor(this Building building, Recipe recipe) 
            => Create(building, new Constructor(building.Game, recipe));

        private static T Create<T>(Building prev, T create) where T : Building
        {
            var building = create;
            prev.AddOutputConveyor(building);
            return building;
        }
    }
}

