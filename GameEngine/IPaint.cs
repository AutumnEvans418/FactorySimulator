namespace GameEngine
{
    public interface IPaint : IDisposable
    {
        public bool IsAntialias { get; set; }
        public float StrokeWidth { get; set; }
        public StrokeCap StrokeCap { get; set; }
    }

}
