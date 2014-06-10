using System.Drawing;

namespace Untipic.Core
{
    public class TextObject : IDrawingObject
    {
        public PointF Location { get; set; }

        public SizeF Size { get; set; }

        public Color Color { get; set; }

        public DrawingObjectType GetObjectType()
        {
            return DrawingObjectType.Text;
        }

        public string Text { get; set; }

        public Font Font { get; set; }

        public int UserId { get; set; }
    }
}
