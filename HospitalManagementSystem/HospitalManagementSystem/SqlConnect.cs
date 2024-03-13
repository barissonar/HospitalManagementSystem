using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HospitalManagementSystem
{
    internal class SqlConnect
    {
        public SqlConnection connection()   // Metodun Dönüş tipi SqlConnection 
        {

            SqlConnection connect = new SqlConnection("Data Source=LAPTOP-VKUUP6M9;Initial Catalog=HospitalManagament;Persist Security Info=True;User ID=sa;Password=0510987aAadc;TrustServerCertificate=True");
            connect.Open();
            return connect;


        }

    }
}
