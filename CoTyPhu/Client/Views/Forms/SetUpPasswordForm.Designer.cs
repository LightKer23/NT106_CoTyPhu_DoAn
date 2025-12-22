namespace Client.Views.Forms
{
    partial class SetUpPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetUpPasswordForm));
            pnlVerifyOTP = new Panel();
            btnSetUpPsswrd = new Button();
            label2 = new Label();
            label1 = new Label();
            lblTxtNewPassword = new Client.Views.User_Controls.LabelTextBoxControl();
            lblTxtPasswordAgain = new Client.Views.User_Controls.LabelTextBoxControl();
            pnlVerifyOTP.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVerifyOTP
            // 
            pnlVerifyOTP.BackColor = Color.Transparent;
            pnlVerifyOTP.BorderStyle = BorderStyle.FixedSingle;
            pnlVerifyOTP.Controls.Add(btnSetUpPsswrd);
            pnlVerifyOTP.Controls.Add(label2);
            pnlVerifyOTP.Controls.Add(label1);
            pnlVerifyOTP.Controls.Add(lblTxtNewPassword);
            pnlVerifyOTP.Controls.Add(lblTxtPasswordAgain);
            pnlVerifyOTP.Location = new Point(100, 87);
            pnlVerifyOTP.Name = "pnlVerifyOTP";
            pnlVerifyOTP.Size = new Size(380, 360);
            pnlVerifyOTP.TabIndex = 1;
            // 
            // btnSetUpPsswrd
            // 
            btnSetUpPsswrd.BackgroundImage = (Image)resources.GetObject("btnSetUpPsswrd.BackgroundImage");
            btnSetUpPsswrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSetUpPsswrd.ForeColor = Color.White;
            btnSetUpPsswrd.Location = new Point(37, 284);
            btnSetUpPsswrd.Name = "btnSetUpPsswrd";
            btnSetUpPsswrd.Size = new Size(298, 29);
            btnSetUpPsswrd.TabIndex = 12;
            btnSetUpPsswrd.Text = "Đặt lại mật khẩu";
            btnSetUpPsswrd.UseVisualStyleBackColor = true;
            btnSetUpPsswrd.Click += btnSetUpPsswrd_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.WindowFrame;
            label2.Location = new Point(37, 70);
            label2.Name = "label2";
            label2.Size = new Size(303, 60);
            label2.TabIndex = 1;
            label2.Text = "Mật khẩu có ít nhất 8 ký tự, bao gồm chữ cái, chữ số, chữ in hoa và các ký hiệu đặc biệt như #, $, !, @";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(65, 26);
            label1.Name = "label1";
            label1.Size = new Size(236, 37);
            label1.TabIndex = 0;
            label1.Text = "Thay đổi mật khẩu";
            // 
            // lblTxtNewPassword
            // 
            lblTxtNewPassword.IsPassword = true;
            lblTxtNewPassword.LabelText = "Mật khẩu mới";
            lblTxtNewPassword.Location = new Point(37, 144);
            lblTxtNewPassword.Margin = new Padding(2);
            lblTxtNewPassword.Name = "lblTxtNewPassword";
            lblTxtNewPassword.PasswordChar = '●';
            lblTxtNewPassword.Size = new Size(298, 76);
            lblTxtNewPassword.TabIndex = 2;
            lblTxtNewPassword.TextBoxReadOnly = false;
            // 
            // lblTxtPasswordAgain
            // 
            lblTxtPasswordAgain.IsPassword = true;
            lblTxtPasswordAgain.LabelText = "Nhập lại mật khẩu";
            lblTxtPasswordAgain.Location = new Point(37, 210);
            lblTxtPasswordAgain.Margin = new Padding(2);
            lblTxtPasswordAgain.Name = "lblTxtPasswordAgain";
            lblTxtPasswordAgain.PasswordChar = '●';
            lblTxtPasswordAgain.Size = new Size(298, 69);
            lblTxtPasswordAgain.TabIndex = 3;
            lblTxtPasswordAgain.TextBoxReadOnly = false;
            // 
            // SetUpPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(582, 532);
            Controls.Add(pnlVerifyOTP);
            Name = "SetUpPasswordForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thiết lập mật khẩu";
            pnlVerifyOTP.ResumeLayout(false);
            pnlVerifyOTP.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlVerifyOTP;
        private Label label2;
        private Label label1;
        private User_Controls.LabelTextBoxControl lblTxtPasswordAgain;
        private User_Controls.LabelTextBoxControl lblTxtNewPassword;
        private Button btnSetUpPsswrd;
    }
}