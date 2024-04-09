using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Connection
    {


        public Connection()
        {

        }

        public static string GetConnectionString()
        {
            string con = "Data Source=OPTIMUM98\\SQLEXPRESS;Initial Catalog=EventModule;Integrated Security=SSPI";
            return con;

        }

    }
}
