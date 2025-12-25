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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyOTPForm));
            pnlVerifyOTP = new Panel();
            label4 = new Label();
            btnVerify = new Button();
            lkSendAgain = new LinkLabel();
            label3 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            pnlVerifyOTP.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVerifyOTP
            // 
            pnlVerifyOTP.BackColor = Color.Transparent;
            pnlVerifyOTP.BorderStyle = BorderStyle.FixedSingle;
            pnlVerifyOTP.Controls.Add(label4);
            pnlVerifyOTP.Controls.Add(btnVerify);
            pnlVerifyOTP.Controls.Add(lkSendAgain);
            pnlVerifyOTP.Controls.Add(label3);
            pnlVerifyOTP.Controls.Add(textBox1);
            pnlVerifyOTP.Controls.Add(label2);
            pnlVerifyOTP.Controls.Add(label1);
            pnlVerifyOTP.Location = new Point(80, 65);
            pnlVerifyOTP.Name = "pnlVerifyOTP";
            pnlVerifyOTP.Size = new Size(380, 280);
            pnlVerifyOTP.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(63, 106);
            label4.Name = "label4";
            label4.Size = new Size(249, 20);
            label4.TabIndex = 6;
            label4.Text = "Vui lòng nhập mã xác thực bên dưới";
            // 
            // btnVerify
            // 
            btnVerify.BackgroundImage = (Image)resources.GetObject("btnVerify.BackgroundImage");
            btnVerify.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVerify.ForeColor = SystemColors.ButtonHighlight;
            btnVerify.Location = new Point(40, 182);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(297, 29);
            btnVerify.TabIndex = 5;
            btnVerify.Text = "Xác thực";
            btnVerify.UseVisualStyleBackColor = true;
            btnVerify.Click += btnVerify_Click;
            // 
            // lkSendAgain
            // 
            lkSendAgain.AutoSize = true;
            lkSendAgain.Location = new Point(229, 223);
            lkSendAgain.Name = "lkSendAgain";
            lkSendAgain.Size = new Size(52, 20);
            lkSendAgain.TabIndex = 4;
            lkSendAgain.TabStop = true;
            lkSendAgain.Text = "Gửi lại";
            lkSendAgain.LinkClicked += lkSendAgain_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(82, 223);
            label3.Name = "label3";
            label3.Size = new Size(150, 18);
            label3.TabIndex = 3;
            label3.Text = "Chưa nhận được mã?";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(40, 139);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 27);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.WindowFrame;
            label2.Location = new Point(63, 57);
            label2.Name = "label2";
            label2.Size = new Size(254, 20);
            label2.TabIndex = 1;
            label2.Text = "Mã xác thực được gửi sang email xxx";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(98, 20);
            label1.Name = "label1";
            label1.Size = new Size(173, 37);
            label1.TabIndex = 0;
            label1.Text = "Xác thực OTP";
            // 
            // VerifyOTPForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(542, 406);
            Controls.Add(pnlVerifyOTP);
            Name = "VerifyOTPForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên mật khẩu";
            pnlVerifyOTP.ResumeLayout(false);
            pnlVerifyOTP.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlVerifyOTP;
        private Button btnVerify;
        private LinkLabel lkSendAgain;
        private Label label3;
        private TextBox textBox1;
        private Label label2;
        private Label label4;
        private Label label1;
    }
}