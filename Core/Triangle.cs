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
        private FillType _fill;

        public Triangle()
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);
            ShapeBrush = new SolidBrush(Color.White);
            _fill = FillType.NoFill;

            // theo chieu kim dong ho
            _vertices.Add(new Point());
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

        [Browsable(false)]
        public Point StartVertex
        {
            get { return _startVertex; }
            set { _startVertex = value; }
        }

        [Browsable(false)]
        public Point EndVertex
        {
            get { return _endVertex; }
            set
            {
                _endVertex = value;
                FinishEdition();
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Triangle"; }
        }

        public bool HitTest(Point point)
        {
            return Util.CheckInnerPoint(_vertices, point);
        }

        public void FinishEdition()
        {
            int x = _startVertex.X < _endVertex.X ? _startVertex.X : _endVertex.X;
            int y = _startVertex.Y < _endVertex.Y ? _startVertex.Y : _endVertex.Y;
            int width = Math.Abs(_startVertex.X - _endVertex.X);
            int height = Math.Abs(_startVertex.Y - _endVertex.Y);

            _vertices[0] = new Point(x + width/2, y);
            _vertices[1] = new Point(x+width, y+height);
            _vertices[2] = new Point(x, y+height);
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.Triangle;
        }

        public IShape Clone()
        {
            var obj = new Triangle
            {
                Fill = _fill
            };
            return obj;
        }
    }
}
