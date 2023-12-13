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
    }
}
