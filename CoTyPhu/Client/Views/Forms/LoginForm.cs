using Client.Services.Network;
using System;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 🔹 KẾT NỐI 1 LẦN DUY NHẤT
                await ClientSession.ConnectAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được server: " + ex.Message);
            }
        }

        private void lkLblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (var registerForm = new RegisterForm())
            {
                registerForm.ShowDialog();
            }

            this.Show();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = lblTxtUsername.Text.Trim();
            string password = lblTxtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.");
                return;
            }

            btnLogin.Enabled = false;

            try
            {
                await ClientSession.ConnectAsync();

                var res = await ClientSession.Tcp.LoginAsync(username, password);

                if (!res.Success)
                {
                    MessageBox.Show(res.Message ?? "Đăng nhập thất bại");
                    return;
                }

                ClientSession.AccountID = res.IDAccount ?? 0;

                MessageBox.Show("Đăng nhập thành công!");

                this.Hide();
                var menuForm = new MenuForm();
                menuForm.FormClosed += (s, args) =>
                {
                    ClientSession.Disconnect();
                    this.Close();
                };
                menuForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void lkLblForgotPsswrd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (var forgotForm = new ForgotPasswordForm())
            {
                forgotForm.ShowDialog();
            }

            this.Show();
        }

    }
}
