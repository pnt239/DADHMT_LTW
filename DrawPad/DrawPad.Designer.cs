namespace TabletC.DrawPad
{
    partial class DrawPad
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlyMain = new System.Windows.Forms.TableLayoutPanel();
            this.ctrDrawArea = new TabletC.DrawPad.GdiArea();
            this.tlyMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlyMain
            // 
            this.tlyMain.ColumnCount = 3;
            this.tlyMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlyMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlyMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlyMain.Controls.Add(this.ctrDrawArea, 1, 1);
            this.tlyMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlyMain.Location = new System.Drawing.Point(0, 0);
            this.tlyMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlyMain.Name = "tlyMain";
            this.tlyMain.RowCount = 3;
            this.tlyMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlyMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlyMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlyMain.Size = new System.Drawing.Size(88, 87);
            this.tlyMain.TabIndex = 0;
            // 
            // ctrDrawArea
            // 
            this.ctrDrawArea.BackColor = System.Drawing.Color.White;
            this.ctrDrawArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrDrawArea.Location = new System.Drawing.Point(29, 28);
            this.ctrDrawArea.Margin = new System.Windows.Forms.Padding(0);
            this.ctrDrawArea.Name = "ctrDrawArea";
            this.ctrDrawArea.Size = new System.Drawing.Size(30, 30);
            this.ctrDrawArea.TabIndex = 0;
            this.ctrDrawArea.Click += new System.EventHandler(this.ctrDrawArea_Click);
            this.ctrDrawArea.Paint += new System.Windows.Forms.PaintEventHandler(this.ctrDrawArea_Paint);
            this.ctrDrawArea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctrDrawArea_KeyDown);
            this.ctrDrawArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctrDrawArea_KeyDown);
            this.ctrDrawArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrDrawArea_MouseDown);
            this.ctrDrawArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ctrDrawArea_MouseMove);
            this.ctrDrawArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrDrawArea_MouseUp);
            // 
            // DrawPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.Controls.Add(this.tlyMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DrawPad";
            this.Size = new System.Drawing.Size(88, 87);
            this.tlyMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlyMain;
        private GdiArea ctrDrawArea;
    }
}
