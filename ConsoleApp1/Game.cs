// See https://aka.ms/new-console-template for more information
public static class Game
{
    public static void Start(Action<Map> m)
    {
        var map = new Map();
        //var timer = new System.Timers.Timer(1000);
        m(map);
        map.Update();
        Console.Clear();
        Console.WriteLine(map.Draw());

        while (true)
        {
            map.Update();
            Console.Clear();
            Console.WriteLine(map.Draw());
            Thread.Sleep(1000);
        }
    }
}

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
