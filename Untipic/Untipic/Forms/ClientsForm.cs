using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Untipic.MetroUI;

namespace Untipic.Forms
{
    public partial class ClientsForm : MetroForm
    {
        public ClientsForm()
        {
            InitializeComponent();

            _clients = new Core.SyncList<Engine.UserInfo>(this);
            lbxClients.DataSource = _clients;
            lbxClients.DisplayMember = "Name";
            lbxClients.ValueMember = "Id";
        }

        public void AddUser(Engine.UserInfo user)
        {
            _clients.Add(user);
        }

        public void RemoveUser(Engine.UserInfo user)
        {
            _clients.Remove(user);
        }

        private readonly Core.SyncList<Engine.UserInfo> _clients;
    }
}
