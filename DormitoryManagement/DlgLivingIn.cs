using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class DlgLivingIn : Form
    {
        public DataTable dt;
        public DlgLivingIn()
        {
            InitializeComponent();
        }

        private void DlgLivingIn_Load(object sender, EventArgs e)
        {
            DataTable datas = new DataTable();
            datas.Columns.Add("厂牌编号", typeof(string));
            datas.Columns.Add("姓名", typeof(string));
            datas.Columns.Add("性别", typeof(string));
            datas.Columns.Add("身份证", typeof(string));
            datas.Columns.Add("家庭地址", typeof(string));
            DataRow row = dt.NewRow();
            for (int i = 0; i < dt.Rows.Count; i++) {
                row = dt.Rows[i];
                datas.Rows.Add(Convert.ToString(row["emp_no"]), Convert.ToString(row["emp_name"]), Convert.ToString(row["sex"]), Convert.ToString(row["id_code"]), Convert.ToString(row["family_addr"]));
            }
            dataGridView1.DataSource = datas;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["性别"].Width = 60;
            dataGridView1.Columns["家庭地址"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmLivingIn liv = (FrmLivingIn)this.Owner;
            liv.lbRowNum.Text = e.RowIndex.ToString();
            liv.Show();
            this.Close();
        }
    }
}
