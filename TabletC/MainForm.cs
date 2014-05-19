using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using TabletC.Core;
using TabletC.Adapters;
using TabletC.Processor;

namespace TabletC
{
    public partial class MainForm : RibbonForm
    {
        private readonly List<DrawPad.DrawPad> _listDrawPad;
        private int _currentTabId;
        private DrawPad.DrawPad _currentDrawPad;
        private AppProcessing _myapp;

        public MainForm()
        {
            InitializeComponent();

            // Init main processing
            _myapp = new AppProcessing();

            // Init color
            cpkBackgroundColor.SelectedColor = Color.White;
            cpkOutlineColor.SelectedColor = Color.Black;

            // Init brush
            btnBrushSolidColor.Tag = new SolidBrush(cpkBackgroundColor.SelectedColor);

            // Add dump shape to control.tag to use for cloning
            btnShapeLine.Tag = new Line();
            btnShapeRectangle.Tag = new Quad();
            btnShapeEllipse.Tag = new Ellipse();
            btnShapeTriangle.Tag = new Triangle();
            btnShapeRegPolygon.Tag = new RegPolygon();
            btnShapePolygon.Tag = new Polygon();

            // Create DrawPad Control List
            _listDrawPad = new List<DrawPad.DrawPad>();
            
            // Create custom page
            _currentTabId = 0; 
            CreateNewPage();

            // Select mode
            _currentDrawPad.SelectMode = DrawPad.SelectMode.Selection;
            // Pen and brush
            _currentDrawPad.CurrentPen.Color = cpkOutlineColor.SelectedColor;
            _currentDrawPad.CurrentBrush = (Brush) btnBrushSolidColor.Tag;
        }

        public void CreateNewPage()
        {
            var dckItem = new DockContainerItem();
            var dclPanel = new PanelDockContainer();
            _currentDrawPad = new DrawPad.DrawPad();

            barCenter.Controls.Add(dclPanel);
            barCenter.Items.Add(dckItem);

            // New tab
            dckItem.Control = dclPanel;
            dckItem.Name = String.Format("tabItem{0}", _currentTabId);
            dckItem.Text = String.Format("Untitled {0}", _currentTabId);

            // New panel for new tab
            dclPanel.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            dclPanel.Controls.Add(_currentDrawPad);
            dclPanel.Name = String.Format("tabPanel{0}", _currentTabId);
            dclPanel.Style.Alignment = StringAlignment.Center;
            dclPanel.Style.BackColor1.ColorSchemePart = eColorSchemePart.BarBackground;
            dclPanel.Style.ForeColor.ColorSchemePart = eColorSchemePart.ItemText;
            dclPanel.Style.GradientAngle = 90;
            dclPanel.TabIndex = 0;

            // New drawpad
            _currentDrawPad.BackColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            _currentDrawPad.Dock = DockStyle.Fill;
            _currentDrawPad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            _currentDrawPad.Name = String.Format("dpUntiled{0}", _currentTabId);
            _currentDrawPad.TabIndex = 0;

            _currentDrawPad.CurrentPage = new CustomPage(new Size(210, 297));
            _currentDrawPad.CurrentPage.Units = MessureUnit.Milimeters;

            sttPageSize.Text = string.Format("{0:F03} , {1:F03} {2}", _currentDrawPad.CurrentPage.PageSize.Width,
                _currentDrawPad.CurrentPage.PageSize.Height, Util.GetUnitSign(_currentDrawPad.ViewPort.Unit));

            _currentDrawPad.ShapeCreated += DrawPad_ShapeCreated;
            _currentDrawPad.MouseChanged += DrawPad_MouseChanged;

            _listDrawPad.Add(_currentDrawPad);

            // Connect to bindinglist
            lbxLayers.DataSource = _currentDrawPad.CurrentPage.Layers;
            lbxLayers.DisplayMember = "Name";
            _currentDrawPad.CurrentPage.Layers.ListChanged += Layers_ListChanged;

            _currentTabId++;
        }

