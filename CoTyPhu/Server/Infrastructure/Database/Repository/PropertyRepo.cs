using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connection;
using Common.Domain.Models.Entities;

namespace Server.Infrastructure.Database.Repository
{
    public class PropertyRepo
    {
        private readonly DBConnection _db;

        public PropertyRepo(DBConnection db)
        {
            _db = db;
        }


        // Lấy 1 property
        public Property GetProperty(int matchId, int propertyId)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    SELECT IDProperty, IDMatch, Name, Value, Level, TypeProperty, PlayerID
                    FROM Property
                    WHERE IDMatch=@mid AND IDProperty=@pid", conn);

                cmd.Parameters.AddWithValue("@mid", matchId);
                cmd.Parameters.AddWithValue("@pid", propertyId);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Property
                    {
                        IDProperty = rd.GetInt32(0),
                        IDMatch = rd.GetInt32(1),
                        Name = rd.GetString(2),
                        Value = rd.GetInt32(3),
                        Level = rd.GetInt32(4),
                        TypeProperty = rd.GetString(5),
                        PlayerID = rd.IsDBNull(6) ? null : rd.GetInt32(6)
                    };
                }
            }
            catch { }

            return null;
        }


        // Lấy tất cả property của match
        public List<Property> GetByMatch(int matchId)
        {
            var list = new List<Property>();

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    SELECT IDProperty, IDMatch, Name, Value, Level, TypeProperty, PlayerID
                    FROM Property
                    WHERE IDMatch=@mid", conn);

                cmd.Parameters.AddWithValue("@mid", matchId);

                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new Property
                    {
                        IDProperty = rd.GetInt32(0),
                        IDMatch = rd.GetInt32(1),
                        Name = rd.GetString(2),
                        Value = rd.GetInt32(3),
                        Level = rd.GetInt32(4),
                        TypeProperty = rd.GetString(5),
                        PlayerID = rd.IsDBNull(6) ? null : rd.GetInt32(6)
                    });
                }
            }
            catch { }

            return list;
        }


        // Thêm một property mới
        public bool InsertProperty(Property p)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
            INSERT INTO Property(IDProperty, IDMatch, Name, Value, Level, TypeProperty, PlayerID)
            VALUES (@prop, @mid, @name, @value, @lv, @type, @pid)", conn);

                cmd.Parameters.AddWithValue("@prop", p.IDProperty);
                cmd.Parameters.AddWithValue("@mid", p.IDMatch);
                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@value", p.Value);
                cmd.Parameters.AddWithValue("@lv", p.Level);
                cmd.Parameters.AddWithValue("@type", p.TypeProperty);
                cmd.Parameters.AddWithValue("@pid", (object?)p.PlayerID ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }


        // Lấy theo loại (Property / Railroad / Utility)
        public List<Property> GetByType(int matchId, string type)
        {
            var list = new List<Property>();

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    SELECT IDProperty, IDMatch, Name, Value, Level, TypeProperty, PlayerID
                    FROM Property
                    WHERE IDMatch=@mid AND TypeProperty=@type", conn);

                cmd.Parameters.AddWithValue("@mid", matchId);
                cmd.Parameters.AddWithValue("@type", type);

                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new Property
                    {
                        IDProperty = rd.GetInt32(0),
                        IDMatch = rd.GetInt32(1),
                        Name = rd.GetString(2),
                        Value = rd.GetInt32(3),
                        Level = rd.GetInt32(4),
                        TypeProperty = rd.GetString(5),
                        PlayerID = rd.IsDBNull(6) ? null : rd.GetInt32(6)
                    });
                }
            }
            catch { }

            return list;
        }


        // Kiểm tra đất (chưa ai/mình/người khác)
        public (bool canBuy, bool isMine) CheckProperty(int matchId, int propertyId, int playerId)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    SELECT PlayerID
                    FROM Property
                    WHERE IDProperty=@prop AND IDMatch=@mid", conn);

                cmd.Parameters.AddWithValue("@prop", propertyId);
                cmd.Parameters.AddWithValue("@mid", matchId);

                var result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return (true, false); // chưa có chủ

                int owner = (int)result;

                if (owner == playerId)
                    return (false, true); // là của mình

                return (false, false);   // chủ là người khác
            }
            catch
            {
                return (false, false);
            }
        }


        // Đổi chủ sở hữu
        public bool UpdateOwner(int matchId, int propertyId, int? newOwner)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Property
                    SET PlayerID=@pid
                    WHERE IDMatch=@mid AND IDProperty=@prop", conn);

                cmd.Parameters.AddWithValue("@mid", matchId);
                cmd.Parameters.AddWithValue("@prop", propertyId);
                cmd.Parameters.AddWithValue("@pid", (object?)newOwner ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Đổi level nhà
        public bool UpdateLevel(int matchId, int propertyId, int newLevel)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Property
                    SET Level=@lv
                    WHERE IDMatch=@mid AND IDProperty=@prop", conn);

                cmd.Parameters.AddWithValue("@mid", matchId);
                cmd.Parameters.AddWithValue("@prop", propertyId);
                cmd.Parameters.AddWithValue("@lv", newLevel);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }


        // Reset đất của 1 player khi phá sản
        public bool ResetPlayerProperties(int matchId, int playerId)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand(@"
                    UPDATE Property
                    SET PlayerID=NULL, Level=0
                    WHERE IDMatch=@mid AND PlayerID=@pid", conn);

                cmd.Parameters.AddWithValue("@mid", matchId);
                cmd.Parameters.AddWithValue("@pid", playerId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }
    }
}
