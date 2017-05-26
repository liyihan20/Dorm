using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace DormitoryManagement
{
    public partial class FrDormInfop : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        AutoCompleteStringCollection autoDorms;
        AutoCompleteStringCollection autoDepts;
        string dormNumber;
        public FrDormInfop()
        {
            InitializeComponent();
        }

        //查询按钮
        private void button1_Click(object sender, EventArgs e)
        {
            string dormName = tbDormNumber.Text.Trim();
            if (string.IsNullOrEmpty(dormName))
            {
                MessageBox.Show("请输入宿舍编号");
                return;
            }

            dormName = dormName.ToUpper();

            var dorms = db.Dorm.Where(dor => dor.number == dormName);
            if (dorms.Count() < 1)
            {
                MessageBox.Show("不存在该宿舍");
                return;
            }

            //必须让焦点停留在一单元格里，否则修改信息时会出错
            if (dgvLiving.Rows.Count > 0) {
                dgvLiving.CurrentCell = dgvLiving[1,0];
            }

            var dorm = dorms.First();
            dormNumber = dorm.number;
            var living = dorm.Lodging.Where(lod => lod.out_date == null);
            var checkout = dorm.Lodging.Where(lod => lod.out_date != null);
            string nowDay = YearAndMonth.toLong(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString());
            //填充lable
            lbAreaName.Text = dorm.Area.name;
            lbDormSex.Text = dorm.dormSex;
            lbDormComment.Text = dorm.comment;
            lbDormTypeName.Text = dorm.DormType.name;
            lbMaxLivingNumber.Text = dorm.DormType.max_number.ToString();
            lbNowLivingNumber.Text = living.Count().ToString();
            lbRemainingNumber.Text = (dorm.DormType.max_number - living.Count()).ToString();
            lbRent.Text = dorm.rent.ToString();
            lbManageCost.Text = dorm.manageCost.ToString();

            //填充在住员工列表
            DataTable dt = new DataTable();
            dt.Columns.Add("房号", typeof(string));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("部门", typeof(string));
            dt.Columns.Add("入住日期", typeof(string));
            dt.Columns.Add("账号", typeof(string));
            dt.Columns.Add("厂牌号码", typeof(string));                        
            dt.Columns.Add("户籍", typeof(string));
            //隐藏列：
            dt.Columns.Add("性别", typeof(string));
            dt.Columns.Add("住宿id", typeof(Int32));
            dt.Columns.Add("员工id", typeof(Int32));

            foreach (Lodging liv in db.Lodging.Where(lod => lod.out_date == null).Where(lodg => lodg.Dorm.number == dormName))
            {
                dt.Rows.Add(liv.Dorm.number, liv.Employee.name, liv.Employee.Department1.name, ((DateTime)liv.in_date).ToShortDateString(), liv.Employee.account_number, liv.Employee.card_number, liv.Employee.household, liv.Employee.sex, liv.id, liv.emp_id);
            }
            dgvLiving.DataSource = dt;

            //填充退宿员工列表
            DataTable dtLeave = new DataTable();
            dtLeave.Columns.Add("姓名", typeof(string));
            dtLeave.Columns.Add("部门", typeof(string));
            dtLeave.Columns.Add("厂牌号码", typeof(string));
            dtLeave.Columns.Add("账号", typeof(string));
            dtLeave.Columns.Add("入住日期", typeof(string));
            dtLeave.Columns.Add("退宿日期", typeof(string));
            dtLeave.Columns.Add("户籍", typeof(string));

            foreach (Lodging lod in (from ou in db.Lodging
                                     where ou.out_date != null
                                     && ou.Dorm.number == dormName
                                     orderby ou.out_date descending
                                     select ou)) {
                                         dtLeave.Rows.Add(lod.Employee.name, lod.Employee.Department1.name, lod.Employee.card_number, lod.Employee.account_number, ((DateTime)lod.in_date).ToShortDateString(), ((DateTime)lod.out_date).ToShortDateString(), lod.Employee.household);
            }

            foreach (AutoQuit quit in db.AutoQuit.Where(au => au.Dorm.number == dormName)) {
                dtLeave.Rows.Add(quit.Employee.name, quit.Employee.Department1.name, quit.Employee.card_number, quit.Employee.account_number, ((DateTime)quit.in_date).ToShortDateString(), ((DateTime)quit.out_date).ToShortDateString(), quit.Employee.household);
            }

            dgvCheckOut.DataSource = dtLeave;
            dgvCheckOut.Columns["户籍"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //填充费用列表，只显示当月的信息
            getFeeGridData(nowDay);
            
            cbMonth.Text = cbMonth.Items[0].ToString();

            MyUtil.WriteEventLog("宿舍查询", dormNumber, "", "");
        }

        //初始化界面
        private void FrDormInfop_Load(object sender, EventArgs e)
        {
            foreach (Control ct in groupBox1.Controls)
            {
                if (ct.Name.StartsWith("lb"))
                {
                    ct.ForeColor = Color.FromArgb(0, 84, 227);
                }
            }
            //设置房间编号的自动提示集合
            var dormNums = from dor in db.Dorm select dor.number;
            autoDorms = new AutoCompleteStringCollection();
            foreach (string item in dormNums)
            {
                autoDorms.Add(item);
            }
            //设置部门名称的自动提示集合
            var depts = from dep in db.Department select dep.name;
            autoDepts = new AutoCompleteStringCollection();
            foreach (string item in depts)
            {
                autoDepts.Add(item);
            }

            //其他月份查询的combolist
            string firstMonth = db.VerifyOrder.Where(v => v.is_verify == 1).First().year_and_month;
            string lastMonth = YearAndMonth.getYearAndMonth(DateTime.Now);
            cbMonth.Items.Add(lastMonth);
            while (!YearAndMonth.previous(lastMonth).Equals(firstMonth)) {
                lastMonth = YearAndMonth.previous(lastMonth);
                cbMonth.Items.Add(lastMonth);
            }
            cbMonth.Items.Add(firstMonth);
            cbMonth.Text = cbMonth.Items[0].ToString();
        }

        //在住表格格式设置
        private void dgvLiving_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvLiving.Columns["房号"].Width = 80;
            dgvLiving.Columns["账号"].Width = 80;
            //dgvLiving.Columns["厂牌号码"].ReadOnly = true;
            //dgvLiving.Columns["账号"].ReadOnly = true;
            dgvLiving.Columns["户籍"].ReadOnly = true;
            dgvLiving.Columns["户籍"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLiving.Columns["性别"].Visible = false;
            dgvLiving.Columns["住宿id"].Visible = false;
            dgvLiving.Columns["员工id"].Visible = false;

            dgvLiving.Columns["房号"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvLiving.Columns["姓名"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvLiving.Columns["部门"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvLiving.Columns["入住日期"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvLiving.Columns["账号"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvLiving.Columns["厂牌号码"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvLiving.Columns["户籍"].DefaultCellStyle.BackColor = Color.LightSeaGreen;
        }

        //修改在住员工验证
        private void dgvLiving_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dgvLiving.Rows[e.RowIndex].ErrorText = "";
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                if (dgvLiving.Rows[e.RowIndex].IsNewRow) { return; }
                string cellValue = e.FormattedValue.ToString().Trim();
                //验证房号
                if (e.ColumnIndex == 0)
                {
                    var dorms = db.Dorm.Where(dor => dor.number == cellValue);
                    if (dorms.Count() < 1)
                    {
                        e.Cancel = true;
                        dgvLiving.Rows[e.RowIndex].ErrorText = "该房号不存在";
                    }
                    else
                    {
                        Dorm dorm = dorms.First();
                        if (!dorm.dormSex.Equals("不限") && !dorm.dormSex.Equals(dgvLiving.CurrentRow.Cells["性别"].Value.ToString()))
                        {
                            e.Cancel = true;
                            dgvLiving.Rows[e.RowIndex].ErrorText = "男女不能混住";
                        }
                        if (dorm.Lodging.Where(lod => lod.out_date == null).Count() == dorm.DormType.max_number && !dorm.number.Equals(dormNumber))
                        {
                            e.Cancel = true;
                            dgvLiving.Rows[e.RowIndex].ErrorText = "该房间已注满";
                        }

                    }

                }
                //验证部门
                else if (e.ColumnIndex == 2)
                {
                    cellValue = cellValue.ToUpper();
                    if (db.Department.Where(dep => dep.name == cellValue).Count() < 1)
                    {
                        e.Cancel = true;
                        dgvLiving.Rows[e.RowIndex].ErrorText = "该部门不存在";
                    }
                }
                else if (e.ColumnIndex == 3) {
                    DateTime dateValue;
                    if (!DateTime.TryParse(cellValue, out dateValue)) {
                        e.Cancel = true;
                        dgvLiving.Rows[e.RowIndex].ErrorText = "日期输入不正确";
                    }
                }
            }
        }

        private void dgvLiving_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //设置自动提示功能
            if (dgvLiving.CurrentCell.ColumnIndex == 0)
            {
                ((TextBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((TextBox)e.Control).AutoCompleteSource = AutoCompleteSource.CustomSource;
                ((TextBox)e.Control).AutoCompleteCustomSource = autoDorms;
            }
            if (dgvLiving.CurrentCell.ColumnIndex == 2)
            {
                ((TextBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((TextBox)e.Control).AutoCompleteSource = AutoCompleteSource.CustomSource;
                ((TextBox)e.Control).AutoCompleteCustomSource = autoDepts;
            }
        }

        //保存在住员工信息的修改
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvLiving.Rows.Count; i++)
            {
                Lodging lodg = db.Lodging.Single(lo => lo.id == Convert.ToInt32(dgvLiving.Rows[i].Cells["住宿id"].Value));
                Dorm dorm = db.Dorm.Single(dor => dor.number ==Convert.ToString(dgvLiving.Rows[i].Cells["房号"].Value));
                Department dep = db.Department.Single(de => de.name == Convert.ToString(dgvLiving.Rows[i].Cells["部门"].Value));
                Employee emp = db.Employee.Single(em => em.id == Convert.ToInt32(dgvLiving.Rows[i].Cells["员工id"].Value));
                emp.name = Convert.ToString(dgvLiving.Rows[i].Cells["姓名"].Value);
                if (emp.department != dep.id)
                {
                    //写入部门转换日志
                    MyUtil utl = new MyUtil();
                    utl.WriteChangeDepLog(emp.name, emp.account_number, emp.Department1.name, dep.name);

                    emp.Department1 = dep;
                    //不需要更新结算表的部门。
                    //IQueryable<Charge> charges;
                    //if (emp.account_number != null)
                    //{
                    //    charges = db.Charge.Where(ch => ch.account == emp.account_number);
                    //}
                    //else {
                    //    charges = db.Charge.Where(ch => ch.employee == emp.name &&  string.IsNullOrEmpty(ch.account));
                    //}
                    //foreach (var cha in charges) {
                    //    cha.department_id = dep.id;
                    //    cha.department = dep.name;
                    //    cha.property = dep.property;
                    //}
                }
                emp.account_number = Convert.ToString(dgvLiving.Rows[i].Cells["账号"].Value);
                emp.card_number = Convert.ToString(dgvLiving.Rows[i].Cells["厂牌号码"].Value);
                lodg.in_date = Convert.ToDateTime(dgvLiving.Rows[i].Cells["入住日期"].Value);
                lodg.Dorm = dorm;
                lodg.real_rent = dorm.rent;
                lodg.real_manage = dorm.manageCost;
                lodg.Employee = emp;

            }
            db.SubmitChanges();
            MessageBox.Show("信息保存成功");
            MyUtil.WriteEventLog("查询宿舍", dormNumber, "", "修改保存宿舍员工信息");
        }

        private void dgvLiving_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
                dgvLiving.CurrentCell.Value = dgvLiving.CurrentCell.Value.ToString().ToUpper();
        }

        //填充费用列表数据
        public void getFeeGridData(string yearMonth) {
            if (dormNumber != null)
            {
                dvgFeeList.DataSource = from fe in db.OtherFee
                                        where fe.Dorm.number == dormNumber
                                        && fe.year_month == yearMonth
                                        select new
                                        {
                                            id = fe.id,
                                            年月份 = fe.year_month.Substring(0, 4) + "-" + fe.year_month.Substring(4, 2),
                                            姓名 = fe.Employee.name,
                                            维修费 = fe.repair_cost,
                                            罚款 = fe.fine,
                                            招待所 = fe.guesthouse_cost,
                                            其他费用 = fe.other_cost,
                                            分摊电费 = fe.out_share_eletricity,
                                            分摊水费 = fe.out_share_water,
                                            费用备注 = fe.comment
                                        };
            }
        }

        //费用列表月份选择框改变事件
        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            getFeeGridData(cbMonth.Text);            
        }

        private void dvgFeeList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dvgFeeList.Columns["id"].Visible = false;
            for (int i = 2; i < 10; i++) {
                dvgFeeList.Columns[i].Width = 76;
            }            
            dvgFeeList.Columns["费用备注"].Width = 300;
        }

        //双击显示员工信息
        private void dgvLiving_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6) {
                FrmEmployeeInfo emp = new FrmEmployeeInfo();
                emp.Show();
                emp.lbID.Text = Convert.ToString(dgvLiving.Rows[e.RowIndex].Cells["员工id"].Value);
            }
        }

        private void FrDormInfop_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2 - 40;
        }

        private void dvgFeeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) {
                if (MessageBox.Show("确定要删除这一条费用记录吗？", "删除确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dvgFeeList.Rows[e.RowIndex].Cells["id"].Value);
                    var del = db.OtherFee.Single(f => f.id == id);
                    if (del.out_share_water > 0 || del.out_share_eletricity > 0) {
                        MessageBox.Show("退宿水电费用不能删除，而有需要可以修改退宿费用或者反退宿。");
                        return;
                    }
                    db.OtherFee.DeleteOnSubmit(db.OtherFee.Single(f => f.id == id));
                    db.SubmitChanges();
                    MyUtil.WriteEventLog("宿舍查询", dormNumber, Convert.ToString(dvgFeeList.Rows[e.RowIndex].Cells["姓名"].Value), "删除费用,id:" + id.ToString());
                    MessageBox.Show("删除成功！");
                    getFeeGridData(Convert.ToString(dvgFeeList.Rows[e.RowIndex].Cells["年月份"].Value).Replace("-",""));

                }
            }
        }
    }
}
