namespace Client.Views.Forms
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // ====== Controls ======
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnEditInfo;
        private System.Windows.Forms.Button btnLogout;

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabHistory;

        private System.Windows.Forms.Label lblTitleInfo;
        private System.Windows.Forms.GroupBox grpAccount;
        private System.Windows.Forms.TableLayoutPanel tblInfo;

        private System.Windows.Forms.Label lblUsernameTitle;
        private System.Windows.Forms.Label lblFullNameTitle;
        private System.Windows.Forms.Label lblEmailTitle;

        private System.Windows.Forms.Label lblUsernameValue;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblEmailValue;

        private System.Windows.Forms.FlowLayoutPanel flpInfoActions;
        private System.Windows.Forms.Button btnSaveInfo;
        private System.Windows.Forms.Button btnCancelEdit;

        private System.Windows.Forms.Label lblTitleHistory;
        private System.Windows.Forms.DataGridView dgvHistory;

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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            splitMain = new SplitContainer();
            pnlMenu = new Panel();
            btnLogout = new Button();
            btnEditInfo = new Button();
            tabMain = new TabControl();
            tabInfo = new TabPage();
            flpInfoActions = new FlowLayoutPanel();
            btnSaveInfo = new Button();
            btnCancelEdit = new Button();
            grpAccount = new GroupBox();
            tblInfo = new TableLayoutPanel();
            lblUsernameTitle = new Label();
            lblUsernameValue = new Label();
            lblFullNameTitle = new Label();
            txtFullName = new TextBox();
            lblEmailTitle = new Label();
            lblEmailValue = new Label();
            lblTitleInfo = new Label();
            tabHistory = new TabPage();
            dgvHistory = new DataGridView();
            lblTitleHistory = new Label();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pnlMenu.SuspendLayout();
            tabMain.SuspendLayout();
            tabInfo.SuspendLayout();
            flpInfoActions.SuspendLayout();
            grpAccount.SuspendLayout();
            tblInfo.SuspendLayout();
            tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(0, 0);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.BackColor = Color.FromArgb(30, 30, 30);
            splitMain.Panel1.Controls.Add(pnlMenu);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.BackColor = Color.White;
            splitMain.Panel2.Controls.Add(tabMain);
            splitMain.Size = new Size(900, 520);
            splitMain.SplitterDistance = 230;
            splitMain.TabIndex = 0;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.LightCyan;
            pnlMenu.BackgroundImage = (Image)resources.GetObject("pnlMenu.BackgroundImage");
            pnlMenu.Controls.Add(btnLogout);
            pnlMenu.Controls.Add(btnEditInfo);
            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Padding = new Padding(12, 12, 12, 12);
            pnlMenu.Size = new Size(230, 520);
            pnlMenu.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(200, 55, 55);
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(12, 464);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(206, 44);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnEditInfo
            // 
            btnEditInfo.BackColor = SystemColors.ControlLightLight;
            btnEditInfo.Dock = DockStyle.Top;
            btnEditInfo.FlatAppearance.BorderSize = 0;
            btnEditInfo.FlatStyle = FlatStyle.Flat;
            btnEditInfo.Font = new Font("Segoe UI", 10F);
            btnEditInfo.ForeColor = Color.FromArgb(192, 0, 0);
            btnEditInfo.Location = new Point(12, 12);
            btnEditInfo.Margin = new Padding(0, 0, 0, 10);
            btnEditInfo.Name = "btnEditInfo";
            btnEditInfo.Size = new Size(206, 44);
            btnEditInfo.TabIndex = 2;
            btnEditInfo.Text = "Chỉnh sửa thông tin";
            btnEditInfo.UseVisualStyleBackColor = false;
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabInfo);
            tabMain.Controls.Add(tabHistory);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(666, 520);
            tabMain.TabIndex = 0;
            // 
            // tabInfo
            // 
            tabInfo.BackColor = Color.White;
            tabInfo.BackgroundImage = (Image)resources.GetObject("tabInfo.BackgroundImage");
            tabInfo.Controls.Add(flpInfoActions);
            tabInfo.Controls.Add(grpAccount);
            tabInfo.Controls.Add(lblTitleInfo);
            tabInfo.Location = new Point(4, 29);
            tabInfo.Name = "tabInfo";
            tabInfo.Padding = new Padding(18, 18, 18, 18);
            tabInfo.Size = new Size(658, 487);
            tabInfo.TabIndex = 0;
            tabInfo.Text = "Thông tin";
            // 
            // flpInfoActions
            // 
            flpInfoActions.BackColor = Color.Transparent;
            flpInfoActions.Controls.Add(btnSaveInfo);
            flpInfoActions.Controls.Add(btnCancelEdit);
            flpInfoActions.Dock = DockStyle.Top;
            flpInfoActions.FlowDirection = FlowDirection.RightToLeft;
            flpInfoActions.Location = new Point(18, 282);
            flpInfoActions.Name = "flpInfoActions";
            flpInfoActions.Padding = new Padding(0, 8, 0, 0);
            flpInfoActions.Size = new Size(622, 52);
            flpInfoActions.TabIndex = 0;
            // 
            // btnSaveInfo
            // 
            btnSaveInfo.BackColor = Color.White;
            btnSaveInfo.Enabled = false;
            btnSaveInfo.FlatStyle = FlatStyle.Flat;
            btnSaveInfo.Location = new Point(509, 11);
            btnSaveInfo.Name = "btnSaveInfo";
            btnSaveInfo.Size = new Size(110, 34);
            btnSaveInfo.TabIndex = 0;
            btnSaveInfo.Text = "Lưu";
            btnSaveInfo.UseVisualStyleBackColor = false;
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BackColor = SystemColors.ButtonHighlight;
            btnCancelEdit.Enabled = false;
            btnCancelEdit.FlatStyle = FlatStyle.Flat;
            btnCancelEdit.Location = new Point(393, 11);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Size = new Size(110, 34);
            btnCancelEdit.TabIndex = 1;
            btnCancelEdit.Text = "Hủy";
            btnCancelEdit.UseVisualStyleBackColor = false;
            // 
            // grpAccount
            // 
            grpAccount.Controls.Add(tblInfo);
            grpAccount.Dock = DockStyle.Top;
            grpAccount.Font = new Font("Segoe UI", 10F);
            grpAccount.Location = new Point(18, 62);
            grpAccount.Name = "grpAccount";
            grpAccount.Padding = new Padding(14, 14, 14, 14);
            grpAccount.Size = new Size(622, 220);
            grpAccount.TabIndex = 1;
            grpAccount.TabStop = false;
            grpAccount.Text = "Thông tin";
            // 
            // tblInfo
            // 
            tblInfo.ColumnCount = 2;
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tblInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblInfo.Controls.Add(lblUsernameTitle, 0, 0);
            tblInfo.Controls.Add(lblUsernameValue, 1, 0);
            tblInfo.Controls.Add(lblFullNameTitle, 0, 1);
            tblInfo.Controls.Add(txtFullName, 1, 1);
            tblInfo.Controls.Add(lblEmailTitle, 0, 2);
            tblInfo.Controls.Add(lblEmailValue, 1, 2);
            tblInfo.Dock = DockStyle.Fill;
            tblInfo.Location = new Point(14, 37);
            tblInfo.Name = "tblInfo";
            tblInfo.RowCount = 3;
            tblInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblInfo.Size = new Size(594, 169);
            tblInfo.TabIndex = 0;
            // 
            // lblUsernameTitle
            // 
            lblUsernameTitle.Dock = DockStyle.Fill;
            lblUsernameTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsernameTitle.Location = new Point(3, 0);
            lblUsernameTitle.Name = "lblUsernameTitle";
            lblUsernameTitle.Size = new Size(134, 55);
            lblUsernameTitle.TabIndex = 0;
            lblUsernameTitle.Text = "Username:";
            lblUsernameTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsernameValue
            // 
            lblUsernameValue.Dock = DockStyle.Fill;
            lblUsernameValue.Font = new Font("Segoe UI", 10F);
            lblUsernameValue.Location = new Point(143, 0);
            lblUsernameValue.Name = "lblUsernameValue";
            lblUsernameValue.Size = new Size(448, 55);
            lblUsernameValue.TabIndex = 1;
            lblUsernameValue.Text = "(username)";
            lblUsernameValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFullNameTitle
            // 
            lblFullNameTitle.Dock = DockStyle.Fill;
            lblFullNameTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFullNameTitle.Location = new Point(3, 55);
            lblFullNameTitle.Name = "lblFullNameTitle";
            lblFullNameTitle.Size = new Size(134, 55);
            lblFullNameTitle.TabIndex = 2;
            lblFullNameTitle.Text = "Họ tên:";
            lblFullNameTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtFullName
            // 
            txtFullName.Dock = DockStyle.Fill;
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(143, 58);
            txtFullName.Name = "txtFullName";
            txtFullName.ReadOnly = true;
            txtFullName.Size = new Size(448, 30);
            txtFullName.TabIndex = 3;
            // 
            // lblEmailTitle
            // 
            lblEmailTitle.Dock = DockStyle.Fill;
            lblEmailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmailTitle.Location = new Point(3, 110);
            lblEmailTitle.Name = "lblEmailTitle";
            lblEmailTitle.Size = new Size(134, 59);
            lblEmailTitle.TabIndex = 4;
            lblEmailTitle.Text = "Email:";
            lblEmailTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblEmailValue
            // 
            lblEmailValue.Dock = DockStyle.Fill;
            lblEmailValue.Font = new Font("Segoe UI", 10F);
            lblEmailValue.Location = new Point(143, 110);
            lblEmailValue.Name = "lblEmailValue";
            lblEmailValue.Size = new Size(448, 59);
            lblEmailValue.TabIndex = 5;
            lblEmailValue.Text = "(email)";
            lblEmailValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitleInfo
            // 
            lblTitleInfo.Dock = DockStyle.Top;
            lblTitleInfo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitleInfo.ForeColor = Color.FromArgb(192, 0, 0);
            lblTitleInfo.Location = new Point(18, 18);
            lblTitleInfo.Name = "lblTitleInfo";
            lblTitleInfo.Size = new Size(622, 44);
            lblTitleInfo.TabIndex = 2;
            lblTitleInfo.Text = "Thông tin tài khoản";
            // 
            // tabHistory
            // 
            tabHistory.BackColor = Color.White;
            tabHistory.Controls.Add(dgvHistory);
            tabHistory.Controls.Add(lblTitleHistory);
            tabHistory.Location = new Point(4, 29);
            tabHistory.Name = "tabHistory";
            tabHistory.Padding = new Padding(18, 18, 18, 18);
            tabHistory.Size = new Size(658, 487);
            tabHistory.TabIndex = 1;
            tabHistory.Text = "Lịch sử";
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ColumnHeadersHeight = 29;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Location = new Point(18, 62);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(622, 407);
            dgvHistory.TabIndex = 0;
            // 
            // lblTitleHistory
            // 
            lblTitleHistory.Dock = DockStyle.Top;
            lblTitleHistory.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitleHistory.Location = new Point(18, 18);
            lblTitleHistory.Name = "lblTitleHistory";
            lblTitleHistory.Size = new Size(622, 44);
            lblTitleHistory.TabIndex = 1;
            lblTitleHistory.Text = "Lịch sử chơi";
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 520);
            Controls.Add(splitMain);
            Name = "SettingForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cài đặt";
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pnlMenu.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            tabInfo.ResumeLayout(false);
            flpInfoActions.ResumeLayout(false);
            grpAccount.ResumeLayout(false);
            tblInfo.ResumeLayout(false);
            tblInfo.PerformLayout();
            tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
