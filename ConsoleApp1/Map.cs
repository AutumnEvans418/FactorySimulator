// See https://aka.ms/new-console-template for more information
using System.Text;

public class Map
{
    public List<IOre> Queries = new();

    public void Update()
    {
        foreach (var query in Queries.OfType<IUpdateable>())
        {
            query.Update();
        }
    }

    internal string Draw()
    {
        var builder = new StringBuilder();
        foreach (var query in Queries.OfType<IDrawable>())
        {
            builder.AppendLine(query.Draw());
        }
        return builder.ToString();
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
