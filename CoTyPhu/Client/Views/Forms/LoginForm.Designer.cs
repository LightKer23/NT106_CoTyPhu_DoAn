namespace Client.Views.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlLogin = new Panel();
            lkLblRegister = new LinkLabel();
            label1 = new Label();
            lkLblForgotPsswrd = new LinkLabel();
            label2 = new Label();
            lblTxtUsername = new Client.Views.User_Controls.LabelTextBoxControl();
            btnLogin = new Button();
            lblTxtPassword = new Client.Views.User_Controls.LabelTextBoxControl();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLogin
            // 
            pnlLogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlLogin.BorderStyle = BorderStyle.FixedSingle;
            pnlLogin.Controls.Add(lkLblRegister);
            pnlLogin.Controls.Add(label1);
            pnlLogin.Controls.Add(lkLblForgotPsswrd);
            pnlLogin.Controls.Add(label2);
            pnlLogin.Controls.Add(lblTxtUsername);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Controls.Add(lblTxtPassword);
            pnlLogin.Location = new Point(100, 135);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(380, 380);
            pnlLogin.TabIndex = 0;
            // 
            // lkLblRegister
            // 
            lkLblRegister.AutoSize = true;
            lkLblRegister.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lkLblRegister.Location = new Point(221, 340);
            lkLblRegister.Name = "lkLblRegister";
            lkLblRegister.Size = new Size(99, 18);
            lkLblRegister.TabIndex = 15;
            lkLblRegister.TabStop = true;
            lkLblRegister.Text = "Đăng ký ngay";
            lkLblRegister.LinkClicked += lkLblRegister_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(60, 340);
            label1.Name = "label1";
            label1.Size = new Size(161, 18);
            label1.TabIndex = 14;
            label1.Text = "Bạn chưa có tài khoản?";
            // 
            // lkLblForgotPsswrd
            // 
            lkLblForgotPsswrd.AutoSize = true;
            lkLblForgotPsswrd.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lkLblForgotPsswrd.Location = new Point(219, 226);
            lkLblForgotPsswrd.Name = "lkLblForgotPsswrd";
            lkLblForgotPsswrd.Size = new Size(117, 18);
            lkLblForgotPsswrd.TabIndex = 13;
            lkLblForgotPsswrd.TabStop = true;
            lkLblForgotPsswrd.Text = "Quên mật khẩu?";
            lkLblForgotPsswrd.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(40, 30);
            label2.Name = "label2";
            label2.Size = new Size(171, 43);
            label2.TabIndex = 8;
            label2.Text = "ĐĂNG NHẬP";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTxtUsername
            // 
            lblTxtUsername.LabelText = "Tên người dùng";
            lblTxtUsername.Location = new Point(40, 100);
            lblTxtUsername.Name = "lblTxtUsername";
            lblTxtUsername.PasswordChar = '\0';
            lblTxtUsername.Size = new Size(300, 72);
            lblTxtUsername.TabIndex = 9;
            lblTxtUsername.TextBoxReadOnly = false;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(40, 280);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(300, 40);
            btnLogin.TabIndex = 11;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // lblTxtPassword
            // 
            lblTxtPassword.LabelText = "Mật khẩu";
            lblTxtPassword.Location = new Point(40, 165);
            lblTxtPassword.Name = "lblTxtPassword";
            lblTxtPassword.PasswordChar = '\0';
            lblTxtPassword.Size = new Size(300, 72);
            lblTxtPassword.TabIndex = 10;
            lblTxtPassword.TextBoxReadOnly = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 653);
            Controls.Add(pnlLogin);
            Name = "LoginForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLogin;
        private Label label1;
        private LinkLabel lkLblForgotPsswrd;
        private Label label2;
        private User_Controls.LabelTextBoxControl lblTxtUsername;
        private Button btnLogin;
        private User_Controls.LabelTextBoxControl lblTxtPassword;
        private LinkLabel lkLblRegister;
    }
}