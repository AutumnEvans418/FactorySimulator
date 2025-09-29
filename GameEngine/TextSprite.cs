namespace GameEngine
{
    public class TextSprite : GameComponent
    {
        public string? Text { get; set; }
        public float Size { get; set; }
        public override void OnPaint(GameEventArgs e)
        {
            var canvas = e.Canvas;
            using var paint = canvas.GetPaint();
            paint.IsAntialias = true;
            paint.StrokeWidth = 5f;
            paint.StrokeCap = StrokeCap.Round;
            using var font = canvas.GetFont();
            font.Size = Size;
            canvas.DrawText(Text, GlobalPosition, TextAlign.Center, font, paint);
            base.OnPaint(e);
        }
    }

}
