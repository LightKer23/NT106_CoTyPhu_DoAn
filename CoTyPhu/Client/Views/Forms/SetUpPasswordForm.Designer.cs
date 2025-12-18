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
            pnlVerifyOTP.Location = new Point(163, 139);
            pnlVerifyOTP.Margin = new Padding(5);
            pnlVerifyOTP.Name = "pnlVerifyOTP";
            pnlVerifyOTP.Size = new Size(616, 575);
            pnlVerifyOTP.TabIndex = 1;
            pnlVerifyOTP.Paint += pnlVerifyOTP_Paint;
            // 
            // btnSetUpPsswrd
            // 
            btnSetUpPsswrd.BackgroundImage = (Image)resources.GetObject("btnSetUpPsswrd.BackgroundImage");
            btnSetUpPsswrd.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSetUpPsswrd.ForeColor = Color.White;
            btnSetUpPsswrd.Location = new Point(60, 455);
            btnSetUpPsswrd.Margin = new Padding(5);
            btnSetUpPsswrd.Name = "btnSetUpPsswrd";
            btnSetUpPsswrd.Size = new Size(485, 46);
            btnSetUpPsswrd.TabIndex = 12;
            btnSetUpPsswrd.Text = "Đặt lại mật khẩu";
            btnSetUpPsswrd.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.WindowFrame;
            label2.Location = new Point(60, 112);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(492, 96);
            label2.TabIndex = 1;
            label2.Text = "Mật khẩu có ít nhất 8 ký tự, bao gồm chữ cái, chữ số, chữ in hoa và các ký hiệu đặc biệt như #, $, !, @";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(105, 42);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(376, 59);
            label1.TabIndex = 0;
            label1.Text = "Thay đổi mật khẩu";
            // 
            // SetUpPasswordForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(946, 852);
            Controls.Add(pnlVerifyOTP);
            Margin = new Padding(5);
            Name = "SetUpPasswordForm";
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
        private Button btnSetUpPsswrd;
    }
}