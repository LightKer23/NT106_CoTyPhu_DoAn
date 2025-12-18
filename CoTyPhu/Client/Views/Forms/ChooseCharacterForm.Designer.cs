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
            btnChar1 = new Button();
            btnChar2 = new Button();
            btnChar4 = new Button();
            btnChar3 = new Button();
            label2 = new Label();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(447, 54);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 45);
            label1.TabIndex = 0;
            // 
            // btnChar1
            // 
            btnChar1.Image = Properties.Resources.icon_1;
            btnChar1.Location = new Point(76, 287);
            btnChar1.Margin = new Padding(5, 5, 5, 5);
            btnChar1.Name = "btnChar1";
            btnChar1.Size = new Size(211, 221);
            btnChar1.TabIndex = 1;
            btnChar1.UseVisualStyleBackColor = true;
            // 
            // btnChar2
            // 
            btnChar2.Image = Properties.Resources.icon_2;
            btnChar2.Location = new Point(548, 287);
            btnChar2.Margin = new Padding(5, 5, 5, 5);
            btnChar2.Name = "btnChar2";
            btnChar2.Size = new Size(211, 221);
            btnChar2.TabIndex = 2;
            btnChar2.UseVisualStyleBackColor = true;
            // 
            // btnChar4
            // 
            btnChar4.Image = Properties.Resources.icon_4;
            btnChar4.Location = new Point(315, 287);
            btnChar4.Margin = new Padding(5, 5, 5, 5);
            btnChar4.Name = "btnChar4";
            btnChar4.Size = new Size(211, 221);
            btnChar4.TabIndex = 3;
            btnChar4.UseVisualStyleBackColor = true;
            // 
            // btnChar3
            // 
            btnChar3.Image = Properties.Resources.icon_3;
            btnChar3.Location = new Point(785, 287);
            btnChar3.Margin = new Padding(5, 5, 5, 5);
            btnChar3.Name = "btnChar3";
            btnChar3.Size = new Size(211, 221);
            btnChar3.TabIndex = 4;
            btnChar3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(192, 0, 0);
            label2.Location = new Point(404, 193);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(293, 45);
            label2.TabIndex = 5;
            label2.Text = "CHỌN NHÂN VẬT";
            // 
            // button1
            // 
            button1.BackColor = Color.MistyRose;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(801, 556);
            button1.Margin = new Padding(5, 5, 5, 5);
            button1.Name = "button1";
            button1.Size = new Size(195, 64);
            button1.TabIndex = 6;
            button1.Text = "Vào phòng";
            button1.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(346, 54);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(384, 100);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // ChooseCharacterForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1076, 707);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(btnChar3);
            Controls.Add(btnChar4);
            Controls.Add(btnChar2);
            Controls.Add(btnChar1);
            Controls.Add(label1);
            Margin = new Padding(5, 5, 5, 5);
            Name = "ChooseCharacterForm";
            ShowIcon = false;
            Text = "Chọn nhân vật";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnChar1;
        private Button btnChar2;
        private Button btnChar4;
        private Button btnChar3;
        private Label label2;
        private Button button1;
        private PictureBox pictureBox1;
    }
}