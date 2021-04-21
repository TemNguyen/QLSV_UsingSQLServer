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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setCBBClass();
            setCBBSort();
            refreshData();
        }
        private void setCBBClass()
        {
            cbb_Class.Items.Add(new CBBItems
            {
                Text = "All",
                Value = 0
            });
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
        private void setCBBSort()
        {
            foreach (var i in QLSV_BLL.Instance.getSVProperties_BLL())
            {
                cbb_Sort.Items.Add(i);
            }
            ((CBBItems)cbb_Sort.Items[cbb_Sort.Items.Count - 1]).Text = "NameLop";
            cbb_Sort.SelectedIndex = 0;
        }
        private void cbb_Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshData();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            refreshData();
        }
        private void btn_Show_Click(object sender, EventArgs e)
        {
            cbb_Class.SelectedIndex = 0;
            cbb_Sort.SelectedIndex = 0;
            textBox1.Text = "";
            refreshData();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            string MSSV = "";
            Form2 f2 = new Form2();
            f2.refresh = refreshData;
            f2.Sender(MSSV);
            f2.Show();
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (isNoRowSelected()) return;
            string MSSV = dataGridView1.CurrentRow.Cells["MSSV"].Value.ToString();
            Form2 f2 = new Form2();
            f2.refresh = refreshData;
            f2.Sender(MSSV);
            f2.Show();
        }
        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (isNoRowSelected()) return;
            DialogResult d = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch(d)
            {
                case DialogResult.Yes:
                    string MSSV = dataGridView1.CurrentRow.Cells["MSSV"].Value.ToString();
                    QLSV_BLL.Instance.deleteSVByMSSV_BLL(MSSV);
                    refreshData();
                    break;
                case DialogResult.No:
                    break;
            }    
        }
        private void btn_Sort_Click(object sender, EventArgs e)
        {
            string property = ((CBBItems)cbb_Sort.SelectedItem).Text;
            List<string> listMSSV = new List<string>();
            foreach(DataGridViewRow dgr in dataGridView1.Rows)
            {
                if (dgr.Cells["MSSV"].Value == null) break;
                    listMSSV.Add(dgr.Cells["MSSV"].Value.ToString());
            }
            dataGridView1.DataSource = QLSV_BLL.Instance.sortSVBy_BLL(QLSV_BLL.Instance.getListSVByListMSSV_BLL(listMSSV), property);
            dataGridView1.Columns[0].Visible = false;
        }
        private void refreshData()
        {
            int ID_Lop = ((CBBItems)cbb_Class.SelectedItem).Value;
            string NameSV = textBox1.Text;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = QLSV_BLL.Instance.getAllSV_BLL(ID_Lop, NameSV);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Rows[0].Selected = false;
        }
        private bool isNoRowSelected()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào được chọn!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }    

        }
    }
}
