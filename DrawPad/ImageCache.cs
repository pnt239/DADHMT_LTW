using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public class ImageCache
    {
        public ImageCache(Size size)
        {
            // Diem bat dau
            _startPos = new Point(0, 0);
            _layers = new List<Layer>();
            _layer = null;

            _imageBuffer = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _graphicsBuffer = Graphics.FromImage(_imageBuffer);
            _layerRender = new LayerRenderer();
        }

        public void Render(Graphics g)
        {
            g.DrawImageUnscaled(_imageBuffer, 0, 0);

            if (_layer == null)
                return;

            _layerRender.Render(_layer);
            g.DrawImageUnscaled(_layer.ImageBuffer, 0, 0);
        }

        public void PushLayer(ref Layer layer)
        {
            if (_layer != null)
            {
                // Add old layer into cache
                _layers.Add(_layer);
                _layerRender.Render(_layer);
                _graphicsBuffer.DrawImageUnscaled(_layer.ImageBuffer, _startPos);
            }

            // Set new Layer as current layer for edition
            _layer = layer;
        }

        private readonly Bitmap _imageBuffer;
        private readonly Graphics _graphicsBuffer;
        private readonly LayerRenderer _layerRender;
        private readonly Point _startPos;

        private Layer _layer;
        private readonly List<Layer> _layers;
    }
}
