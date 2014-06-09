namespace Untipic.Forms
{
    partial class OpenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenForm));
            this.rdbLocal = new System.Windows.Forms.RadioButton();
            this.rdbNetwork = new System.Windows.Forms.RadioButton();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new Untipic.MetroUI.MetroButton();
            this.btnOK = new Untipic.MetroUI.MetroButton();
            this.btnCancel = new Untipic.MetroUI.MetroButton();
            this.txtIpAddress = new Untipic.Controls.IPAddressTextBox();
            this.SuspendLayout();
            // 
            // rdbLocal
            // 
            this.rdbLocal.AutoSize = true;
            this.rdbLocal.BackColor = System.Drawing.Color.Transparent;
            this.rdbLocal.Checked = true;
            this.rdbLocal.Location = new System.Drawing.Point(5, 68);
            this.rdbLocal.Name = "rdbLocal";
            this.rdbLocal.Size = new System.Drawing.Size(107, 21);
            this.rdbLocal.TabIndex = 3;
            this.rdbLocal.TabStop = true;
            this.rdbLocal.Text = "Open local file";
            this.rdbLocal.UseVisualStyleBackColor = false;
            this.rdbLocal.CheckedChanged += new System.EventHandler(this.rdbLocal_CheckedChanged);
            // 
            // rdbNetwork
            // 
            this.rdbNetwork.AutoSize = true;
            this.rdbNetwork.BackColor = System.Drawing.Color.Transparent;
            this.rdbNetwork.Location = new System.Drawing.Point(5, 126);
            this.rdbNetwork.Name = "rdbNetwork";
            this.rdbNetwork.Size = new System.Drawing.Size(135, 21);
            this.rdbNetwork.TabIndex = 4;
            this.rdbNetwork.Text = "Open from network";
            this.rdbNetwork.UseVisualStyleBackColor = false;
            this.rdbNetwork.CheckedChanged += new System.EventHandler(this.rdbNetwork_CheckedChanged);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(5, 95);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(298, 25);
            this.txtFilePath.TabIndex = 5;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.White;
            this.btnBrowse.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBrowse.Location = new System.Drawing.Point(309, 95);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnBrowse.Size = new System.Drawing.Size(35, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnOK.Location = new System.Drawing.Point(188, 195);
            this.btnOK.Name = "btnOK";
            this.btnOK.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnCancel.Location = new System.Drawing.Point(269, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Enabled = false;
            this.txtIpAddress.Location = new System.Drawing.Point(5, 153);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(298, 25);
            this.txtIpAddress.TabIndex = 10;
            this.txtIpAddress.TextChanged += new System.EventHandler(this.txtIpAddress_TextChanged);
            // 
            // OpenForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = Untipic.MetroUI.MetroBorderStyle.FixedSingle;
            this.BorderWidth = 2;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(349, 224);
            this.ControlBox = false;
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.rdbNetwork);
            this.Controls.Add(this.rdbLocal);
            this.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OpenForm";
            this.Padding = new System.Windows.Forms.Padding(2, 81, 2, 3);
            this.Text = "Open Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbLocal;
        private System.Windows.Forms.RadioButton rdbNetwork;
        private System.Windows.Forms.TextBox txtFilePath;
        private MetroUI.MetroButton btnBrowse;
        private MetroUI.MetroButton btnOK;
        private MetroUI.MetroButton btnCancel;
        private Controls.IPAddressTextBox txtIpAddress;
    }
}