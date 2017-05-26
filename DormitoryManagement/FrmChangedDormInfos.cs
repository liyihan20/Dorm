using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmChangedDormInfos : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmChangedDormInfos()
        {
            InitializeComponent();
        }

        private void FrmChangedDormInfos_Load(object sender, EventArgs e)
        {
            cbYearMonth.DataSource = from ver in db.VerifyOrder
                                     where ver.can_export == 1
                                     orderby ver.year_and_month descending
                                     select ver.year_and_month;
            List<string> list = (from are in db.Area
                                    select are.name).ToList();
            list.Insert(0, "所有区");
            cbArea.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yearMonth = cbYearMonth.Text;
            string areaNum = cbArea.Text;
            if(areaNum.Equals("所有区"))
                dataGridView1.DataSource = db.VwDormsChanged.Where(vw => vw.年月份 == yearMonth).OrderBy(v=>v.账号);
            else
                dataGridView1.DataSource = db.VwDormsChanged.Where(vw => vw.年月份 == yearMonth).Where(vwd => vwd.区号 == areaNum).OrderBy(v => v.账号); 
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["区号"].Width = 60;
            dataGridView1.Columns["房号"].Width = 60;
            dataGridView1.Columns["部门"].Width = 80;
            dataGridView1.Columns["年月份"].Visible = false;
            dataGridView1.Columns["备注"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FrmChangedDormInfos_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
        }
    }
}
