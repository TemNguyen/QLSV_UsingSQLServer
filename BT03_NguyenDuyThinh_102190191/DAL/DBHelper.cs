using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT03_NguyenDuyThinh_102190191
{
    class DBHelper
    {
        private static SqlConnection cnn;
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DBHelper();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static DBHelper _Instance;
        private DBHelper()
        {
            cnn = new SqlConnection();
            cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        }
        public DataTable GetRecord(string s)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(s, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cnn.Open();
            da.Fill(dt);
            cnn.Close();
            return dt;
        }
        public void ExcuteDB(string s)
        {
            SqlCommand cmd = new SqlCommand(s, cnn);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
