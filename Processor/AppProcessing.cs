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
        //public double CalculateArea(AreaMethod method, IShape shape)
        //{
        //    switch (shape.GetShapeType())
        //    {
        //        case ShapeType.Ellipse:
        //            var elip = (Ellipse) shape;
        //            return Math.PI*elip.MajorAxis*elip.MinorAxis;
        //    }

        //    switch (method)
        //    {
        //        case AreaMethod.Common:
        //            return CommonArea.Area(shape.Vertices);
        //        case AreaMethod.Triangulator:
        //        {
        //            Triangulator tri = new Triangulator(shape.Vertices);
        //            return tri.Area();
        //        }
        //    }

        //    IntegralArea ia = new IntegralArea();
        //    return ia.CalculatePolygonArea(shape); //ia.CalculatePolygonArea(ref shape);
        //}
    }
}
