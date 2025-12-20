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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            btnPlayWithCmp = new Button();
            btnPlayWithPlayer = new Button();
            btnGuide = new Button();
            btnSetting = new Button();
            btnExit = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnPlayWithCmp
            // 
            btnPlayWithCmp.BackColor = Color.MistyRose;
            btnPlayWithCmp.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithCmp.Location = new Point(128, 162);
            btnPlayWithCmp.Name = "btnPlayWithCmp";
            btnPlayWithCmp.Size = new Size(150, 44);
            btnPlayWithCmp.TabIndex = 1;
            btnPlayWithCmp.Text = "Chơi với máy";
            btnPlayWithCmp.UseVisualStyleBackColor = false;
            // 
            // btnPlayWithPlayer
            // 
            btnPlayWithPlayer.BackColor = Color.MistyRose;
            btnPlayWithPlayer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithPlayer.Location = new Point(128, 212);
            btnPlayWithPlayer.Name = "btnPlayWithPlayer";
            btnPlayWithPlayer.Size = new Size(150, 44);
            btnPlayWithPlayer.TabIndex = 2;
            btnPlayWithPlayer.Text = "Chơi với người";
            btnPlayWithPlayer.UseVisualStyleBackColor = false;
            // 
            // btnGuide
            // 
            btnGuide.BackColor = Color.MistyRose;
            btnGuide.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuide.Location = new Point(128, 262);
            btnGuide.Name = "btnGuide";
            btnGuide.Size = new Size(150, 44);
            btnGuide.TabIndex = 3;
            btnGuide.Text = "Hướng dẫn chơi";
            btnGuide.UseVisualStyleBackColor = false;
            // 
            // btnSetting
            // 
            btnSetting.BackColor = Color.MistyRose;
            btnSetting.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSetting.Location = new Point(128, 312);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(150, 44);
            btnSetting.TabIndex = 4;
            btnSetting.Text = "Cài đặt";
            btnSetting.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.MistyRose;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(128, 363);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 44);
            btnExit.TabIndex = 5;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(0, 10);
            pictureBox1.Margin = new Padding(2, 2, 2, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(410, 103);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(415, 490);
            Controls.Add(pictureBox1);
            Controls.Add(btnExit);
            Controls.Add(btnSetting);
            Controls.Add(btnGuide);
            Controls.Add(btnPlayWithPlayer);
            Controls.Add(btnPlayWithCmp);
            Name = "MenuForm";
            ShowIcon = false;
            Text = "Cờ tỷ phú";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnPlayWithCmp;
        private Button btnPlayWithPlayer;
        private Button btnGuide;
        private Button btnSetting;
        private Button btnExit;
        private PictureBox pictureBox1;
    }
}