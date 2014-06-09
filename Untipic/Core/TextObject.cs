using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class TextObject : IDrawingObject
    {
        public PointF Location { get; set; }

        public SizeF Size { get; set; }

        public DrawingObjectType GetObjectType()
        {
            return DrawingObjectType.Text;
        }
    }
}
