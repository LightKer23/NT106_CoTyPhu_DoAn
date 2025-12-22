using Client.Services.Network;
using Client.Views.Forms;
using Common.Constracts.Room;
using Common.Domain.Models.Entities;
//using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client.Views.Forms
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();

        }

        private async void btnPlayWithPlayer_Click(object sender, EventArgs e)
        {
            try
            {
                await ClientSession.ConnectAsync();

                var req = new CreateRoomRequest
                {
                    AccountID = ClientSession.AccountID
                };
                ////////


                var res = await ClientSession.Tcp.CreateRoomAsync(ClientSession.AccountID);


                if (!res.Success)
                {
                    MessageBox.Show("Tạo phòng thất bại");
                    return;
                }

                int roomId = res.RoomID;

                // 👉 MỞ FORM CHỌN NHÂN VẬT
                var chooseForm = new ChooseCharacterForm(ClientSession.Tcp, ClientSession.AccountID)
                {
                    RoomId = roomId
                };

                this.Hide();

                chooseForm.FormClosed += (s, args) =>
                {
                    this.Show();
                };

                chooseForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo phòng: " + ex.Message);
            }
        }

        private async void btnPlayWithCmp_Click(object sender, EventArgs e)
        {
            try
            {
                await ClientSession.ConnectAsync();

                // 1️⃣ Gửi SearchRoom
                var res = await ClientSession.Tcp.SearchRoomAsync(ClientSession.AccountID);

                if (res == null || res.Success == false)
                {
                    MessageBox.Show("Không tìm thấy phòng nào");
                    return;
                }

                int roomId = res.RoomId;

                var chooseForm = new ChooseCharacterForm(
                    ClientSession.Tcp,
                    ClientSession.AccountID
                )
                {
                    RoomId = roomId
                };

                this.Hide();

                chooseForm.FormClosed += (s, args) =>
                {
                    this.Show();
                };

                chooseForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm phòng: " + ex.Message);
            }
        }


    }
}
