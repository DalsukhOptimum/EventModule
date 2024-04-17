using System;
using System.Collections.Generic;
using System.Configuration;
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
            //databsse connection string
            string con = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            return con;

        }

    }
}
