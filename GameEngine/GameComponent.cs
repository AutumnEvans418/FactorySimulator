using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameEngine
{
    public class GameComponent
    {
        public PointF Position { get; set; }
        public List<GameComponent> ChildComponents { get; set; } = [];
        public PositionBaseline PositionBaseline { get; set; }
        public PointF GlobalPosition { get; private set; }
        public int? Row { get; set; }
        public int? Column { get; set; }
        public virtual void OnPaintBefore(GameEventArgs e, GameComponent? parent = null)
        {
            GlobalPosition = Position;

            if (PositionBaseline == PositionBaseline.ScreenCenter)
            {
                GlobalPosition = new PointF((e.Width / 2f), (e.Height / 2f)).Add(Position);
            }

            if (parent != null && PositionBaseline == PositionBaseline.Relative)
            {
                GlobalPosition = parent.GlobalPosition.Add(Position);
            }

            if (PositionBaseline == PositionBaseline.Grid && Row != null && Column != null)
            {
                GlobalPosition = new PointF(e.Width / 12 * Column.Value, e.Height / 12 * Row.Value).Add(Position);
            }

            foreach (var component in ChildComponents)
            {
                component.OnPaintBefore(e, this);
            }
        }

        public virtual void OnPaint(GameEventArgs e)
        {
            foreach (var sprite in ChildComponents)
            {
                sprite.OnPaint(e);
            }
        }
    }

}
