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
            btnPlayWithCmp.Location = new Point(208, 259);
            btnPlayWithCmp.Margin = new Padding(5);
            btnPlayWithCmp.Name = "btnPlayWithCmp";
            btnPlayWithCmp.Size = new Size(244, 70);
            btnPlayWithCmp.TabIndex = 1;
            btnPlayWithCmp.Text = "Chơi với máy";
            btnPlayWithCmp.UseVisualStyleBackColor = false;
            // 
            // btnPlayWithPlayer
            // 
            btnPlayWithPlayer.BackColor = Color.MistyRose;
            btnPlayWithPlayer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayWithPlayer.Location = new Point(208, 339);
            btnPlayWithPlayer.Margin = new Padding(5);
            btnPlayWithPlayer.Name = "btnPlayWithPlayer";
            btnPlayWithPlayer.Size = new Size(244, 70);
            btnPlayWithPlayer.TabIndex = 2;
            btnPlayWithPlayer.Text = "Chơi với người";
            btnPlayWithPlayer.UseVisualStyleBackColor = false;
            // 
            // btnGuide
            // 
            btnGuide.BackColor = Color.MistyRose;
            btnGuide.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuide.Location = new Point(208, 419);
            btnGuide.Margin = new Padding(5);
            btnGuide.Name = "btnGuide";
            btnGuide.Size = new Size(244, 71);
            btnGuide.TabIndex = 3;
            btnGuide.Text = "Hướng dẫn chơi";
            btnGuide.UseVisualStyleBackColor = false;
            // 
            // btnSetting
            // 
            btnSetting.BackColor = Color.MistyRose;
            btnSetting.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSetting.Location = new Point(208, 500);
            btnSetting.Margin = new Padding(5);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(244, 71);
            btnSetting.TabIndex = 4;
            btnSetting.Text = "Cài đặt";
            btnSetting.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.MistyRose;
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(208, 581);
            btnExit.Margin = new Padding(5);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(244, 70);
            btnExit.TabIndex = 5;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(117, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(428, 101);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(674, 784);
            Controls.Add(pictureBox1);
            Controls.Add(btnExit);
            Controls.Add(btnSetting);
            Controls.Add(btnGuide);
            Controls.Add(btnPlayWithPlayer);
            Controls.Add(btnPlayWithCmp);
            Margin = new Padding(5);
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