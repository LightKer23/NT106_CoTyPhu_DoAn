namespace Client.Views.Forms
{
    partial class ForgotPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPasswordForm));
            pnlForgotPsswrd = new Panel();
            btnCont = new Button();
            labelTextBoxControl1 = new Client.Views.User_Controls.LabelTextBoxControl();
            label2 = new Label();
            pnlForgotPsswrd.SuspendLayout();
            SuspendLayout();
            // 
            // pnlForgotPsswrd
            // 
            pnlForgotPsswrd.BackColor = Color.Transparent;
            pnlForgotPsswrd.BorderStyle = BorderStyle.FixedSingle;
            pnlForgotPsswrd.Controls.Add(btnCont);
            pnlForgotPsswrd.Controls.Add(labelTextBoxControl1);
            pnlForgotPsswrd.Controls.Add(label2);
            pnlForgotPsswrd.Location = new Point(95, 83);
            pnlForgotPsswrd.Name = "pnlForgotPsswrd";
            pnlForgotPsswrd.Size = new Size(380, 235);
            pnlForgotPsswrd.TabIndex = 0;
            // 
            // btnCont
            // 
            btnCont.BackgroundImage = (Image)resources.GetObject("btnCont.BackgroundImage");
            btnCont.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCont.ForeColor = SystemColors.ButtonHighlight;
            btnCont.Location = new Point(40, 165);
            btnCont.Name = "btnCont";
            btnCont.Size = new Size(297, 29);
            btnCont.TabIndex = 12;
            btnCont.Text = "Đặt lại mật khẩu";
            btnCont.UseVisualStyleBackColor = true;
            btnCont.Click += btnCont_Click;
            // 
            // labelTextBoxControl1
            // 
            labelTextBoxControl1.IsPassword = false;
            labelTextBoxControl1.LabelText = "Vui lòng nhập địa chỉ email";
            labelTextBoxControl1.Location = new Point(39, 85);
            labelTextBoxControl1.Margin = new Padding(5, 5, 5, 5);
            labelTextBoxControl1.Name = "labelTextBoxControl1";
            labelTextBoxControl1.PasswordChar = '\0';
            labelTextBoxControl1.Size = new Size(297, 72);
            labelTextBoxControl1.TabIndex = 10;
            labelTextBoxControl1.TextBoxReadOnly = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(192, 0, 0);
            label2.Location = new Point(90, 31);
            label2.Name = "label2";
            label2.Size = new Size(198, 37);
            label2.TabIndex = 9;
            label2.Text = "Quên mật khẩu";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(566, 406);
            Controls.Add(pnlForgotPsswrd);
            Name = "ForgotPasswordForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên mật khẩu";
            pnlForgotPsswrd.ResumeLayout(false);
            pnlForgotPsswrd.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlForgotPsswrd;
        private Label label2;
        private User_Controls.LabelTextBoxControl labelTextBoxControl1;
        private Button btnCont;
    }
}