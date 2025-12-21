namespace Client.Views.Forms
{
    partial class RoomHubForm
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
            lblTitle = new Label();
            btnCreateRoom = new Button();
            lblOr = new Label();
            txtRoomId = new TextBox();
            btnJoinRoom = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(154, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(213, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CHƠI VỚI NGƯỜI";
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.Location = new Point(150, 80);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(220, 40);
            btnCreateRoom.TabIndex = 1;
            btnCreateRoom.Text = "Tạo phòng";
            btnCreateRoom.UseVisualStyleBackColor = true;
            // 
            // lblOr
            // 
            lblOr.AutoSize = true;
            lblOr.Location = new Point(221, 135);
            lblOr.Name = "lblOr";
            lblOr.Size = new Size(79, 20);
            lblOr.TabIndex = 2;
            lblOr.Text = "— hoặc —";
            // 
            // txtRoomId
            // 
            txtRoomId.Location = new Point(150, 160);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.PlaceholderText = "Nhập Room ID";
            txtRoomId.Size = new Size(220, 27);
            txtRoomId.TabIndex = 3;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.Location = new Point(150, 200);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(220, 40);
            btnJoinRoom.TabIndex = 4;
            btnJoinRoom.Text = "Vào phòng";
            btnJoinRoom.UseVisualStyleBackColor = true;
            // 
            // RoomHubForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(520, 270);
            Controls.Add(lblTitle);
            Controls.Add(btnCreateRoom);
            Controls.Add(lblOr);
            Controls.Add(txtRoomId);
            Controls.Add(btnJoinRoom);
            Name = "RoomHubForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tạo / Vào phòng";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnCreateRoom;
        private Label lblOr;
        private TextBox txtRoomId;
        private Button btnJoinRoom;
    }
}
