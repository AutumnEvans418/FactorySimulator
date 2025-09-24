using FactorySimulator.Factories.Buildings;
using FactorySimulator.Factories.Items;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Globalization;

namespace FactorySimulator.Factories
{
    public static class BuildingExtensions
    {
        public static Storage Storage(this Building building)
            => building.Create(new Storage(building.OnBuildingCreated));

        public static Merge Merge(this IEnumerable<Building> buildings)
        {
            if (!buildings.Any())
                throw new Exception();

            return buildings.First().Merge([.. buildings.Skip(1)]);
        }

        public static Merge Merge(this Building building, params Building[] secondary)
        {
            var c = building.Create(new Merge(building.OnBuildingCreated));
            foreach (var item in secondary)
            {
                item.AddOutputConveyor(c);
            }
            return c;
        }

        public static Assembler Assembler(this Building building, Recipe recipe)
            => building.Create(new Assembler(building.OnBuildingCreated, recipe));

        public static Smelter Smelter(this Building building)
            => building.Create(new Smelter(building.OnBuildingCreated));

        public static List<Building> Split(this Building building, params Action<Split>[] actions)
        {
            var split = building.Create(new Split(building.OnBuildingCreated, actions));
            return split.GetEndOutputConveyors();
        }

        public static Constructor Constructor(this Building building, Recipe recipe)
            => building.Create(new Constructor(building.OnBuildingCreated, recipe));
    }
}

