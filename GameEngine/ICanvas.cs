using System.Drawing;

namespace GameEngine
{
    public interface ICanvas
    {
        IPaint GetPaint();
        IFont GetFont();
        void DrawText(string? text, PointF position, TextAlign textAlign, IFont font, IPaint paint);
    }

}
