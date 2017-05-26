using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace DormitoryManagement
{
    public partial class FrmBlackList : FormWithID
    {
        DormDBDataContext db = new DormDBDataContext();
        int inEmpId;
        public FrmBlackList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string content = tbCardId.Text.Trim();
            Employee emp = null;
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("请先输入搜索内容");
                tbCardId.Focus();
                return;
            }
            var emps = db.Employee.Where(em => em.card_number.Equals(content) || em.name.Equals(content));
            if (emps.Count() < 1) {
                MessageBox.Show("查询不到该员工");
                tbCardId.Focus();
                return;
            }
            else if (emps.Count() > 1)
            {
                FrmEmpSearchResult searchForm = new FrmEmpSearchResult(emps, this);
                searchForm.ShowDialog();
                emp = db.Employee.Single(em => em.id == this.myId);
            }
            else
            {
                emp = emps.First();
            }
            inEmpId = emp.id;
            lbName.Text = emp.name;
            lbIdentify.Text = emp.identify_number;
            lbSex.Text = emp.sex;
            lbAddr.Text = emp.household;

            if (emp.picture != null && emp.picture.Length > 1)
            {
                byte[] imageByte = emp.picture.ToArray();
                using (MemoryStream ms = new MemoryStream(imageByte))
                {
                    Bitmap bm = new Bitmap(ms);
                    pbHead.Image = bm;
                }
            }
            else
            {
                pbSelected.Image = Properties.Resources.DefaultImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(lbName.Text.Trim())){
                MessageBox.Show("请先查询员工信息");
                return;
            }

            string inReason = tbInReason.Text.Trim();

            //判断是否已经存在与黑名单之中
            if (db.BlackList.Where(bl => bl.emp_id == inEmpId).Where(bla => bla.out_date == null).Count() > 0) {
                MessageBox.Show("该员工已存在于黑名单之中");
                return;
            }

            BlackList black = new BlackList();
            black.emp_id = inEmpId;
            black.in_date = DateTime.Now;
            black.in_operator = LoginUser.username;
            black.in_reason = inReason;
            db.BlackList.InsertOnSubmit(black);
            db.SubmitChanges();
            MyUtil.WriteEventLog("黑名单管理", "", lbName.Text, "添加至黑名单："+inReason);
            dataGridView1.Rows.Add(false, black.Employee.name, black.Employee.sex, black.Employee.card_number, black.Employee.household, black.in_date, black.in_reason, black.id);
        }

        private void FrmBlackList_Load(object sender, EventArgs e)
        {
            var list = db.BlackList.Where(bl => bl.out_date == null);
            foreach (BlackList black in list) {
                dataGridView1.Rows.Add(false, black.Employee.name, black.Employee.sex, black.Employee.card_number, black.Employee.household, black.in_date, black.in_reason,black.id);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                return;
            int thisId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
            Employee thisEmp = db.BlackList.Single(b => b.id == thisId).Employee;
            if (thisEmp.picture != null && thisEmp.picture.Length > 1)
            {
                byte[] imageByte = thisEmp.picture.ToArray();
                using (MemoryStream ms = new MemoryStream(imageByte))
                {
                    Bitmap bm = new Bitmap(ms);
                    pbSelected.Image = bm;
                }
            }
            else {
                pbSelected.Image = Properties.Resources.DefaultImage;
            }

            dataGridView1.CurrentRow.Cells["选择"].Value = !Convert.ToBoolean(dataGridView1.CurrentRow.Cells["选择"].Value);
            if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells["选择"].Value))
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SkyBlue;
                lbPeople.Items.Add(thisEmp.name);
            }
            else {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                lbPeople.Items.Remove(thisEmp.name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string outReason = tbOutReason.Text.Trim();
            int blackId;
            List<int> selectedRowIndex = new List<int>();
            BlackList black;
            if (dataGridView1.Rows.Count < 1)
                return;

            //循环更新黑名单记录
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (Convert.ToBoolean(row.Cells["选择"].Value)) {
                    blackId = Convert.ToInt32(row.Cells["id"].Value);
                    black = db.BlackList.Single(bl => bl.id == blackId);
                    black.out_date = DateTime.Now;
                    black.out_operator = LoginUser.username;
                    black.out_reason = outReason;
                    db.SubmitChanges();
                    selectedRowIndex.Add(row.Index);
                    MyUtil.WriteEventLog("黑名单管理", "", lbName.Text, "移出黑名单："+outReason);
                }
            }

            //自后向前删除选中的行
            for (int i = selectedRowIndex.Count - 1; i >= 0; i--) {
                dataGridView1.Rows.RemoveAt(selectedRowIndex[i]);
            }
        }

        private void FrmBlackList_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string content = tbSearchList.Text;
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (row.Cells["姓名"].Value.ToString().Contains(content) || (row.Cells["厂牌号码"].Value != null && row.Cells["厂牌号码"].Value.ToString().Contains(content)))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[1];
                    return;
                }
            }
        }        
      
    }
}
