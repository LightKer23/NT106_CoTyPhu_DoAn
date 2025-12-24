using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connection;
using Common.Domain.Models.Entities;

public class MatchRepo
{
    private readonly DBConnection _db;

    public MatchRepo(DBConnection db)
    {
        _db = db;
    }

    // 1️⃣ Tạo phòng (chưa có player)
    public int CreateMatch()
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
            INSERT INTO Match (NumberPlayer, Turn, Status)
            OUTPUT INSERTED.IDMatch
            VALUES (0, 0, 'Waiting')", conn);

        return (int)cmd.ExecuteScalar();
    }

    // 2️⃣ + người
    public void IncreasePlayer(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(
            "UPDATE Match SET NumberPlayer = NumberPlayer + 1 WHERE IDMatch=@id", conn);
        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }

    // 3️⃣ - người
    public void DecreasePlayer(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(
            "UPDATE Match SET NumberPlayer = NumberPlayer - 1 WHERE IDMatch=@id", conn);
        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }

    // 4️⃣ Set host / đổi host
    public void UpdateTurn(int matchId, int playerId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(
            "UPDATE Match SET Turn=@pid WHERE IDMatch=@id", conn);
        cmd.Parameters.AddWithValue("@pid", playerId);
        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }

    // 5️⃣ Start game
    public void StartMatch(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
            UPDATE Match
            SET Status='Playing',
                StartTime=GETDATE()
            WHERE IDMatch=@id", conn);

        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }

    // 6️⃣ End game
    public void EndMatch(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(@"
            UPDATE Match
            SET Status='End',
                EndTime=GETDATE()
            WHERE IDMatch=@id", conn);

        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }

    // 7️⃣ Hủy phòng
    public void DeleteMatch(int matchId)
    {
        using var conn = _db.GetConnection();
        conn.Open();

        var cmd = new SqlCommand(
            "DELETE FROM Match WHERE IDMatch=@id", conn);
        cmd.Parameters.AddWithValue("@id", matchId);
        cmd.ExecuteNonQuery();
    }
}
