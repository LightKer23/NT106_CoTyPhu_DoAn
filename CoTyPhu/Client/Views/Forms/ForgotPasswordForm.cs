using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private string userEmail;
        private string OTP;

        private void btnCont_Click(object sender, EventArgs e)
        {
            userEmail = labelTextBoxUserEmail.Text.Trim();
            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("Vui lòng nhập Email!");
            }
            GenerateOTP();

            try
            {
                //SQL Kiểm tra Mail có trong Database không
                sendOTPMail(userEmail, OTP);
                MessageBox.Show("Mã OTP đã được gửi đến Email!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VerifyOTPForm verify = new VerifyOTPForm(userEmail, OTP);
                this.Hide();
                verify.ShowDialog();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Đã xảy ra lỗi, vui lòng kiểm tra lại Email");
            }
        }

        private void GenerateOTP()
        {
            Random random = new Random();
            OTP = random.Next(100000, 999999).ToString();
        }

        private void sendOTPMail(string userMail, string OTP)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("", "");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("");
            message.To.Add(userEmail);
            message.Subject = "XÁC MINH TÀI KHOẢN CỜ TỶ PHÚ";
            message.Body = $"Mã OTP xác minh tài khoản của bạn: {OTP}";

            client.Send(message);
        }
    }
}
