using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Untipic.Forms
{
    public partial class OpenForm : MetroUI.MetroForm
    {
        public OpenForm()
        {
            InitializeComponent();

            IsLocal = true;
        }

        public bool IsLocal { get; set; }

        public string FilePath { get; set; }

        public string IpAddress { get; set; }

        private void rdbLocal_CheckedChanged(object sender, EventArgs e)
        {
            IsLocal = rdbLocal.Checked;
            txtFilePath.Enabled = true;
            txtIpAddress.Enabled = false;
        }

        private void rdbNetwork_CheckedChanged(object sender, EventArgs e)
        {
            IsLocal = false;
            txtFilePath.Enabled = false;
            txtIpAddress.Enabled = true;
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            FilePath = txtFilePath.Text;
        }

        private void txtIpAddress_TextChanged(object sender, EventArgs e)
        {
            IpAddress = txtIpAddress.GetPureIPAddress();
        }

        private void OpenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool flag = true;

            if (rdbLocal.Checked && txtIpAddress.Text == "")
            {
                txtIpAddress.Focus();
                flag = false;
            }

            if (rdbNetwork.Checked && txtIpAddress.GetPureIPAddress() == "0.0.0.0")
            {
                txtIpAddress.Focus();
                flag = false;
            }

            if (!flag)
            {
                e.Cancel = true;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = @"Draw project (*.unp)|*.unp";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}
