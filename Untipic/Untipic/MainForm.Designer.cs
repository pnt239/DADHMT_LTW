namespace Untipic
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.drawPad1 = new Untipic.Controls.DrawPad();
            this.metroToolStrip2 = new Untipic.MetroUI.MetroToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.metroToolStrip1 = new Untipic.MetroUI.MetroToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.metroNameBar1 = new Untipic.Controls.MetroNameBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.metroToolStrip2.SuspendLayout();
            this.metroToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.metroToolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroNameBar1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 65);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(696, 428);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tableLayoutPanel2.Controls.Add(this.drawPad1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.metroToolStrip2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 67);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(696, 341);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // drawPad1
            // 
            this.drawPad1.AutoScroll = true;
            this.drawPad1.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.drawPad1.AutoScrollMinSize = new System.Drawing.Size(540, 240);
            this.drawPad1.BackColor = System.Drawing.SystemColors.Control;
            this.drawPad1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPad1.Location = new System.Drawing.Point(53, 0);
            this.drawPad1.Margin = new System.Windows.Forms.Padding(0);
            this.drawPad1.Name = "drawPad1";
            this.drawPad1.Size = new System.Drawing.Size(516, 341);
            this.drawPad1.TabIndex = 0;
            // 
            // metroToolStrip2
            // 
            this.metroToolStrip2.BackColor = System.Drawing.Color.White;
            this.metroToolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.metroToolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.metroToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.metroToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.metroToolStrip2.Name = "metroToolStrip2";
            this.metroToolStrip2.Size = new System.Drawing.Size(53, 341);
            this.metroToolStrip2.TabIndex = 1;
            this.metroToolStrip2.Text = "metroToolStrip2";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(50, 52);
            this.toolStripButton2.Text = "toolStripButton2";
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
            this.metroToolStrip1.Size = new System.Drawing.Size(696, 37);
            this.metroToolStrip1.TabIndex = 1;
            this.metroToolStrip1.Text = "metroToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 34);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // metroNameBar1
            // 
            this.metroNameBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.metroNameBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroNameBar1.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroNameBar1.Location = new System.Drawing.Point(0, 37);
            this.metroNameBar1.Margin = new System.Windows.Forms.Padding(0);
            this.metroNameBar1.Name = "metroNameBar1";
            this.metroNameBar1.ProjectName = "Untitled";
            this.metroNameBar1.Size = new System.Drawing.Size(696, 30);
            this.metroNameBar1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(706, 498);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.metroToolStrip2.ResumeLayout(false);
            this.metroToolStrip2.PerformLayout();
            this.metroToolStrip1.ResumeLayout(false);
            this.metroToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.DrawPad drawPad1;
        private MetroUI.MetroToolStrip metroToolStrip2;
        private MetroUI.MetroToolStrip metroToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private Controls.MetroNameBar metroNameBar1;
    }
}