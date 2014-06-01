using Untipic.Controls;
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroToolStrip1 = new Untipic.MetroUI.MetroToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.nameBar = new Untipic.Controls.MetroNameBar();
            this.metroStatusStrip1 = new Untipic.MetroUI.MetroStatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.metroToolStrip2 = new Untipic.MetroUI.MetroToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tsbShape = new Untipic.Controls.ToolStripShapeSelectorButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbColorSize = new Untipic.Controls.ToolStripOutlineButton();
            this.toolStripFillButton1 = new Untipic.Controls.ToolStripFillButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.metroToolStrip1.SuspendLayout();
            this.metroStatusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.metroToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.metroToolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameBar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.metroStatusStrip1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 65);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 559);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // metroToolStrip1
            // 
            this.metroToolStrip1.BackColor = System.Drawing.Color.White;
            this.metroToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.metroToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.metroToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.metroToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.metroToolStrip1.Name = "metroToolStrip1";
            this.metroToolStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.metroToolStrip1.Size = new System.Drawing.Size(706, 39);
            this.metroToolStrip1.TabIndex = 0;
            this.metroToolStrip1.Text = "metroToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "toolStripButton1";
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
            this.nameBar.Size = new System.Drawing.Size(706, 30);
            this.nameBar.TabIndex = 1;
            // 
            // metroStatusStrip1
            // 
            this.metroStatusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.metroStatusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.metroStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.metroStatusStrip1.Location = new System.Drawing.Point(0, 537);
            this.metroStatusStrip1.Name = "metroStatusStrip1";
            this.metroStatusStrip1.Size = new System.Drawing.Size(706, 22);
            this.metroStatusStrip1.SizingGrip = false;
            this.metroStatusStrip1.TabIndex = 2;
            this.metroStatusStrip1.Text = "metroStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.metroToolStrip2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 69);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(706, 468);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // metroToolStrip2
            // 
            this.metroToolStrip2.BackColor = System.Drawing.Color.White;
            this.metroToolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.metroToolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.metroToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.tsbShape,
            this.toolStripButton6,
            this.toolStripSeparator1,
            this.tsbColorSize,
            this.toolStripFillButton1});
            this.metroToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.metroToolStrip2.Name = "metroToolStrip2";
            this.metroToolStrip2.Padding = new System.Windows.Forms.Padding(0, 5, 1, 0);
            this.metroToolStrip2.Size = new System.Drawing.Size(62, 468);
            this.metroToolStrip2.TabIndex = 2;
            this.metroToolStrip2.Text = "metroToolStrip2";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Untipic.Properties.Resources.Selection;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(59, 52);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Untipic.Properties.Resources.DirectSelection;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(59, 52);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Untipic.Properties.Resources.Text;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(59, 52);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // tsbShape
            // 
            this.tsbShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbShape.Image = global::Untipic.Properties.Resources.Line;
            this.tsbShape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShape.Name = "tsbShape";
            this.tsbShape.Size = new System.Drawing.Size(59, 52);
            this.tsbShape.Text = "Line";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::Untipic.Properties.Resources.Crop;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(59, 52);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(59, 6);
            // 
            // tsbColorSize
            // 
            this.tsbColorSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbColorSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbColorSize.Image")));
            this.tsbColorSize.Name = "tsbColorSize";
            this.tsbColorSize.OutlineColor = System.Drawing.Color.Black;
            this.tsbColorSize.OutlineWidth = 2F;
            this.tsbColorSize.Size = new System.Drawing.Size(59, 52);
            this.tsbColorSize.Text = "tsbColorSize";
            // 
            // toolStripFillButton1
            // 
            this.toolStripFillButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFillButton1.FillColor = System.Drawing.Color.Transparent;
            this.toolStripFillButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFillButton1.Image")));
            this.toolStripFillButton1.Name = "toolStripFillButton1";
            this.toolStripFillButton1.Size = new System.Drawing.Size(59, 52);
            this.toolStripFillButton1.Text = "toolStripFillButton1";
            // 
            // MainGui
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(716, 629);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainGui";
            this.Text = "Unipic";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.metroToolStrip1.ResumeLayout(false);
            this.metroToolStrip1.PerformLayout();
            this.metroStatusStrip1.ResumeLayout(false);
            this.metroStatusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.metroToolStrip2.ResumeLayout(false);
            this.metroToolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroToolStrip metroToolStrip1;
        private Controls.MetroNameBar nameBar;
        private MetroUI.MetroStatusStrip metroStatusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private MetroToolStrip metroToolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private ToolStripShapeSelectorButton tsbShape;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripOutlineButton tsbColorSize;
        private ToolStripFillButton toolStripFillButton1;



    }
}

