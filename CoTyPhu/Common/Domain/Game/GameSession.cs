using Common.Domain.Game.Enums;

namespace Common.Domain.Game
{
    public class GameSession
    {
        public Guid Id { get; set; }
        public int RoomId { get; set; }

        public Board Board { get; set; }              
        public IList<Player> Players { get; set; } = new List<Player>();

        public int CurrentPlayerIndex { get; set; }   
        public GameStatus Status { get; set; }        

        public IList<Turn> Turns { get; set; } = new List<Turn>();
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public int? WinnerPlayerId { get; set; }

        public Player GetCurrentPlayer()
        {
            return Players[CurrentPlayerIndex];
        }

        public void NextPlayer()
        {
            if (Players.Count == 0) return;
            do
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
            }
            while (Players[CurrentPlayerIndex].Status == PlayerStatus.Bankrupt);
        }

        public bool IsFinished() => Players.Count(p => p.Status != PlayerStatus.Bankrupt) <= 1;
      
    }
}
