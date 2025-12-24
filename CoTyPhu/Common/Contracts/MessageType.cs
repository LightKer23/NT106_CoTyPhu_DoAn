using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts
{
    public enum MessageType
    {
        // ===== AUTH =====
        LoginRequest,              // client gửi đăng nhập
        LoginResponse,             // server trả kết quả đăng nhập

        RegisterRequest,           // client gửi đăng ký
        RegisterResponse,          // server trả kết quả đăng ký

        LogoutRequest,             // client yêu cầu đăng xuất

        ForgotPasswordRequest,     // client yêu cầu quên mật khẩu
        ForgotPasswordResponse,    // server phản hồi gửi mail / mã

        VerifyOTPRequest,
        VerifyOTPResponse,

        ResetPasswordRequest,      // client gửi mật khẩu mới
        ResetPasswordResponse,     // server xác nhận đổi mật khẩu

        // ===== ROOM / MATCH =====
        CreateRoomRequest,         // tạo phòng chơi
        CreateRoomResponse,        // server trả info phòng

        JoinRoomRequest,           // vào phòng
        JoinRoomResponse,          // server xác nhận vào phòng

        SearchRoomRequest,
        SearchRoomResponse,

        LeaveRoomRequest,          // rời phòng trước khi chơi
        LeaveRoomEvent,            // server broadcast người rời phòng

        StartMatchRequest,           // server bắt đầu trận
        StartMatchResponse,             // server kết thúc trận

        RoomUpdatedEvent,
        RoomStateEvent,            // cập nhật danh sách player trong phòng

        PlayerSurrenderRequest,
        PlayerSurrenderEvent,

        // ===== GAME ACTION (Client → Server) =====
        RollDiceRequest,           // yêu cầu tung xúc xắc

        BuyDecisionRequest,        // quyết định mua / không mua đất

        GetOutOfJailRequest,       // chọn cách ra tù

        SellPropertyRequest,       // bán đất để lấy tiền

        UpgradePropertyRequest,    // xây nhà / khách sạn
        DowngradePropertyRequest,  // bán nhà

        EndTurnRequest,            // kết thúc lượt

        LeaveMatchRequest,         // thoát giữa trận
        PropertyUpdatedEvent,

        EndTurnReponse,

        // ===== CHAT =====
        SendChatMessageRequest,    // gửi tin nhắn chat
        ChatMessageEvent,          // broadcast tin nhắn chat

        // ===== GAME EVENT (Server → Client) =====
        DiceRolledEvent,         // kết quả tung xúc xắc
        TurnResultEvent,           // kết quả xử lý 1 lượt
        AskBuyPropertyEvent,       // server hỏi mua đất
        PlayerMovedEvent,          // player di chuyển
        MoneyChangedEvent,         // thay đổi tiền
        PlayerJailedEvent,         // player bị vào tù
        PlayerReleasedFromJailEvent,// player ra tù
        PlayerBankruptEvent,       // player phá sản
        PlayerLeftEvent,           // player rời trận
        DrawChanceCardEvent,       // rút thẻ cơ hội
        DrawCommunityChestEvent,   // rút thẻ khí vận
        TaxPaidEvent,              // đóng thuế

        // ===== SYSTEM / ERROR =====
        ErrorResponse,             // lỗi từ server
        ServerNotificationEvent    // thông báo hệ thống
    }
}
