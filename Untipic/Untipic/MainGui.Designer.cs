﻿using Untipic.Controls;
using Untipic.MetroUI;

namespace Untipic
{
    partial class MainGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGui));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.mtsEdit = new Untipic.MetroUI.MetroToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbFitSize = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFont = new System.Windows.Forms.ToolStripButton();
            this.tsbAccounts = new System.Windows.Forms.ToolStripButton();
            this.metroStatusStrip1 = new Untipic.MetroUI.MetroStatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpView = new System.Windows.Forms.TableLayoutPanel();
            this.mtsTool = new Untipic.MetroUI.MetroToolStrip();
            this.tsbToolSelection = new Untipic.MetroUI.MetroToolStripButton();
            this.tsbToolDirectSel = new Untipic.MetroUI.MetroToolStripButton();
            this.tsbToolText = new Untipic.MetroUI.MetroToolStripButton();
            this.tsbToolShape = new Untipic.Controls.ToolStripShapeSelectorButton();
            this.tsbToolBrush = new Untipic.MetroUI.MetroToolStripButton();
            this.tsbToolEraser = new Untipic.MetroUI.MetroToolStripButton();
            this.tsbToolBucket = new Untipic.MetroUI.MetroToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbToolOutline = new Untipic.Controls.ToolStripOutlineButton();
            this.tsbToolFill = new Untipic.Controls.ToolStripFillButton();
            this.drawPad = new Untipic.Controls.DrawPad();
            this.nameBar = new Untipic.Controls.MetroNameBar();
            this.tlpMain.SuspendLayout();
            this.mtsEdit.SuspendLayout();
            this.metroStatusStrip1.SuspendLayout();
            this.tlpView.SuspendLayout();
            this.mtsTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.mtsEdit, 0, 0);
            this.tlpMain.Controls.Add(this.metroStatusStrip1, 0, 3);
            this.tlpMain.Controls.Add(this.tlpView, 0, 2);
            this.tlpMain.Controls.Add(this.nameBar, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(5, 65);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(465, 630);
            this.tlpMain.TabIndex = 0;
            // 
            // mtsEdit
            // 
            this.mtsEdit.BackColor = System.Drawing.Color.White;
            this.mtsEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtsEdit.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mtsEdit.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mtsEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbSaveAs,
            this.toolStripSeparator2,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator3,
            this.tsbZoomIn,
            this.tsbFitSize,
            this.tsbZoomOut,
            this.toolStripSeparator4,
            this.tsbFont,
            this.tsbAccounts});
            this.mtsEdit.Location = new System.Drawing.Point(0, 0);
            this.mtsEdit.Name = "mtsEdit";
            this.mtsEdit.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.mtsEdit.Size = new System.Drawing.Size(465, 39);
            this.mtsEdit.TabIndex = 2;
            this.mtsEdit.Text = "mtsEdit";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::Untipic.Properties.Resources.New;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(36, 36);
            this.tsbNew.Text = "New";
            this.tsbNew.ToolTipText = "New page";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::Untipic.Properties.Resources.Open;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(36, 36);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.ToolTipText = "Open page";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::Untipic.Properties.Resources.Save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(36, 36);
            this.tsbSave.Text = "Save";
            this.tsbSave.ToolTipText = "Save page";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAs.Image = global::Untipic.Properties.Resources.SaveAs;
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(36, 36);
            this.tsbSaveAs.Text = "Save as";
            this.tsbSaveAs.ToolTipText = "Save as page";
            this.tsbSaveAs.Click += new System.EventHandler(this.tsbSaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = global::Untipic.Properties.Resources.Undo;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(36, 36);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = global::Untipic.Properties.Resources.Redo;
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(36, 36);
            this.tsbRedo.Text = "Redo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = global::Untipic.Properties.Resources.ZoomIn;
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(36, 36);
            this.tsbZoomIn.Text = "Zoom In";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbFitSize
            // 
            this.tsbFitSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFitSize.Image = global::Untipic.Properties.Resources.FitSize;
            this.tsbFitSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFitSize.Name = "tsbFitSize";
            this.tsbFitSize.Size = new System.Drawing.Size(36, 36);
            this.tsbFitSize.Text = "Fit Size";
            this.tsbFitSize.Click += new System.EventHandler(this.tsbFitSize_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = global::Untipic.Properties.Resources.ZoomOut;
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(36, 36);
            this.tsbZoomOut.Text = "Zoom Out";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton1
            // 
            this.tsbFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFont.Image = global::Untipic.Properties.Resources.Font;
            this.tsbFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFont.Name = "toolStripButton1";
            this.tsbFont.Size = new System.Drawing.Size(36, 36);
            this.tsbFont.Text = "tsbFont";
            this.tsbFont.Click += new System.EventHandler(this.tsbFont_Click);
            // 
            // tsbAccounts
            // 
            this.tsbAccounts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAccounts.Image = global::Untipic.Properties.Resources.Accounts;
            this.tsbAccounts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAccounts.Name = "tsbAccounts";
            this.tsbAccounts.Size = new System.Drawing.Size(36, 36);
            this.tsbAccounts.Text = "Accounts";
            this.tsbAccounts.Click += new System.EventHandler(this.tsbAccounts_Click);
            // 
            // metroStatusStrip1
            // 
            this.metroStatusStrip1.AutoSize = false;
            this.metroStatusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.metroStatusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroStatusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.metroStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.metroStatusStrip1.Location = new System.Drawing.Point(0, 600);
            this.metroStatusStrip1.Name = "metroStatusStrip1";
            this.metroStatusStrip1.Size = new System.Drawing.Size(465, 30);
            this.metroStatusStrip1.SizingGrip = false;
            this.metroStatusStrip1.TabIndex = 6;
            this.metroStatusStrip1.Text = "metroStatusStrip1";
            // 
            // tslStatus
            // 
            this.tslStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(0, 25);
            // 
            // tlpView
            // 
            this.tlpView.ColumnCount = 2;
            this.tlpView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tlpView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpView.Controls.Add(this.mtsTool, 0, 0);
            this.tlpView.Controls.Add(this.drawPad, 1, 0);
            this.tlpView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpView.Location = new System.Drawing.Point(0, 69);
            this.tlpView.Margin = new System.Windows.Forms.Padding(0);
            this.tlpView.Name = "tlpView";
            this.tlpView.RowCount = 1;
            this.tlpView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpView.Size = new System.Drawing.Size(465, 531);
            this.tlpView.TabIndex = 1;
            // 
            // mtsTool
            // 
            this.mtsTool.BackColor = System.Drawing.Color.White;
            this.mtsTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.mtsTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mtsTool.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.mtsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbToolSelection,
            this.tsbToolDirectSel,
            this.tsbToolText,
            this.tsbToolShape,
            this.tsbToolBrush,
            this.tsbToolEraser,
            this.tsbToolBucket,
            this.toolStripSeparator1,
            this.tsbToolOutline,
            this.tsbToolFill});
            this.mtsTool.Location = new System.Drawing.Point(0, 0);
            this.mtsTool.Name = "mtsTool";
            this.mtsTool.Padding = new System.Windows.Forms.Padding(0, 5, 1, 0);
            this.mtsTool.Size = new System.Drawing.Size(53, 531);
            this.mtsTool.TabIndex = 3;
            this.mtsTool.Text = "metroToolStrip2";
            // 
            // tsbToolSelection
            // 
            this.tsbToolSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolSelection.Image = global::Untipic.Properties.Resources.Selection;
            this.tsbToolSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolSelection.IsDropDownButton = false;
            this.tsbToolSelection.Name = "tsbToolSelection";
            this.tsbToolSelection.Size = new System.Drawing.Size(50, 52);
            this.tsbToolSelection.Text = "Selection";
            this.tsbToolSelection.ToolTipText = "Selection";
            // 
            // tsbToolDirectSel
            // 
            this.tsbToolDirectSel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolDirectSel.Image = global::Untipic.Properties.Resources.DirectSelection;
            this.tsbToolDirectSel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolDirectSel.IsDropDownButton = false;
            this.tsbToolDirectSel.Name = "tsbToolDirectSel";
            this.tsbToolDirectSel.Size = new System.Drawing.Size(50, 52);
            this.tsbToolDirectSel.Text = "Direct Selection";
            this.tsbToolDirectSel.ToolTipText = "Direct Selection";
            // 
            // tsbToolText
            // 
            this.tsbToolText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolText.Image = global::Untipic.Properties.Resources.Text;
            this.tsbToolText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolText.IsDropDownButton = false;
            this.tsbToolText.Name = "tsbToolText";
            this.tsbToolText.Size = new System.Drawing.Size(50, 52);
            this.tsbToolText.Text = "Text";
            this.tsbToolText.ToolTipText = "Draw text";
            // 
            // tsbToolShape
            // 
            this.tsbToolShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolShape.Image = global::Untipic.Properties.Resources.Line;
            this.tsbToolShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolShape.IsDropDownButton = true;
            this.tsbToolShape.Name = "tsbToolShape";
            this.tsbToolShape.Size = new System.Drawing.Size(50, 52);
            this.tsbToolShape.Text = "Shape";
            this.tsbToolShape.ToolTipText = "Draw shape";
            // 
            // tsbToolBrush
            // 
            this.tsbToolBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolBrush.Image = global::Untipic.Properties.Resources.Brush;
            this.tsbToolBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolBrush.IsDropDownButton = false;
            this.tsbToolBrush.Name = "tsbToolBrush";
            this.tsbToolBrush.Size = new System.Drawing.Size(50, 52);
            this.tsbToolBrush.Text = "Crop";
            this.tsbToolBrush.ToolTipText = "Crop";
            // 
            // tsbToolEraser
            // 
            this.tsbToolEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolEraser.Image = global::Untipic.Properties.Resources.Eraser;
            this.tsbToolEraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolEraser.Name = "tsbToolEraser";
            this.tsbToolEraser.Size = new System.Drawing.Size(50, 52);
            this.tsbToolEraser.Text = "toolStripButton2";
            // 
            // tsbToolBucket
            // 
            this.tsbToolBucket.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolBucket.Image = global::Untipic.Properties.Resources.Bucket;
            this.tsbToolBucket.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToolBucket.Name = "tsbToolBucket";
            this.tsbToolBucket.Size = new System.Drawing.Size(50, 52);
            this.tsbToolBucket.Text = "toolStripButton3";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(50, 6);
            // 
            // tsbToolOutline
            // 
            this.tsbToolOutline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolOutline.Image = ((System.Drawing.Image)(resources.GetObject("tsbToolOutline.Image")));
            this.tsbToolOutline.IsDropDownButton = true;
            this.tsbToolOutline.Name = "tsbToolOutline";
            this.tsbToolOutline.OutlineColor = System.Drawing.Color.Black;
            this.tsbToolOutline.OutlineDash = System.Drawing.Drawing2D.DashStyle.Solid;
            this.tsbToolOutline.OutlineWidth = 2F;
            this.tsbToolOutline.Size = new System.Drawing.Size(50, 52);
            this.tsbToolOutline.Text = "Outline";
            this.tsbToolOutline.ToolTipText = "Width and Color of outline";
            this.tsbToolOutline.OutlineChanged += new System.EventHandler(this.tsbToolOutline_OutlineChange);
            // 
            // tsbToolFill
            // 
            this.tsbToolFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolFill.FillColor = System.Drawing.Color.Transparent;
            this.tsbToolFill.Image = ((System.Drawing.Image)(resources.GetObject("tsbToolFill.Image")));
            this.tsbToolFill.IsDropDownButton = true;
            this.tsbToolFill.Name = "tsbToolFill";
            this.tsbToolFill.Size = new System.Drawing.Size(50, 52);
            this.tsbToolFill.Text = "Fill";
            this.tsbToolFill.ToolTipText = "Fill Color";
            this.tsbToolFill.FillChanged += new System.EventHandler(this.tsbToolFill_FillChanged);
            // 
            // drawPad
            // 
            this.drawPad.AutoScroll = true;
            this.drawPad.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.drawPad.AutoScrollMinSize = new System.Drawing.Size(540, 240);
            this.drawPad.BackColor = System.Drawing.SystemColors.Control;
            this.drawPad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPad.Location = new System.Drawing.Point(53, 0);
            this.drawPad.Margin = new System.Windows.Forms.Padding(0);
            this.drawPad.Name = "drawPad";
            this.drawPad.Resolution = 0F;
            this.drawPad.Size = new System.Drawing.Size(673, 468);
            this.drawPad.TabIndex = 4;
            this.drawPad.ViewportHeith = 0;
            this.drawPad.ViewportWidth = 0;
            this.drawPad.Zoom = 1F;
            this.drawPad.GdiMouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPad_GdiMouseMove);
            this.drawPad.GdiPaint += new System.Windows.Forms.PaintEventHandler(this.drawPad_GdiPaint);
            this.drawPad.GdiControlBoxLoad += new System.EventHandler(this.drawPad_GdiControlBoxLoad);
            this.drawPad.GdiControlBoxUpdated += new System.EventHandler(this.drawPad_GdiControlBoxUpdated);
            this.drawPad.GdiAddedVertex += new System.Windows.Forms.MouseEventHandler(this.drawPad_GdiAddedVertex);
            this.drawPad.CommandChanged += new EventArguments.CommandChangedEventHandler(this.drawPad_CommandChanged);
            this.drawPad.ShapeCreated += new Core.EventArguments.ShapeCreatedEventHandler(this.drawPad_ShapeCreated);
            this.drawPad.TextCreated += new Core.EventArguments.TextEventHandler(this.drawPad_TextCreated);
            this.drawPad.TextChanged += new Core.EventArguments.TextEventHandler(this.drawPad_TextChanged);
            // 
            // nameBar
            // 
            this.nameBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.nameBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameBar.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBar.Location = new System.Drawing.Point(0, 39);
            this.nameBar.Margin = new System.Windows.Forms.Padding(0);
            this.nameBar.Name = "nameBar";
            this.nameBar.ProjectName = "Untitled";
            this.nameBar.Size = new System.Drawing.Size(465, 30);
            this.nameBar.TabIndex = 3;
            // 
            // MainGui
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(475, 700);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(475, 700);
            this.Name = "MainGui";
            this.Text = "Unipic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainGui_FormClosing);
            this.Load += new System.EventHandler(this.MainGui_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.mtsEdit.ResumeLayout(false);
            this.mtsEdit.PerformLayout();
            this.metroStatusStrip1.ResumeLayout(false);
            this.metroStatusStrip1.PerformLayout();
            this.tlpView.ResumeLayout(false);
            this.tlpView.PerformLayout();
            this.mtsTool.ResumeLayout(false);
            this.mtsTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private MetroToolStrip mtsEdit;
        private MetroUI.MetroStatusStrip metroStatusStrip1;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.TableLayoutPanel tlpView;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private MetroToolStrip mtsTool;
        private ToolStripShapeSelectorButton tsbToolShape;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripOutlineButton tsbToolOutline;
        private ToolStripFillButton tsbToolFill;
        private MetroToolStripButton tsbToolSelection;
        private MetroToolStripButton tsbToolDirectSel;
        private MetroToolStripButton tsbToolText;
        private MetroToolStripButton tsbToolBrush;
        private MetroToolStripButton tsbToolEraser;
        private MetroToolStripButton tsbToolBucket;
        private DrawPad drawPad;
        private MetroNameBar nameBar;
        //private LayerManagerPanel layerManagerPanel;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbAccounts;
        private System.Windows.Forms.ToolStripButton tsbFitSize;
        private System.Windows.Forms.ToolStripButton tsbFont;



    }
}

