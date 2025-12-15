using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Server.Infrastructure.Database.Connection
{
    public class DBConnection
    {
        public readonly string _connectionString =
            "Server=localhost;Database=MonopolyDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
