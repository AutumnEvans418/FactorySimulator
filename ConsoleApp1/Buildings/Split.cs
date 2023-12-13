// See https://aka.ms/new-console-template for more information


public class Split : IDrawable, IUpdateable
{
    public Split(IItem i)
    {

    }
    public IItem Output1 { get; set; }
    public IItem Output2 { get; set; }

    public string Draw()
    {
        return $"Split";
    }

    public void Update()
    {
        if (Output1 is IUpdateable u)
            u.Update();
        if (Output2 is IUpdateable u1)
            u1.Update();
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
