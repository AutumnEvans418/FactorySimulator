using SkiaSharp;

namespace GameEngine.SkiaSharp
{
    public class Paint : SKPaint, IPaint
    {
        StrokeCap IPaint.StrokeCap
        {
            get => (StrokeCap)base.StrokeCap;
            set => base.StrokeCap = (SKStrokeCap)value;
        }
    }
}
