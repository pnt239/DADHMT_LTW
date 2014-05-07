using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Edge
    {
        public Edge()
        {
            _start = _end = new Point();
        }

        public Edge(Point start, Point end)
        {
            _start = start;
            _end = end;
        }

        public Point StartVertex
        {
            get { return _start; }
            set { _start = value; }
        }

        public Point EndVertex
        {
            get { return _end; }
            set { _end = value; }
        }

        private Point _start;
        private Point _end;
        private float _m;
    }
}
