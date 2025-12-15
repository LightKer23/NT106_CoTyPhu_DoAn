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
            pnlRegister = new Panel();
            lblTxtPsswrdAgain = new Client.Views.User_Controls.LabelTextBoxControl();
            lblTxtPsswrd = new Client.Views.User_Controls.LabelTextBoxControl();
            lblTxtEmail = new Client.Views.User_Controls.LabelTextBoxControl();
            lkLblLogin = new LinkLabel();
            label1 = new Label();
            label2 = new Label();
            lblTxtFullname = new Client.Views.User_Controls.LabelTextBoxControl();
            btnRegister = new Button();
            lblTxtUsername = new Client.Views.User_Controls.LabelTextBoxControl();
            pnlRegister.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRegister
            // 
            pnlRegister.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlRegister.BorderStyle = BorderStyle.FixedSingle;
            pnlRegister.Controls.Add(lblTxtPsswrdAgain);
            pnlRegister.Controls.Add(lblTxtPsswrd);
            pnlRegister.Controls.Add(lblTxtEmail);
            pnlRegister.Controls.Add(lkLblLogin);
            pnlRegister.Controls.Add(label1);
            pnlRegister.Controls.Add(label2);
            pnlRegister.Controls.Add(lblTxtFullname);
            pnlRegister.Controls.Add(btnRegister);
            pnlRegister.Controls.Add(lblTxtUsername);
            pnlRegister.Location = new Point(100, 52);
            pnlRegister.Name = "pnlRegister";
            pnlRegister.Size = new Size(380, 513);
            pnlRegister.TabIndex = 1;
            // 
            // lblTxtPsswrdAgain
            // 
            lblTxtPsswrdAgain.IsPassword = true;
            lblTxtPsswrdAgain.LabelText = "Nhập lại mật khẩu";
            lblTxtPsswrdAgain.Location = new Point(40, 332);
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
            lblTxtPsswrd.Location = new Point(40, 271);
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
            lblTxtEmail.Location = new Point(40, 209);
            lblTxtEmail.Name = "lblTxtEmail";
            lblTxtEmail.PasswordChar = '\0';
            lblTxtEmail.Size = new Size(300, 68);
            lblTxtEmail.TabIndex = 16;
            lblTxtEmail.TextBoxReadOnly = false;
            // 
            // lkLblLogin
            // 
            lkLblLogin.AutoSize = true;
            lkLblLogin.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lkLblLogin.Location = new Point(209, 480);
            lkLblLogin.Name = "lkLblLogin";
            lkLblLogin.Size = new Size(76, 17);
            lkLblLogin.TabIndex = 15;
            lkLblLogin.TabStop = true;
            lkLblLogin.Text = "Đăng nhập";
            lkLblLogin.LinkClicked += lkLblLogin_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(92, 480);
            label1.Name = "label1";
            label1.Size = new Size(111, 17);
            label1.TabIndex = 14;
            label1.Text = "Đã có tài khoản?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(40, 19);
            label2.Name = "label2";
            label2.Size = new Size(103, 30);
            label2.TabIndex = 8;
            label2.Text = "Đăng ký";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTxtFullname
            // 
            lblTxtFullname.IsPassword = false;
            lblTxtFullname.LabelText = "Họ tên người dùng";
            lblTxtFullname.Location = new Point(40, 86);
            lblTxtFullname.Name = "lblTxtFullname";
            lblTxtFullname.PasswordChar = '\0';
            lblTxtFullname.Size = new Size(300, 68);
            lblTxtFullname.TabIndex = 9;
            lblTxtFullname.TextBoxReadOnly = false;
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegister.Location = new Point(40, 423);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(300, 38);
            btnRegister.TabIndex = 11;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblTxtUsername
            // 
            lblTxtUsername.IsPassword = false;
            lblTxtUsername.LabelText = "Tên người dùng";
            lblTxtUsername.Location = new Point(40, 147);
            lblTxtUsername.Name = "lblTxtUsername";
            lblTxtUsername.PasswordChar = '\0';
            lblTxtUsername.Size = new Size(300, 68);
            lblTxtUsername.TabIndex = 10;
            lblTxtUsername.TextBoxReadOnly = false;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 620);
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
        private Label label2;
        private Views.User_Controls.LabelTextBoxControl lblTxtFullname;
        private Button btnRegister;
        private Views.User_Controls.LabelTextBoxControl lblTxtUsername;
        private Views.User_Controls.LabelTextBoxControl lblTxtEmail;
        private Views.User_Controls.LabelTextBoxControl lblTxtPsswrd;
        private Views.User_Controls.LabelTextBoxControl lblTxtPsswrdAgain;
    }
}
