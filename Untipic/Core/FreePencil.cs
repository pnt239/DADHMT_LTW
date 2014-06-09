using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class FreePencil : PolygonBase
    {
        public FreePencil()
        {
            IsClosedFigure = false;
            DrawMethod = DrawMethod.ByDragDrop;
            CanResize = false;
            CanMove = false;
        }

        public override ShapeType GetShapeType()
        {
            return ShapeType.FreePencil;
        }

        public override ShapeBase Clone()
        {
            var shape = new FreePencil
            {
                Location = Location,
                Size = Size,
                DrawMethod = DrawMethod,
                OutlineColor = OutlineColor,
                OutlineWidth = OutlineWidth,
                FillColor = FillColor,
                IsClosedFigure = IsClosedFigure,
                Vertices = (VertexCollection)Vertices.Clone()
            };
            return shape;
        }
    }
}
