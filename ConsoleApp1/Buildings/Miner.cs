// See https://aka.ms/new-console-template for more information


//public class Miner : IDrawable, IUpdateable
//{
//    public Item Output { get; set; }
//    private IOre q;

//    public int Speed { get; set; } = 30;

//    public int Interval { get; set; }

//    public Miner(IOre q)
//    {
//        this.q = q;
//    }

//    public string Draw()
//    {
//        return $"Mine ({Speed}min)->{Output?.Draw()}";
//    }

//    void IUpdateable.Update()
//    {
//        if (Output != null)
//        {
//            Output.Update();
//        }
//        Interval++;

//        var when = 60 / Speed;

//        if (Interval % when == 0)
//        {

//            Interval = 0;
//            if (Output != null && q.Count > 0)
//            {
//                q.Count--;
//                Output.Count++;
//            }
//        }


//    }
//}

//public class Query
//{
//	public Item Output { get; set; }
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
