namespace Client.Views.Forms
{
    partial class MenuForm
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
            label1 = new Label();
            btnPlayWithCmp = new Button();
            btnPlayWithPlayer = new Button();
            btnGuide = new Button();
            btnSetting = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(302, 42);
            label1.Name = "label1";
            label1.Size = new Size(157, 30);
            label1.TabIndex = 0;
            label1.Text = "NOEL - POLY";
            // 
            // btnPlayWithCmp
            // 
            btnPlayWithCmp.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithCmp.Location = new Point(315, 155);
            btnPlayWithCmp.Name = "btnPlayWithCmp";
            btnPlayWithCmp.Size = new Size(150, 48);
            btnPlayWithCmp.TabIndex = 1;
            btnPlayWithCmp.Text = "Chơi với máy";
            btnPlayWithCmp.UseVisualStyleBackColor = true;
            btnPlayWithCmp.Click += btnPlayWithCmp_Click;
            // 
            // btnPlayWithPlayer
            // 
            btnPlayWithPlayer.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithPlayer.Location = new Point(315, 102);
            btnPlayWithPlayer.Name = "btnPlayWithPlayer";
            btnPlayWithPlayer.Size = new Size(150, 48);
            btnPlayWithPlayer.TabIndex = 2;
            btnPlayWithPlayer.Text = "Chơi với người";
            btnPlayWithPlayer.UseVisualStyleBackColor = true;
            btnPlayWithPlayer.Click += btnPlayWithPlayer_Click;
            // 
            // btnGuide
            // 
            btnGuide.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuide.Location = new Point(315, 208);
            btnGuide.Name = "btnGuide";
            btnGuide.Size = new Size(150, 48);
            btnGuide.TabIndex = 3;
            btnGuide.Text = "Hướng dẫn chơi";
            btnGuide.UseVisualStyleBackColor = true;
            btnGuide.Click += btnGuide_Click;
            // 
            // btnSetting
            // 
            btnSetting.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSetting.Location = new Point(315, 261);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(150, 48);
            btnSetting.TabIndex = 4;
            btnSetting.Text = "Cài đặt";
            btnSetting.UseVisualStyleBackColor = true;
            btnSetting.Click += btnSetting_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(315, 314);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 48);
            btnExit.TabIndex = 5;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 383);
            Controls.Add(btnExit);
            Controls.Add(btnSetting);
            Controls.Add(btnGuide);
            Controls.Add(btnPlayWithPlayer);
            Controls.Add(btnPlayWithCmp);
            Controls.Add(label1);
            Name = "MenuForm";
            ShowIcon = false;
            Text = "Cờ tỷ phú";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnPlayWithCmp;
        private Button btnPlayWithPlayer;
        private Button btnGuide;
        private Button btnSetting;
        private Button btnExit;
    }
}