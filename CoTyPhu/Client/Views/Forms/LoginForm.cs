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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void lkLblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (RegisterForm registerForm = new RegisterForm())
            {
                registerForm.ShowDialog();
            }

            this.Show();
        }

        private void lkLblForgotPsswrd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            using (ForgotPasswordForm forgotForm = new ForgotPasswordForm())
            {
                forgotForm.ShowDialog();
            }

            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = lblTxtUsername.Text.Trim();
            string password = lblTxtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.");
                return;
            }

            //SQL kiểm tra username và password
            MessageBox.Show("Đăng nhập thành công!");

            this.Hide();
            MenuForm menu = new MenuForm();
            menu.Show();
        }


    }
}
