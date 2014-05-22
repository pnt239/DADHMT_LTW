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
        public ImageCache(ref ViewPort viewport)
        {
            // Diem bat dau
            _viewPort = viewport;
            _startPos = new Point(0, 0);
            _layers = new List<Layer>();
            _layer = null;

            _imageBuffer = new Bitmap(_viewPort.IWidth, _viewPort.IHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _graphicsBuffer = Graphics.FromImage(_imageBuffer);

            _imageTemp = new Bitmap(_viewPort.IWidth, _viewPort.IHeight);
            _graphicsTemp = Graphics.FromImage(_imageTemp);

            _layerRender = new LayerRenderer();
        }

        public void Render(Graphics graphs)
        {
            graphs.DrawImageUnscaled(_imageBuffer, 0, 0);

            if (_layer == null)
                return;

            // Render selected layer
            GraphDrawingContext graphContext = new GraphDrawingContext();
            graphContext.Graphs = _graphicsTemp;
            graphContext.ViewPort = _viewPort;
            
            _layerRender.Render(_layer, graphContext);

            // Draw selected layer on viewport
            graphs.DrawImageUnscaled(_imageTemp, 0, 0);
        }

        public void PushLayer(ref Layer layer)
        {
            if (_layer != null)
            {
                // Add old layer into cache
                _layers.Add(_layer);
                //_layerRender.Render(_layer);
                _graphicsBuffer.DrawImageUnscaled(_imageTemp, _startPos);
            }

            // Set new Layer as current layer for edition
            _layer = layer;
        }

        public void Dispose()
        {
            _graphicsTemp.Dispose();
            _imageTemp.Dispose();

            _graphicsBuffer.Dispose();
            _imageBuffer.Dispose();
        }

        private readonly Bitmap _imageBuffer;
        private readonly Graphics _graphicsBuffer;

        private Image _imageTemp;
        private Graphics _graphicsTemp;

        private readonly LayerRenderer _layerRender;

        private ViewPort _viewPort;
        private readonly Point _startPos;

        private Layer _layer;
        private readonly List<Layer> _layers;
    }
}
