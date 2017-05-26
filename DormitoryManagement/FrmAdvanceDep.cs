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
    public partial class FrmAdvanceDep : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmAdvanceDep()
        {
            InitializeComponent();
        }

        private void FrmAdvanceDep_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“advanceDepDataSet.dormitory_department”中。您可以根据需要移动或删除它。
            this.dormitory_departmentTableAdapter.Fill(this.advanceDepDataSet.dormitory_department);

            List<string> depsIn = (from de in db.Department
                                 orderby de.number
                                 select de.name).ToList();            
            depsIn.Insert(0, "");
            List<string> depsOut = new List<string>(depsIn);
            cbDepIn.DataSource = depsIn;
            cbDepOut.DataSource = depsOut;

            //写操作说明
            rtb1.AppendText("部门管理操作指引：\n");
            rtb1.AppendText("   1. 部门增加：在表格最后一行输入部门编号，名称，属性和备注，点击保存按钮\n");
            rtb1.AppendText("   2. 部门修改：直接在对应部门行修改，然后点击保存按钮则可\n");
            rtb1.AppendText("   3. 部门删除：点击删除按钮，再点击保存按钮（删除之前必须要将该部门员工进行转移，否则不可删除）");

            rtb2.AppendText("员工转移部门步骤指引：\n");
            rtb2.AppendText("   1. 在转出部门下拉框中输入或选择需要转出员工的部门，完成后下面列表显示该部门所有的员工\n");
            rtb2.AppendText("   2. 在转入部门下拉框中输入或选择需要转入员工的部门\n");
            rtb2.AppendText("   3. 可以通过鼠标、ctrl和shift组合选择需要转出的员工，点击转移部门按钮\n");
            rtb2.AppendText("   4. 确认无误后，点击保存，完成对员工的转移部门操作");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1) {
                if (dataGridView1.CurrentRow.IsNewRow) { return; }
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value);
                Department dep = db.Department.Single(de => de.id == id);
                if (dep.Employee.Count() > 0)
                {
                    MessageBox.Show("该部门还有员工，请先将这些员工转移部门后再删除！");
                    return;
                }
                else
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = dormitory_departmentTableAdapter.Update(advanceDepDataSet);
            MessageBox.Show("保存成功!");
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dataGridView1.CurrentRow.ErrorText = "";
            if (dataGridView1.CurrentRow.IsNewRow) { return; }
            if (e.ColumnIndex == 2) {                
                int cellValue;
                if (!Int32.TryParse(e.FormattedValue.ToString(), out cellValue)) {
                    e.Cancel = true;
                    dataGridView1.CurrentRow.ErrorText = "编号必须为正整数";
                }
            }
            if (e.ColumnIndex == 3) {
                string name = e.FormattedValue.ToString();
                for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                    if (i != e.RowIndex) {
                        if (name.Equals(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value))) {
                            e.Cancel = true;
                            dataGridView1.CurrentRow.ErrorText = "部门名不能重复";
                        }
                    }
                }
            }
        }        

        //部门转移
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbDepIn.Text.Trim())) {
                MessageBox.Show("请先选择要转入的部门");
                cbDepIn.Focus();
                return;
            }
            var selected = lbOut.SelectedItems;
            if (selected.Count > 0)
            {
                object[] objs = new Object[selected.Count];
                selected.CopyTo(objs, 0);
                lbIn.Items.AddRange(objs);
                foreach (var s in objs)
                {
                    lbOut.Items.Remove(s);
                }
            }
        }

        //撤销部门转移
        private void button4_Click(object sender, EventArgs e)
        {
            var selected = lbIn.SelectedItems;
            if (selected.Count > 0)
            {
                object[] objs = new Object[selected.Count];
                selected.CopyTo(objs, 0);
                foreach (var s in objs)
                {
                    lbOut.Items.Add(s);
                    lbIn.Items.Remove(s);
                }
            }
        }

        //保存部门变动
        private void button3_Click(object sender, EventArgs e)
        {
            var items = lbIn.Items;
            int empId;
            string account;
            string name;
            var depIn = db.Department.Where(de => de.name == cbDepIn.Text);
            int depOutId;
            if (depIn.Count() < 1)
            {
                MessageBox.Show("该部门不存在");
                return;
            }
            else {
                depOutId = depIn.First().id;
            }
            if (MessageBox.Show(string.Format("确定要将这{0}位员工从{1}调到{2}吗？", items.Count, cbDepOut.Text, cbDepIn.Text), "警告", MessageBoxButtons.YesNo) == DialogResult.No) {
                return;
            }
            foreach (string item in items) {
                var arr =item.Split(new char[] { '|' });
                
                empId = Convert.ToInt32(arr[0]);
                name = arr[1];
                account = arr[2];
                var emp = db.Employee.Single(em => em.id == empId);
                emp.department = depOutId;

                //写入部门转换日志 
                MyUtil utl = new MyUtil();
                utl.WriteChangeDepLog(name, account, Convert.ToString(cbDepOut.Text), cbDepIn.Text);
                //IQueryable<Charge> charges;
                //if (string.IsNullOrEmpty(arr[2]))
                //{
                //    charges = db.Charge.Where(ch => ch.employee == name);
                //}
                //else
                //{
                //    charges = db.Charge.Where(ch => ch.account == account);
                //}
                //foreach (var cha in charges) {
                //    cha.department_id = depOutId;
                //}
            }
            db.SubmitChanges();
            MessageBox.Show("员工部门调整成功");

        }

        private void cbDepIn_Leave(object sender, EventArgs e)
        {
            lbIn.Items.Clear();
        }

        private void cbDepOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbOut.Items.Clear();
            string dep = cbDepOut.Text.Trim();
            string item;
            if (string.IsNullOrEmpty(dep)) { return; }
            var emps = from emp in db.Employee
                       where emp.Department1.name.Equals(dep)
                       orderby emp.account_number
                       select emp;
            
            foreach (var emp in emps)
            {
                if (emp.account_number != null)
                {
                    item = emp.id + "|" + emp.name + "|" + emp.account_number;
                }
                else
                {
                    item = emp.id + "|" + emp.name;
                }
                if (!string.IsNullOrEmpty(emp.card_number))
                {
                    item += "|" + emp.card_number;
                }
                lbOut.Items.Add(item);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string thing = tbSearch.Text;
            string account;
            string name;
            string item;
            if (string.IsNullOrWhiteSpace(thing)) {
                return;
            }
            for (int i = 0; i < lbOut.Items.Count; i++) {
                item = lbOut.Items[i].ToString();
                var arr = item.Split(new char[] { '|' });
                name = arr[1];
                account = arr[2];
                if (name.Contains(thing) || account.Contains(thing)) {
                    lbOut.SelectedIndex = i;
                    return;
                }
            }
        }
      
    
      
    }
}
