using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connetion;
using Server.Infrastructure.Database.Models;

namespace Server.Infrastructure.Database.Repository
{
    public class PlayerRepo
    {
        private readonly DBConnection _db;

        public PlayerRepo(DBConnection db)
        {
            _db = db;
        }

        public Player GetById(int idPlayer)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Player WHERE IDPlayer=@id", conn);
                cmd.Parameters.AddWithValue("@id", idPlayer);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Player
                    {
                        IDPlayer = rd.GetInt32(0),
                        IDMatch = rd.GetInt32(1),
                        IDAccount = rd.GetInt32(2),
                        Rank = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                        CrashTime = rd.IsDBNull(4) ? null : rd.GetDateTime(4),
                        Money = rd.GetInt32(5),
                        Position = rd.GetInt32(6),
                        Turn = rd.GetInt32(7),
                        StatusPlayer = rd.GetString(8)
                    };
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET Player: " + ex.Message);
            }

            return null;
        }

        // UPDATE MONEY
        public bool UpdateMoney(int idPlayer, int money)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Player SET Money=@m WHERE IDPlayer=@id", conn);

                cmd.Parameters.AddWithValue("@id", idPlayer);
                cmd.Parameters.AddWithValue("@m", money);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi UPDATE Money: " + ex.Message);
                return false;
            }
        }

        // UPDATE POSITION
        public bool UpdatePosition(int idPlayer, int pos)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Player SET Position=@p WHERE IDPlayer=@id", conn);

                cmd.Parameters.AddWithValue("@id", idPlayer);
                cmd.Parameters.AddWithValue("@p", pos);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi UPDATE Position: " + ex.Message);
                return false;
            }
        }

        // UPDATE TURN
        public bool UpdateTurn(int idPlayer, int turn)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Player SET Turn=@t WHERE IDPlayer=@id", conn);

                cmd.Parameters.AddWithValue("@id", idPlayer);
                cmd.Parameters.AddWithValue("@t", turn);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi UPDATE Turn: " + ex.Message);
                return false;
            }
        }
    }
}
