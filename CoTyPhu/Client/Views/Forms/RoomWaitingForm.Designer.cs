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
            lblRoomId = new Label();
            lstPlayers = new ListBox();
            btnChooseCharacter = new Button();
            btnStart = new Button();
            btnLeave = new Button();
            SuspendLayout();
            // 
            // lblRoomId
            // 
            lblRoomId.AutoSize = true;
            lblRoomId.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRoomId.Location = new Point(20, 20);
            lblRoomId.Name = "lblRoomId";
            lblRoomId.Size = new Size(38, 28);
            lblRoomId.TabIndex = 0;
            lblRoomId.Text = "ID:";
            // 
            // lstPlayers
            // 
            lstPlayers.Location = new Point(25, 65);
            lstPlayers.Name = "lstPlayers";
            lstPlayers.Size = new Size(470, 164);
            lstPlayers.TabIndex = 2;
            // 
            // btnChooseCharacter
            // 
            btnChooseCharacter.Location = new Point(25, 260);
            btnChooseCharacter.Name = "btnChooseCharacter";
            btnChooseCharacter.Size = new Size(150, 40);
            btnChooseCharacter.TabIndex = 3;
            btnChooseCharacter.Text = "Chọn nhân vật";
            btnChooseCharacter.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(192, 260);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(150, 40);
            btnStart.TabIndex = 4;
            btnStart.Text = "Bắt đầu";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // btnLeave
            // 
            btnLeave.Location = new Point(355, 260);
            btnLeave.Name = "btnLeave";
            btnLeave.Size = new Size(140, 40);
            btnLeave.TabIndex = 5;
            btnLeave.Text = "Rời phòng";
            btnLeave.UseVisualStyleBackColor = true;
            // 
            // RoomWaitingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 330);
            Controls.Add(lblRoomId);
            Controls.Add(lstPlayers);
            Controls.Add(btnChooseCharacter);
            Controls.Add(btnStart);
            Controls.Add(btnLeave);
            Name = "RoomWaitingForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phòng chờ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblRoomId;
        private ListBox lstPlayers;
        private Button btnChooseCharacter;
        private Button btnStart;
        private Button btnLeave;
    }
}
