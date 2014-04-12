using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        BindingList<Layer> Layers { get; set; }

        IPage Clone();
    }
}
