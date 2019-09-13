using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class AppContext
    {
        private SqlConnection _sqlConnection = null;

        public AppContext(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public SqlDataReader StartReader(string query)
        {
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            return cmd.ExecuteReader();
        }

        public int StartNonQuery(string sql)
        {
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
            return cmd.ExecuteNonQuery();
        }

        public int ExecuteScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, _sqlConnection);
            return Decimal.ToInt32((decimal)cmd.ExecuteScalar());
        }

        public void CloseConnection()
        {
            if (_sqlConnection.State!=ConnectionState.Closed)
                _sqlConnection.Close();
        }
    }
}
