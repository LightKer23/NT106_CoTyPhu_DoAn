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

        private void btnPlayWithPlayer_Click(object sender, EventArgs e)
        {
            this.Hide();

            var frm = new RoomHubForm();
            frm.Owner = this;
            frm.Show();

        }
    }
}
