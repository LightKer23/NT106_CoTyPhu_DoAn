using Common.Domain.Game.Enums;

namespace Common.Domain.Game
{
    public class Player
    {
        public int Id { get; set; }              
        public string Name { get; set; }
        public int Position { get; set; }        
        public int Money { get; set; }
        public PlayerStatus Status { get; set; } 

        public List<int> OwnedPropertyTileIds { get; set; } = new();
        public int JailTurnCount { get; set; }

        public void Move(int steps, int boardSize)
        {
            Position = (Position + steps) % boardSize;
        }

        public void ChangeMoney(int amount)
        {
            Money += amount;
        }

        public bool CanPay(int amount) => Money >= amount;
        public bool IsBankrupt => Money < 0 || Status == PlayerStatus.Bankrupt;
    }
}
