using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace WindowsFormsApp9
{
    class Login
    {
        DBConnect db = new DBConnect();
        string login;
        string password;
        
        public Login(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
        public bool Enter()
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM LogPass WHERE Login = @login AND Password = @password", db.GetConnection());
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            db.OpenConnection();
            if (dataTable.Rows.Count == 1)
            {
                return true;

            }
            else
            {
                return false;
            }

            db.ClosedConnection();
        }
    }
}
