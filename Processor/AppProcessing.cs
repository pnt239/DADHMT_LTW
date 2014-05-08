using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.Processor
{
    public enum AreaMethod
    {
        Integral, Common, Triangulator
    }

    public class AppProcessing
    {
        public double CalculateArea(AreaMethod method, IShape shape)
        {
            IntegralArea ia = new IntegralArea();

            switch (method)
            {
                case AreaMethod.Triangulator:
                {
                    Triangulator tri = new Triangulator(shape.Vertices);
                    return tri.Area();
                }
            }
            return 0; //ia.CalculatePolygonArea(ref shape);
        }
    }
}
