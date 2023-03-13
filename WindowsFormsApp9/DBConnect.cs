using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp9
{
    class DBConnect
    {
        SqlConnection connection = new SqlConnection(@"Data Source=YOUR_DATA_SOURCE;Database=YOUR_DB;Integrated Security=true;");

        public void OpenConnection()
        {
            if (connection.State==System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void ClosedConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
