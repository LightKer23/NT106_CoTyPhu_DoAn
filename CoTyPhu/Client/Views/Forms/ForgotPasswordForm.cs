using Client.Services.Network;
using Common.Contracts.Auth;
using System;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private async void btnCont_Click(object sender, EventArgs e)
        {
            string email = labelTextBoxControl1.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email");
                return;
            }

            btnCont.Enabled = false;

            try
            {
                await ClientSession.ConnectAsync();

                var res = await ClientSession.Tcp.ForgotPasswordAsync(email);

                if (!res.Success)
                {
                    MessageBox.Show(res.Message);
                    return;
                }

                MessageBox.Show("OTP đã được gửi về email");

                this.Hide();

                using (var f = new VerifyOTPForm(email))
                {
                    f.ShowDialog();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                btnCont.Enabled = true;
            }
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
