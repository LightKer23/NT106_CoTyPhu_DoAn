using Client.Services.Network;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class SettingForm : Form
    {
        private bool _isEditing = false;

        public SettingForm()
        {
            InitializeComponent();

            // Events
            Load += SettingForm_Load;
            btnEditInfo.Click += BtnEditInfo_Click;
            btnCancelEdit.Click += BtnCancelEdit_Click;
            btnSaveInfo.Click += BtnSaveInfo_Click;
            btnLogout.Click += BtnLogout_Click;
            tabMain.SelectedIndexChanged += TabMain_SelectedIndexChanged;
        }


        private void SettingForm_Load(object? sender, EventArgs e)
        {
            LoadAccountInfo();
            SetupHistoryGrid();
        }


        private void LoadAccountInfo()
        {
            lblUsernameValue.Text = ClientSession.Username;
            txtFullName.Text = ClientSession.DisplayName;
            lblEmailValue.Text = ClientSession.Email;

            SetEditMode(false);
        }

        private void SetEditMode(bool enable)
        {
            _isEditing = enable;

            txtFullName.ReadOnly = !enable;
            btnSaveInfo.Enabled = enable;
            btnCancelEdit.Enabled = enable;
            btnEditInfo.Enabled = !enable;
        }

        private void BtnEditInfo_Click(object? sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void BtnCancelEdit_Click(object? sender, EventArgs e)
        {
            txtFullName.Text = ClientSession.DisplayName;
            SetEditMode(false);
        }

        private void BtnSaveInfo_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Đã lưu thông tin!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetEditMode(false);
        }


        private async void TabMain_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabHistory)
            {
                await LoadHistoryAsync();
            }
        }

        private void SetupHistoryGrid()
        {
            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID Trận",
                DataPropertyName = "MatchId"
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Bắt đầu",
                DataPropertyName = "StartTime"
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Kết thúc",
                DataPropertyName = "EndTime"
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Hạng",
                DataPropertyName = "Rank"
            });

        }

        private async Task LoadHistoryAsync()
        {
            dgvHistory.Rows.Clear();

            var resp = await ClientSession.Tcp.GetMatchHistoryAsync(ClientSession.AccountID);

            if (!resp.Success || resp.History.Count == 0)
            {
                return;
            }

            foreach (var h in resp.History)
            {
                dgvHistory.Rows.Add(
                    h.MatchId,
                    h.StartTime.ToString("dd/MM/yyyy HH:mm"),
                    h.EndTime?.ToString("dd/MM/yyyy HH:mm") ?? "-",
                    h.Rank
                );
            }
        }

        // =========================
        // LOGOUT
        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            ClientSession.Disconnect();

            LoginForm login = null!;

            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm lf)
                {
                    login = lf;
                    break;
                }
            }

            if (login == null)
            {
                login = new LoginForm();
                login.Show();
            }
            else
            {
                login.Show();
                login.BringToFront();
            }

            foreach (Form f in Application.OpenForms.Cast<Form>().ToList())
            {
                if (f != login)
                    f.Close();
            }
        }
    }
}
