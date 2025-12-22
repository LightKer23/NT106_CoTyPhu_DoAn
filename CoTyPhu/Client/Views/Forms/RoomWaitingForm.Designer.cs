namespace Client.Views.Forms
{
    partial class RoomWaitingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomWaitingForm));
            lblRoomId = new Label();
            lstPlayers = new ListBox();
            btnStart = new Button();
            btnLeave = new Button();
            SuspendLayout();
            // 
            // lblRoomId
            // 
            lblRoomId.AutoSize = true;
            lblRoomId.BackColor = Color.Transparent;
            lblRoomId.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRoomId.ForeColor = Color.Red;
            lblRoomId.Location = new Point(32, 32);
            lblRoomId.Margin = new Padding(5, 0, 5, 0);
            lblRoomId.Name = "lblRoomId";
            lblRoomId.Size = new Size(63, 45);
            lblRoomId.TabIndex = 0;
            lblRoomId.Text = "ID:";
            // 
            // lstPlayers
            // 
            lstPlayers.Location = new Point(41, 104);
            lstPlayers.Margin = new Padding(5);
            lstPlayers.Name = "lstPlayers";
            lstPlayers.Size = new Size(761, 260);
            lstPlayers.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.MistyRose;
            btnStart.Location = new Point(41, 416);
            btnStart.Margin = new Padding(5);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(244, 64);
            btnStart.TabIndex = 4;
            btnStart.Text = "Bắt đầu";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnLeave
            // 
            btnLeave.BackColor = Color.White;
            btnLeave.Location = new Point(577, 416);
            btnLeave.Margin = new Padding(5);
            btnLeave.Name = "btnLeave";
            btnLeave.Size = new Size(228, 64);
            btnLeave.TabIndex = 5;
            btnLeave.Text = "Rời phòng";
            btnLeave.UseVisualStyleBackColor = true;
            btnLeave.Click += btnLeave_Click;
            // 
            // RoomWaitingForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(845, 528);
            Controls.Add(lblRoomId);
            Controls.Add(lstPlayers);
            Controls.Add(btnStart);
            Controls.Add(btnLeave);
            Margin = new Padding(5);
            Name = "RoomWaitingForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phòng chờ";
            Load += RoomWaitingForm_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblRoomId;
        private ListBox lstPlayers;
        private Button btnStart;
        private Button btnLeave;
    }
}
