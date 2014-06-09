using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Untipic.MetroUI
{
    public class MetroDropDownButton : MetroButton
    {
        public MetroDropDownButton()
        {
            Direction = DockStyle.Left;
        }

        public ToolStripDropDown DropDown { get; set; }

        public DockStyle Direction { get; set; }

        protected override void OnClick(EventArgs e)
        {
            if (DropDown != null)
                DropDown.Show(GetPostionDropDown());
            base.OnClick(e);
        }

        private Point GetPostionDropDown()
        {
            var p = Parent.PointToScreen(Location);
            switch (Direction)
            {
                case DockStyle.Left:
                    p.X -= DropDown.GetPreferredSize(Size.Empty).Width + 10;
                    break;
            }

            return p;
        }
    }
}
