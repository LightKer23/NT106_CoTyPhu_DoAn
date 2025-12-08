using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connection;
using Common.Domain.Models.Entities;

namespace Server.Infrastructure.Database.Repository
{
    public class PlayerRepo
    {
        private readonly DBConnection _db;

        public PlayerRepo(DBConnection db)
        {
            _db = db;
        }


        // Thêm 1 Player trong trận đó khi chưa bắt đầu
        public int InsertPlayer(int idMatch, int idAccount)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                // Lấy IDPlayer mới: COUNT(*) + 1
                var getNewIdCmd = new SqlCommand(@"
            SELECT COUNT(*) + 1 
            FROM Player 
            WHERE IDMatch = @mid", conn);

                getNewIdCmd.Parameters.AddWithValue("@mid", idMatch);

                int newIdPlayer = (int)getNewIdCmd.ExecuteScalar();

                // Insert player
                var insertCmd = new SqlCommand(@"
            INSERT INTO Player(IDMatch, IDPlayer, IDAccount, Money, Position, StatusPlayer)
            VALUES (@mid, @pid, @acc, 1500, 0, 'Ready')", conn);

                insertCmd.Parameters.AddWithValue("@mid", idMatch);
                insertCmd.Parameters.AddWithValue("@pid", newIdPlayer);
                insertCmd.Parameters.AddWithValue("@acc", idAccount);

                insertCmd.ExecuteNonQuery();

                return newIdPlayer;
            }
            catch
            {
                return -1;
            }
        }


        // Lấy thông tin của Player
        public Player GetPlayer(int idMatch, int idPlayer)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    SELECT IDPlayer, IDMatch, IDAccount, Rank, Money, Position, StatusPlayer
                    FROM Player 
                    WHERE IDMatch=@mid AND IDPlayer=@pid",
                    conn);

                cmd.Parameters.AddWithValue("@mid", idMatch);
                cmd.Parameters.AddWithValue("@pid", idPlayer);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Player
                    {
                        IDPlayer = rd.GetInt32(0),
                        IDMatch = rd.GetInt32(1),
                        IDAccount = rd.GetInt32(2),
                        Rank = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                        Money = rd.GetInt32(4),
                        Position = rd.GetInt32(5),
                        StatusPlayer = rd.GetString(6)
                    };
                }
            }
            catch { }

            return null;
        }


        // Update sau khi xử lý logic trong RAM
        public bool UpdateLogic(int idMatch, int idPlayer, int money, int position, string status)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Player
                    SET Money=@m, Position=@p, StatusPlayer=@s
                    WHERE IDMatch=@mid AND IDPlayer=@pid", conn);

                cmd.Parameters.AddWithValue("@mid", idMatch);
                cmd.Parameters.AddWithValue("@pid", idPlayer);
                cmd.Parameters.AddWithValue("@m", money);
                cmd.Parameters.AddWithValue("@p", position);
                cmd.Parameters.AddWithValue("@s", status);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Player bị crash
        public bool SetCrash(int idMatch, int idPlayer)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Player
                    SET StatusPlayer='Crash'
                    WHERE IDMatch=@mid AND IDPlayer=@pid", conn);

                cmd.Parameters.AddWithValue("@mid", idMatch);
                cmd.Parameters.AddWithValue("@pid", idPlayer);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Đếm số player còn sống trong match (không Bankrupt)
        public int CountAlive(int idMatch)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
            SELECT COUNT(*) 
            FROM Player
            WHERE IDMatch=@mid 
            AND StatusPlayer NOT IN ('Bankrupt')", conn);

                cmd.Parameters.AddWithValue("@mid", idMatch);

                return (int)cmd.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
        }


        // Player phá sản
        public bool SetBankrupt(int idMatch, int idPlayer)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                UPDATE Player
                SET StatusPlayer='Bankrupt',
                    CrashTime = GETDATE()
                WHERE IDMatch=@mid AND IDPlayer=@pid", conn);

                cmd.Parameters.AddWithValue("@mid", idMatch);
                cmd.Parameters.AddWithValue("@pid", idPlayer);
                cmd.ExecuteNonQuery();
            }
            catch { return false; }

            try
            {
                var propertyRepo = new PropertyRepo(_db);
                propertyRepo.ResetPlayerProperties(idMatch, idPlayer);
            }
            catch
            { /* không ảnh hưởng phá sản, tiếp tục*/ }

            try
            {
                var matchRepo = new MatchRepo(_db);

                matchRepo.IncreasePlayerCount(idMatch: idMatch, -1);
            }
            catch { }

            try
            {
                var matchRepo = new MatchRepo(_db);
                var playerRepo = new PlayerRepo(_db);

                int alive = playerRepo.CountAlive(idMatch);

                if (alive == 1)
                    matchRepo.UpdateEnd(idMatch);
            }
            catch { }

            return true;
        }
    }
}
