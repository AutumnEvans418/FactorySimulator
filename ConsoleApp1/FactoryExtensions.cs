// See https://aka.ms/new-console-template for more information


public static class FactoryExtensions
{
    //public static Split Split(this IItem i)
    //{
    //    var split = new Split();
    //    i.Output = split;
    //    return split;
    //}

    public static IronOre IronOre(this Map q)
    {
        var ore = new IronOre();
        ore.Count = int.MaxValue;
        q.Queries.Add(ore);
        return ore;
    }
    public static IronIngot IronIngot(this Miner m)
    {
        var output = new IronIngot();
        m.Output = output;
        return output;
    }


    public static Miner Miner(this IOre q)
    {
        var miner = new Miner(q);
        q.Output = miner;
        return miner;
    }

    public static IronRod IronRod(this Constructor c)
    {
        var output = new IronRod();
        c.Output = output;
        return output;
    }


    public static Inventory Inv(this Item i) => new Inventory();

    public static Constructor Constructor(this IConstructable i)
    {
        var con = new Constructor(i);
        i.Output = con;
        return con;
    }

}

//public class Query
//{
//	public Item Output { get; se36654t; }
//
//	public int Speed { get; set; } = 60;
//
//	public int Interval { get; set; }
//
//	internal string Draw()
//	{
//		return $"Query ({Speed}min)->{Output.Draw()}";
//	}
//
//	internal void Update()
//	{
//		Interval++;
//		
//		var when = 60 / Speed;
//		
//		if(Interval % when == 0){
//		
//			Interval = 0;
//			if (Output != null)
//			{
//				Output.Count++;
//			}
//		}
//		
//		if (Output != null)
//		{
//			Output.Update();
//		}
//	}
//}
