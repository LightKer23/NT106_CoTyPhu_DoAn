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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            pnlLogin = new Panel();
            lkLblRegister = new LinkLabel();
            label1 = new Label();
            lkLblForgotPsswrd = new LinkLabel();
            label2 = new Label();
            lblTxtUsername = new Client.Views.User_Controls.LabelTextBoxControl();
            btnLogin = new Button();
            lblTxtPassword = new Client.Views.User_Controls.LabelTextBoxControl();
            header = new Label();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlLogin
            // 
            pnlLogin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlLogin.BackColor = Color.Transparent;
            pnlLogin.BorderStyle = BorderStyle.FixedSingle;
            pnlLogin.Controls.Add(lkLblRegister);
            pnlLogin.Controls.Add(label1);
            pnlLogin.Controls.Add(lkLblForgotPsswrd);
            pnlLogin.Controls.Add(label2);
            pnlLogin.Controls.Add(lblTxtUsername);
            pnlLogin.Controls.Add(btnLogin);
            pnlLogin.Controls.Add(lblTxtPassword);
            pnlLogin.Location = new Point(170, 144);
            pnlLogin.Margin = new Padding(5);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(631, 482);
            pnlLogin.TabIndex = 0;
            // 
            // lkLblRegister
            // 
            lkLblRegister.AutoSize = true;
            lkLblRegister.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lkLblRegister.Location = new Point(343, 389);
            lkLblRegister.Margin = new Padding(5, 0, 5, 0);
            lkLblRegister.Name = "lkLblRegister";
            lkLblRegister.Size = new Size(161, 32);
            lkLblRegister.TabIndex = 15;
            lkLblRegister.TabStop = true;
            lkLblRegister.Text = "Đăng ký ngay";
            lkLblRegister.LinkClicked += lkLblRegister_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(125, 389);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(218, 32);
            label1.TabIndex = 14;
            label1.Text = "Chưa có tài khoản?";
            // 
            // lkLblForgotPsswrd
            // 
            lkLblForgotPsswrd.AutoSize = true;
            lkLblForgotPsswrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lkLblForgotPsswrd.Location = new Point(368, 282);
            lkLblForgotPsswrd.Margin = new Padding(5, 0, 5, 0);
            lkLblForgotPsswrd.Name = "lkLblForgotPsswrd";
            lkLblForgotPsswrd.Size = new Size(191, 32);
            lkLblForgotPsswrd.TabIndex = 13;
            lkLblForgotPsswrd.TabStop = true;
            lkLblForgotPsswrd.Text = "Quên mật khẩu?";
            lkLblForgotPsswrd.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(192, 0, 0);
            label2.Location = new Point(190, 19);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(246, 50);
            label2.TabIndex = 8;
            label2.Text = "ĐĂNG NHẬP";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Click += label2_Click;
            // 
            // lblTxtUsername
            // 
            lblTxtUsername.LabelText = "Tên người dùng";
            lblTxtUsername.Location = new Point(68, 77);
            lblTxtUsername.Margin = new Padding(8);
            lblTxtUsername.Name = "lblTxtUsername";
            lblTxtUsername.PasswordChar = '\0';
            lblTxtUsername.Size = new Size(488, 115);
            lblTxtUsername.TabIndex = 9;
            lblTxtUsername.TextBoxReadOnly = false;
            // 
            // btnLogin
            // 
            btnLogin.BackgroundImage = (Image)resources.GetObject("btnLogin.BackgroundImage");
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = SystemColors.ButtonHighlight;
            btnLogin.Location = new Point(65, 329);
            btnLogin.Margin = new Padding(5);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(485, 46);
            btnLogin.TabIndex = 11;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblTxtPassword
            // 
            lblTxtPassword.LabelText = "Mật khẩu";
            lblTxtPassword.Location = new Point(68, 173);
            lblTxtPassword.Margin = new Padding(8);
            lblTxtPassword.Name = "lblTxtPassword";
            lblTxtPassword.PasswordChar = '\0';
            lblTxtPassword.Size = new Size(488, 115);
            lblTxtPassword.TabIndex = 10;
            lblTxtPassword.TextBoxReadOnly = false;
            // 
            // header
            // 
            header.AutoSize = true;
            header.BackColor = Color.Transparent;
            header.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            header.ForeColor = Color.FromArgb(192, 0, 0);
            header.Location = new Point(125, 53);
            header.Name = "header";
            header.Size = new Size(711, 50);
            header.TabIndex = 1;
            header.Text = "CHÀO MỪNG BẠN ĐẾN VỚI CỜ TỶ PHÚ";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(971, 763);
            Controls.Add(header);
            Controls.Add(pnlLogin);
            Margin = new Padding(5);
            Name = "LoginForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Label header;
    }
}