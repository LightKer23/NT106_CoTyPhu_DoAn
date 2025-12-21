using Client.Services.Network;
using Common.Contracts.Auth;
using System;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.AcceptButton = btnRegister;
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                await ClientSession.ConnectAsync();

                var req = new RegisterRequest
                {
                    Username = lblTxtUsername.Text.Trim(),
                    Email = lblTxtEmail.Text.Trim(),
                    Password = lblTxtPsswrd.Text,
                    DisplayName = lblTxtFullname.Text.Trim()
                };

                var resp = await ClientSession.Tcp.RegisterAsync(req);

                MessageBox.Show(resp.Message);

                if (resp.Success)
                    this.Close(); // quay về LoginForm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }

        private void lkLblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

    }
}
