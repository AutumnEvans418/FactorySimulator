using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Gpt.Item
{

    public static class RecipeList
    {
        public static Recipe IronRod { get; } = new(ItemName.IronIngot, ItemName.IronRod, 1, 1, 15);
        public static Recipe IronPlate { get; } = new(ItemName.IronIngot, ItemName.IronPlate, 3, 2, 30);
        public static Recipe Screw { get; } = new(ItemName.IronRod, ItemName.Screw, 1, 4, 10);
        public static Recipe ReinforcedPlate { get; } 
            = new(
                [new RecipeItem(ItemName.Screw, 12), new RecipeItem(ItemName.IronPlate, 6)], 
                [new RecipeItem(ItemName.ReinforcedPlate, 1)], 5);
    }
}
