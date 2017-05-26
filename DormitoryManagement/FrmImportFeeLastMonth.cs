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
    public partial class FrmImportFeeLastMonth : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string oldYearMonth;
        string yearMonth;
        DateTime firstdayInMonth;
        public FrmImportFeeLastMonth()
        {
            InitializeComponent();
        }

        //预览按钮
        private void button1_Click(object sender, EventArgs e)
        {
            string[] splitThing = new string[] { "\n", "\r\n", "\t" };
            string input = textBox1.Text.Trim();
            string[] records = input.Split(splitThing, StringSplitOptions.RemoveEmptyEntries);

            if (records.Count() < 2 || records.Count() % 2 != 0)
            {
                MessageBox.Show("格式不合法！");
                return;
            }

            string[] account = new string[records.Count() / 2];
            decimal[] money = new decimal[records.Count() / 2];

            for (int i = 0; i < records.Count(); i++)
            {
                if (i % 2 == 0)
                {
                    account[i / 2] = records[i];
                }
                else
                {
                    if (!decimal.TryParse(records[i], out money[(i - 1) / 2]))
                    {
                        MessageBox.Show("必须为数字：" + records[i]);
                        return;
                    }
                }
            }
            //清空数据
            dataGridView1.Rows.Clear();
            string result = "";
            int flag = 0;
            int? dormId = 0;
            for (int i = 0; i < account.Count(); i++)
            {
                if (account[i].Length < 5) { account[i] = "0" + account[i]; }  //如果是4位数的账号，在前面补0
                var emp = db.Employee.Where(em => em.account_number == account[i]);
                if (emp.Count() == 0)
                {
                    MessageBox.Show("在宿舍系统中不存在该账号：" + account[i]);
                    return;
                }
                else if (emp.Count() > 1)
                {
                    MessageBox.Show("在宿舍系统中存在重复账号：" + account[i]);
                    return;
                }
                if (emp.First().Lodging.Count() == 0)
                {//自离
                    result = "已自离，不能导入";
                    flag = -1;
                    dormId = 0;
                }
                else if (emp.First().Lodging.Where(l => l.out_date == null).Count() > 0)
                { //在住
                    result = "在住，可导入";
                    flag = 0;
                    dormId = emp.First().Lodging.Where(l => l.out_date == null).First().dorm_id;
                }
                else if (emp.First().Lodging.Where(l => l.out_date >= firstdayInMonth).Count() > 0)
                { //当月退宿
                    result = "当月退宿,可导入";
                    flag = 0;
                    dormId = emp.First().Lodging.Where(l => l.out_date >= firstdayInMonth).First().dorm_id;
                }
                else
                {
                    result = "之前已退宿，需会计部手工录入工资系统";
                    flag = 1;
                    dormId = 0;
                }
                dataGridView1.Rows.Add(i + 1, emp.First().Department1.name, emp.First().name, emp.First().account_number, money[i], result, flag, emp.First().id, dormId);
            }
            setBackColor();
        }

        private void setBackColor()
        {
            int flag;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                flag = Convert.ToInt32(row.Cells["flag"].Value);
                if (flag == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.YellowGreen;
                }
                else if (flag == -1)
                {
                    row.DefaultCellStyle.BackColor = Color.Orchid;
                }
                else if (flag == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.Goldenrod;
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        //保存结果
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int flag = Convert.ToInt32(row.Cells["flag"].Value);
                string account = Convert.ToString(row.Cells["账号"].Value);
                string name = Convert.ToString(row.Cells["姓名"].Value);
                decimal money = Convert.ToDecimal(row.Cells["金额"].Value);
                if (flag == 0)
                {
                    int dormId = Convert.ToInt32(row.Cells["dormId"].Value);
                    int empId = Convert.ToInt32(row.Cells["empId"].Value);
                    db.OtherFee.InsertOnSubmit(new OtherFee()
                    {
                        emp_id = empId,
                        dorm_id = dormId,
                        other_cost = money,
                        date = YearAndMonth.firstDayInMonth(yearMonth),
                        year_month = yearMonth,
                        comment = string.Format("补扣上月房租{0}元", money)
                    });
                }
                db.ExtraFee.InsertOnSubmit(new ExtraFee()
                {
                    account = account,
                    name = name,
                    fee = (double)money,
                    year_month = yearMonth
                });
            }
            db.SubmitChanges();
            MessageBox.Show("房租导入成功！");
            init();
        }

        private void FrmImportFeeLastMonth_Load(object sender, EventArgs e)
        {
            oldYearMonth = (from veri in db.VerifyOrder
                            where veri.is_verify == 1
                            orderby veri.year_and_month descending
                            select veri.year_and_month).First();
            yearMonth = YearAndMonth.next(oldYearMonth);
            firstdayInMonth = YearAndMonth.firstDayInMonth(yearMonth);

            init();
        }

        //如果已录入，则填充datagrid，初始化界面
        private void init()
        {
            dataGridView1.Rows.Clear();
            lbTitle.Text += "(" + oldYearMonth + ")";
            if (db.ExtraFee.Where(ex => ex.year_month == yearMonth).Count() > 0)
            {                
                button1.Enabled = button2.Enabled = false;
                lbTitle.Text += "(已导入)";
                int suc = 0;
                string result = "";
                int number = 0;
                foreach (ExtraFee ef in db.ExtraFee.Where(ex => ex.year_month == yearMonth))
                {
                    number++;
                    var emp = db.Employee.Where(em => em.account_number == ef.account).First();
                    if (emp.OtherFee.Where(o => o.year_month == yearMonth && o.other_cost == (decimal)ef.fee).Count() > 0)
                    {
                        result = "已导入";
                        suc = 0;
                    }
                    else if (emp.Lodging.Count() < 1)
                    {
                        result = "自离，未导入";
                        suc = -1;
                    }
                    else if (emp.Lodging.Where(l => l.out_date != null && l.out_date < firstdayInMonth).Count() > 0)
                    {
                        result = "之前退宿，未导入，需会计部手动录入";
                        suc = 1;
                    }
                    else
                    {
                        result = "已导入，可能因退宿关系金额被人为修改";
                        suc = 1;
                    }
                    dataGridView1.Rows.Add(number, emp.Department1.name, emp.name, emp.account_number, ef.fee, result, suc, 0, 0);
                }
                setBackColor();
            }
        }

        //删除未扣房租
        private void button3_Click(object sender, EventArgs e)
        {
            var extra = db.ExtraFee.Where(ex => ex.year_month == yearMonth);
            if (extra.Count() < 1) {
                MessageBox.Show("本月不存在上月未扣房租");
                return;
            }
            if (MessageBox.Show("确定要删除吗?", "确认删除", MessageBoxButtons.YesNo) == DialogResult.No) {
                return;
            }
            foreach (ExtraFee ef in extra) {
                var otherFee = db.OtherFee.Where(o => o.Employee.account_number == ef.account && o.year_month == yearMonth && o.other_cost == (decimal)ef.fee && o.comment.StartsWith("补扣上月房租"));
                db.OtherFee.DeleteAllOnSubmit(otherFee);
            }
            db.ExtraFee.DeleteAllOnSubmit(extra);
            db.SubmitChanges();
            MessageBox.Show("删除完成");
            init();
        }
    }
}
