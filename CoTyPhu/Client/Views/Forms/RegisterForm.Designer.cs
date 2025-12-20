namespace Client.Views.Forms
{
    partial class RegisterForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            pnlRegister = new Panel();
            lblTxtPsswrdAgain = new Client.Views.User_Controls.LabelTextBoxControl();
            lblTxtPsswrd = new Client.Views.User_Controls.LabelTextBoxControl();
            lblTxtEmail = new Client.Views.User_Controls.LabelTextBoxControl();
            lkLblLogin = new LinkLabel();
            label1 = new Label();
            header = new Label();
            lblTxtFullname = new Client.Views.User_Controls.LabelTextBoxControl();
            btnRegister = new Button();
            lblTxtUsername = new Client.Views.User_Controls.LabelTextBoxControl();
            pnlRegister.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRegister
            // 
            pnlRegister.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlRegister.BackColor = Color.Transparent;
            pnlRegister.BorderStyle = BorderStyle.FixedSingle;
            pnlRegister.Controls.Add(lblTxtPsswrdAgain);
            pnlRegister.Controls.Add(lblTxtPsswrd);
            pnlRegister.Controls.Add(lblTxtEmail);
            pnlRegister.Controls.Add(lkLblLogin);
            pnlRegister.Controls.Add(label1);
            pnlRegister.Controls.Add(header);
            pnlRegister.Controls.Add(lblTxtFullname);
            pnlRegister.Controls.Add(btnRegister);
            pnlRegister.Controls.Add(lblTxtUsername);
            pnlRegister.Location = new Point(100, 55);
            pnlRegister.Name = "pnlRegister";
            pnlRegister.Size = new Size(380, 523);
            pnlRegister.TabIndex = 1;
            // 
            // lblTxtPsswrdAgain
            // 
            lblTxtPsswrdAgain.IsPassword = true;
            lblTxtPsswrdAgain.LabelText = "Nhập lại mật khẩu";
            lblTxtPsswrdAgain.Location = new Point(40, 329);
            lblTxtPsswrdAgain.Margin = new Padding(5, 5, 5, 5);
            lblTxtPsswrdAgain.Name = "lblTxtPsswrdAgain";
            lblTxtPsswrdAgain.PasswordChar = '●';
            lblTxtPsswrdAgain.Size = new Size(300, 72);
            lblTxtPsswrdAgain.TabIndex = 19;
            lblTxtPsswrdAgain.TextBoxReadOnly = false;
            // 
            // lblTxtPsswrd
            // 
            lblTxtPsswrd.IsPassword = true;
            lblTxtPsswrd.LabelText = "Mật khẩu";
            lblTxtPsswrd.Location = new Point(40, 262);
            lblTxtPsswrd.Margin = new Padding(5, 5, 5, 5);
            lblTxtPsswrd.Name = "lblTxtPsswrd";
            lblTxtPsswrd.PasswordChar = '●';
            lblTxtPsswrd.Size = new Size(300, 72);
            lblTxtPsswrd.TabIndex = 18;
            lblTxtPsswrd.TextBoxReadOnly = false;
            // 
            // lblTxtEmail
            // 
            lblTxtEmail.IsPassword = false;
            lblTxtEmail.LabelText = "Email";
            lblTxtEmail.Location = new Point(40, 192);
            lblTxtEmail.Margin = new Padding(5, 5, 5, 5);
            lblTxtEmail.Name = "lblTxtEmail";
            lblTxtEmail.PasswordChar = '\0';
            lblTxtEmail.Size = new Size(300, 72);
            lblTxtEmail.TabIndex = 16;
            lblTxtEmail.TextBoxReadOnly = false;
            lblTxtEmail.Load += lblTxtEmail_Load;
            // 
            // lkLblLogin
            // 
            lkLblLogin.AutoSize = true;
            lkLblLogin.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lkLblLogin.LinkColor = Color.Blue;
            lkLblLogin.Location = new Point(208, 470);
            lkLblLogin.Name = "lkLblLogin";
            lkLblLogin.Size = new Size(82, 20);
            lkLblLogin.TabIndex = 15;
            lkLblLogin.TabStop = true;
            lkLblLogin.Text = "Đăng nhập";
            lkLblLogin.LinkClicked += lkLblLogin_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(94, 469);
            label1.Name = "label1";
            label1.Size = new Size(120, 20);
            label1.TabIndex = 14;
            label1.Text = "Đã có tài khoản?";
            // 
            // header
            // 
            header.AutoSize = true;
            header.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            header.ForeColor = Color.FromArgb(192, 0, 0);
            header.Location = new Point(136, 27);
            header.Name = "header";
            header.Size = new Size(123, 32);
            header.TabIndex = 8;
            header.Text = "ĐĂNG KÝ";
            header.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTxtFullname
            // 
            lblTxtFullname.IsPassword = false;
            lblTxtFullname.LabelText = "Họ tên người dùng";
            lblTxtFullname.Location = new Point(40, 58);
            lblTxtFullname.Margin = new Padding(5, 5, 5, 5);
            lblTxtFullname.Name = "lblTxtFullname";
            lblTxtFullname.PasswordChar = '\0';
            lblTxtFullname.Size = new Size(300, 72);
            lblTxtFullname.TabIndex = 9;
            lblTxtFullname.TextBoxReadOnly = false;
            // 
            // btnRegister
            // 
            btnRegister.BackgroundImage = (Image)resources.GetObject("btnRegister.BackgroundImage");
            btnRegister.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(38, 429);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(298, 29);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // lblTxtUsername
            // 
            lblTxtUsername.IsPassword = false;
            lblTxtUsername.LabelText = "Tên người dùng";
            lblTxtUsername.Location = new Point(40, 127);
            lblTxtUsername.Margin = new Padding(5, 5, 5, 5);
            lblTxtUsername.Name = "lblTxtUsername";
            lblTxtUsername.PasswordChar = '\0';
            lblTxtUsername.Size = new Size(300, 72);
            lblTxtUsername.TabIndex = 10;
            lblTxtUsername.TextBoxReadOnly = false;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(582, 653);
            Controls.Add(pnlRegister);
            Name = "RegisterForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng ký";
            pnlRegister.ResumeLayout(false);
            pnlRegister.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlRegister;
        private LinkLabel lkLblLogin;
        private Label label1;
        private LinkLabel lkLblForgotPsswrd;
        private Label header;
        private Views.User_Controls.LabelTextBoxControl lblTxtFullname;
        private Button btnRegister;
        private Views.User_Controls.LabelTextBoxControl lblTxtUsername;
        private Views.User_Controls.LabelTextBoxControl lblTxtEmail;
        private Views.User_Controls.LabelTextBoxControl lblTxtPsswrd;
        private Views.User_Controls.LabelTextBoxControl lblTxtPsswrdAgain;
    }
}
