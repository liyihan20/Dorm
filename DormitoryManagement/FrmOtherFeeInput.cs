using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DormitoryManagement
{
    public partial class FrmOtherFeeInput : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        private int dormID;
        public FrmOtherFeeInput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = cbKey.Text.Trim();
            string content = tbContent.Text.Trim();
            Dorm dorm;
            //添加数据源的列
            DataTable dt = new DataTable();
            dt.Columns.Add("员工ID", typeof(Int32));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("性别", typeof(string));
            dt.Columns.Add("厂牌编号", typeof(string));
            dt.Columns.Add("身份证号", typeof(string));
            dt.Columns.Add("工资类型", typeof(string));
            dt.Columns.Add("入住日期", typeof(string));

            //如果查询条件是宿舍编号
            if (key.Equals("宿舍编号"))
            {
                var dorms = db.Dorm.Where(dor => dor.number == content);
                if (dorms.Count() < 1)
                {
                    MessageBox.Show("查询不到此宿舍");
                    return;
                }
                else
                    dorm = dorms.First();
                if (!dorm.Area.name.Equals(LoginUser.operated_area)) {
                    MessageBox.Show("查询的宿舍不属于你可操作的宿舍区！");
                    return;
                }
                var lodges = dorm.Lodging.Where(lod => lod.out_date == null);
                foreach (Lodging lodge in lodges)
                {                    
                    dt.Rows.Add(lodge.Employee.id, lodge.Employee.name, lodge.Employee.sex, lodge.Employee.card_number, lodge.Employee.identify_number, lodge.Employee.salary_type, ((DateTime)lodge.in_date).ToShortDateString());
                }
                dataGridView1.DataSource = dt;
            }
            //如若查询条件是员工厂牌号码
            else
            {
                var emp = db.Employee.Where(em => em.card_number == content || em.account_number == content);
                if (emp.Count() < 1)
                {
                    MessageBox.Show("查询不到该员工");
                    return;
                }
                var lodge = emp.First().Lodging.Where(lod => lod.out_date == null);
                if (lodge.Count() == 0)
                {
                    MessageBox.Show("该员工已退宿");
                    return;
                }                
                //绑定数据
                dorm = lodge.First().Dorm;
                if (!dorm.Area.name.Equals(LoginUser.operated_area)) {
                    MessageBox.Show("查询的员工不属于你可操作的宿舍区！");
                    return;
                }
                var em1 = emp.First();
                dt.Rows.Add(em1.id, em1.name, em1.sex, em1.card_number, em1.identify_number, em1.salary_type,((DateTime)lodge.First().in_date).ToShortDateString());
                //添加到datagridview
                dataGridView1.DataSource = dt;

            }

            //设置宿舍ID以供保存时使用
            dormID = dorm.id;

            //输出宿舍信息
            lbDormType.Text = dorm.DormType.name;
            lbDormNumber.Text = dorm.number;
            lbDormComent.Text = dorm.comment;
            lbMaxNum.Text = dorm.DormType.max_number.ToString();

            dateTimePicker1.Focus();
        }

        private void FrmOtherFeeInput_Load(object sender, EventArgs e)
        {
            cbKey.Text = "宿舍编号";
            //dateTimePicker1.MinDate = YearAndMonth.firstDayInMonth(DateTime.Now.Year + "" + DateTime.Now.Month);
            //dateTimePicker1.MaxDate = YearAndMonth.lastDayInMonth(DateTime.Now.Year + "" + DateTime.Now.Month);
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }
            dataGridView1.Columns["员工ID"].Visible = false;
            dataGridView1.Columns["选择"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["性别"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["身份证号"].Width = 130;
            dataGridView1.Columns["入住日期"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DateTime inDate = Convert.ToDateTime(row.Cells["入住日期"].Value);
                DateTime feeDate = dateTimePicker1.Value;
                
                TimeSpan span = feeDate - inDate;
                if (span.Days >= 7)
                    dataGridView1[0, row.Index].Value = true;
                else
                {
                    dataGridView1[0, row.Index].Value = false;
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //至少应该选中一人
            if (selectedNum() == 0)
            {
                MessageBox.Show("请至少选择一员工");
                return;
            }

            //至少应该输入一项费用的验证
            if (string.IsNullOrEmpty(tbFine.Text.Trim()) && string.IsNullOrEmpty(tbHouse.Text.Trim()) && string.IsNullOrEmpty(tbOther.Text.Trim()) && string.IsNullOrEmpty(tbRepair.Text.Trim()))
            {
                MessageBox.Show("请至少输入一项费用");
                return;
            }

            decimal repair = 0, fine = 0, house = 0, others = 0;

            //验证输入完整性
            if (!string.IsNullOrEmpty(tbRepair.Text.Trim()))
            {
                if (!decimal.TryParse(tbRepair.Text.Trim(), out repair))
                {
                    MessageBox.Show("维修费输入不正确");
                    tbRepair.Focus();
                    return;
                }
            }
            repair = Math.Round(repair/selectedNum(),1);

            if (!string.IsNullOrEmpty(tbFine.Text.Trim()))
            {
                if (!decimal.TryParse(tbFine.Text.Trim(), out fine))
                {
                    MessageBox.Show("罚款输入不正确");
                    tbFine.Focus();
                    return;
                }
            }
            fine = Math.Round(fine / selectedNum(), 1);

            if (!string.IsNullOrEmpty(tbHouse.Text.Trim()))
            {
                if (!decimal.TryParse(tbHouse.Text.Trim(), out house))
                {
                    MessageBox.Show("招待所费用输入不正确");
                    tbHouse.Focus();
                    return;
                }
            }
            house = Math.Round(house / selectedNum(), 1);

            if (!string.IsNullOrEmpty(tbOther.Text.Trim()))
            {
                if (!decimal.TryParse(tbOther.Text.Trim(), out others))
                {
                    MessageBox.Show("其他费用输入不正确");
                    tbOther.Focus();
                    return;
                }
            }
            others = Math.Round(others / selectedNum(), 1);

            //保存信息
            List<OtherFee> list = new List<OtherFee>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    list.Add(new OtherFee()
                    {
                        dorm_id = dormID,
                        emp_id = Convert.ToInt32(row.Cells["员工ID"].Value),
                        fine = fine != 0 ? fine : (decimal?)null,
                        repair_cost = repair != 0 ? repair : (decimal?)null,
                        other_cost = others != 0 ? others : (decimal?)null,
                        guesthouse_cost = house != 0 ? house : (decimal?)null,
                        year_month = YearAndMonth.toLong(dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.Month.ToString()),
                        //comment = rtbDescription.Text,
                        comment=cbComment.Text,
                        date = dateTimePicker1.Value
                    });
                }
            }
            db.OtherFee.InsertAllOnSubmit(list);
            db.SubmitChanges();
            MyUtil.WriteEventLog("费用录入", cbKey.Text.Equals("宿舍编号") ? tbContent.Text : "", cbKey.Text.Equals("员工厂牌编号") ? tbContent.Text : "", string.Format("人数：{0}，维修：{1}，罚款:{2}，招待所：{3}，其它：{4}，备注：{5}", selectedNum(), repair, fine, house, others,cbComment.Text));
            clearInput();
            MessageBox.Show("费用新增成功");
        }

        //全选与全部选的方法实现
        private bool isSelected()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    return true;
            }
            return false;
        }

        //获取选择员工的人数
        private int selectedNum() {
            int num = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    num++;
            }
            return num;
        }

        //保存后清空输入的内容
        private void clearInput() {
            foreach (Control ct in groupBox1.Controls) {
                if (ct.Name.StartsWith("tb") || ct.Name.StartsWith("rtb") || ct.Name.StartsWith("lb")) {
                    ct.Text = "";
                }
            }
            dataGridView1.DataSource = null;
        }

        private void FrmOtherFeeInput_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 40;
        }
    }
}
