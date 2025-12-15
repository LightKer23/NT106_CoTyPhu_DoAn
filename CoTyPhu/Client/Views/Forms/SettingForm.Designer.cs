namespace Client.Views.Forms
{
    partial class SettingForm
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
            btnLogOut = new Button();
            btnEditInfo = new Button();
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            btnHistory = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogOut
            // 
            btnLogOut.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogOut.Location = new Point(34, 146);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(200, 50);
            btnLogOut.TabIndex = 0;
            btnLogOut.Text = "Đăng xuất";
            btnLogOut.UseVisualStyleBackColor = true;
            // 
            // btnEditInfo
            // 
            btnEditInfo.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEditInfo.Location = new Point(34, 34);
            btnEditInfo.Name = "btnEditInfo";
            btnEditInfo.Size = new Size(200, 50);
            btnEditInfo.TabIndex = 1;
            btnEditInfo.Text = "Chỉnh sửa thông tin";
            btnEditInfo.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(258, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(459, 382);
            panel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(25, 49);
            label2.Name = "label2";
            label2.Size = new Size(46, 18);
            label2.TabIndex = 1;
            label2.Text = "Email:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 16);
            label1.Name = "label1";
            label1.Size = new Size(114, 18);
            label1.TabIndex = 0;
            label1.Text = "Tên đăng nhập:";
            // 
            // btnHistory
            // 
            btnHistory.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHistory.Location = new Point(34, 90);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(200, 50);
            btnHistory.TabIndex = 3;
            btnHistory.Text = "Lịch sử chơi";
            btnHistory.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 450);
            Controls.Add(btnHistory);
            Controls.Add(panel1);
            Controls.Add(btnEditInfo);
            Controls.Add(btnLogOut);
            Name = "SettingForm";
            ShowIcon = false;
            Text = "Cài đặt";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLogOut;
        private Button btnEditInfo;
        private Panel panel1;
        private Label label2;
        private Label label1;
        private Button btnHistory;
    }
}