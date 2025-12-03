using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connetion;
using Server.Infrastructure.Database.Models;

namespace Server.Infrastructure.Database.Repository
{
    public class MatchRepo
    {
        private readonly DBConnection _db;

        public MatchRepo(DBConnection db)
        {
            _db = db;
        }

        public Match GetById(int id)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Match WHERE IDMatch=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Match
                    {
                        IDMatch = rd.GetInt32(0),
                        StartTime = rd.GetDateTime(1),
                        EndTime = rd.IsDBNull(2) ? null : rd.GetDateTime(2),
                        NumberPlayer = rd.GetInt32(3),
                        Status = rd.GetString(4)
                    };
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET Match: " + ex.Message);
            }

            return null;
        }

        public List<Match> GetAll()
        {
            var list = new List<Match>();

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Match", conn);
                using var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    list.Add(new Match
                    {
                        IDMatch = rd.GetInt32(0),
                        StartTime = rd.GetDateTime(1),
                        EndTime = rd.IsDBNull(2) ? null : rd.GetDateTime(2),
                        NumberPlayer = rd.GetInt32(3),
                        Status = rd.GetString(4)
                    });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET ALL Match: " + ex.Message);
            }

            return list;
        }

        public bool Insert(Match m)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "INSERT INTO Match (NumberPlayer, Status) VALUES (@n, @s)", conn);

                cmd.Parameters.AddWithValue("@n", m.NumberPlayer);
                cmd.Parameters.AddWithValue("@s", m.Status);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi INSERT Match: " + ex.Message);
                return false;
            }
        }

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

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi UPDATE Match: " + ex.Message);
                return false;
            }
        }
    }
}
