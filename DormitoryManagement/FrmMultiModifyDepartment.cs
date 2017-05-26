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
    public partial class FrmMultiModifyDepartment : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmMultiModifyDepartment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] splitThing = new string[] { "\n","\r\n","\t"};
            string input = richTextBox1.Text.Trim();
            string[] records=input.Split(splitThing,StringSplitOptions.RemoveEmptyEntries);

            if (records.Count() < 2 || records.Count() % 2 != 0) {
                MessageBox.Show("error");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("账号", typeof(string));
            dt.Columns.Add("原部门", typeof(string));
            dt.Columns.Add("现部门", typeof(string));

            string account="", newDep="";
            int updateCount = 0;
            Department dep=null;
            Employee emp;
            for (int i = 0; i < records.Count() / 2; i++) {
                account = records[i*2].Trim();
                var emps = db.Employee.Where(em => em.account_number == account);
                if (emps.Count() < 1)
                {
                    continue;
                }
                else {
                    emp = emps.First();
                }
                if (!newDep.Equals(records[i*2 + 1].Trim())) {
                    newDep = records[i*2 + 1];
                    var deps = db.Department.Where(de => de.name == newDep);
                    if (deps.Count() < 1)
                    {
                        MessageBox.Show("error with department");
                        return;
                    }
                    else {
                        dep = deps.First();
                    }
                }
                dt.Rows.Add(emp.name, emp.account_number, emp.Department1.name, newDep);
                emp.Department1 = dep;
                updateCount++;
            }
            db.SubmitChanges();
            MessageBox.Show("update count:" + updateCount.ToString());
            dataGridView1.DataSource = dt;
        }
    }
}
