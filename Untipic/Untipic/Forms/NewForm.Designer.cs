namespace Untipic.Forms
{
    partial class NewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtViewWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtViewHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.lbResolutionUnit = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.lbRealUnit = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWinWidth = new System.Windows.Forms.TextBox();
            this.txtWinHeight = new System.Windows.Forms.TextBox();
            this.cbxWinUnit = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOk = new Untipic.MetroUI.MetroButton();
            this.btnCancel = new Untipic.MetroUI.MetroButton();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxServerIp = new System.Windows.Forms.ComboBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbxServerIp, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 65);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(407, 280);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Viewport Size:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtViewWidth, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtViewHeight, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtResolution, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbResolutionUnit, 2, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(30, 17);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(377, 94);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Width:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtViewWidth
            // 
            this.txtViewWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtViewWidth.Location = new System.Drawing.Point(90, 3);
            this.txtViewWidth.Name = "txtViewWidth";
            this.txtViewWidth.ReadOnly = true;
            this.txtViewWidth.Size = new System.Drawing.Size(148, 25);
            this.txtViewWidth.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(244, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "pixels";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Height:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtViewHeight
            // 
            this.txtViewHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtViewHeight.Location = new System.Drawing.Point(90, 34);
            this.txtViewHeight.Name = "txtViewHeight";
            this.txtViewHeight.ReadOnly = true;
            this.txtViewHeight.Size = new System.Drawing.Size(148, 25);
            this.txtViewHeight.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(244, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 31);
            this.label5.TabIndex = 5;
            this.label5.Text = "pixels";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 32);
            this.label6.TabIndex = 6;
            this.label6.Text = "Resolution:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResolution
            // 
            this.txtResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResolution.Location = new System.Drawing.Point(90, 65);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(148, 25);
            this.txtResolution.TabIndex = 7;
            this.txtResolution.TextChanged += new System.EventHandler(this.txtResolution_TextChanged);
            this.txtResolution.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            this.txtResolution.Validated += new System.EventHandler(this.TextBox_Validated);
            // 
            // lbResolutionUnit
            // 
            this.lbResolutionUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResolutionUnit.Location = new System.Drawing.Point(244, 62);
            this.lbResolutionUnit.Name = "lbResolutionUnit";
            this.lbResolutionUnit.Size = new System.Drawing.Size(130, 32);
            this.lbResolutionUnit.TabIndex = 8;
            this.lbResolutionUnit.Text = "pixels/cm";
            this.lbResolutionUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Window Size:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbRealUnit, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtWinWidth, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtWinHeight, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbxWinUnit, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(30, 128);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(377, 62);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 31);
            this.label8.TabIndex = 0;
            this.label8.Text = "Width:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRealUnit
            // 
            this.lbRealUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRealUnit.Location = new System.Drawing.Point(244, 0);
            this.lbRealUnit.Name = "lbRealUnit";
            this.lbRealUnit.Size = new System.Drawing.Size(130, 31);
            this.lbRealUnit.TabIndex = 1;
            this.lbRealUnit.Text = "cm";
            this.lbRealUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 31);
            this.label10.TabIndex = 2;
            this.label10.Text = "Height:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWinWidth
            // 
            this.txtWinWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWinWidth.Location = new System.Drawing.Point(90, 3);
            this.txtWinWidth.Name = "txtWinWidth";
            this.txtWinWidth.Size = new System.Drawing.Size(148, 25);
            this.txtWinWidth.TabIndex = 3;
            this.txtWinWidth.TextChanged += new System.EventHandler(this.txtWinWidth_TextChanged);
            this.txtWinWidth.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            this.txtWinWidth.Validated += new System.EventHandler(this.TextBox_Validated);
            // 
            // txtWinHeight
            // 
            this.txtWinHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWinHeight.Location = new System.Drawing.Point(90, 34);
            this.txtWinHeight.Name = "txtWinHeight";
            this.txtWinHeight.Size = new System.Drawing.Size(148, 25);
            this.txtWinHeight.TabIndex = 4;
            this.txtWinHeight.TextChanged += new System.EventHandler(this.txtWinHeight_TextChanged);
            this.txtWinHeight.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            this.txtWinHeight.Validated += new System.EventHandler(this.TextBox_Validated);
            // 
            // cbxWinUnit
            // 
            this.cbxWinUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxWinUnit.FormattingEnabled = true;
            this.cbxWinUnit.Items.AddRange(new object[] {
            "cm",
            "inch"});
            this.cbxWinUnit.Location = new System.Drawing.Point(244, 34);
            this.cbxWinUnit.Name = "cbxWinUnit";
            this.cbxWinUnit.Size = new System.Drawing.Size(130, 25);
            this.cbxWinUnit.TabIndex = 5;
            this.cbxWinUnit.Text = "cm";
            this.cbxWinUnit.SelectedIndexChanged += new System.EventHandler(this.cbxWinUnit_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.Controls.Add(this.btnOk, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 235);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(407, 45);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.White;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnOk.Location = new System.Drawing.Point(210, 10);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnOk.Size = new System.Drawing.Size(94, 32);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnCancel.Location = new System.Drawing.Point(310, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnCancel.Size = new System.Drawing.Size(94, 32);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Server Ip:";
            // 
            // cbxServerIp
            // 
            this.cbxServerIp.FormattingEnabled = true;
            this.cbxServerIp.Location = new System.Drawing.Point(30, 210);
            this.cbxServerIp.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.cbxServerIp.Name = "cbxServerIp";
            this.cbxServerIp.Size = new System.Drawing.Size(172, 25);
            this.cbxServerIp.TabIndex = 6;
            this.cbxServerIp.SelectedIndexChanged += new System.EventHandler(this.cbxServerIp_SelectedIndexChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // NewForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = Untipic.MetroUI.MetroBorderStyle.FixedSingle;
            this.BorderWidth = 2;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(417, 350);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NewForm";
            this.Padding = new System.Windows.Forms.Padding(5, 65, 5, 5);
            this.Text = "New Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtViewWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtViewHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWinWidth;
        private System.Windows.Forms.TextBox txtWinHeight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private MetroUI.MetroButton btnOk;
        private MetroUI.MetroButton btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbResolutionUnit;
        private System.Windows.Forms.Label lbRealUnit;
        private System.Windows.Forms.ComboBox cbxWinUnit;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxServerIp;
    }
}