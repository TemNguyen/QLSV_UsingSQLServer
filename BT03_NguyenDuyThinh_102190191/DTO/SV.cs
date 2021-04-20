using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT03_NguyenDuyThinh_102190191
{
    class SV
    {
        public string MSSV { get; set; }
        public string NameSV { get; set; }
        public bool Gender { get; set; }
        public DateTime NS { get; set; }
        public int ID_Lop { get; set; }

        public static bool cmpMSSV(SV s1, SV s2)
        {
            if (String.Compare(s1.MSSV, s2.MSSV) > 0) return true;
            return false;
        }
        public static bool cmpNameSV(SV s1, SV s2)
        {
            if (String.Compare(s1.NameSV, s2.NameSV) > 0) return true;
            return false;
        }
        public static bool cmpGender(SV s1, SV s2)
        {
            if (!s1.Gender && s2.Gender) return true;
            return false;
        }
        public static bool cmpNS(SV s1, SV s2)
        {
            if (s1.NS > s2.NS) return true;
            return false;
        }
        public static bool cmpID_Lop(SV s1, SV s2)
        {
            if (s1.ID_Lop > s2.ID_Lop) return true;
            return false;
        }
    }
}
