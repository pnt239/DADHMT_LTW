﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public class LayerRenderer
    {
        public static void Render(Layer layer)
        {
            if (layer.IsRendered)
                return;

            ShapeDrawer sd = new ShapeDrawer();

            layer.GraphicsBuffer.Clear(Color.FromArgb(0));

            foreach (IShape shape in layer.Shapes)
                sd.Draw(layer.GraphicsBuffer, shape);
            
            //layer.IsRendered = true;
        }
    }
}