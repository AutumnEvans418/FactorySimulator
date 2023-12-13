namespace ConsoleApp1.Gpt.Buildings
{
    public interface IBuilding
    {
        void AddOutputConveyor(IBuilding outputBuilding);
        Dictionary<string, int> InputResources { get; set; }
    }
}

