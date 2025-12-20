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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToPlayForm));
            rtbRule = new RichTextBox();
            label1 = new Label();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // rtbRule
            // 
            rtbRule.Location = new Point(84, 88);
            rtbRule.Margin = new Padding(2, 2, 2, 2);
            rtbRule.Name = "rtbRule";
            rtbRule.Size = new Size(316, 342);
            rtbRule.TabIndex = 0;
            rtbRule.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(118, 32);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(254, 28);
            label1.TabIndex = 1;
            label1.Text = "HƯỚNG DẪN CÁCH CHƠI";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Cornsilk;
            btnCancel.Location = new Point(360, 464);
            btnCancel.Margin = new Padding(2, 2, 2, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(111, 38);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Hủy bỏ";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // HowToPlayForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(482, 513);
            Controls.Add(btnCancel);
            Controls.Add(label1);
            Controls.Add(rtbRule);
            Name = "HowToPlayForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hướng dẫn chơi";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbRule;
        private Label label1;
        private Button btnCancel;
    }
}