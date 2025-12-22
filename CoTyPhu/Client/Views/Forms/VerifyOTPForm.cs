using Client.Services.Network;
using System;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class VerifyOTPForm : Form
    {
        private readonly string _email;

        public VerifyOTPForm(string email)
        {
            InitializeComponent();
            _email = email;

            label3.Text = $"Mã xác thực được gửi sang email {_email}";
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            string otp = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(otp))
            {
                MessageBox.Show("Vui lòng nhập mã OTP");
                return;
            }

            btnVerify.Enabled = false;

            try
            {
                await ClientSession.ConnectAsync();

                var res = await ClientSession.Tcp.VerifyOTPAsync(_email, otp);

                if (!res.Success)
                {
                    MessageBox.Show(res.Message ?? "OTP không hợp lệ");
                    return;
                }

                MessageBox.Show("Xác thực OTP thành công");

                this.Hide();
                using (var f = new SetUpPasswordForm(_email))
                {
                    f.ShowDialog();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xác thực OTP: " + ex.Message);
            }
            finally
            {
                btnVerify.Enabled = true;
            }
        }

        private async void lkSendAgain_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                await ClientSession.ConnectAsync();
                await ClientSession.Tcp.ForgotPasswordAsync(_email);
                MessageBox.Show("OTP mới đã được gửi về email");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi lại OTP: " + ex.Message);
            }
        }
    }
}
