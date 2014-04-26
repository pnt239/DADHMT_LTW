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
            _startPos = new Point(0, 0);
            _nameLayers = new List<string>();
            _imageBuffer = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _graphicsBuffer = Graphics.FromImage(_imageBuffer);
        }

        public Bitmap ImageBuffer
        {
            get { return _imageBuffer; }
            set { _imageBuffer = value; }
        }

        public void AddLayers(BindingList<Layer> layers)
        {
            foreach (Layer layer in layers)
            {
                if (!_nameLayers.Contains(layer.Name))
                    _graphicsBuffer.DrawImageUnscaled(layer.ImageBuffer, _startPos);
            }
        }

        public bool AddLayer(Layer layer)
        {
            if (!_nameLayers.Contains(layer.Name))
            {
                _nameLayers.Add(layer.Name);
                _graphicsBuffer.DrawImageUnscaled(layer.ImageBuffer, _startPos);
                return true;
            }

            return false;
        }

        public bool IsExist(Layer layer)
        {
            return _nameLayers.Contains(layer.Name);
        }

        private Bitmap _imageBuffer;
        private Graphics _graphicsBuffer;
        private Point _startPos;

        private List<string> _nameLayers;
    }
}
