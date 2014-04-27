using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TabletC.Core
{
    public class RegPolygon : IShape
    {
        private List<Point> _vertices;
        private int _sides;
        private Point _endVertex;
        private FillType _fill;

        public RegPolygon()
        {
            _vertices = new List<Point>();
            ShapePen = new Pen(Color.Black);
            _fill = FillType.NoFill;

            _sides = 5;
            StartVertex = new Point();
            _endVertex = new Point();
        }

        [Browsable(false)]
        public List<Point> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
            //test
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
        public Point StartVertex { get; set; }

        [Browsable(false)]
        public Point EndVertex {
            get { return _endVertex; }
            set
            {
                _endVertex = value;
                FinishEdition();
            }
        }

        public int Sides
        {
            get { return _sides; }
            set
            {
                _sides = value;
                if (_sides < 3) _sides = 3;
                if (_sides > 100) _sides = 100;

                _vertices.Clear();
                for (int i = 0; i < _sides; i++)
                    _vertices.Add(new Point());
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return "Regular Polygon"; }
        }

        public void FinishEdition()
        {
            float step = 360.0f / _sides;

            var radius =
                (int)
                    Math.Sqrt((StartVertex.X - EndVertex.X)*(StartVertex.X - EndVertex.X) +
                              (StartVertex.Y - EndVertex.Y)*(StartVertex.Y - EndVertex.Y));
            var startingAngle = XYToDegrees(EndVertex, StartVertex);

            var angle = startingAngle; //starting angle
            double i;
            int k;

            for (i = startingAngle, k = 0; i < startingAngle + 360.0; i += step) //go in a full circle
            {
                _vertices[k++] = DegreesToXY(angle, radius, StartVertex); //code snippet from above
                angle += step;
            }
        }

        public ShapeType GetShapeType()
        {
            return ShapeType.RegPolygon;
        }

        public IShape Clone()
        {
            var obj = new RegPolygon
            {
                Sides = _sides,
                Fill = _fill
            };
            return obj;
        }

        private Point DegreesToXY(float degrees, float radius, Point origin)
        {
            var xy = new Point();
            double radians = degrees * Math.PI / 180.0;

            xy.X = (int)(Math.Cos(radians) * radius + origin.X);
            xy.Y = (int)(Math.Sin(-radians) * radius + origin.Y);

            return xy;
        }

        private float XYToDegrees(Point xy, Point origin)
        {
            int deltaX = origin.X - xy.X;
            int deltaY = origin.Y - xy.Y;

            double radAngle = Math.Atan2(deltaY, deltaX);
            double degreeAngle = radAngle * 180.0 / Math.PI;

            return (float)(180.0 - degreeAngle);
        }
    }
}
