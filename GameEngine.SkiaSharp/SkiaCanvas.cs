using SkiaSharp;
using System.Drawing;

namespace GameEngine.SkiaSharp
{
    public class SkiaCanvas : ICanvas
    {
        public required SKCanvas Canvas { get; set; }
        public void DrawText(string? text, PointF position, TextAlign textAlign, IFont font, IPaint paint)
        {
            Canvas.DrawText(text, position.X, position.Y, (SKTextAlign)textAlign, font as Font, paint as Paint);
        }

        public IFont GetFont()
        {
            return new Font();
        }

        public IPaint GetPaint()
        {
            return new Paint();
        }
    }
}
