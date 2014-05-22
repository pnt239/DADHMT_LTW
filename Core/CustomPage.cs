using System.ComponentModel;
using System.Drawing;

namespace TabletC.Core
{
    public class CustomPage : IPage
    {
        public CustomPage(MessureUnit unit, SizeF size)
        {
            _unit = unit;
            _pageSize = size;
            _orientation = PageOrientation.Horizontal;
            _view = new ViewPort(_unit, size);

            _layers = new BindingList<Layer>
            {
                new Layer(_pageSize) {Name = "Layer 1"}
            };
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The messur unit is used in current page.
        /// </value>
        public MessureUnit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public SizeF PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>
        /// The orientation of the page.
        /// </value>
        public PageOrientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }

        /// <summary>
        /// Gets the viewport.
        /// </summary>
        /// <value>
        /// The viewport is tool which manage all conversion unit form window to viewport and contrariwise.
        /// </value>
        public ViewPort View
        {
            get { return _view; }
        }

        /// <summary>
        /// Gets or sets the layers.
        /// </summary>
        /// <value>
        /// The list of layers in the page.
        /// </value>
        public BindingList<Layer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        /// <summary>
        /// Gets the type of the page.
        /// </summary>
        /// <returns></returns>
        public PageType GetPageType()
        {
            return PageType.Custom;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public IPage Clone()
        {
            return new CustomPage(_unit, _pageSize);
        }

        /// <summary>
        /// The messure unit
        /// </summary>
        private MessureUnit _unit;

        /// <summary>
        /// The page's size
        /// </summary>
        private SizeF _pageSize;

        /// <summary>
        /// The orientation of page
        /// </summary>
        private PageOrientation _orientation;

        /// <summary>
        /// The list of layers
        /// </summary>
        private BindingList<Layer> _layers;

        /// <summary>
        /// The viewport
        /// </summary>
        private ViewPort _view;
    }
}
