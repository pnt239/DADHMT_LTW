using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.Processor
{
    public enum AreaMethod
    {
        Integral, Common
    }

    public class AppProcessing
    {
        public double CalculateArea(AreaMethod method, IShape shape)
        {
            IntegralArea ia = new IntegralArea();

            switch (method)
            {
                case AreaMethod.Common:
                {
                    //
                }
                    break;
            }
            return 0; //ia.CalculatePolygonArea(ref shape);
        }
    }
}
