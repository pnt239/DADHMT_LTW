using System.Drawing;
using TabletC.Core;

namespace TabletC.DrawPad
{
    public class GraphDrawingContext
    {
        public GraphDrawingContext()
        {
        }

        public Graphics Graphs { get; set; }

        public Bitmap Bitmap { get; set; }

        public ViewPort ViewPort { get; set; }
    }
}
