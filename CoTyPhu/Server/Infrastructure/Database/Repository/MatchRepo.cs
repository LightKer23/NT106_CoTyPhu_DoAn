using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connection;
using Common.Domain.Models.Entities;

namespace Server.Infrastructure.Database.Repository
{
    public class MatchRepo
    {
        private readonly DBConnection _db;

        public MatchRepo(DBConnection db)
        {
            _db = db;
        }

        // Tạo match mới (Waiting)
        public int CreateMatch()
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new SqlCommand(@"
            INSERT INTO Match(NumberPlayer, Status)
            OUTPUT INSERTED.IDMatch
            VALUES (1, 'Waiting')", conn);

            return (int)cmd.ExecuteScalar();
        }

        // Khi host bấm Start
        public bool StartMatch(int idMatch)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new SqlCommand(@"
            UPDATE Match
            SET Status = 'Playing',
                StartTime = GETDATE(),
                Turn = 1
            WHERE IDMatch = @id", conn);

            cmd.Parameters.AddWithValue("@id", idMatch);
            return cmd.ExecuteNonQuery() > 0;
        }

        // Tăng số người (chỉ để thống kê)
        public bool IncreasePlayerCount(int idMatch)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new SqlCommand(
                "UPDATE Match SET NumberPlayer = NumberPlayer + 1 WHERE IDMatch = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", idMatch);
            return cmd.ExecuteNonQuery() > 0;
        }

        // Giảm số người (khi leave)
        public bool DecreasePlayerCount(int idMatch)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new SqlCommand(
                "UPDATE Match SET NumberPlayer = NumberPlayer - 1 WHERE IDMatch = @id",
                conn);

            cmd.Parameters.AddWithValue("@id", idMatch);
            return cmd.ExecuteNonQuery() > 0;
        }

        // Kết thúc trận
        public bool EndMatch(int idMatch)
        {
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new SqlCommand(@"
            UPDATE Match
            SET EndTime = GETDATE(),
                Status = 'End'
            WHERE IDMatch = @id", conn);

            cmd.Parameters.AddWithValue("@id", idMatch);
            return cmd.ExecuteNonQuery() > 0;
        }
    }


}
