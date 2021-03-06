﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using Untipic.Core;

namespace Untipic.Visualization
{
    public class ShapeDrawer
    {
        public ShapeDrawer()
        {
            _filler = new Filler();
        }

        public void Draw(ShapeBase shape, Graphics graphic)
        {
            if (shape == null)
                return;
            /* Draw shape*/
            switch (shape.GetShapeType())
            {
                case ShapeType.Line:
                case ShapeType.IsoscelesTriangle:
                case ShapeType.Oblong:
                case ShapeType.FreePencil:
                case ShapeType.Polygon:
                    DrawPolygon((PolygonBase)shape, graphic);
                    break;
                case ShapeType.Ellipse:
                    DrawEllipse((Ellipse)shape, graphic);
                    break;
            }
        }

        public void DrawLine(Line line, Graphics graphs)
        {
            var path = new GraphicsPath();
            path.AddLines(line.Vertices.ToPoints());

            using (var p = new Pen(line.OutlineColor, line.OutlineWidth))
            {
                p.DashStyle = line.OutlineDash;
                graphs.DrawPath(p, path);
            }
        }

        private void DrawPolygon(PolygonBase polygon, Graphics graphs)
        {
            if (polygon.Vertices.Count < 2)
                return;

            var path = new GraphicsPath();
            if (polygon.IsClosedFigure) path.StartFigure();
            path.AddLines(polygon.Vertices.ToPoints());
            if (polygon.IsClosedFigure) path.CloseFigure();

            using (var b = new SolidBrush(polygon.FillColor))
            using (var p = new Pen(polygon.OutlineColor, polygon.OutlineWidth))
            {
                p.DashStyle = polygon.OutlineDash;
                if (polygon.GetShapeType() != ShapeType.FreePencil) graphs.FillPath(b, path);
                //_filler.FillByScanline(graphs, polygon, polygon.FillColor);
                graphs.DrawPath(p, path);
            }
        }

        private void DrawEllipse(Ellipse ellipse, Graphics graphs)
        {
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddEllipse(Util.GetShapeBound(ellipse));
            path.CloseFigure();

            using (var b = new SolidBrush(ellipse.FillColor))
            using (var p = new Pen(ellipse.OutlineColor, ellipse.OutlineWidth))
            {
                p.DashStyle = ellipse.OutlineDash;
                graphs.FillPath(b, path);
                //_filler.FillByScanline(graphs, ellipse, ellipse.FillColor);
                graphs.DrawPath(p, path);
            }
        }

        private Filler _filler;
    }
}
