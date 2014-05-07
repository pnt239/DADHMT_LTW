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

    public enum PageType
    {
        Custom = 1,
        A4
    }

    public interface IPage
    {
        Size PageSize { get; set; }

        PageOrientation Orientation { get; set; }

        BindingList<Layer> Layers { get; set; }

        PageType GetPageType();

        IPage Clone();
    }
}
