using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLotDAL.DataOperations
{
    public class InventoryDAL
    {
        private readonly string _connectionString;
        public InventoryDAL() : this(@"Data Source = (localdb)\mssqllocaldb; Initial Catalog = AutoLot; Integrated Security = True")
        {}
        public InventoryDAL(string connectionString)
            => _connectionString = connectionString;
        private SqlConnection _connection = null;
        private void OpenConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }
        private void CloseConnection()
        {
            if (_connection?.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
