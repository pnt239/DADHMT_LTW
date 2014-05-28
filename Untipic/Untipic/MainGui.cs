using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Untipic.Controls;

namespace Untipic
{
    public partial class MainGui : MetroForm
    {
        public MainGui()
        {
            InitializeComponent();

            tsbColorSize.Image = GenerateThumbSizeColor(Color.Black, 2F);
        }

        private void tsbShape_Click(object sender, EventArgs e)
        {
            var frmShape = new ShapeSelectionForm();
            Point pt =
                tsbShape.GetCurrentParent()
                    .PointToScreen(new Point(tsbShape.Bounds.Location.X + tsbShape.Bounds.Width + 10,
                        tsbShape.Bounds.Location.Y));
            frmShape.Location = pt;
            frmShape.FormClosed += frmShape_FormClosed;
            frmShape.Show();
            //tsbShape.Image = frmShape.SelectedButton.Image;
        }

        void frmShape_FormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = (ShapeSelectionForm)sender;
            if (frm.SelectedButton != null)
                tsbShape.Image = frm.SelectedButton.Image;
            frm.Dispose();
        }

        private Image GenerateThumbSizeColor(Color color, float width)
        {
            Image img = new Bitmap(48, 48);
            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.Transparent);
                using (var b = new SolidBrush(color))
                using (var p = new Pen(b, width))
                    g.DrawRectangle(p, new Rectangle(5, 9, 38, 30));
            }
            return img;
        }
    }
}
