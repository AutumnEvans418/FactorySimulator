namespace ConsoleApp1.Gpt.Buildings
{
    public class BuildingList : List<Building>
    {
        public BuildingList(IEnumerable<Building> buildings) : base(buildings)
        {
            
        }

        public Merge Merge()
        {
            if (this.Count == 0)
                throw new Exception();

            return this.First().Merge(this.Skip(1).ToArray());
        }
    }
}

