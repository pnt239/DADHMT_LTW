using System.Drawing;

namespace Untipic.Core
{
    public enum DrawingObjectType
    {
        Shape = 0,
        Text,
        Image
    }

    public interface IDrawingObject 
    {
        PointF Location { get; set; }

        SizeF Size { get; set; }

        DrawingObjectType GetObjectType();

        int UserId { get; set; }
    }
}
