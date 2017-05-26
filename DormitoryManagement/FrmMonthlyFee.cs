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
    public partial class FrmMonthlyFee : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string oldYearMonth;
        string yearMonth;
        DateTime firstdayInMonth;
        DlgMonthlyFee dmf;

        public FrmMonthlyFee()
        {
            InitializeComponent();
        }

        //新增
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dmf == null || dmf.IsDisposed)
            {
                dmf = new DlgMonthlyFee();
            }
            dmf.Owner = this;
            dmf.ShowDialog();
            dmf.StartPosition = FormStartPosition.CenterParent;
            dmf.Text = "新增每月固定扣费";
        }

        //删除
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row.Index < 0)
            {
                MessageBox.Show("请选择需要删除的行");
                return;
            }
            if (MessageBox.Show("确定要删除吗？", "操作确认", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                int id = Convert.ToInt32(row.Cells["id"].Value);
                db.MonthlyFee.DeleteOnSubmit(db.MonthlyFee.Single(m => m.id == id));
                db.SubmitChanges();
                MessageBox.Show("删除成功");                
                
                init();
            }
        }

        //编辑
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row.Index < 0)
            {
                MessageBox.Show("请选择需要修改的行");
                return;
            }

            if (dmf == null || dmf.IsDisposed)
            {
                dmf = new DlgMonthlyFee();
            }
            dmf.Owner = this;            
            dmf.StartPosition = FormStartPosition.CenterParent;
            dmf.Text = "修改每月固定扣费";
            dmf.setFormData(Convert.ToInt32(row.Cells["id"].Value), Convert.ToString(row.Cells["dorm"].Value),
                Convert.ToString(row.Cells["empName"].Value), Convert.ToDecimal(row.Cells["fee"].Value),
                Convert.ToString(row.Cells["comment"].Value));
            dmf.ShowDialog();

        }

        //导入到费用
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int flag, empId, dormId;
            decimal fee;
            string comment;
            int number = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                flag = Convert.ToInt32(row.Cells["flag"].Value);
                if (flag == 0) {
                    number++;
                    empId = Convert.ToInt32(row.Cells["empId"].Value);
                    dormId = Convert.ToInt32(row.Cells["dormId"].Value); 
                    fee = Convert.ToDecimal(row.Cells["fee"].Value);
                    comment = Convert.ToString(row.Cells["comment"].Value);

                    OtherFee of = new OtherFee();
                    of.emp_id = empId;
                    of.dorm_id = dormId;
                    of.other_cost = fee;
                    of.comment = comment;
                    of.year_month = yearMonth;
                    of.date = DateTime.Now;

                    db.OtherFee.InsertOnSubmit(of);
                }
            }
            db.SubmitChanges();
            MessageBox.Show("成功导入到当月费用的记录数有：" + number.ToString());
            init();
        }

        //查询
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string searchVal = toolStripTextBox1.Text;
            if (string.IsNullOrWhiteSpace(searchVal)) {
                MessageBox.Show("必须填写搜索内容");
                return;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (Convert.ToString(row.Cells["dorm"].Value).Contains(searchVal) ||
                    Convert.ToString(row.Cells["empName"].Value).Contains(searchVal))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[1];
                    return;
                }
            }
            MessageBox.Show("未查询到符合条件的行");
        }

        //初始化界面
        public void init() {

            if (db.MonthlyFee.Count() > 0) {
                int flag = 0;
                string result = "";
                int number = 0;
                dataGridView1.Rows.Clear();
                var items = db.MonthlyFee.OrderByDescending(o => o.fee);
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, items);
                foreach (var item in items)
                {
                    number++;
                    if (db.OtherFee.Where(o => o.emp_id == item.emp_id && o.dorm_id == item.dorm_id && o.other_cost == item.fee && o.comment == item.comment && o.year_month==yearMonth).Count() > 0)
                    {
                        flag = 1;
                        result = "已导入：" + yearMonth;
                    }
                    else if (db.Lodging.Where(l => l.emp_id == item.emp_id && l.dorm_id == item.dorm_id).Count() < 1)
                    {
                        flag = -1;
                        result = "已自离，不能导入";
                    }
                    else if (db.Lodging.Where(l => l.emp_id == item.emp_id && l.dorm_id == item.dorm_id && l.out_date <= firstdayInMonth).Count() > 0)
                    {
                        flag = -1;
                        result = "本月之前已退宿，不能导入";
                    }
                    else
                    {
                        flag = 0;
                        result = "未导入：" + yearMonth;
                    }
                    dataGridView1.Rows.Add(number, item.dorm_number, item.emp_name, item.fee, item.comment, result, item.emp_id, item.dorm_id, item.id, flag);
                }
                setBackColor();
            }

        }

        //染色
        private void setBackColor()
        {
            int flag;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                flag = Convert.ToInt32(row.Cells["flag"].Value);
                if (flag == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.YellowGreen;
                }
                else if (flag == -1)
                {
                    row.DefaultCellStyle.BackColor = Color.Orchid;
                }                
            }
        }

        //加载窗体
        private void FrmMonthlyFee_Load(object sender, EventArgs e)
        {
            oldYearMonth = (from veri in db.VerifyOrder
                            where veri.is_verify == 1
                            orderby veri.year_and_month descending
                            select veri.year_and_month).First();
            yearMonth = YearAndMonth.next(oldYearMonth);
            firstdayInMonth = YearAndMonth.firstDayInMonth(yearMonth);

            init();
        }

    }

}
