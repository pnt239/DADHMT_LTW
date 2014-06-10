using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Core
{
    public class Ellipse : ShapeBase
    {
        /// <summary>
        /// Gets the type of the shape.
        /// </summary>
        /// <returns></returns>
        public override ShapeType GetShapeType()
        {
            return ShapeType.Ellipse;
        }

        public IVertex OrginalPoint
        {
            get
            {
                return new Vertex(Location.X + Size.Width/2, Location.Y + Size.Height/2);
            }
        }

        public float MajorAxis
        {
            get
            {
                return Size.Width/2;
            }
        }

        public float MinorAxis
        {
            get
            {
                return Size.Height / 2;
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// The same shape
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ShapeBase Clone()
        {
            var shape = new Ellipse
            {
                Location = Location,
                Size = Size,
                DrawMethod = DrawMethod,
                OutlineColor = OutlineColor,
                OutlineWidth = OutlineWidth,
                FillColor = FillColor
            };
            return shape;
        }
    }
}
