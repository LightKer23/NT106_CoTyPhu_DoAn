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
    public partial class SetUpPasswordForm : Form
    {
        public SetUpPasswordForm()
        {
            InitializeComponent();
        }

        private void btnSetUpPsswrd_Click(object sender, EventArgs e)
        {
            //SQL cập nhật lại password
            MessageBox.Show("Đổi mật khẩu thành công!");
            this.Close();
        }
    }
}
