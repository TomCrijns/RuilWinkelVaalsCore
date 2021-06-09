using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuilWinkelVaals.UITests
{
    public static class DBDataHelper
    {
        public static string GetSaltFromDB()
        {
            string connString = "Server=DamelotSVR.Damelot.com; Database=DB_DevOps;User Id=sa;Password=Acce$$dbg01!;";
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT Salt FROM AccountData WHERE ProfileId = 3");
            cmd.Connection = conn;
            conn.Open();
            string salt = (string)cmd.ExecuteScalar();
            conn.Close();
            return salt;
        }
    }
}
