using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT03_NguyenDuyThinh_102190191
{
    class QLSV_DAL
    {
        public static QLSV_DAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QLSV_DAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static QLSV_DAL _Instance;
        public List<SV> getAllSV_DAL(int ID_Lop, string NameSV)
        {
            List<SV> ListStudent = new List<SV>();
            string query = "select * from SV";
            foreach (DataRow dr in DBHelper.Instance.GetRecord(query).Rows)
            {
                if (ID_Lop == 0)
                {
                    if (NameSV != "")
                    {
                        if (dr["NameSV"].ToString().ToUpper().Contains(NameSV.ToUpper()))
                            ListStudent.Add(get1SV(dr));
                    }
                    else
                        ListStudent.Add(get1SV(dr));
                }
                else
                {
                    if (NameSV != "")
                    {
                        if (Convert.ToInt32(dr["ID_Lop"]) == ID_Lop && dr["NameSV"].ToString().ToUpper().Contains(NameSV.ToUpper()))
                            ListStudent.Add(get1SV(dr));
                    }
                    else
                    {
                        if (Convert.ToInt32(dr["ID_Lop"]) == ID_Lop)
                            ListStudent.Add(get1SV(dr));
                    }
                }
            }
            return ListStudent;
        }
        public List<LopSH> getAllLSH_DAL()
        {
            List<LopSH> lsh = new List<LopSH>();
            string query = "select * from LopSH";
            foreach(DataRow dr in DBHelper.Instance.GetRecord(query).Rows)
            {
                lsh.Add(get1LopSH(dr));
            }
            return lsh;
        }
        public List<CBBItems> getSVProperties_DAL()
        {
            List<CBBItems> l = new List<CBBItems>();
            string query = "select * from SV";
            int value = 1;
            foreach(DataColumn dc in DBHelper.Instance.GetRecord(query).Columns)
            {
                l.Add(new CBBItems()
                {
                    Text = dc.ColumnName,
                    Value = value
                });
                value++;
            }
            return l;
        }
        public bool addSV_DAL(SV s)
        {
            string query = "insert into SV values ('";
            query += s.MSSV + "', '" + s.NameSV + "', '" + s.Gender +
                "', '" + s.NS + "', '" + s.ID_Lop + "');";
            DBHelper.Instance.ExcuteDB(query);
            return true;
        }
        public SV getSVByMSSV_DAL(string MSSV)
        {
            string query = "select * from SV where MSSV = " + MSSV;
            return get1SV(DBHelper.Instance.GetRecord(query).Rows[0]);
        }
        public bool setSVByMSSV_DAL(SV s)
        {
            string query = "update SV set MSSV = '" + s.MSSV + "', NameSV = '" + s.NameSV + "', Gender = '" + s.Gender
                + "', NS = '" + s.NS + "', ID_Lop = '" + s.ID_Lop + "' where MSSV = '" + s.MSSV + "'";
            DBHelper.Instance.ExcuteDB(query);
            return true;
        }
        public bool deleteSVByMSSV_DAL(string MSSV)
        {
            string query = "delete from SV where MSSV = " + MSSV;
            DBHelper.Instance.ExcuteDB(query);
            return true;
        }
        public List<SV> getListSVByListMSSV_DAL(List<string> list)
        {
            List<SV> s = new List<SV>();
            string query = "select * from SV where MSSV = ";
            foreach(var i in list)
            {
                s.Add(get1SV(DBHelper.Instance.GetRecord(query + i).Rows[0]));
            }
            return s;
        }
        public bool isExistMSSV_DAL(string MSSV)
        {
            string query = "select * from SV where MSSV = " + MSSV;
            if (DBHelper.Instance.GetRecord(query).Rows.Count > 0) return true;
            return false;
        }
        public string getNameLopByIDLop(int ID_Lop)
        {
            string query = "select * from LopSH where ID_Lop = " + ID_Lop;
            DataRow dr = DBHelper.Instance.GetRecord(query).Rows[0];
            return dr["NameLop"].ToString();
        }
        private SV get1SV(DataRow dr)
        {
            SV s = new SV();
            s.MSSV = dr["MSSV"].ToString();
            s.NameSV = dr["NameSV"].ToString();
            s.Gender = Convert.ToBoolean(dr["Gender"]);
            s.NS = Convert.ToDateTime(dr["NS"]);
            s.ID_Lop = Convert.ToInt32(dr["ID_Lop"]);
            return s;
        }
        private LopSH get1LopSH(DataRow dr)
        {
            LopSH c = new LopSH();
            c.ID_Lop = Convert.ToInt32(dr["ID_Lop"]);
            c.NameLop = dr["NameLop"].ToString();
            return c;
        }
        
    }
}
