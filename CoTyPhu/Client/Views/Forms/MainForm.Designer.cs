namespace Client.Views.Forms
{
    partial class MainForm
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
            pbBoard = new PictureBox();
            pbDie1 = new PictureBox();
            pbDie2 = new PictureBox();
            btnRollDice = new Button();
            pnlInfoTile = new Panel();
            pbTile = new PictureBox();
            btnBuy = new Button();
            btnEndTurn = new Button();
            pnlChat = new Panel();
            btnSend = new Button();
            textBox2 = new TextBox();
            lbChat = new ListBox();
            pnlInforPlayer = new Panel();
            label1 = new Label();
            lvPlayerInfo = new ListView();
            colName = new ColumnHeader();
            colCurrentMoney = new ColumnHeader();
            lbHistory = new ListBox();
            btnExit = new Button();
            btnUpgrade = new Button();
            ((System.ComponentModel.ISupportInitialize)pbBoard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDie1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDie2).BeginInit();
            pnlInfoTile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbTile).BeginInit();
            pnlChat.SuspendLayout();
            pnlInforPlayer.SuspendLayout();
            SuspendLayout();
            // 
            // pbBoard
            // 
            pbBoard.Image = Properties.Resources.ban_co_ty_phu;
            pbBoard.Location = new Point(270, 0);
            pbBoard.Name = "pbBoard";
            pbBoard.Size = new Size(800, 750);
            pbBoard.SizeMode = PictureBoxSizeMode.StretchImage;
            pbBoard.TabIndex = 0;
            pbBoard.TabStop = false;
            // 
            // pbDie1
            // 
            pbDie1.Location = new Point(520, 378);
            pbDie1.Name = "pbDie1";
            pbDie1.Size = new Size(100, 100);
            pbDie1.TabIndex = 0;
            pbDie1.TabStop = false;
            // 
            // pbDie2
            // 
            pbDie2.Location = new Point(691, 378);
            pbDie2.Name = "pbDie2";
            pbDie2.Size = new Size(100, 100);
            pbDie2.TabIndex = 2;
            pbDie2.TabStop = false;
            // 
            // btnRollDice
            // 
            btnRollDice.BackColor = SystemColors.ButtonHighlight;
            btnRollDice.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRollDice.Location = new Point(520, 517);
            btnRollDice.Name = "btnRollDice";
            btnRollDice.Size = new Size(120, 50);
            btnRollDice.TabIndex = 0;
            btnRollDice.Text = "Tung xúc xắc";
            btnRollDice.UseVisualStyleBackColor = false;
            // 
            // pnlInfoTile
            // 
            pnlInfoTile.BorderStyle = BorderStyle.FixedSingle;
            pnlInfoTile.Controls.Add(btnUpgrade);
            pnlInfoTile.Controls.Add(pbTile);
            pnlInfoTile.Controls.Add(btnBuy);
            pnlInfoTile.Location = new Point(0, 0);
            pnlInfoTile.Name = "pnlInfoTile";
            pnlInfoTile.Size = new Size(270, 350);
            pnlInfoTile.TabIndex = 3;
            // 
            // pbTile
            // 
            pbTile.Location = new Point(0, 0);
            pbTile.Name = "pbTile";
            pbTile.Size = new Size(270, 280);
            pbTile.TabIndex = 2;
            pbTile.TabStop = false;
            // 
            // btnBuy
            // 
            btnBuy.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBuy.Location = new Point(12, 297);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(100, 40);
            btnBuy.TabIndex = 1;
            btnBuy.Text = "Mua";
            btnBuy.UseVisualStyleBackColor = true;
            // 
            // btnEndTurn
            // 
            btnEndTurn.BackColor = SystemColors.ButtonHighlight;
            btnEndTurn.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEndTurn.Location = new Point(671, 517);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(120, 50);
            btnEndTurn.TabIndex = 4;
            btnEndTurn.Text = "Kết thúc lượt";
            btnEndTurn.UseVisualStyleBackColor = false;
            // 
            // pnlChat
            // 
            pnlChat.BorderStyle = BorderStyle.FixedSingle;
            pnlChat.Controls.Add(btnSend);
            pnlChat.Controls.Add(textBox2);
            pnlChat.Controls.Add(lbChat);
            pnlChat.Location = new Point(1070, 0);
            pnlChat.Name = "pnlChat";
            pnlChat.Size = new Size(410, 330);
            pnlChat.TabIndex = 0;
            // 
            // btnSend
            // 
            btnSend.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSend.Location = new Point(325, 283);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 27);
            btnSend.TabIndex = 2;
            btnSend.Text = "Gửi";
            btnSend.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(4, 283);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(314, 27);
            textBox2.TabIndex = 1;
            // 
            // lbChat
            // 
            lbChat.FormattingEnabled = true;
            lbChat.Location = new Point(0, 0);
            lbChat.Name = "lbChat";
            lbChat.Size = new Size(410, 264);
            lbChat.TabIndex = 0;
            // 
            // pnlInforPlayer
            // 
            pnlInforPlayer.BorderStyle = BorderStyle.FixedSingle;
            pnlInforPlayer.Controls.Add(label1);
            pnlInforPlayer.Controls.Add(lvPlayerInfo);
            pnlInforPlayer.Location = new Point(0, 350);
            pnlInforPlayer.Name = "pnlInforPlayer";
            pnlInforPlayer.Size = new Size(270, 399);
            pnlInforPlayer.TabIndex = 7;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(52, 15);
            label1.Name = "label1";
            label1.Size = new Size(163, 33);
            label1.TabIndex = 0;
            label1.Text = "NGƯỜI CHƠI\r\n";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lvPlayerInfo
            // 
            lvPlayerInfo.Columns.AddRange(new ColumnHeader[] { colName, colCurrentMoney });
            lvPlayerInfo.FullRowSelect = true;
            lvPlayerInfo.GridLines = true;
            lvPlayerInfo.Location = new Point(0, 63);
            lvPlayerInfo.Name = "lvPlayerInfo";
            lvPlayerInfo.Size = new Size(270, 336);
            lvPlayerInfo.TabIndex = 1;
            lvPlayerInfo.UseCompatibleStateImageBehavior = false;
            // 
            // colName
            // 
            colName.Text = "Tên";
            // 
            // colCurrentMoney
            // 
            colCurrentMoney.Text = "Tiền hiện tại";
            // 
            // lbHistory
            // 
            lbHistory.FormattingEnabled = true;
            lbHistory.Location = new Point(1070, 330);
            lbHistory.Name = "lbHistory";
            lbHistory.Size = new Size(410, 364);
            lbHistory.TabIndex = 9;
            // 
            // btnExit
            // 
            btnExit.BackColor = SystemColors.ButtonHighlight;
            btnExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(1350, 702);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 40);
            btnExit.TabIndex = 10;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            // 
            // btnUpgrade
            // 
            btnUpgrade.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpgrade.Location = new Point(154, 297);
            btnUpgrade.Name = "btnUpgrade";
            btnUpgrade.Size = new Size(100, 40);
            btnUpgrade.TabIndex = 3;
            btnUpgrade.Text = "Nâng cấp";
            btnUpgrade.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 753);
            Controls.Add(btnExit);
            Controls.Add(lbHistory);
            Controls.Add(pnlInforPlayer);
            Controls.Add(pnlChat);
            Controls.Add(btnEndTurn);
            Controls.Add(pnlInfoTile);
            Controls.Add(btnRollDice);
            Controls.Add(pbDie2);
            Controls.Add(pbDie1);
            Controls.Add(pbBoard);
            Name = "MainForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cờ tỷ phú";
            ((System.ComponentModel.ISupportInitialize)pbBoard).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDie1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDie2).EndInit();
            pnlInfoTile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbTile).EndInit();
            pnlChat.ResumeLayout(false);
            pnlChat.PerformLayout();
            pnlInforPlayer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbBoard;
        private PictureBox pbDie1;
        private PictureBox pbDie2;
        private Button btnRollDice;
        private Panel pnlInfoTile;
        private Button btnBuy;
        private Button btnEndTurn;
        private PictureBox pbTile;
        private Panel pnlChat;
        private ListBox lbChat;
        private Button btnSend;
        private TextBox textBox2;
        private Panel pnlInforPlayer;
        private Label label1;
        private ListView lvPlayerInfo;
        private ColumnHeader colName;
        private ColumnHeader colCurrentMoney;
        private ListBox lbHistory;
        private Button btnExit;
        private Button btnUpgrade;
    }
}