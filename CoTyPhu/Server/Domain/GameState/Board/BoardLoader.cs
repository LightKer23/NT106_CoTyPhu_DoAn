using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Game.Enums;

namespace Server.Domain.Board
{
    public static class BoardLoader
    {
        public static List<Tile> LoadDefaultBoard()
        {
            var board = new List<Tile>()
            {
                //PropertyTile: Name, landPrice, housePrice, HotelPrice, rentPrice (Theo cấp độ từ đất đến khách sạn), sellPrice
                //RailRoadTile: Name, buyPrice, sellPrice
                //UtilityTile: Name, buyPrice, sellPrice
                //TaxTile: Name, TaxType (Income/Special)
                new StartTile(),
                new PropertyTile("Nguyen Hue", 60, 50, 50, new List<int>{ 2, 10, 30, 90, 160, 250 }, 30),
                new CommunityChestTile(),
                new PropertyTile("Le Loi", 60, 50, 50, new List<int>{4, 20, 60, 160, 320, 450}, 30),
                new TaxTile("Thue Thu Nhap", TaxType.Income),
                new RailRoadTile("Ben Xe Can Giuoc", 200, 100),
                new PropertyTile("Luong Dinh Cua", 100, 200, 200, new List<int>{20, 120, 290, 550, 930, 1020}, 50),
                new ChanceTile(),
                new PropertyTile("Vo Thi Sau", 100, 200, 200, new List<int>{28, 150, 450, 1000, 1200, 1400}, 50),
                new PropertyTile("Hai Ba Trung", 120, 200, 200, new List<int>{20, 120, 290, 550, 930, 1020}, 60),
                new JailTile(),
                new PropertyTile("Nguyen Tat Thanh", 140, 100, 100, new List<int>{20, 120, 290, 550, 930, 1020}, 70),
                new UtilityTile("Cong Ty Dien luc", 200, 75),
                new PropertyTile("Nguyen Trai", 140, 100, 100, new List<int>{10, 50, 150, 450, 625, 750}, 70),
                new PropertyTile("An Duong Vuong", 160, 100, 100, new List<int>{12, 60, 180, 500, 700, 900}, 80),
                new RailRoadTile("Ben Xe Mien Tay", 200, 100),
                new PropertyTile("Hau Giang", 180, 100, 100, new List<int>{16, 80, 220, 600, 800, 1000}, 150),
                new CommunityChestTile(),
                new PropertyTile("Huynh Thuc Khang", 180, 150, 150, new List<int>{16, 80, 220, 600, 800, 1000}, 150),
                new PropertyTile("Hung Vuong", 200, 150, 150, new List<int>{20, 100, 300, 750, 925, 1100}, 160),
                new FreeParkingTile(),
                new PropertyTile("Phan The Hien", 220, 150, 150, new List<int>{18, 90, 250, 700, 875, 1050}, 110),
                new ChanceTile(),
                new PropertyTile("Kha Van Can", 220, 150, 150, new List<int>{18, 90, 250, 700, 875, 1050}, 110),
                new RailRoadTile("Ben Xe Cho Lon", 200, 100),
                new PropertyTile("Le Dai Hanh", 260, 150, 150, new List<int>{22, 110, 330, 800, 975, 1150}, 130),
                new PropertyTile("Truong Chinh", 260, 150, 150, new List<int>{22, 110, 330, 800, 975, 1150}, 130),
                new UtilityTile("Cong Ty Cap Nuoc", 200, 75),
                new PropertyTile("Hoang Van Thu", 280, 150, 150, new List<int>{24, 120, 360, 850, 1025, 1200}, 140),
                new GoToJailTile(),
                new PropertyTile("Cong Hoa", 300, 200, 200, new List<int>{26, 130, 390, 900, 1100, 1275}, 150),
                new PropertyTile("Nguyen Kiem", 300, 200, 200, new List<int>{26, 130, 390, 900, 1100, 1275}, 150),
                new CommunityChestTile(),
                new PropertyTile("Quang Trung", 320, 200, 200, new List<int>{28, 150, 450, 1000, 1200, 1400}, 160),
                new RailRoadTile("Ben Xe Mien Dong", 200, 100),
                new ChanceTile(),
                new PropertyTile("Luy Ban Bich", 350, 170, 200, new List<int>{35, 175, 500, 1100, 1300, 1500}, 200),
                new TaxTile("Thue Dac Biet", TaxType.Special),
                new PropertyTile("Tan Ky Tan Quy", 400, 200, 200, new List<int>{50, 200, 600, 1400, 1700, 2000}, 200),
            };

            return board;
        }
    }
}
