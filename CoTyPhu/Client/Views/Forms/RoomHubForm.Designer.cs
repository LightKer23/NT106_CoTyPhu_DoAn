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
            lblTitle.Location = new Point(250, 32);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(333, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CHƠI VỚI NGƯỜI";
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.Azure;
            btnCreateRoom.Location = new Point(244, 128);
            btnCreateRoom.Margin = new Padding(5);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(358, 53);
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
            lblOr.Location = new Point(359, 186);
            lblOr.Margin = new Padding(5, 0, 5, 0);
            lblOr.Name = "lblOr";
            lblOr.Size = new Size(127, 32);
            lblOr.TabIndex = 2;
            lblOr.Text = "— hoặc —";
            // 
            // txtRoomId
            // 
            txtRoomId.Location = new Point(244, 223);
            txtRoomId.Margin = new Padding(5);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.PlaceholderText = "Nhập Room ID";
            txtRoomId.Size = new Size(355, 39);
            txtRoomId.TabIndex = 3;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.Azure;
            btnJoinRoom.Location = new Point(241, 281);
            btnJoinRoom.Margin = new Padding(5);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(358, 52);
            btnJoinRoom.TabIndex = 4;
            btnJoinRoom.Text = "Vào phòng";
            btnJoinRoom.UseVisualStyleBackColor = true;
            btnJoinRoom.Click += btnJoinRoom_Click;
            // 
            // RoomHubForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(781, 432);
            Controls.Add(lblTitle);
            Controls.Add(btnCreateRoom);
            Controls.Add(lblOr);
            Controls.Add(txtRoomId);
            Controls.Add(btnJoinRoom);
            Margin = new Padding(5);
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
