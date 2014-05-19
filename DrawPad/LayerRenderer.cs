using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public class LayerRenderer
    {
        private readonly ShapeDrawer _shapeDraw;
        private readonly ShapeFiller _shapeFill;

        public LayerRenderer()
        {
            _shapeDraw = new ShapeDrawer();
            _shapeFill = new ShapeFiller();
        }

        public void Render(Layer layer, GraphDrawingContext graphContext)
        {
            if (layer == null || layer.IsRendered)
                return;

            graphContext.Graphs.Clear(Color.Transparent);

            foreach (var shape in layer.Shapes)
            {
                //if (shape.Fill == FillType.ScanlineFill)
                //    _shapeFill.FillByScanline(g, shape);
                //_shapeFill.GdiFill(layer, shape);
                IShape viewShape = graphContext.ViewPort.WinToView(shape);

                _shapeDraw.Draw(viewShape, graphContext.Graphs);

                if (shape.Fill == FillType.FloodFill)
                    _shapeFill.FillByFlood(graphContext, viewShape, null);
            }
            
            layer.IsRendered = true;
        }
    }
}
