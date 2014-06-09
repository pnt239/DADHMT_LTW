using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
    }
}
