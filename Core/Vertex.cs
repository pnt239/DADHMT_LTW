using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Vertex : IVertex
    {
        const float EPSILON = 1.0e-15f;
        private double _x;
        private double _y;

        public Vertex()
        {
            _x = _y = 0;
        }

        public Vertex(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Vertex(Point point)
        {
            _x = point.X;
            _y = point.Y;
        }

        public bool IsEmpty
        {
            get { return (Math.Abs(X) < EPSILON) && (Math.Abs(Y) < EPSILON); }
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Point ToPoint()
        {
            return new Point((int) Math.Round(_x), (int) Math.Round(_y));
        }

        public IVertex Clone()
        {
            return new Vertex(_x, _y);
        }
    }
}
