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

        // Tạo match mới với trạng thái Waiting, chưa start
        public int CreateMatch()
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    INSERT INTO Match(StartTime, EndTime, NumberPlayer, Turn, Status)
                    OUTPUT INSERTED.IDMatch
                    VALUES (NULL, NULL, 1, 1, 'Waiting')", conn);

                return (int)cmd.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }


        // Lấy toàn bộ thông tin có trong Match 
        public Match GetById(int idMatch)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "SELECT IDMatch, NumberPlayer, Turn, Status FROM Match WHERE IDMatch=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", idMatch);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Match
                    {
                        IDMatch = rd.GetInt32(0),
                        NumberPlayer = rd.GetInt32(1),
                        Turn = rd.GetInt32(2),
                        Status = rd.GetString(3)
                    };
                }
            }
            catch { }

            return null;
        }


        // Cập nhập số lương người trong phòng
        public bool IncreasePlayerCount(int idMatch, int i)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Match SET NumberPlayer = NumberPlayer + 1 WHERE IDMatch=@id", conn);

                cmd.Parameters.AddWithValue("@id", idMatch);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Khi player #1 bấm start game
        public bool StartMatch(int idMatch)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Match
                    SET Status='Playing',
                        StartTime = GETDATE(),
                        Turn = 1
                    WHERE IDMatch=@id", conn);

                cmd.Parameters.AddWithValue("@id", idMatch);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Đổi lượt
        public bool UpdateTurn(int idMatch, int playerTurn)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Match SET Turn=@t WHERE IDMatch=@id", conn);

                cmd.Parameters.AddWithValue("@id", idMatch);
                cmd.Parameters.AddWithValue("@t", playerTurn);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Cập nhật trạng thái Waiting / Playing
        public bool UpdateStatus(int idMatch, string status)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Match SET Status=@s WHERE IDMatch=@id", conn);

                cmd.Parameters.AddWithValue("@id", idMatch);
                cmd.Parameters.AddWithValue("@s", status);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Kết thúc trận: đặt EndTime và trạng thái = End
        public bool UpdateEnd(int idMatch)
        {
            try
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
            catch
            {
                return false;
            }
        }
    }
}
