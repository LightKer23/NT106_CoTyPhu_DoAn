using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts
{
    public enum MessageType
    {
        LoginRequest,
        LoginResponse,
        RegisterRequest,
        RegisterResponse,

        // -------- ROOM ----------
        CreateRoomRequest,
        CreateRoomResponse,
        JoinRoomRequest,
        JoinRoomResponse,
        StartMatchEvent,

        // -------- GAME ACTION (client → server) ----------
        // Client random xong gửi lên để server xử lý logic
        RollDiceRequest,          // {MatchID, PlayerID, Roll1, Roll2}
        BuyDecisionRequest,       // {MatchID, PlayerID, PropertyID, Accept}
        EndTurnRequest,           // {MatchID, PlayerID}

        // -------- GAME EVENT (server → client) ----------
        // Kết quả xử lý 1 lượt
        TurnResultEvent,          // gói tổng hợp mọi update sau khi xử lý
        AskBuyPropertyEvent,
    }
}
