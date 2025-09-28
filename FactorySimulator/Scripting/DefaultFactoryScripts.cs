namespace FactorySimulator.Scripting
{
    public static class DefaultFactoryScripts
    {
        public const string MinersMergeStorage = "[f.Miner(0),f.Miner(1)].Merge().Storage()";
        public const string MinerMergeStorage = "f.Miner(0).Merge().Storage()";

        public const string MinerSplitSmelter = "f.Miner(0).Split(s => s.Smelter(), s => s.Smelter())";
        
        public const string MinerSplitSmelterMergeStorage = "f.M(0).Sp(s => s.Sm(), s => s.Sm()).Me().St()";
        
        public const string MinerSplitSmelterConstructor = """
            f.M(0).Sp(
                s => s.Sm().Sp(
                    b => b.Co(r.IronRod), 
                    b => b.Co(r.IronRod)), 
                s => s.Sm().Sp(
                    b => b.Co(r.IronRod), 
                    b => b.Co(r.IronRod)))
            """;

        public const string MinerSmelterConstructorPlate = "f.M(0).Sm().Co(r.IronPlate)";
        public const string MinerSmelterConstructorRod = "f.M(0).Sm().Co(r.IronRod)";
    }
}
