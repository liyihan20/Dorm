using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmSettle : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        int empId, dormId;

        public FrmSettle()
        {
            InitializeComponent();
        }

        private void FrmSettle_Load(object sender, EventArgs e)
        {
            cbClassifyType.Text = "计";
            dataBindings();       

        }

        private void dataBindings() {
            var emps = from tempL in db.TempLodging
                       orderby tempL.Employee.sex
                   select new
                   {
                       ID = tempL.user_id,
                       部门 = tempL.Employee.Department1.name,
                       姓名 = tempL.Employee.name,
                       性别 = tempL.Employee.sex,
                       工资类型 = tempL.Employee.salary_type,
                   };
            dataGridView1.DataSource = emps;

            //需要修改该datagridview的内容，不能用以上的方法，数据源只能用datatable，原因还不是很清楚。
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("宿舍编号", typeof(string));
            dt.Columns.Add("已住人数", typeof(Int32));
            dt.Columns.Add("剩余可住人数", typeof(Int32));
            dt.Columns.Add("宿舍类型", typeof(string));
            
            var dorms = from dor in db.Dorm
                        where dor.Area.name == LoginUser.operated_area
                        && dor.Lodging.Where(dorg=>dorg.out_date==null).Count() < dor.DormType.max_number
                        && dor.available == 0
                        select dor;
            foreach (Dorm dorm in dorms)
            {
                int livingNum = dorm.Lodging.Where(dorg => dorg.out_date == null).Count();
                dt.Rows.Add(dorm.id, dorm.number, livingNum,dorm.DormType.max_number - livingNum, dorm.DormType.name);
            }
            dataGridView2.DataSource = dt;
                       
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[dataGridView2.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                lbName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["姓名"].Value);
                empId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                lbNumber.Text = Convert.ToString(dataGridView2.CurrentRow.Cells["宿舍编号"].Value);
                dormId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["ID"].Value);
                var dorm = db.Dorm.Single(dor => dor.id == dormId);
                tbRent.Text = dorm.DormType.rent.ToString();
                tbManage.Text = dorm.DormType.manage_cost.ToString();
                tbGuarantee.Text = BaseInfo.guarantee.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //验证数据完整性
            if (string.IsNullOrEmpty(lbName.Text))
            {
                MessageBox.Show("请先选择需要分配宿舍的员工");
                return;
            }
            if (string.IsNullOrEmpty(lbNumber.Text))
            {
                MessageBox.Show("请先选择需要分配的宿舍");
                return;
            }
            decimal rent, manage, guarantee;
            if (!decimal.TryParse(tbRent.Text, out rent) || rent < 0)
            {
                MessageBox.Show("实际租金输入不合法");
                tbRent.Focus();
                return;
            }
            if (!decimal.TryParse(tbManage.Text, out manage) || manage < 0)
            {
                MessageBox.Show("管理费输入不合法");
                tbManage.Focus();
                return;
            }
            if (!decimal.TryParse(tbGuarantee.Text, out guarantee) || guarantee < 0)
            {
                MessageBox.Show("押金输入不合法");
                tbGuarantee.Focus();
                return;
            }

            //若员工已分配到预览datagrid，则删除该行
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (empId == Convert.ToInt32(row.Cells["ID"].Value))
                {
                    dataGridView1.Rows.Remove(row);
                    break;
                }
            }

            //若该宿舍被分配，如果可住人数为0，则删除该行，否则可住人数减1
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (dormId == Convert.ToInt32(row.Cells["ID"].Value))
                {
                    int num = Convert.ToInt32(row.Cells["剩余可住人数"].Value);
                    if (num > 1)
                        dataGridView2[3, row.Index].Value = num - 1;
                    else
                        dataGridView2.Rows.Remove(row);
                    break;
                }
            }

            //将分配预览结果放进datagridview3
            var emp = db.Employee.Single(em => em.id == empId);
            var dorm = db.Dorm.Single(dor => dor.id == dormId);
            dataGridView3.Rows.Add(dormId, empId, dorm.number, dorm.DormType.name, emp.name, emp.sex, emp.salary_type, tbRent.Text, tbManage.Text, tbGuarantee.Text, cbClassifyType.Text, DateTime.Now.ToShortDateString());

            //清空标签和输入框的内容
            lbName.Text = "";
            lbNumber.Text = "";
            tbGuarantee.Clear();
            tbRent.Clear();
            tbManage.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //清除datagridview3的内容，重新绑定datagridview1和datagridview2
            dataGridView3.Rows.Clear();
            dataBindings();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count < 1) {
                MessageBox.Show("没有需要保存的记录");
                return;
            }

            //将安排宿舍的结果批量插入住宿表
            List<Lodging> list = new List<Lodging>();
            foreach (DataGridViewRow row in dataGridView3.Rows) {
                list.Add(new Lodging() {
                    dorm_id=Convert.ToInt32(row.Cells["IDdorm"].Value),
                    emp_id=Convert.ToInt32(row.Cells["IDemp"].Value),
                    real_rent=Convert.ToDecimal(row.Cells["rent"].Value),
                    real_manage=Convert.ToDecimal(row.Cells["manageCost"].Value),
                    guarantee=Convert.ToDecimal(row.Cells["guarantee"].Value),
                    in_date=DateTime.Parse(DateTime.Now.ToShortDateString()),
                    classify_property=Convert.ToString(row.Cells["classifyType"].Value)
                });
            }
            db.Lodging.InsertAllOnSubmit(list);

            //将已经安排的员工从临时表中清除
            for (int i = 0; i < list.Count; i++) {
                var temps = db.TempLodging.Where(temp => temp.user_id == list[i].emp_id);
                db.TempLodging.DeleteAllOnSubmit(temps);
            }

            //提交事务
            db.SubmitChanges();
            MessageBox.Show("成功为" + list.Count() + "位员工安排宿舍");   
         
            //必须要关闭再重新打开窗口才可以刷新数据。。。
            FrmSettle settle = new FrmSettle();
            settle.MdiParent = this.MdiParent;
            settle.WindowState = FormWindowState.Maximized;
            settle.Show();
            this.Close();
        }
    }
}
