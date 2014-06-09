using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Untipic.Util;

namespace Untipic.Controls
{
    public class LayerListBoxItem : Control
    {
        public LayerListBoxItem()
        {
            Initialize();

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        }

        public event EventHandler SelectionChanged = null;

        public event EventHandler LayerVisibleChanged = null;

        public int Id { get; set; }

        public Image ThumbImage
        {
            get { return _thumbImage; }
            set { _thumbImage = value; }
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_isHovered)
            {
                using (var b = new SolidBrush(Color.FromArgb(0xe6, 0xe6, 0xe6)))
                    pevent.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
            }

            if (_selected)
            {
                using (var b = new SolidBrush(Color.FromArgb(0x4d, 0x4d, 0x4d)))
                    pevent.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
            }

            if (_thumbImage != null)
            {
                var myThumbnail = _thumbImage.GetThumbnailImage(
                    _thumbBound.Width - (int)WidthBorder, _thumbBound.Height - (int)WidthBorder, 
                    ThumbnailCallback, IntPtr.Zero);

                pevent.Graphics.DrawImage(Drawer.RoundImage(myThumbnail, 5, Color.White),
                    _thumbBound.X + WidthBorder/2, _thumbBound.Y + WidthBorder/2);
            }

            using (var p = new Pen(Color.FromArgb(125, 0xcc, 0xcc, 0xcc), WidthBorder))
                pevent.Graphics.DrawPath(p, Drawer.RoundRectangle(_thumbBound, 10, Corners.All));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _thumbBound = new Rectangle(_thumbMargin.Left, _thumbMargin.Top,
                Width - _thumbMargin.Left - _thumbMargin.Right, Height - _thumbMargin.Top - _thumbMargin.Bottom);

            base.OnSizeChanged(e);
        }

        private void Initialize()
        {
            // Init margin-
            _thumbMargin = new Padding(15, 15, 15, 15);
            _thumbBound = new Rectangle(_thumbMargin.Left, _thumbMargin.Top,
                Width - _thumbMargin.Left - _thumbMargin.Right, Height - _thumbMargin.Top - _thumbMargin.Bottom);

            // Init some field
            _selected = false;
            _isHovered = false;
            _layerVisible = true;

            // Init control
            _singleCheckBox = new SingleCheckBox();

            _thumbImage = null;

            SuspendLayout();

            _singleCheckBox.BackColor = Color.Transparent;
            _singleCheckBox.Checked = _layerVisible;
            _singleCheckBox.Size = new Size(24, 24);
            _singleCheckBox.Location = new Point(_thumbBound.Left - _singleCheckBox.Width/2,
                _thumbBound.Top - _singleCheckBox.Height/2);
            _singleCheckBox.Click += SingleCheckBox_Click;

            Controls.Add(_singleCheckBox);
            MouseClick += LayerListBoxItem_MouseClick;
            MouseEnter += LayerListBoxItem_MouseEnter;
            MouseLeave += LayerListBoxItem_MouseLeave;
            ResumeLayout();
        }

        private void SingleCheckBox_Click(object sender, EventArgs e)
        {
            if (LayerVisibleChanged != null)
                LayerVisibleChanged(this, EventArgs.Empty);
        }

        private void LayerListBoxItem_MouseLeave(object sender, EventArgs e)
        {
            if (_isHovered)
            {
                _isHovered = false;
                Invalidate();
            }
        }

        private void LayerListBoxItem_MouseEnter(object sender, EventArgs e)
        {
            if (!_selected & !_isHovered)
            {
                _isHovered = true;
                Invalidate();
            }
        }

        private void LayerListBoxItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_selected)
            {
                _selected = true;
                _isHovered = false;

                Invalidate();

                if (SelectionChanged != null)
                    SelectionChanged(this, EventArgs.Empty);
            }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
        
        private SingleCheckBox _singleCheckBox;

        private const float WidthBorder = 7f;
        private Padding _thumbMargin;
        private Rectangle _thumbBound;

        private Image _thumbImage;
        private bool _selected;
        private bool _layerVisible;
        private bool _isHovered;
    }
}
