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
            label1 = new Label();
            btnChar1 = new Button();
            btnChar2 = new Button();
            btnChar4 = new Button();
            btnChar3 = new Button();
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(275, 34);
            label1.Name = "label1";
            label1.Size = new Size(114, 28);
            label1.TabIndex = 0;
            label1.Text = "NOEL-POLY";
            // 
            // btnChar1
            // 
            btnChar1.Image = Properties.Resources.icon_1;
            btnChar1.Location = new Point(48, 160);
            btnChar1.Name = "btnChar1";
            btnChar1.Size = new Size(130, 138);
            btnChar1.TabIndex = 1;
            btnChar1.UseVisualStyleBackColor = true;
            // 
            // btnChar2
            // 
            btnChar2.Image = Properties.Resources.icon_2;
            btnChar2.Location = new Point(338, 160);
            btnChar2.Name = "btnChar2";
            btnChar2.Size = new Size(130, 138);
            btnChar2.TabIndex = 2;
            btnChar2.UseVisualStyleBackColor = true;
            // 
            // btnChar4
            // 
            btnChar4.Image = Properties.Resources.icon_4;
            btnChar4.Location = new Point(193, 160);
            btnChar4.Name = "btnChar4";
            btnChar4.Size = new Size(130, 138);
            btnChar4.TabIndex = 3;
            btnChar4.UseVisualStyleBackColor = true;
            // 
            // btnChar3
            // 
            btnChar3.Image = Properties.Resources.icon_3;
            btnChar3.Location = new Point(483, 160);
            btnChar3.Name = "btnChar3";
            btnChar3.Size = new Size(130, 138);
            btnChar3.TabIndex = 4;
            btnChar3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(279, 94);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 5;
            label2.Text = "Chọn nhân vật:";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(493, 321);
            button1.Name = "button1";
            button1.Size = new Size(120, 40);
            button1.TabIndex = 6;
            button1.Text = "Vào phòng";
            button1.UseVisualStyleBackColor = true;
            // 
            // ChooseCharacterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 393);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(btnChar3);
            Controls.Add(btnChar4);
            Controls.Add(btnChar2);
            Controls.Add(btnChar1);
            Controls.Add(label1);
            Name = "ChooseCharacterForm";
            ShowIcon = false;
            Text = "Chọn nhân vật";
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
    }
}