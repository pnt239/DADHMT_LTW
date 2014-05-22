using System.ComponentModel;
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

    public enum MessureUnit
    {
        Pixel, Centimeters, Milimeters, Inches
    }

    public interface IPage
    {
        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The messur unit is used in current page.
        /// </value>
        MessureUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        SizeF PageSize { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>
        /// The orientation of the page.
        /// </value>
        PageOrientation Orientation { get; set; }

        /// <summary>
        /// Gets the viewport.
        /// </summary>
        /// <value>
        /// The viewport is tool which manage all conversion unit form window to viewport and contrariwise.
        /// </value>
        ViewPort View { get; }

        /// <summary>
        /// Gets or sets the layers.
        /// </summary>
        /// <value>
        /// The list of layers in the page.
        /// </value>
        BindingList<Layer> Layers { get; set; }

        /// <summary>
        /// Gets the type of the page.
        /// </summary>
        /// <returns></returns>
        PageType GetPageType();

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        IPage Clone();
    }
}
