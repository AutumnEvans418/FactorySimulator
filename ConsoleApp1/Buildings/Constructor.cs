// See https://aka.ms/new-console-template for more information


public class Constructor : IDrawable, IUpdateable
{
    private IConstructable c;
    public Item Output { get; set; }
    public int Speed { get; set; } = 15;
    public int Interval { get; set; }
    public Constructor(IConstructable c)
    {
        this.c = c;
    }

    public string Draw()
    {
        return $"Constructor->{Output?.Draw()}";
    }

    void IUpdateable.Update()
    {
        if (Output != null)
        {
            Output.Update();
        }
        Interval++;

        var when = 60 / Speed;

        if (Interval % when == 0)
        {

            Interval = 0;
            if (Output != null && c.Count > 0)
            {
                c.Count--;
                Output.Count++;
            }
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
