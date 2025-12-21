using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connection;
using Common.Domain.Models.Entities;

namespace Server.Infrastructure.Database.Repository
{
    public class AccountRepo
    {
        private readonly DBConnection _db;

        public AccountRepo(DBConnection db)
        {
            _db = db;
        }

        public Account GetById(int id)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "SELECT IDAccount, Username, PasswordHash, Email, DisplayName FROM Account WHERE IDAccount = @id",
                    conn);
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
                        DisplayName = rd.IsDBNull(4) ? null : rd.GetString(4)
                    };
                }
            }
            catch { }

            return null;
        }

        //Để đăng kí
        public bool CheckUsername(string username)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Account WHERE Username = @u",
                    conn);

                cmd.Parameters.AddWithValue("@u", username);

                return (int)cmd.ExecuteScalar() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckEmail(string email)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM Account WHERE Email=@e",
                    conn);

                cmd.Parameters.AddWithValue("@e", email);

                return (int)cmd.ExecuteScalar() > 0;
            }
            catch
            {
                return false;
            }
        }
        public int Insert(Account acc)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    INSERT INTO Account(Username, PasswordHash, Email, DisplayName)
                    OUTPUT INSERTED.IDAccount
                    VALUES(@u, @p, @e, @d)", conn);

                cmd.Parameters.AddWithValue("@u", acc.Username);
                cmd.Parameters.AddWithValue("@p", acc.PasswordHash);
                cmd.Parameters.AddWithValue("@e", acc.Email);
                cmd.Parameters.AddWithValue("@d", (object)acc.DisplayName ?? DBNull.Value);

                return (int)cmd.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }


        //Để đăng nhập
        public bool CheckLogin(string username, string passwordHash)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
            SELECT COUNT(1)
            FROM Account
            WHERE Username=@u AND PasswordHash=@p",
                    conn);

                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", passwordHash);

                return (int)cmd.ExecuteScalar() > 0;
            }
            catch
            {
                return false;
            }
        }


        public bool ChangePasswordByEmail(string email, string newPasswordHash)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var updateCmd = new SqlCommand(@"
            UPDATE Account
            SET PasswordHash=@new
            WHERE Email=@e", conn);

                updateCmd.Parameters.AddWithValue("@new", newPasswordHash);
                updateCmd.Parameters.AddWithValue("@e", email);

                return updateCmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }




        public bool Update(Account acc)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Account
                    SET Username=@u,
                        PasswordHash=@p,
                        Email=@e,
                        DisplayName=@d
                    WHERE IDAccount=@id", conn);

                cmd.Parameters.AddWithValue("@id", acc.IDAccount);
                cmd.Parameters.AddWithValue("@u", acc.Username);
                cmd.Parameters.AddWithValue("@p", acc.PasswordHash);
                cmd.Parameters.AddWithValue("@e", acc.Email);
                cmd.Parameters.AddWithValue("@d", (object)acc.DisplayName ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }


        public int GetIdByLogin(string username, string passwordHash)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
            SELECT IDAccount
            FROM Account
            WHERE Username = @u AND PasswordHash = @p", conn);

                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", passwordHash);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                    return (int)result;
            }
            catch
            {
                // log nếu cần
            }

            return -1; // login thất bại
        }

        //XÓA TÀI KHOẢN
        public bool Delete(int id)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(
                    "DELETE FROM Account WHERE IDAccount=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
