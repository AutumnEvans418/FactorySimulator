namespace GameEngine
{
    public class GameEventArgs
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public required ICanvas Canvas { get; set; }
    }

}
