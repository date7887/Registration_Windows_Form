using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSQL_App
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; username=root; password = root; database=date");

        // function connection us with database if we didn`t connect
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // function disconnect us with database if we were connected
        public void closedConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //dunction return value of connection
        public MySqlConnection getConnection()
        {
            return connection;
        }

    }
}
