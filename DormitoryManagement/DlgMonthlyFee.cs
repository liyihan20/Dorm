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
    public partial class DlgMonthlyFee : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        private int monthlyFeeId = 0;

        public DlgMonthlyFee()
        {
            InitializeComponent();
        }

        public void setFormData(int id, string dorm,string emp,decimal fee,string comment) {
            monthlyFeeId = id;
            tbDorm.Text = dorm;
            cbEmpName.Text = emp;
            nudFee.Value = fee;
            cbComment.Text = comment;
        }


        private void DlgMonthlyFee_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection acs = new AutoCompleteStringCollection();
            foreach (var dorm in db.Dorm.Where(d => d.available == 0).Select(d => d.number)) {
                acs.Add(dorm);
            }
            tbDorm.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbDorm.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbDorm.AutoCompleteCustomSource = acs;
            
        }

        private void tbDorm_Leave(object sender, EventArgs e)
        {
            var empNames = from lo in db.Lodging
                           where lo.Dorm.number == tbDorm.Text
                           && lo.out_date == null
                           select lo.Employee.name;
            cbEmpName.DataSource = empNames;
        }

        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            //验证
            string dorm = tbDorm.Text;
            string empName = cbEmpName.Text;
            decimal fee = nudFee.Value;
            string comment = cbComment.Text;
            int? dormId=0, empId=0;
            if (string.IsNullOrWhiteSpace(tbDorm.Text)) {
                MessageBox.Show("宿舍号不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(cbEmpName.Text)) {
                MessageBox.Show("姓名不能为空");
                return;
            }
            if (db.Dorm.Where(d => d.number == dorm).Count() > 0)
            {
                dormId = db.Dorm.Single(d => d.number == dorm).id;
            }
            else {
                MessageBox.Show("宿舍号不存在");
                return;
            }
            if (db.Lodging.Where(l => l.dorm_id == dormId && l.Employee.name == empName).Count() > 0)
            {
                empId = db.Lodging.Where(l => l.dorm_id == dormId && l.Employee.name == empName).First().emp_id;
            }
            else {
                MessageBox.Show("此宿舍没有此员工！");
                return;
            }

            if (monthlyFeeId > 0)
            {
                //修改
                MonthlyFee mf = db.MonthlyFee.Single(m => m.id == monthlyFeeId);
                mf.dorm_id = dormId;
                mf.dorm_number = dorm;
                mf.emp_id = empId;
                mf.emp_name = empName;
                mf.fee = fee;
                mf.comment = comment;
            }
            else { 
                //新增
                MonthlyFee mf = new MonthlyFee();
                mf.dorm_id = dormId;
                mf.dorm_number = dorm;
                mf.emp_id = empId;
                mf.emp_name = empName;
                mf.fee = fee;
                mf.comment = comment;
                db.MonthlyFee.InsertOnSubmit(mf);
            }
            db.SubmitChanges();
            MessageBox.Show("保存成功");
            FrmMonthlyFee fmf = (FrmMonthlyFee)this.Owner;
            fmf.init();
            this.Close();
        }

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
