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
            pnlForgotPsswrd = new Panel();
            btnCont = new Button();
            labelTextBoxControl1 = new Client.Views.User_Controls.LabelTextBoxControl();
            label2 = new Label();
            pnlForgotPsswrd.SuspendLayout();
            SuspendLayout();
            // 
            // pnlForgotPsswrd
            // 
            pnlForgotPsswrd.BorderStyle = BorderStyle.FixedSingle;
            pnlForgotPsswrd.Controls.Add(btnCont);
            pnlForgotPsswrd.Controls.Add(labelTextBoxControl1);
            pnlForgotPsswrd.Controls.Add(label2);
            pnlForgotPsswrd.Location = new Point(100, 200);
            pnlForgotPsswrd.Name = "pnlForgotPsswrd";
            pnlForgotPsswrd.Size = new Size(380, 250);
            pnlForgotPsswrd.TabIndex = 0;
            // 
            // btnCont
            // 
            btnCont.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCont.Location = new Point(40, 165);
            btnCont.Name = "btnCont";
            btnCont.Size = new Size(300, 40);
            btnCont.TabIndex = 12;
            btnCont.Text = "Đặt lại mật khẩu";
            btnCont.UseVisualStyleBackColor = true;
            // 
            // labelTextBoxControl1
            // 
            labelTextBoxControl1.LabelText = "Vui lòng nhập địa chỉ email";
            labelTextBoxControl1.Location = new Point(40, 100);
            labelTextBoxControl1.Name = "labelTextBoxControl1";
            labelTextBoxControl1.PasswordChar = '\0';
            labelTextBoxControl1.Size = new Size(297, 72);
            labelTextBoxControl1.TabIndex = 10;
            labelTextBoxControl1.TextBoxReadOnly = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(40, 30);
            label2.Name = "label2";
            label2.Size = new Size(209, 34);
            label2.TabIndex = 9;
            label2.Text = "Quên mật khẩu";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 653);
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