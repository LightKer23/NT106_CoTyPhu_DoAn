using Client.Services.Network;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class SetUpPasswordForm : Form
    {
        private readonly string _email;

        public SetUpPasswordForm(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private async void btnSetUpPsswrd_Click(object sender, EventArgs e)
        {
            string newPass = lblTxtNewPassword.Text.Trim();
            string confirm = lblTxtPasswordAgain.Text.Trim();

            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu");
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp");
                return;
            }

            if (!IsStrongPassword(newPass))
            {
                MessageBox.Show("Mật khẩu chưa đủ mạnh");
                return;
            }

            btnSetUpPsswrd.Enabled = false;

            try
            {
                await ClientSession.ConnectAsync();

                var res = await ClientSession.Tcp.ResetPasswordAsync(_email, newPass);

                MessageBox.Show(res.Message);

                if (res.Success)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đặt lại mật khẩu: " + ex.Message);
            }
            finally
            {
                btnSetUpPsswrd.Enabled = true;
            }
        }

        private bool IsStrongPassword(string password)
        {
            // >= 8 ký tự, có chữ hoa, chữ thường, số, ký tự đặc biệt
            return Regex.IsMatch(password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w]).{8,}$");
        }
    }
}
