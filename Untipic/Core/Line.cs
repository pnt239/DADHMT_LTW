using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class Line : PolygonBase
    {
        public Line()
        {
            Vertices.Add();
            Vertices.Add();
        }

        public override ShapeType GetShapeType()
        {
            return ShapeType.Line;
        }

        public override ShapeBase Clone()
        {
            var shape = new Line
            {
                Location = Location,
                Size = Size,
                DrawMethod = DrawMethod,
                OutlineColor = OutlineColor,
                OutlineWidth = OutlineWidth,
                FillColor = FillColor,
                Vertices = (VertexCollection)Vertices.Clone()
            };
            return shape;
        }
    }
}
