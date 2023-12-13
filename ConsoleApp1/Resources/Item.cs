// See https://aka.ms/new-console-template for more information


public abstract class Item : IItem
{
    public int Count { get; set; }
    public string Name => GetType().Name;

    public object Output { get; set; }

    public string Draw()
    {
        if (Output is IDrawable d)
        {
            return $"{Count} {Name}->{d.Draw()}";
        }
        return $"{Count} {Name}";
    }

    public void Update()
    {
        if (Output is IUpdateable u)
        {
            u.Update();
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
