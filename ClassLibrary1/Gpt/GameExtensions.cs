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
    }
}

