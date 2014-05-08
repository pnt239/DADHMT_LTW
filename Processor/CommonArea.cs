using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TabletC.Processor
{
    public class CommonArea
    {
        public static double Area(List<Point> point)
        {
            int n = point.Count();
            Point[] point1 = new Point[n + 1];
            point1[n] = point[0];
            
            for (int i = 0; i < n; i++)
            {
                point1[i] = point[i];
            }

            double s1 = 0;
            double s2 = 0;

            for (int i = 0; i < n; i++)
                s1 = s1 + point1[i].X * point1[i + 1].Y;

            for (int i = 0; i < n; i++)
                s2 = s2 + point1[i].Y * point1[i + 1].X;

            if ((s1 - s2) < 0)
                return -(s1 - s2) / 2 * 1.0;

            return (s1 - s2) / 2 * 1.0;

        }

    }
}
