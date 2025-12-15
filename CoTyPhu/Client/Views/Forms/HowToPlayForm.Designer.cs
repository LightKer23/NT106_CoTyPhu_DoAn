namespace Client.Views.Forms
{
    partial class HowToPlayForm
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
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(132, 40);
            label1.Name = "label1";
            label1.Size = new Size(234, 32);
            label1.TabIndex = 0;
            label1.Text = "HƯỚNG DẪN CHƠI";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(32, 104);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(446, 334);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // HowToPlayForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 470);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Name = "HowToPlayForm";
            ShowIcon = false;
            Text = "Hướng dẫn chơi";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RichTextBox richTextBox1;
    }
}