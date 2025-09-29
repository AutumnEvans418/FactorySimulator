namespace GameEngine
{
    public class FpsSprite : GameComponent
    {
        private TextSprite textSprite;
        public FpsSprite()
        {
            textSprite = new TextSprite();
            ChildComponents.Add(textSprite);
            PositionBaseline = PositionBaseline.Grid;
            Row = 12;
            Column = 6;
        }

        int tickIndex = 0;
        long tickSum = 0;
        long[] tickList = new long[100];
        long lastTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        double GetCurrentFPS()
        {
            var newTick = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var delta = newTick - lastTick;
            lastTick = newTick;

            tickSum -= tickList[tickIndex];
            tickSum += delta;
            tickList[tickIndex] = delta;

            if (++tickIndex == tickList.Length)
                tickIndex = 0;

            return 1000.0 / ((double)tickSum / tickList.Length);
        }

        public override void OnPaint(GameEventArgs e)
        {
            var fps = GetCurrentFPS();

            textSprite.Text = $"{fps:0.00}fps";
            textSprite.Size = 24;

            base.OnPaint(e);
        }

    }

}
