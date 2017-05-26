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
    public partial class FrmApplyList : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmApplyList()
        {
            InitializeComponent();
        }

        private void FrmApplyList_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“applyDataSet.dormitory_apply”中。您可以根据需要移动或删除它。
            this.dormitory_applyTableAdapter.Fill(this.applyDataSet.dormitory_apply);

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["handleMark"].ReadOnly = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows) {                
                if (Convert.ToBoolean(row.Cells["handleMark"].Value)) {
                    int id = Convert.ToInt32(row.Cells["idDataGridViewTextBoxColumn"].Value);
                    Apply ap = db.Apply.Single(a => a.id == id);
                    ap.handle_time = DateTime.Now;
                }
            }
            db.SubmitChanges();
            MessageBox.Show("保存成功");

            this.dormitory_applyTableAdapter.Fill(this.applyDataSet.dormitory_apply);
        }
    }
}
