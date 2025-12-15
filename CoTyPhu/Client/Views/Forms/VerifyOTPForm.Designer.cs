namespace Client.Views.Forms
{
    partial class VerifyOTPForm
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
            label4 = new Label();
            btnVerify = new Button();
            lkLblResendOTP = new LinkLabel();
            label3 = new Label();
            txtOTP = new TextBox();
            lblToEmail = new Label();
            label1 = new Label();
            pnlVerifyOTP.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVerifyOTP
            // 
            pnlVerifyOTP.BorderStyle = BorderStyle.FixedSingle;
            pnlVerifyOTP.Controls.Add(label4);
            pnlVerifyOTP.Controls.Add(btnVerify);
            pnlVerifyOTP.Controls.Add(lkLblResendOTP);
            pnlVerifyOTP.Controls.Add(label3);
            pnlVerifyOTP.Controls.Add(txtOTP);
            pnlVerifyOTP.Controls.Add(lblToEmail);
            pnlVerifyOTP.Controls.Add(label1);
            pnlVerifyOTP.Location = new Point(100, 166);
            pnlVerifyOTP.Name = "pnlVerifyOTP";
            pnlVerifyOTP.Size = new Size(380, 285);
            pnlVerifyOTP.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(68, 124);
            label4.Name = "label4";
            label4.Size = new Size(231, 17);
            label4.TabIndex = 6;
            label4.Text = "Vui lòng nhập mã xác thực bên dưới";
            // 
            // btnVerify
            // 
            btnVerify.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVerify.Location = new Point(40, 190);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(300, 38);
            btnVerify.TabIndex = 5;
            btnVerify.Text = "Xác thực";
            btnVerify.UseVisualStyleBackColor = true;
            btnVerify.Click += btnVerify_Click;
            // 
            // lkLblResendOTP
            // 
            lkLblResendOTP.AutoSize = true;
            lkLblResendOTP.Location = new Point(235, 233);
            lkLblResendOTP.Name = "lkLblResendOTP";
            lkLblResendOTP.Size = new Size(47, 19);
            lkLblResendOTP.TabIndex = 4;
            lkLblResendOTP.TabStop = true;
            lkLblResendOTP.Text = "Gửi lại";
            lkLblResendOTP.LinkClicked += lkLblResendOTP_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(89, 233);
            label3.Name = "label3";
            label3.Size = new Size(142, 17);
            label3.TabIndex = 3;
            label3.Text = "Chưa nhận được mã?";
            // 
            // txtOTP
            // 
            txtOTP.Location = new Point(40, 157);
            txtOTP.Name = "txtOTP";
            txtOTP.Size = new Size(300, 26);
            txtOTP.TabIndex = 2;
            // 
            // lblToEmail
            // 
            lblToEmail.AutoSize = true;
            lblToEmail.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblToEmail.ForeColor = SystemColors.WindowFrame;
            lblToEmail.Location = new Point(40, 66);
            lblToEmail.Name = "lblToEmail";
            lblToEmail.Size = new Size(231, 19);
            lblToEmail.TabIndex = 1;
            lblToEmail.Text = "Mã xác thực được gửi sang email xxx";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(40, 28);
            label1.Name = "label1";
            label1.Size = new Size(165, 30);
            label1.TabIndex = 0;
            label1.Text = "Xác thực OTP";
            // 
            // VerifyOTPForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 620);
            Controls.Add(pnlVerifyOTP);
            Name = "VerifyOTPForm";
            ShowIcon = false;
            Text = "Quên mật khẩu";
            pnlVerifyOTP.ResumeLayout(false);
            pnlVerifyOTP.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlVerifyOTP;
        private Label label1;
        private Button btnVerify;
        private LinkLabel lkLblResendOTP;
        private Label label3;
        private TextBox txtOTP;
        private Label lblToEmail;
        private Label label4;
    }
}