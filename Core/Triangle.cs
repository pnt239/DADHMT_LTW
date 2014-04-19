using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Triangle : IShape
    {
        private List<Point> _vertices;
        private Point _startVertex;
        private Point _endVertex;

        public Triangle(Point start, Point end)
        {
            _vertices = new List<Point> { start, end };
            ShapePen = new Pen(Color.Black);
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen { get; set; }

        public Brush ShapeBrush { get; set; }

        public Point StartVertex
        {
            get { return _startVertex; }
            set { _startVertex = value; }
        }

        public Point EndVertex
        {
            get { return _endVertex; }
            set { _endVertex = value; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Triangle"; }
        }

        public void FinishEdition()
        {
            //
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Triangle;
        }

        public IShape Clone()
        {
            return new Triangle(new Point(), new Point());
        }
    }
}
