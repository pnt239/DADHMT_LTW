using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Untipic.Visualization.FillAlgorithm
{
    internal class CActiveEdge : IComparable<CActiveEdge>
    {
        public int YUper { get; set; }
        public double XIntersection { get; set; }
        public double ReciSlope { get; set; }

        public int CompareTo(CActiveEdge other)
        {
            if (XIntersection < other.XIntersection)
                return -1;
            if (XIntersection > other.XIntersection)
                return 1;
            return 0;
        }
    }
    
}
