using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using Server.Infrastructure.Database.Connetion;
using Server.Infrastructure.Database.Models;

namespace Server.Infrastructure.Database.Repository
{
    public class PropertyRepo
    {
        private readonly DBConnection _db;

        public PropertyRepo(DBConnection db)
        {
            _db = db;
        }

        public Property GetById(int id)
        {
            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Property WHERE IDProperty=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return new Property
                    {
                        IDProperty = rd.GetInt32(0),
                        Name = rd.GetString(1),
                        Value = rd.GetInt32(2),
                        Level = rd.GetInt32(3)
                    };
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET Property: " + ex.Message);
            }

            return null;
        }

        public List<Property> GetAll()
        {
            var list = new List<Property>();

            try
            {
                using var conn = _db.GetConnection();
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Property", conn);
                using var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    list.Add(new Property
                    {
                        IDProperty = rd.GetInt32(0),
                        Name = rd.GetString(1),
                        Value = rd.GetInt32(2),
                        Level = rd.GetInt32(3)
                    });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi GET ALL Property: " + ex.Message);
            }

            return list;
        }
    }
}
