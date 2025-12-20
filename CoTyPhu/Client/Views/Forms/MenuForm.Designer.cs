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
            pictureBox1 = new PictureBox();
            btnExit = new Button();
            btnSetting = new Button();
            btnGuide = new Button();
            btnPlayWithPlayer = new Button();
            btnPlayWithCmp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(47, -62);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(320, 261);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.MistyRose;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(132, 358);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 44);
            btnExit.TabIndex = 11;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            // 
            // btnSetting
            // 
            btnSetting.BackColor = Color.MistyRose;
            btnSetting.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSetting.Location = new Point(132, 307);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(150, 44);
            btnSetting.TabIndex = 10;
            btnSetting.Text = "Cài đặt";
            btnSetting.UseVisualStyleBackColor = false;
            // 
            // btnGuide
            // 
            btnGuide.BackColor = Color.MistyRose;
            btnGuide.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuide.Location = new Point(132, 257);
            btnGuide.Name = "btnGuide";
            btnGuide.Size = new Size(150, 44);
            btnGuide.TabIndex = 9;
            btnGuide.Text = "Hướng dẫn chơi";
            btnGuide.UseVisualStyleBackColor = false;
            // 
            // btnPlayWithPlayer
            // 
            btnPlayWithPlayer.BackColor = Color.MistyRose;
            btnPlayWithPlayer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithPlayer.Location = new Point(132, 207);
            btnPlayWithPlayer.Name = "btnPlayWithPlayer";
            btnPlayWithPlayer.Size = new Size(150, 44);
            btnPlayWithPlayer.TabIndex = 8;
            btnPlayWithPlayer.Text = "Chơi với người";
            btnPlayWithPlayer.UseVisualStyleBackColor = false;
            // 
            // btnPlayWithCmp
            // 
            btnPlayWithCmp.BackColor = Color.MistyRose;
            btnPlayWithCmp.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithCmp.Location = new Point(132, 157);
            btnPlayWithCmp.Name = "btnPlayWithCmp";
            btnPlayWithCmp.Size = new Size(150, 44);
            btnPlayWithCmp.TabIndex = 7;
            btnPlayWithCmp.Text = "Chơi với máy";
            btnPlayWithCmp.UseVisualStyleBackColor = false;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(415, 447);
            Controls.Add(btnExit);
            Controls.Add(btnSetting);
            Controls.Add(btnGuide);
            Controls.Add(btnPlayWithPlayer);
            Controls.Add(btnPlayWithCmp);
            Controls.Add(pictureBox1);
            Name = "MenuForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cờ tỷ phú";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Button btnExit;
        private Button btnSetting;
        private Button btnGuide;
        private Button btnPlayWithPlayer;
        private Button btnPlayWithCmp;
    }
}