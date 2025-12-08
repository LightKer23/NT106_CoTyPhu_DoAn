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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            btnVerify = new Button();
            label4 = new Label();
            pnlVerifyOTP.SuspendLayout();
            SuspendLayout();
            // 
            // pnlVerifyOTP
            // 
            pnlVerifyOTP.BorderStyle = BorderStyle.FixedSingle;
            pnlVerifyOTP.Controls.Add(label4);
            pnlVerifyOTP.Controls.Add(btnVerify);
            pnlVerifyOTP.Controls.Add(linkLabel1);
            pnlVerifyOTP.Controls.Add(label3);
            pnlVerifyOTP.Controls.Add(textBox1);
            pnlVerifyOTP.Controls.Add(label2);
            pnlVerifyOTP.Controls.Add(label1);
            pnlVerifyOTP.Location = new Point(100, 175);
            pnlVerifyOTP.Name = "pnlVerifyOTP";
            pnlVerifyOTP.Size = new Size(380, 300);
            pnlVerifyOTP.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(40, 30);
            label1.Name = "label1";
            label1.Size = new Size(183, 34);
            label1.TabIndex = 0;
            label1.Text = "Xác thực OTP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.WindowFrame;
            label2.Location = new Point(40, 70);
            label2.Name = "label2";
            label2.Size = new Size(254, 20);
            label2.TabIndex = 1;
            label2.Text = "Mã xác thực được gửi sang email xxx";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(40, 165);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 27);
            textBox1.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(89, 245);
            label3.Name = "label3";
            label3.Size = new Size(150, 18);
            label3.TabIndex = 3;
            label3.Text = "Chưa nhận được mã?";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(235, 245);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(52, 20);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Gửi lại";
            // 
            // btnVerify
            // 
            btnVerify.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVerify.Location = new Point(40, 200);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(300, 40);
            btnVerify.TabIndex = 5;
            btnVerify.Text = "Xác thực";
            btnVerify.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(68, 130);
            label4.Name = "label4";
            label4.Size = new Size(244, 18);
            label4.TabIndex = 6;
            label4.Text = "Vui lòng nhập mã xác thực bên dưới";
            // 
            // VerifyOTPForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 653);
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
        private LinkLabel linkLabel1;
        private Label label3;
        private TextBox textBox1;
        private Label label2;
        private Label label4;
    }
}