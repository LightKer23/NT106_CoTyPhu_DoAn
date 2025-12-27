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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomHubForm));
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
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(192, 0, 0);
            lblTitle.Location = new Point(154, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(213, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CHƠI VỚI NGƯỜI";
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.Azure;
            btnCreateRoom.Location = new Point(150, 80);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(220, 33);
            btnCreateRoom.TabIndex = 1;
            btnCreateRoom.Text = "Tạo phòng";
            btnCreateRoom.UseVisualStyleBackColor = true;
            btnCreateRoom.Click += btnCreateRoom_Click;
            // 
            // lblOr
            // 
            lblOr.AutoSize = true;
            lblOr.BackColor = Color.Transparent;
            lblOr.ForeColor = Color.Black;
            lblOr.Location = new Point(221, 116);
            lblOr.Name = "lblOr";
            lblOr.Size = new Size(79, 20);
            lblOr.TabIndex = 2;
            lblOr.Text = "— hoặc —";
            // 
            // txtRoomId
            // 
            txtRoomId.Location = new Point(150, 139);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.PlaceholderText = "Nhập Room ID";
            txtRoomId.Size = new Size(220, 27);
            txtRoomId.TabIndex = 3;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.Azure;
            btnJoinRoom.Location = new Point(148, 176);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(220, 32);
            btnJoinRoom.TabIndex = 4;
            btnJoinRoom.Text = "Vào phòng";
            btnJoinRoom.UseVisualStyleBackColor = true;
            btnJoinRoom.Click += btnJoinRoom_Click;
            // 
            // RoomHubForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(481, 270);
            Controls.Add(lblTitle);
            Controls.Add(btnCreateRoom);
            Controls.Add(lblOr);
            Controls.Add(txtRoomId);
            Controls.Add(btnJoinRoom);
            Name = "RoomHubForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tạo / Vào phòng";
            FormClosing += RoomHubForm_FormClosing;
            Load += RoomHubForm_Load;
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
