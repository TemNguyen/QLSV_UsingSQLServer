using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT03_NguyenDuyThinh_102190191
{
    class QLSV_BLL
    {
        public delegate bool Compare(SV s1, SV s2);
        public static QLSV_BLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QLSV_BLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static QLSV_BLL _Instance;
        public DataTable getAllSV_BLL(int ID_Lop, string NameSV)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV"),
                new DataColumn("NameSV"),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("NS", typeof(DateTime)),
                new DataColumn("NameLop")
            });
            foreach(var i in QLSV_DAL.Instance.getAllSV_DAL(ID_Lop, NameSV))
            {
                DataRow dr = dt.NewRow();
                dr["MSSV"] = i.MSSV;
                dr["NameSV"] = i.NameSV;
                dr["Gender"] = i.Gender;
                dr["NS"] = i.NS;
                dr["NameLop"] = QLSV_DAL.Instance.getNameLopByIDLop(i.ID_Lop);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public List<LopSH> getAllLSH_BLL()
        {
            return QLSV_DAL.Instance.getAllLSH_DAL();
        }
        public List<CBBItems> getSVProperties_BLL()
        {
            return QLSV_DAL.Instance.getSVProperties_DAL();
        }
        public bool addSV_BLL(SV s)
        {
            return QLSV_DAL.Instance.addSV_DAL(s);
        }
        public SV getSVByMSSV_BLL(string MSSV)
        {
            return QLSV_DAL.Instance.getSVByMSSV_DAL(MSSV);
        }
        public bool setSVByMSSV_BLL(SV s)
        {
            return QLSV_DAL.Instance.setSVByMSSV_DAL(s);
        }
        public bool deleteSVByMSSV_BLL(string MSSV)
        {
            return QLSV_DAL.Instance.deleteSVByMSSV_DAL(MSSV);
        }
        public DataTable sortSVBy_BLL(List<SV> s, string property)
        {
            Compare cmp;
            switch (property)
            {
                case "MSSV":
                    cmp = SV.cmpMSSV;
                    break;
                case "NameSV":
                    cmp = SV.cmpNameSV;
                    break;
                case "Gender":
                    cmp = SV.cmpGender;
                    break;
                case "NS":
                    cmp = SV.cmpNS;
                    break;
                case "ID_Lop":
                    cmp = SV.cmpID_Lop;
                    break;
                default:
                    cmp = SV.cmpMSSV;
                    break;
            }
            for (int i = 0; i < s.Count; i++)
            {
                for (int j = i + 1; j < s.Count; j++)
                {
                    if(cmp(s[i], s[j]))
                    {
                        SV temp = s[i];
                        s[i] = s[j];
                        s[j] = temp;
                    }
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV"),
                new DataColumn("NameSV"),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("NS", typeof(DateTime)),
                new DataColumn("NameLop")
            });
            foreach (var i in s)
            {
                DataRow dr = dt.NewRow();
                dr["MSSV"] = i.MSSV;
                dr["NameSV"] = i.NameSV;
                dr["Gender"] = i.Gender;
                dr["NS"] = i.NS;
                dr["NameLop"] = QLSV_DAL.Instance.getNameLopByIDLop(i.ID_Lop);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public List<SV> getListSVByListMSSV_BLL(List<string> list)
        {
            return QLSV_DAL.Instance.getListSVByListMSSV_DAL(list);
        }
        public bool isExistMSSV_BLL(string MSSV)
        {
            return QLSV_DAL.Instance.isExistMSSV_DAL(MSSV);
        }
    }
}
