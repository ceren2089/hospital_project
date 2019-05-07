using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    class sqlbaglantisi
    {
        public SqlConnection connection()
        {
            SqlConnection con = new SqlConnection("Data Source=CEREN-4GMQ01D;Initial Catalog=HastaneProje;Integrated Security=True");
            con.Open();
            return con;
        }
    }
}
