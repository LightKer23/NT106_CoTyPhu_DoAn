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
            pnlVerifyOTP = new Panel();
            label2 = new Label();
            label1 = new Label();
            lblTxtNewPassword = new Client.Views.User_Controls.LabelTextBoxControl();
            lblTxtPasswordAgain = new Client.Views.User_Controls.LabelTextBoxControl();
            btnVerify = new Button();
            pnlVerifyOTP.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVerifyOTP
            // 
            pnlVerifyOTP.Controls.Add(btnVerify);
            pnlVerifyOTP.Controls.Add(lblTxtPasswordAgain);
            pnlVerifyOTP.Controls.Add(lblTxtNewPassword);
            pnlVerifyOTP.Controls.Add(label2);
            pnlVerifyOTP.Controls.Add(label1);
            pnlVerifyOTP.Location = new Point(100, 145);
            pnlVerifyOTP.Name = "pnlVerifyOTP";
            pnlVerifyOTP.Size = new Size(380, 360);
            pnlVerifyOTP.TabIndex = 1;
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
            label1.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(40, 30);
            label1.Name = "label1";
            label1.Size = new Size(249, 34);
            label1.TabIndex = 0;
            label1.Text = "Thay đổi mật khẩu";
            // 
            // lblTxtNewPassword
            // 
            lblTxtNewPassword.LabelText = "Mật khẩu mới";
            lblTxtNewPassword.Location = new Point(40, 160);
            lblTxtNewPassword.Name = "lblTxtNewPassword";
            lblTxtNewPassword.PasswordChar = '\0';
            lblTxtNewPassword.Size = new Size(300, 72);
            lblTxtNewPassword.TabIndex = 10;
            lblTxtNewPassword.TextBoxReadOnly = false;
            // 
            // lblTxtPasswordAgain
            // 
            lblTxtPasswordAgain.LabelText = "Nhập lại mật khẩu";
            lblTxtPasswordAgain.Location = new Point(40, 225);
            lblTxtPasswordAgain.Name = "lblTxtPasswordAgain";
            lblTxtPasswordAgain.PasswordChar = '\0';
            lblTxtPasswordAgain.Size = new Size(300, 72);
            lblTxtPasswordAgain.TabIndex = 11;
            lblTxtPasswordAgain.TextBoxReadOnly = false;
            // 
            // btnVerify
            // 
            btnVerify.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVerify.Location = new Point(40, 300);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(300, 40);
            btnVerify.TabIndex = 12;
            btnVerify.Text = "Xác thực";
            btnVerify.UseVisualStyleBackColor = true;
            // 
            // SetUpPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 653);
            Controls.Add(pnlVerifyOTP);
            Name = "SetUpPassword";
            ShowIcon = false;
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
        private Button btnVerify;
    }
}