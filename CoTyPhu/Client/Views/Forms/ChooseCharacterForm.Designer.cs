namespace Client.Views.Forms
{
    partial class ChooseCharacterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseCharacterForm));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            label2 = new Label();
            btnChar3 = new Button();
            btnChar4 = new Button();
            btnChar2 = new Button();
            btnChar1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(275, 34);
            label1.Name = "label1";
            label1.Size = new Size(0, 28);
            label1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(96, -93);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(450, 350);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.MistyRose;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(496, 351);
            button1.Name = "button1";
            button1.Size = new Size(120, 40);
            button1.TabIndex = 13;
            button1.Text = "Vào phòng";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(192, 0, 0);
            label2.Location = new Point(230, 135);
            label2.Name = "label2";
            label2.Size = new Size(182, 28);
            label2.TabIndex = 12;
            label2.Text = "CHỌN NHÂN VẬT";
            // 
            // btnChar3
            // 
            btnChar3.Image = Properties.Resources.icon_3;
            btnChar3.Location = new Point(486, 193);
            btnChar3.Name = "btnChar3";
            btnChar3.Size = new Size(130, 140);
            btnChar3.TabIndex = 11;
            btnChar3.UseVisualStyleBackColor = true;
            // 
            // btnChar4
            // 
            btnChar4.Image = Properties.Resources.icon_4;
            btnChar4.Location = new Point(178, 195);
            btnChar4.Name = "btnChar4";
            btnChar4.Size = new Size(130, 140);
            btnChar4.TabIndex = 10;
            btnChar4.UseVisualStyleBackColor = true;
            // 
            // btnChar2
            // 
            btnChar2.Image = Properties.Resources.icon_2;
            btnChar2.Location = new Point(332, 193);
            btnChar2.Name = "btnChar2";
            btnChar2.Size = new Size(130, 140);
            btnChar2.TabIndex = 9;
            btnChar2.UseVisualStyleBackColor = true;
            // 
            // btnChar1
            // 
            btnChar1.Image = Properties.Resources.icon_1;
            btnChar1.Location = new Point(24, 195);
            btnChar1.Name = "btnChar1";
            btnChar1.Size = new Size(130, 140);
            btnChar1.TabIndex = 8;
            btnChar1.UseVisualStyleBackColor = true;
            // 
            // ChooseCharacterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(642, 413);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(btnChar3);
            Controls.Add(btnChar4);
            Controls.Add(btnChar2);
            Controls.Add(btnChar1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "ChooseCharacterForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chọn nhân vật";
            Load += ChooseCharacterForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Button button1;
        private Label label2;
        private Button btnChar3;
        private Button btnChar4;
        private Button btnChar2;
        private Button btnChar1;
    }
}