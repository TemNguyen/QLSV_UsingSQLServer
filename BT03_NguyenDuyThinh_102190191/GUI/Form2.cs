using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT03_NguyenDuyThinh_102190191
{
    public partial class Form2 : Form
    {
        public delegate void MyDel(string MSSV);
        public MyDel Sender;
        public delegate void refreshData();
        public refreshData refresh;
        string MSSV = "";
        private void getMSSV(string _MSSV)
        {
            MSSV = _MSSV;
        }
        public Form2()
        {
            InitializeComponent();
            Sender = new MyDel(getMSSV);
            setCBBClass();
            rbtn_Male.Checked = true;
        }
        private void setCBBClass()
        {
            foreach (var i in QLSV_BLL.Instance.getAllLSH_BLL())
            {
                cbb_Class.Items.Add(new CBBItems
                {
                    Text = i.NameLop,
                    Value = i.ID_Lop
                });
            }
            cbb_Class.SelectedIndex = 0;
        }
        private SV getStudent()
        {
            SV s = new SV();
            s.MSSV = tb_StudentID.Text;
            s.NameSV = tb_Name.Text;
            if (rbtn_Female.Checked) s.Gender = false;
            else s.Gender = true;
            s.NS = Convert.ToDateTime(dateTimePicker1.Value);
            s.ID_Lop = ((CBBItems)cbb_Class.SelectedItem).Value;
            return s;
        }
        private void setStudent()
        {
            SV s = QLSV_BLL.Instance.getSVByMSSV_BLL(MSSV);
            tb_StudentID.Text = s.MSSV;
            tb_Name.Text = s.NameSV;
            if (s.Gender == true) rbtn_Male.Checked = true;
            else rbtn_Female.Checked = true;
            dateTimePicker1.Value = s.NS;
            cbb_Class.SelectedIndex = s.ID_Lop - 1;
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (MSSV == "")
            {
                if (QLSV_BLL.Instance.isExistMSSV_BLL(tb_StudentID.Text))
                {
                    MessageBox.Show("MSSV = " + tb_StudentID.Text + " đã tồn tại!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (tb_Name.Text == "" || tb_StudentID.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("Thêm thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QLSV_BLL.Instance.addSV_BLL(getStudent());
            }
            else
            {
                if (tb_Name.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("Chỉnh sửa thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QLSV_BLL.Instance.setSVByMSSV_BLL(getStudent());
            }
            refresh();
            this.Dispose();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            refresh();
            this.Dispose();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (MSSV != "")
            {
                setStudent();
                tb_StudentID.Enabled = false;
            }
        }
    }
}
