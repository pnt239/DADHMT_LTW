using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TabletC.Core
{
    public enum PageOrientation
    {
        Vertical = 1, Horizontal
    }

    public interface IPage
    {
        Size PageSize { get; set; }

        PageOrientation Orientation { get; set; }

        /* List of shapes */
        List<IShape> Shapes { get; set; }

        IPage Clone();
    }
}
