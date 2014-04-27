using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class Circle : IShape
    {
        private List<Point> _vertices;
        private FillType _fill;

        public Circle()
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);

            _vertices.Add(new Point());
            _vertices.Add(new Point());
        }

        [Browsable(false)]
        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        [Browsable(false)]
        public Pen ShapePen { get; set; }

        [Browsable(false)]
        public Brush ShapeBrush { get; set; }

        public FillType Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        /* Radius of circle */
        public float Radius
        {
            get
            {
                return (float) Math.Sqrt(Math.Pow(StartVertex.X - EndVertex.X, 2) + Math.Pow(StartVertex.Y - EndVertex.Y, 2));
            }
        }

        [Browsable(false)]
        public Point StartVertex
        {
            get { return _vertices[0]; }
            set { Vertices[0] = value; }
        }

        [Browsable(false)]
        public Point EndVertex
        {
            get { return _vertices[1]; }
            set { _vertices[1] = value; }
        }

        [Browsable(false)]
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
            var obj = new Circle
            {
                Fill = _fill,
                ShapePen = ShapePen,
                ShapeBrush = ShapeBrush
            };
            return obj;
        }
    }
}
