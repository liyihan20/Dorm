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
    public partial class FrmChangeToSalaryDep : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmChangeToSalaryDep()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lastMonth = db.VerifyOrder.OrderByDescending(v => v.id).First().year_and_month;
            DateTime firstDay = YearAndMonth.firstDayInMonth(YearAndMonth.next(lastMonth));
            try
            {
                if (db.getDiffDep(firstDay).Count() == 0) {
                    MessageBox.Show("没有需要转换部门的员工");
                    return;
                }
                dataGridView1.DataSource = (from d in db.getDiffDep(firstDay)
                                            select new
                                            {
                                                empId = d.empId,
                                                宿舍号 = d.number,
                                                姓名 = d.empName,
                                                账号 = d.account_number,
                                                原部门 = d.depName,
                                                工资部门 = d.salaryDep,
                                                newDepId = d.new_dep_id
                                            }).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("数据库繁忙，请稍后再查询。");
                MyUtil.WriteEventLog("员工部门更新", "", "", "数据库繁忙，请稍后再查询", false);
            }
            
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["empId"].Visible = false;
            dataGridView1.Columns["newDepId"].Visible = false;
            dataGridView1.Columns["原部门"].Width = 200;
            dataGridView1.Columns["工资部门"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyUtil utl = new MyUtil();
            if (dataGridView1.Rows.Count < 1) {
                MessageBox.Show("请先查询出需要转换部门的人员在操作");
                return;
            }
            if (MessageBox.Show("确定要更新以下列表中的人的部门吗?", "确认对话框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) {
                return;
            }
            int i = 0;
            int empId;
            int newDepId;
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (row.Cells["newDepId"].Value != DBNull.Value && row.Cells["newDepId"].Value != null) {
                    i++;
                    newDepId = Convert.ToInt32(row.Cells["newDepId"].Value);
                    empId = Convert.ToInt32(row.Cells["empId"].Value);
                    Employee emp = db.Employee.Single(em => em.id == empId);
                    emp.department = newDepId;
                    //转化最近一个月结算的结算表中的部门
                    if (!string.IsNullOrWhiteSpace(emp.account_number))
                    {
                        string yearMonth = db.VerifyOrder.OrderByDescending(v => v.id).First().year_and_month;
                        var charges = db.Charge.Where(c => c.account == emp.account_number && c.year_month == yearMonth);
                        foreach (var cha in charges)
                        {
                            cha.department_id = newDepId;
                            cha.department = Convert.ToString(row.Cells["工资部门"].Value);
                            cha.property = db.Department.Single(d => d.id == newDepId).property;
                        }
                    }
                    //写入部门转换日志                    
                    utl.WriteChangeDepLog(emp.name, emp.account_number, Convert.ToString(row.Cells["原部门"].Value), Convert.ToString(row.Cells["工资部门"].Value));
                }
            }
            db.SubmitChanges();
            MessageBox.Show("成功更新部门的人数有：" + i + "人");
            MyUtil.WriteEventLog("员工部门更新", "", "", "成功更新部门的人数有：" + i);
            //button1.PerformClick();
        }

        private void FrmChangeToSalaryDep_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 40;
        }
    }
}
