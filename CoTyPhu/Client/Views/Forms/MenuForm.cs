using Client.Services.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        public MenuForm(int accountId)
        {
            InitializeComponent();
            ClientSession.AccountID = accountId;
        }

        private void btnPlayWithPlayer_Click(object sender, EventArgs e)
        {
            this.Hide();

            var frm = new RoomHubForm();
            frm.Owner = this;
            frm.Show();

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            var setting = new SettingForm();
            setting.Owner = this;   
            setting.ShowDialog();
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            (new HowToPlayForm()).Show();
        }

        private void btnPlayWithCmp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
