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
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);

            // theo chieu kim dong ho
            _vertices.Add(start);
            _vertices.Add(end);
            _vertices.Add(new Point(start.X, end.Y));
        }

        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Pen ShapePen { get; set; }

        public Brush ShapeBrush { get; set; }

        public FillType FileType { get; set; }

        public Point StartVertex
        {
            get { return _startVertex; }
            set { _startVertex = value; }
        }

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
            var obj = new Triangle(new Point(), new Point()) { FileType = FileType };
            return obj;
        }
    }
}
