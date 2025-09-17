using ClassLibrary1.Gpt.Item;

namespace ConsoleApp1.Gpt.Buildings
{
    public class Miner : Building, INodeProcessor
    {
        public Miner(MaterialNode input, Factory game) : base("Miner", input.ToInput(), game)
        {
            input.Building = this;
            Recipes.Add(Recipe.List.IronOre);

            SetRecipe(Recipes.First(r => r.Input.First().Item == input.Item));
            Node = input.Id;
        }

        public Guid Node { get; set; }
    }
}

