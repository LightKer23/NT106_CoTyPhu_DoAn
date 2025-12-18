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
            btnReturn = new Button();
            SuspendLayout();
            // 
            // rtbRule
            // 
            rtbRule.Location = new Point(137, 141);
            rtbRule.Name = "rtbRule";
            rtbRule.Size = new Size(511, 544);
            rtbRule.TabIndex = 0;
            rtbRule.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(192, 0, 0);
            label1.Location = new Point(191, 51);
            label1.Name = "label1";
            label1.Size = new Size(410, 45);
            label1.TabIndex = 1;
            label1.Text = "HƯỚNG DẪN CÁCH CHƠI";
            // 
            // btnReturn
            // 
            btnReturn.BackColor = Color.Cornsilk;
            btnReturn.Location = new Point(29, 730);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(180, 60);
            btnReturn.TabIndex = 2;
            btnReturn.Text = "Quay lại";
            btnReturn.UseVisualStyleBackColor = false;
            // 
            // HowToPlayForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(789, 827);
            Controls.Add(btnReturn);
            Controls.Add(label1);
            Controls.Add(rtbRule);
            Margin = new Padding(5, 5, 5, 5);
            Name = "HowToPlayForm";
            ShowIcon = false;
            Text = "Hướng dẫn chơi";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbRule;
        private Label label1;
        private Button btnReturn;
    }
}