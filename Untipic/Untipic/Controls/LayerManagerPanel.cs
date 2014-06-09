using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Untipic.MetroUI;

namespace Untipic.Controls
{
    public partial class LayerManagerPanel : UserControl
    {
        private enum LayerCommand
        {
            Add = 0,
            Remove,
            Up,
            Down
        }

        private class LayerControl : Control
        {
            public LayerControl()
            {
                _btnRemove = new MetroButton();
                _btnDown = new MetroButton();
                _btnUp = new MetroButton();
                SuspendLayout();

                _btnRemove.Name = "_btnRemove";
                _btnRemove.Size = new Size(60, 40);
                _btnRemove.Location = new Point(0, 0);
                _btnRemove.TabIndex = 1;
                _btnRemove.Image = Properties.Resources.Remove;
                _btnRemove.Tag = LayerCommand.Remove;
                _btnRemove.Click += Button_Click;

                _btnUp.Size = new Size(60, 40);
                _btnUp.Location = new Point(60, 0);
                _btnUp.Image = Properties.Resources.Up;
                _btnUp.Tag = LayerCommand.Up;
                _btnUp.Click += Button_Click;

                _btnDown.Size = new Size(60, 40);
                _btnDown.Location = new Point(120, 0);
                _btnDown.Image = Properties.Resources.Down;
                _btnDown.Tag = LayerCommand.Down;
                _btnDown.Click += Button_Click;

                Controls.Add(_btnRemove);
                Controls.Add(_btnUp);
                Controls.Add(_btnDown);
                base.BackColor = Color.White;
                Size = new Size(180, 40);
                ResumeLayout(false);
            }

            public event EventHandler ControlsClicked = null;

            void Button_Click(object sender, EventArgs e)
            {
                if (ControlsClicked != null)
                    ControlsClicked(sender, e);
            }

            private MetroButton _btnRemove;
            private MetroButton _btnDown;
            private MetroButton _btnUp;
        }

        public LayerManagerPanel()
        {
            InitializeComponent();

            // Create other command
            var control = new LayerControl();
            control.ControlsClicked += LayerCommand_ControlsClicked;
            // Add to dropdown list
            var dropdown = new ToolStripDropDown();
            dropdown.Items.Add(new ToolStripControlHost(control));
            btnLayer.DropDown = dropdown;

            // Add sign
            btnAdd.Tag = LayerCommand.Add;

            // Set border width
            _borderWidth = 4F;

            // Set serveral option for paint
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint, true);
        }

        private void LayerManagerPanel_Load(object sender, EventArgs e)
        {
            layerListBox.Add("Layer 1");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var b = new SolidBrush(Color.FromArgb(0xcc, 0xcc, 0xcc)))
            using (var p = new Pen(b, _borderWidth))
            {
                e.Graphics.DrawLine(p, 1, 0, Width, 1);
                e.Graphics.DrawLine(p, 1, 0, 1, Height);
                e.Graphics.DrawLine(p, 0, Height - 1, Width, Height - 1);
            }
        }

        private void LayerCommand_ControlsClicked(object sender, EventArgs e)
        {
            var ctrl = sender as Control;
            switch ((LayerCommand) ctrl.Tag)
            {
                case LayerCommand.Add:
                    AddLayer();
                    break;
                case LayerCommand.Remove:
                    RemoveSelectedLayer();
                    btnLayer.DropDown.Close();
                    break;
                case LayerCommand.Down:
                    MoveDownSelectedLayer();
                    btnLayer.DropDown.Close();
                    break;
                case LayerCommand.Up:
                    MoveUpSelectedLayer();
                    btnLayer.DropDown.Close();
                    break;
            }
        }

        private void AddLayer()
        {
            layerListBox.Add("Layer " + (layerListBox.Count + 1).ToString(CultureInfo.InvariantCulture));
        }

        private void RemoveSelectedLayer()
        {
            int id = layerListBox.SelectedIndex;
            if (id >= 0)
                layerListBox.Remove(id);
        }

        private void MoveUpSelectedLayer()
        {
            int id = layerListBox.SelectedIndex;
            layerListBox.SwapLayer(id, id + 1);
        }

        private void MoveDownSelectedLayer()
        {
            int id = layerListBox.SelectedIndex;
            layerListBox.SwapLayer(id, id - 1);
        }

        private float _borderWidth;
    }
}
