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
    }
}
