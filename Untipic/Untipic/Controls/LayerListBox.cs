using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Untipic.Controls
{
    public class LayerListBox : Control
    {
        public LayerListBox()
        {
            InitializeComponent();

            _lastSelected = null;
        }

        public event EventHandler ItemClicked = null;

        public int Count {get { return _flpListBox.Controls.Count; }}

        public int SelectedIndex
        {
            get
            {
                if (_lastSelected != null)
                    return _lastSelected.Id;
                return -1;
            }
        }

        public void Add(string name = "")
        {
            var item = new LayerListBoxItem
            {
                Name = name,
                Size = new Size(Width - SystemInformation.VerticalScrollBarWidth, 80),
                Margin = new Padding(0),
                Id = Count
            };

            item.SelectionChanged += Item_SelectionChanged;
            item.LayerVisibleChanged += Item_LayerVisibleChanged;

            _flpListBox.Controls.Add(item);
            SelectLayer(item.Id);
            SetupAnchors();
        }

        public void Remove(int id)
        {
            _flpListBox.Controls.RemoveAt(id);
            if (Count > 0)
                SelectLayer(id);
        }

        public void SwapLayer(int idLayer1, int idLayer2)
        {
            if (idLayer1 < 0 || idLayer1 >= Count)
                return;
            if (idLayer2 < 0 || idLayer2 >= Count)
                return;

            var item1 = (LayerListBoxItem)_flpListBox.Controls[idLayer1];
            var item2 = (LayerListBoxItem)_flpListBox.Controls[idLayer2];
            item1.Id = idLayer2;
            item2.Id = idLayer1;

            var location1 = item1.Location;
            var location2 = item2.Location;

            item2.Location = location1;
            item1.Location = location2;

            _flpListBox.Controls.SetChildIndex(item1, idLayer2);
            _flpListBox.Controls.SetChildIndex(item2, idLayer1);
        }

        public void SelectLayer(int id)
        {
            if (id < 0 || id >= Count)
            {
                _lastSelected = null;
                return;
            }

            if (_lastSelected != null)
                _lastSelected.Selected = false;
            (_lastSelected = (LayerListBoxItem) _flpListBox.Controls[id]).Selected = true;
        }

        protected void OnItemClicked(object sender, EventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked(sender, e);

            _flpListBox.Focus();
        }

        private void InitializeComponent()
        {
            _flpListBox = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // _flpListBox
            // 
            _flpListBox.AutoScroll = true;
            _flpListBox.HorizontalScroll.Visible = false;
            _flpListBox.Dock = DockStyle.Fill;
            _flpListBox.FlowDirection = FlowDirection.BottomUp;
            _flpListBox.Location = new Point(0, 0);
            _flpListBox.Margin = new Padding(0);
            _flpListBox.Name = "_flpListBox";
            _flpListBox.Size = new Size(100, 208);
            _flpListBox.TabIndex = 0;
            _flpListBox.WrapContents = false;
            _flpListBox.Resize += _flpListBox_Resize;
            // 
            // LayerListBox
            // 
            BackColor = Color.White;
            Controls.Add(_flpListBox);
            Font = new Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LayerListBox";
            Size = new Size(100, 208);
            ResumeLayout(false);
        }

        private void Item_SelectionChanged(object sender, EventArgs e)
        {
            if (_lastSelected != null)
                _lastSelected.Selected = false;
            _lastSelected = (LayerListBoxItem) sender;

            OnItemClicked(_lastSelected, EventArgs.Empty);
        }

        private void Item_LayerVisibleChanged(object sender, EventArgs e)
        {
            //
        }

        private void _flpListBox_Resize(object sender, EventArgs e)
        {
            //
        }

        private void SetupAnchors()
        {
            if (_flpListBox.Controls.Count > 0)
            {
                Control c = _flpListBox.Controls[0];
                // Its the first control, all subsequent controls follow 
                // the anchor behavior of this control.
                c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                c.Width = _flpListBox.Width - SystemInformation.VerticalScrollBarWidth;

                for (int i = 1; i < _flpListBox.Controls.Count - 1; i++)
                {
                    c = _flpListBox.Controls[i];

                    // It is not the first control. Set its anchor to
                    // copy the width of the first control in the list.
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                }

            }
        }

        private FlowLayoutPanel _flpListBox;
        private LayerListBoxItem _lastSelected;
    }
}
