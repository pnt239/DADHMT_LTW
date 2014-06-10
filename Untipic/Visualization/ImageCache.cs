using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Untipic.Core;
using Untipic.Core.EventArguments;

namespace Visualization
{
    public class ImageCache
    {
        public ImageCache(Viewport viewport, ShapeDrawer shapeDrawer, Filler filler, Page page, int width, int height)
        {
            _viewport = viewport;
            _shapeDrawer = shapeDrawer;
            _filler = filler;
            _page = page;

            _page.ImageBuffer = new Bitmap(width, height);
            using (var graph = Graphics.FromImage(_page.ImageBuffer))
            {
                graph.Clear(Color.White);
            }

            _page.AddedShape += Page_AddedShape;
        }

        public void Paint(ShapeBase shape)
        {
            using (var graph = Graphics.FromImage(_page.ImageBuffer))
            {
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                _shapeDrawer.Draw(_viewport.WinToView(shape), graph);
            }
        }

        public void Render(Graphics g)
        {
            g.DrawImageUnscaled(_page.ImageBuffer, 0, 0);
        }

        public void SaveFile(Stream stream, System.Drawing.Imaging.ImageFormat format)
        {
            _page.ImageBuffer.Save(stream, format);
        }

        private void Page_AddedShape(object sender, AddedObjectEventArgs e)
        {
            if (e.Object.GetObjectType() == DrawingObjectType.Shape)
                Paint((ShapeBase) e.Object);
        }

        private ShapeDrawer _shapeDrawer;
        private Filler _filler;
        private Page _page;
        private Viewport _viewport;
    }
}
