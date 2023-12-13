using ConsoleApp1.Gpt.Buildings;

namespace ConsoleApp1.Gpt
{
    public interface IGame
    {
        Dictionary<string, int> Node(int node);
        void AddBuilding(Building building);
    }
}

