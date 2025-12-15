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
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client.Views.Forms
{
    public partial class VerifyOTPForm : Form
    {

        private string generatedOTP;
        private string userEmail;
        public VerifyOTPForm(string userEmail, string OTP)
        {
            InitializeComponent();
            generatedOTP = OTP;
            this.userEmail = userEmail;
        }


        private void btnVerify_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text.Trim();
            if(generatedOTP == enteredOTP)
            {
                MessageBox.Show("Xác thực mã OTP thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                SetUpPasswordForm setUpPasswordForm = new SetUpPasswordForm();
                setUpPasswordForm.ShowDialog();
                this.Close();
            }

            else
            {
                MessageBox.Show(Text = "Mã OTP chưa chính xác, vui lòng thử lại.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void GenerateOTP()
        {
            Random random = new Random();
            generatedOTP = random.Next(100000, 999999).ToString();
        }

        private void sendOTPMail(string userMail, string OTP)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("", "");

            MailMessage message = new MailMessage();
            message.From = new MailAddress("");
            message.To.Add(userMail);
            message.Subject = "XÁC MINH TÀI KHOẢN CỜ TỶ PHÚ";
            message.Body = $"Mã OTP xác minh tài khoản của bạn: {OTP}";

            client.Send(message);
        }

        private void lkLblResendOTP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtOTP.Clear();
            GenerateOTP();
            try
            {
                //SQL Kiểm tra Mail có trong Database không
                sendOTPMail(userEmail, generatedOTP);
                MessageBox.Show("Mã OTP đã được gửi đến Email!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Đã xảy ra lỗi, vui lòng kiểm tra lại Email");
            }
        }
    }
}
