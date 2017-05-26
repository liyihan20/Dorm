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
    public partial class DlgMovingOut : Form
    {
        public Dorm dorm;
        public IQueryable<Employee> emps;

        public DlgMovingOut()
        {
            InitializeComponent();
        }

        private void DlgMovingOut_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("员工id", typeof(Int32));
            dt.Columns.Add("宿舍编号", typeof(string));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("厂牌编号", typeof(string));
            dt.Columns.Add("部门", typeof(string));
            if (dorm != null)
            {
                foreach (Lodging lodg in dorm.Lodging.Where(lo => lo.out_date == null))
                {
                    dt.Rows.Add(lodg.emp_id, lodg.Dorm.number, lodg.Employee.name, lodg.Employee.card_number, lodg.Employee.Department1.name);
                }
            }
            else if (emps != null) {
                foreach (var emp in emps) {
                    Lodging lod = emp.Lodging.Where(lo => lo.out_date == null).First();
                    dt.Rows.Add(emp.id, lod.Dorm.number, emp.name, emp.card_number, emp.Department1.name);
                }
            }
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FrmMovingOut mov=(FrmMovingOut)this.Owner;
            mov.lbEmpNo.Text =Convert.ToString(dataGridView1.CurrentRow.Cells["员工id"].Value);
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["员工id"].Visible = false;
            dataGridView1.Columns["部门"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
