using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TabletC.Core
{
    public class Vector
    {
        public Vector()
        {
            X = Y = 0;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double SnapAngle(double angleSegment)
        {
            double r = Math.Sqrt(_x*_x + _y*_y);

            if (_y > 0)
            {
                //
            }
            else
            {
                //
            }

            return 0.1;
        }

        private int _x;
        private int _y;
    }
}
