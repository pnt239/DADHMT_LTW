using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Circle : IShape
    {
        private List<Point> _vertices;

        public Circle(Point start, Point end)
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);

            _vertices.Add(start);
            _vertices.Add(end);
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen { get; set; }

        public Brush ShapeBrush { get; set; }

        public FillType FileType { get; set; }

        /* Radius of circle */
        public float Radius
        {
            get
            {
                return (float) Math.Sqrt(Math.Pow(StartVertex.X - EndVertex.X, 2) + Math.Pow(StartVertex.Y - EndVertex.Y, 2));
            }
        }

        public Point StartVertex
        {
            get { return _vertices[0]; }
            set { Vertices[0] = value; }
        }

        public Point EndVertex
        {
            get { return _vertices[1]; }
            set { _vertices[1] = value; }
        }

        public string Name
        {
            get { return "Circle"; }
        }

        public void FinishEdition()
        {
            //
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Circle;
        }

        public IShape Clone()
        {
            return new Circle(new Point(), new Point());
        }
    }
}