        private void Layers_ListChanged(object sender, ListChangedEventArgs e)
        {
            lbxLayers.SelectedIndex = -1;
            lbxLayers.SelectedIndex = e.NewIndex;
        }

        private void DrawPad_ShapeCreated(object sender, EventArgs e)
        {
            pgdOptions.SelectedObject = _currentDrawPad.CurrentShape;
        }

        private void DrawPad_MouseChanged(object sender, MouseEventArgs e)
        {
            sttLocation.Text = string.Format("{0:F03} , {1:F03} {2}", _currentDrawPad.ViewPort.ViewToWin(e.X),
                _currentDrawPad.ViewPort.ViewToWin(e.Y), Util.GetUnitSign(_currentDrawPad.ViewPort.Unit));
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
            btnShapeEllipse.Checked = false;
            btnShapeLine.Checked = false;
            btnShapePolygon.Checked = false;
            btnShapeRectangle.Checked = false;
            btnShapeRegPolygon.Checked = false;
            btnShapeTriangle.Checked = false;
            var bi = (ButtonItem) sender;
            _currentDrawPad.CurrentShape = (IShape)bi.Tag;
            bi.Checked = true;


            _currentDrawPad.SelectMode = DrawPad.SelectMode.Draw;

            _currentDrawPad.Cursor = Cursors.Cross;

            pgdOptions.SelectedObject = _currentDrawPad.CurrentShape;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            _currentDrawPad.SelectMode = DrawPad.SelectMode.Selection;
            _currentDrawPad.Cursor = Cursors.Cross;
        }

        private void cpkBackgroundColor_SelectedColorChanged(object sender, EventArgs e)
        {
            if (_currentDrawPad != null)
                ((SolidBrush)_currentDrawPad.CurrentBrush).Color = cpkBackgroundColor.SelectedColor;
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

        private void btnDash_Click(object sender, EventArgs e)
        {
            var bti = (ButtonItem) sender;
            var dstyle = (System.Drawing.Drawing2D.DashStyle) int.Parse(bti.Tag.ToString());
            _currentDrawPad.CurrentPen.DashStyle = dstyle;
        }

        private void btnItemCursor_Click(object sender, EventArgs e)
        {
            _currentDrawPad.SelectMode = DrawPad.SelectMode.Selection;
            _currentDrawPad.Cursor = Cursors.Default;
        }

        private void btnItemDirectSel_Click(object sender, EventArgs e)
        {
            _currentDrawPad.SelectMode = DrawPad.SelectMode.DirectSelection;
            _currentDrawPad.Cursor = Cursors.Default;
        }

        private void mnuMergeShapes_Click(object sender, EventArgs e)
        {
            int count = lbxLayers.SelectedItems.Count;
            var l = (Layer)lbxLayers.SelectedItems[0];
            lbxLayers.SelectedItems.Remove(lbxLayers.SelectedItems[0]);

            var selobj = new List<Layer>();

            foreach (Layer layer in lbxLayers.SelectedItems)
                selobj.Add(layer);

            foreach (Layer layer in selobj)
            {
                l.Shapes.AddRange(layer.Shapes);
                _currentDrawPad.CurrentPage.Layers.Remove(layer);
            }
            l.IsRendered = false;
        }

        private void btnItemPaint_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var t = new SimpleAdapter();
            t.SaveTablet(_currentDrawPad.CurrentPage);
        }

        private void btnAreaCommon_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_myapp.CalculateArea(AreaMethod.Common, _currentDrawPad.CurrentShape).ToString(CultureInfo.InvariantCulture));
        }

        private void btnAreaIntegral_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_myapp.CalculateArea(AreaMethod.Integral, _currentDrawPad.CurrentShape).ToString(CultureInfo.InvariantCulture));
        }

        private void btnAreaTriangulator_Click(object sender, EventArgs e)
        {
            //var rec = Util.CreateBorder(_currentDrawPad.CurrentShape);
            //MessageBox.Show(_myapp.CalculateArea(AreaMethod.Triangulator, _currentDrawPad.CurrentShape).ToString(CultureInfo.InvariantCulture));
        }
    }
}