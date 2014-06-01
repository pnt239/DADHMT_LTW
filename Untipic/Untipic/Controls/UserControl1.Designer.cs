namespace Untipic.Controls
{
    partial class UserControl1
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
            this._mpnMain = new Untipic.Controls.MultiPanel();
            this._pgeBasic = new Untipic.Controls.MultiPanelPage();
            this._pgeAdvance = new Untipic.Controls.MultiPanelPage();
            this._tlpAdvance = new System.Windows.Forms.TableLayoutPanel();
            this._llbSwitchBasic = new System.Windows.Forms.LinkLabel();
            this._tlpColorSelector = new System.Windows.Forms.TableLayoutPanel();
            this._clrWheel = new Untipic.Controls.ColorWheel();
            this.colorEditor1 = new Untipic.Controls.ColorEditor();
            this.metroButton1 = new Untipic.MetroUI.MetroButton();
            this._mpnMain.SuspendLayout();
            this._pgeAdvance.SuspendLayout();
            this._tlpAdvance.SuspendLayout();
            this._tlpColorSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mpnMain
            // 
            this._mpnMain.BackColor = System.Drawing.Color.Transparent;
            this._mpnMain.Controls.Add(this._pgeBasic);
            this._mpnMain.Controls.Add(this._pgeAdvance);
            this._mpnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mpnMain.Location = new System.Drawing.Point(0, 0);
            this._mpnMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._mpnMain.Name = "_mpnMain";
            this._mpnMain.SelectedPage = this._pgeAdvance;
            this._mpnMain.Size = new System.Drawing.Size(210, 275);
            this._mpnMain.TabIndex = 0;
            // 
            // _pgeBasic
            // 
            this._pgeBasic.BackColor = System.Drawing.Color.Transparent;
            this._pgeBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pgeBasic.Location = new System.Drawing.Point(0, 0);
            this._pgeBasic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pgeBasic.Name = "_pgeBasic";
            this._pgeBasic.Size = new System.Drawing.Size(210, 275);
            this._pgeBasic.TabIndex = 0;
            this._pgeBasic.Text = "Basic";
            // 
            // _pgeAdvance
            // 
            this._pgeAdvance.Controls.Add(this._tlpAdvance);
            this._pgeAdvance.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pgeAdvance.Location = new System.Drawing.Point(0, 0);
            this._pgeAdvance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pgeAdvance.Name = "_pgeAdvance";
            this._pgeAdvance.Size = new System.Drawing.Size(210, 275);
            this._pgeAdvance.TabIndex = 1;
            this._pgeAdvance.Text = "Advance";
            // 
            // _tlpAdvance
            // 
            this._tlpAdvance.BackColor = System.Drawing.Color.Transparent;
            this._tlpAdvance.ColumnCount = 1;
            this._tlpAdvance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpAdvance.Controls.Add(this._llbSwitchBasic, 0, 0);
            this._tlpAdvance.Controls.Add(this._tlpColorSelector, 0, 1);
            this._tlpAdvance.Controls.Add(this.metroButton1, 0, 2);
            this._tlpAdvance.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlpAdvance.Location = new System.Drawing.Point(0, 0);
            this._tlpAdvance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._tlpAdvance.Name = "_tlpAdvance";
            this._tlpAdvance.RowCount = 3;
            this._tlpAdvance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this._tlpAdvance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpAdvance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._tlpAdvance.Size = new System.Drawing.Size(210, 275);
            this._tlpAdvance.TabIndex = 1;
            this._tlpAdvance.Paint += new System.Windows.Forms.PaintEventHandler(this._tlpAdvance_Paint);
            // 
            // _llbSwitchBasic
            // 
            this._llbSwitchBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this._llbSwitchBasic.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(123)))), ((int)(((byte)(205)))));
            this._llbSwitchBasic.Location = new System.Drawing.Point(3, 0);
            this._llbSwitchBasic.Name = "_llbSwitchBasic";
            this._llbSwitchBasic.Size = new System.Drawing.Size(204, 25);
            this._llbSwitchBasic.TabIndex = 0;
            this._llbSwitchBasic.TabStop = true;
            this._llbSwitchBasic.Text = "Advance";
            this._llbSwitchBasic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _tlpColorSelector
            // 
            this._tlpColorSelector.ColumnCount = 1;
            this._tlpColorSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpColorSelector.Controls.Add(this._clrWheel, 0, 0);
            this._tlpColorSelector.Controls.Add(this.colorEditor1, 0, 1);
            this._tlpColorSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlpColorSelector.Location = new System.Drawing.Point(3, 28);
            this._tlpColorSelector.Name = "_tlpColorSelector";
            this._tlpColorSelector.RowCount = 2;
            this._tlpColorSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlpColorSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._tlpColorSelector.Size = new System.Drawing.Size(204, 184);
            this._tlpColorSelector.TabIndex = 3;
            // 
            // _clrWheel
            // 
            this._clrWheel.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._clrWheel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clrWheel.Location = new System.Drawing.Point(3, 3);
            this._clrWheel.Name = "_clrWheel";
            this._clrWheel.Size = new System.Drawing.Size(198, 138);
            this._clrWheel.TabIndex = 0;
            // 
            // colorEditor1
            // 
            this.colorEditor1.BackColor = System.Drawing.Color.Transparent;
            this.colorEditor1.Color = System.Drawing.Color.Empty;
            this.colorEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorEditor1.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorEditor1.Location = new System.Drawing.Point(3, 148);
            this.colorEditor1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.colorEditor1.Name = "colorEditor1";
            this.colorEditor1.Size = new System.Drawing.Size(198, 32);
            this.colorEditor1.TabIndex = 1;
            this.colorEditor1.Text = "colorEditor1";
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.White;
            this.metroButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.metroButton1.Location = new System.Drawing.Point(15, 230);
            this.metroButton1.Margin = new System.Windows.Forms.Padding(15);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.metroButton1.Size = new System.Drawing.Size(180, 30);
            this.metroButton1.TabIndex = 4;
            this.metroButton1.Text = "metroButton1";
            this.metroButton1.UseVisualStyleBackColor = false;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this._mpnMain);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(210, 275);
            this._mpnMain.ResumeLayout(false);
            this._pgeAdvance.ResumeLayout(false);
            this._tlpAdvance.ResumeLayout(false);
            this._tlpColorSelector.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MultiPanel _mpnMain;
        private MultiPanelPage _pgeBasic;
        private MultiPanelPage _pgeAdvance;
        private System.Windows.Forms.TableLayoutPanel _tlpAdvance;
        private System.Windows.Forms.LinkLabel _llbSwitchBasic;
        private System.Windows.Forms.TableLayoutPanel _tlpColorSelector;
        private ColorWheel _clrWheel;
        private MetroUI.MetroButton metroButton1;
        private ColorEditor colorEditor1;

    }
}
