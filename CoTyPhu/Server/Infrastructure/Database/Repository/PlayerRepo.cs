using Common.Contracts.Auth;
using Common.Domain.Models.Entities;
using Server.Infrastructure.Database.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

public class PlayerRepo
{
    private readonly DBConnection _db;
    public PlayerRepo(DBConnection db) => _db = db;

    // 1️ Join room
    public void InsertPlayer(int matchId, int playerId, int accountId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
        INSERT INTO Player (IDMatch, IDPlayer, IDAccount, Status)
        VALUES (@mid, @pid, @acc, 'Waiting')", conn);

        cmd.Parameters.AddWithValue("@mid", matchId);
        cmd.Parameters.AddWithValue("@pid", playerId);
        cmd.Parameters.AddWithValue("@acc", accountId);

        cmd.ExecuteNonQuery();
    }


    // 2️ Out khi chưa Playing
    public void DeletePlayer(int matchId, int playerId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
        DELETE FROM Player 
        WHERE IDMatch=@mid AND IDPlayer=@pid", conn);

        cmd.Parameters.AddWithValue("@mid", matchId);
        cmd.Parameters.AddWithValue("@pid", playerId);

        cmd.ExecuteNonQuery();
    }


    // 3️ Set Playing khi start game
    public void SetAllPlaying(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
            UPDATE Player
            SET Status='Playing'
            WHERE IDMatch=@mid", conn);

        cmd.Parameters.AddWithValue("@mid", matchId);
        cmd.ExecuteNonQuery();
    }

    // 4️ Player kết thúc (Bankrupt / Crash)
    public void EndPlayer(int playerId, string status, int rank)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
            UPDATE Player
            SET Status=@st,
                Rank=@rank,
                CrashTime=GETDATE()
            WHERE IDPlayer=@id", conn);

        cmd.Parameters.AddWithValue("@st", status);
        cmd.Parameters.AddWithValue("@rank", rank);
        cmd.Parameters.AddWithValue("@id", playerId);
        cmd.ExecuteNonQuery();
    }

    // 5️ Đếm player còn chơi
    public int CountAlive(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
            SELECT COUNT(*)
            FROM Player
            WHERE IDMatch=@mid AND Status='Playing'", conn);

        cmd.Parameters.AddWithValue("@mid", matchId);
        return (int)cmd.ExecuteScalar();
    }

    // 6 Hiển thị lịch sử đấu
    public List<MatchHistoryItem> GetHistoryByAccount(int accountId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
        SELECT 
            m.IDMatch,
            m.StartTime,
            m.EndTime,
            p.Rank,
            p.Status
        FROM Player p
        JOIN Match m ON p.IDMatch = m.IDMatch
        WHERE p.IDAccount = @acc AND m.Status = 'End'
        ORDER BY m.StartTime DESC
    ", conn);

        cmd.Parameters.AddWithValue("@acc", accountId);

        var list = new List<MatchHistoryItem>();
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            list.Add(new MatchHistoryItem
            {
                MatchId = rd.GetInt32(0),
                StartTime = rd.GetDateTime(1),
                EndTime = rd.IsDBNull(2) ? null : rd.GetDateTime(2),
                Rank = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                Status = rd.GetString(4)
            });
        }

        return list;
    }
}
