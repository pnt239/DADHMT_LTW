using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Untipic.Core;
using Untipic.MetroUI;

namespace Untipic.Forms
{
    public partial class NewForm : MetroForm
    {
        public NewForm()
        {
            InitializeComponent();

            IPHostEntry ipHost = Dns.GetHostEntry(""); 
            //IPAddress ipAddr = ipHost.AddressList[0];

            foreach (var ip in ipHost.AddressList)
            {
                cbxServerIp.Items.Add(ip);
            }
            cbxServerIp.Items.Add(IPAddress.Parse("127.0.0.1"));
        }

        public NewForm(float winWidth, float winHeight, float resolution) : this()
        {
            WinWidth = winWidth;
            WinHeight = winHeight;
            Resolution = resolution;
            Unit = MessureUnit.Cm;

            //ViewWidth = (int) Math.Round(WinWidth*Resolution);
            //ViewHeight = (int) Math.Round(WinHeight*Resolution);

            //txtViewWidth.Text = ViewWidth.ToString(CultureInfo.InvariantCulture);
            //txtViewHeight.Text = ViewHeight.ToString(CultureInfo.InvariantCulture);
            txtWinWidth.Text = WinWidth.ToString(CultureInfo.InvariantCulture);
            txtWinHeight.Text = WinHeight.ToString(CultureInfo.InvariantCulture);
            txtResolution.Text = Resolution.ToString(CultureInfo.InvariantCulture);
        }

        public int ViewWidth { get; set; }
        public int ViewHeight { get; set; }
        public float WinWidth { get; set; }
        public float WinHeight { get; set; }
        public float Resolution { get; set; }
        public MessureUnit Unit { get; set; }
        public IPAddress Ip { get; set; }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            var ctrl = sender as Control;
            if (ctrl == null) return;

            if (!CheckNumber(ctrl.Text))
            {
                errorProvider.SetError(ctrl, "Your must enter integer number!");
                e.Cancel = true;
            }
        }

        private void TextBox_Validated(object sender, EventArgs e)
        {
            var ctrl = sender as Control;
            if (ctrl != null) errorProvider.SetError(ctrl, "");
        }

        private void cbxWinUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lastUnit = Unit;
            Unit = (MessureUnit) cbxWinUnit.SelectedIndex;

            lbRealUnit.Text = (string) cbxWinUnit.Items[cbxWinUnit.SelectedIndex];
            lbResolutionUnit.Text = @"pixels/" + lbRealUnit.Text;

            if (lastUnit != Unit)
            {
                if (Unit == MessureUnit.Inch)
                {
                    Resolution *= 2.54F;
                }
                else if (Unit == MessureUnit.Cm)
                {
                    Resolution /= 2.54F;
                }
                Resolution = (float) Math.Round(Resolution, 2);
                txtResolution.Text = Resolution.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (!CheckNotNull())
                    e.Cancel = true;
            }
        }

        private void txtResolution_TextChanged(object sender, EventArgs e)
        {
            float t;
            float.TryParse(txtResolution.Text, out t);
            Resolution = t;

            txtWinWidth_TextChanged(sender, e);
            txtWinHeight_TextChanged(sender, e);
        }

        private void txtWinWidth_TextChanged(object sender, EventArgs e)
        {
            float t;
            float.TryParse(txtWinWidth.Text, out t);
            WinWidth = t;

            ViewWidth = (int)Math.Round(WinWidth * Resolution);
            txtViewWidth.Text = ViewWidth.ToString(CultureInfo.InvariantCulture);
        }

        private void txtWinHeight_TextChanged(object sender, EventArgs e)
        {
            float t;
            float.TryParse(txtWinHeight.Text, out t);
            WinHeight = t;

            ViewHeight = (int)Math.Round(WinHeight * Resolution);
            txtViewHeight.Text = ViewHeight.ToString(CultureInfo.InvariantCulture);
        }

        private void cbxServerIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ip = (IPAddress)cbxServerIp.SelectedItem;
        }

        private bool CheckNumber(string text)
        {
            var regex = new Regex(@"^[+-]?\d+(\.\d+)?$");
            return regex.IsMatch(text);
        }

        private bool CheckNotNull()
        {
            if (txtViewWidth.Text == "")
            {
                errorProvider.SetError(txtViewWidth, "Input is not null!");
                return false;
            }

            if (txtViewHeight.Text == "")
            {
                errorProvider.SetError(txtViewHeight, "Input is not null!");
                return false;
            }

            if (txtWinWidth.Text == "")
            {
                errorProvider.SetError(txtWinWidth, "Input is not null!");
                return false;
            }
            if (txtWinHeight.Text == "")
            {
                errorProvider.SetError(txtWinHeight, "Input is not null!");
                return false;
            }

            return true;
        }
    }
}
