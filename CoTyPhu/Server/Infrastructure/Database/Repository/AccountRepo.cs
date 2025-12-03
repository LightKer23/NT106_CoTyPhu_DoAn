using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connetion;
using Server.Infrastructure.Database.Models;

namespace Server.Infrastructure.Database.Repository
{
    public class AccountRepo
    {
        private readonly DBConnection _db;

        public AccountRepo(DBConnection db)
        {
            _db = db;
        }

        // GET BY ID
        public Account GetById(int id)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Account WHERE IDAccount = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Account
                    {
                        IDAccount = rd.GetInt32(0),
                        Username = rd.GetString(1),
                        PasswordHash = rd.GetString(2),
                        Email = rd.GetString(3),
                        DisplayName = rd.IsDBNull(4) ? null : rd.GetString(4),
                        CreatedDate = rd.GetDateTime(5)
                    };
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET Account: " + ex.Message);
            }

            return null;
        }

        // GET ALL
        public List<Account> GetAll()
        {
            var list = new List<Account>();

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Account", conn);
                using var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    list.Add(new Account
                    {
                        IDAccount = rd.GetInt32(0),
                        Username = rd.GetString(1),
                        PasswordHash = rd.GetString(2),
                        Email = rd.GetString(3),
                        DisplayName = rd.IsDBNull(4) ? null : rd.GetString(4),
                        CreatedDate = rd.GetDateTime(5)
                    });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET ALL Account: " + ex.Message);
            }

            return list;
        }

        // INSERT
        public bool Insert(Account acc)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "INSERT INTO Account (Username, PasswordHash, Email, DisplayName) VALUES (@u, @p, @e, @d)", conn);

                cmd.Parameters.AddWithValue("@u", acc.Username);
                cmd.Parameters.AddWithValue("@p", acc.PasswordHash);
                cmd.Parameters.AddWithValue("@e", acc.Email);
                cmd.Parameters.AddWithValue("@d", acc.DisplayName ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi INSERT Account: " + ex.Message);
                return false;
            }
        }

        // UPDATE
        public bool Update(Account acc)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "UPDATE Account SET Username=@u, PasswordHash=@p, Email=@e, DisplayName=@d WHERE IDAccount=@id", conn);

                cmd.Parameters.AddWithValue("@id", acc.IDAccount);
                cmd.Parameters.AddWithValue("@u", acc.Username);
                cmd.Parameters.AddWithValue("@p", acc.PasswordHash);
                cmd.Parameters.AddWithValue("@e", acc.Email);
                cmd.Parameters.AddWithValue("@d", acc.DisplayName ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi UPDATE Account: " + ex.Message);
                return false;
            }
        }

        // DELETE
        public bool Delete(int id)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("DELETE FROM Account WHERE IDAccount=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi DELETE Account: " + ex.Message);
                return false;
            }
        }
    }
}
