using System.Drawing;

namespace Untipic.Core
{
    public class ImageObject : IDrawingObject
    {
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public DrawingObjectType GetObjectType()
        {
            return DrawingObjectType.Image;
        }

        public int UserId { get; set; }
    }
}
