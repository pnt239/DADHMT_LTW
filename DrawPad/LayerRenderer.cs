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
            if (layer.IsRendered)
                return;

            layer.GraphicsBuffer.Clear(Color.FromArgb(0));

            foreach (IShape shape in layer.Shapes)
            {
                _shapeDraw.Draw(layer.GraphicsBuffer, shape);
                _shapeFill.FloodFill(layer, shape, null);
            }
            
            layer.IsRendered = true;
        }
    }
}
