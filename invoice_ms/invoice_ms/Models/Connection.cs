using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace invoice_ms.Models
{
    public static class Connection
    {
        public static SqlConnection con;

        public static SqlConnection getConnection() {
            
            if (con == null) {
                var conString = WebConfigurationManager.ConnectionStrings["invoice_con"].ConnectionString;
                con = new SqlConnection(conString);
                con.Open();
                return con;
            }

            return con;
            
        }
    }
}