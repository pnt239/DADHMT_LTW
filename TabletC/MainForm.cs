using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using TabletC.Core;

namespace TabletC
{
    public partial class MainForm : RibbonForm
    {
        private readonly List<DrawPad.DrawPad> _listDrawPad;
        private int _currentTabId;
        private DrawPad.DrawPad _currentDrawPad;

        public MainForm()
        {
            InitializeComponent();

            // Add dump shape to control.tag to use for cloning
            btnShapeLine.Tag = new Line(new Point(), new Point());
            btnShapeRectangle.Tag = new Quad(new Point(), new Point());
            btnShapeEllipse.Tag = new Ellipse(new Point(), new Point());
            btnShapeTriangle.Tag = new Triangle(new Point(), new Point());
            btnItemPolygon.Tag = new Polygon(new Point(), new Point());

            // Init color
            cpkBackgroundColor.SelectedColor = Color.White;
            cpkOutlineColor.SelectedColor = Color.Black;

            // Create DrawPad Control List
            _listDrawPad = new List<DrawPad.DrawPad>();
            
            // Create custom page
            _currentTabId = 0;
            CreateNewPage();
        }

        public void CreateNewPage()
        {
            var dckItem = new DockContainerItem();
            var dclPanel = new PanelDockContainer();
            _currentDrawPad = new DrawPad.DrawPad();

            barCenter.Controls.Add(dclPanel);
            barCenter.Items.Add(dckItem);

            dckItem.Control = dclPanel;
            dckItem.Name = String.Format("tabItem{0}", _currentTabId);
            dckItem.Text = String.Format("Untitled {0}", _currentTabId);

            dclPanel.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            dclPanel.Controls.Add(_currentDrawPad);
            dclPanel.Name = String.Format("tabPanel{0}", _currentTabId);
            dclPanel.Style.Alignment = StringAlignment.Center;
            dclPanel.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            dclPanel.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            dclPanel.Style.GradientAngle = 90;
            dclPanel.TabIndex = 0;

            _currentDrawPad.BackColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            _currentDrawPad.Dock = DockStyle.Fill;
            _currentDrawPad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _currentDrawPad.Name = String.Format("dpUntiled{0}", _currentTabId);
            _currentDrawPad.TabIndex = 0;
            _currentDrawPad.CurrentPage = new CustomPage(new Size(720, 480));
            _listDrawPad.Add(_currentDrawPad);

            _currentTabId++;
        }

        private void barCenter_DockTabChange(object sender, DockTabChangeEventArgs e)
        {
            if (e.NewTab == null || barCenter.SelectedDockTab < 0 || barCenter.SelectedDockTab > _listDrawPad.Count - 1)
                return;

            _currentDrawPad = _listDrawPad[barCenter.SelectedDockTab];
        }

        private void btnShape_Click(object sender, EventArgs e)
        {
            // Get shape type
            var bi = (ButtonItem) sender;
            _currentDrawPad.CurrentShape = (IShape)bi.Tag;
            _currentDrawPad.DrawMode = DrawPad.DrawMode.Draw;

            _currentDrawPad.Cursor = Cursors.Cross;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _currentDrawPad.DrawMode = DrawPad.DrawMode.Select;
            _currentDrawPad.Cursor = Cursors.Cross;
        }

        private void cpkBackgroundColor_SelectedColorChanged(object sender, EventArgs e)
        {
            // Comming soon
        }

        private void cpkOutlineColor_SelectedColorChanged(object sender, EventArgs e)
        {
            // Get color of pen
            if (_currentDrawPad != null)
                _currentDrawPad.CurrentPen.Color = cpkOutlineColor.SelectedColor;
        }

        private void btnItemSize_Click(object sender, EventArgs e)
        {
            // Get size of pen
            var bti = (ButtonItem) sender;
            _currentDrawPad.CurrentPen.Width = int.Parse(bti.Tag.ToString());
        }

        private void btnItemUseAlg_Click(object sender, EventArgs e)
        {
            // Choose Algorithm to draw shape
            //lblDrawBy.Text = "Algorithm";
            //dpMain.UseLibrary = false;
        }

        private void btnItemUseLibrary_Click(object sender, EventArgs e)
        {
            // Choose openGL Library to draw shape
            //lblDrawBy.Text = "Library";
            //dpMain.UseLibrary = true;
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            var bti = (ButtonItem) sender;
            var dstyle = (System.Drawing.Drawing2D.DashStyle) int.Parse(bti.Tag.ToString());
            _currentDrawPad.CurrentPen.DashStyle = dstyle;
        }

        private void btnItemCursor_Click(object sender, EventArgs e)
        {
            _currentDrawPad.DrawMode = DrawPad.DrawMode.Select;
            _currentDrawPad.Cursor = Cursors.Default;
        }
    }
}