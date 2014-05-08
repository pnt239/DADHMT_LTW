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

        public void Render(Layer layer)
        {
            if (layer == null || layer.IsRendered)
                return;

            layer.GraphicsBuffer.Clear(Color.FromArgb(0));

            foreach (var shape in layer.Shapes)
            {
                if (shape.Fill == FillType.ScanlineFill)
                    _shapeFill.FillByScanline(ref layer, shape);
                //_shapeFill.GdiFill(layer, shape);

                _shapeDraw.Draw(layer.GraphicsBuffer, shape);

                if (shape.Fill == FillType.FloodFill)
                    _shapeFill.FillByFlood(layer, shape, null);
            }
            
            layer.IsRendered = true;
        }
    }
}
