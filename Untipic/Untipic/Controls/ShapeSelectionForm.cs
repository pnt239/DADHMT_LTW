#region Copyright (c) 2013 Pham Ngoc Thanh, https://github.com/panoti/DADHMT_LTW/
/**
 * MetroUI - Windows Modern UI for .NET WinForms applications
 * Copyright (c) 2014 Pham Ngoc Thanh, https://github.com/panoti/DADHMT_LTW/
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Untipic.Controls
{
    public class ShapeSelectionForm : MetroForm
    {
        private MetroButton btnLine;
        private MetroButton btnBezier;
        private MetroButton btnTriangle;
        private MetroButton btnQuad;
        private MetroButton btnPolygon;
        private MetroButton btnEllipse;
        private TableLayoutPanel tableLayoutPanel1;
    
        public ShapeSelectionForm()
        {
            InitializeComponent();
            BorderStyle = MetroBorderStyle.None;
            ShowInTaskbar = false;

            LostFocus += ShapeSelectionForm_LostFocus;
            SelectedButton = null;
        }

        public MetroButton SelectedButton { get; set; }

        void ShapeSelectionForm_LostFocus(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnLine = new Untipic.Controls.MetroButton();
            this.btnBezier = new Untipic.Controls.MetroButton();
            this.btnTriangle = new Untipic.Controls.MetroButton();
            this.btnQuad = new Untipic.Controls.MetroButton();
            this.btnPolygon = new Untipic.Controls.MetroButton();
            this.btnEllipse = new Untipic.Controls.MetroButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Controls.Add(this.btnLine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBezier, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTriangle, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnQuad, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPolygon, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEllipse, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 48);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnLine
            // 
            this.btnLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLine.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnLine.Image = global::Untipic.Properties.Resources.Line;
            this.btnLine.Location = new System.Drawing.Point(3, 3);
            this.btnLine.Name = "btnLine";
            this.btnLine.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnLine.Size = new System.Drawing.Size(42, 42);
            this.btnLine.TabIndex = 0;
            this.btnLine.Text = "Line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.OnShapeSelected);
            // 
            // btnBezier
            // 
            this.btnBezier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBezier.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBezier.Image = global::Untipic.Properties.Resources.Bezier;
            this.btnBezier.Location = new System.Drawing.Point(51, 3);
            this.btnBezier.Name = "btnBezier";
            this.btnBezier.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnBezier.Size = new System.Drawing.Size(42, 42);
            this.btnBezier.TabIndex = 1;
            this.btnBezier.Text = "Bezier";
            this.btnBezier.UseVisualStyleBackColor = true;
            this.btnBezier.Click += new System.EventHandler(this.OnShapeSelected);
            // 
            // btnTriangle
            // 
            this.btnTriangle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriangle.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnTriangle.Image = global::Untipic.Properties.Resources.Triangle;
            this.btnTriangle.Location = new System.Drawing.Point(99, 3);
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnTriangle.Size = new System.Drawing.Size(42, 42);
            this.btnTriangle.TabIndex = 2;
            this.btnTriangle.Text = "Triangle";
            this.btnTriangle.UseVisualStyleBackColor = true;
            this.btnTriangle.Click += new System.EventHandler(this.OnShapeSelected);
            // 
            // btnQuad
            // 
            this.btnQuad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnQuad.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnQuad.Image = global::Untipic.Properties.Resources.Quad;
            this.btnQuad.Location = new System.Drawing.Point(147, 3);
            this.btnQuad.Name = "btnQuad";
            this.btnQuad.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnQuad.Size = new System.Drawing.Size(42, 42);
            this.btnQuad.TabIndex = 3;
            this.btnQuad.Text = "Quad";
            this.btnQuad.UseVisualStyleBackColor = true;
            this.btnQuad.Click += new System.EventHandler(this.OnShapeSelected);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPolygon.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnPolygon.Image = global::Untipic.Properties.Resources.Polygon;
            this.btnPolygon.Location = new System.Drawing.Point(195, 3);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnPolygon.Size = new System.Drawing.Size(42, 42);
            this.btnPolygon.TabIndex = 4;
            this.btnPolygon.Text = "Polygon";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.OnShapeSelected);
            // 
            // btnEllipse
            // 
            this.btnEllipse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEllipse.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnEllipse.Image = global::Untipic.Properties.Resources.Ellipse;
            this.btnEllipse.Location = new System.Drawing.Point(243, 3);
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnEllipse.Size = new System.Drawing.Size(42, 42);
            this.btnEllipse.TabIndex = 5;
            this.btnEllipse.Text = "Ellipse";
            this.btnEllipse.UseVisualStyleBackColor = true;
            this.btnEllipse.Click += new System.EventHandler(this.OnShapeSelected);
            // 
            // ShapeSelectionForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = Untipic.Controls.MetroBorderStyle.None;
            this.BorderWidth = 2;
            this.ClientSize = new System.Drawing.Size(292, 52);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ShapeSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.ShapeSelectionForm_Deactivate);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void ShapeSelectionForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        private void OnShapeSelected(object sender, EventArgs e)
        {
            SelectedButton = (MetroButton) sender;
            Close();
        }
    }
}
