using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace DormitoryManagement
{
    public partial class FrmEmployeeInfo : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        int? empId;
        public FrmEmployeeInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = cbSearchKey.Text.Trim();
            string content = tbContent.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("请输入查询内容");
                tbContent.Focus();
                return;
            }

            IQueryable<Employee> emps = null;
            if (key.Equals("厂牌编号"))
                emps = db.Employee.Where(em => em.card_number.Contains(content));
            else if (key.Equals("姓名"))
                emps = db.Employee.Where(em => em.name.Contains(content));
            else if (key.Equals("账号"))
                emps = db.Employee.Where(em => em.account_number.Contains(content));
            else if (key.Equals("身份证号"))
                emps = db.Employee.Where(em => em.identify_number.Contains(content));

            if (emps.Count() < 1)
            {
                MessageBox.Show("查询结果不存在");
                return;
            }
            else if (emps.Count() > 1)
            {                
                FrmEmpSearchResult res = new FrmEmpSearchResult(emps, this);
                res.ShowDialog();
            }
            else
            {
                showDatas(emps.First());
            }

        }

        private void FrmEmployeeInfo_Load(object sender, EventArgs e)
        {
            cbSearchKey.Text = "厂牌编号";
            pbHead.Image = Properties.Resources.DefaultImage;
            foreach (Control ct in groupBox1.Controls)
            {
                if (ct.Name.StartsWith("lb"))
                {
                    ct.ForeColor = Color.DarkGreen;
                }
            }
        }

        private void lbID_TextChanged(object sender, EventArgs e)
        {
            showDatas(Convert.ToInt32(lbID.Text));
        }

        private void showDatas(int empId)
        {
            var emp = db.Employee.Single(em => em.id == empId);
            showDatas(emp);
        }

        private void showDatas(Employee emp)
        {
            MyUtil.WriteEventLog("查询入住员工", "", emp.name + ":" + emp.card_number, "");

            empId = emp.id;

            //输出基本信息放在标签上
            lbName.Text = emp.name;
            lbSex.Text = emp.sex;
            lbCardNumber.Text = emp.card_number;
            lbAccount.Text = emp.account_number;
            lbIdentify.Text = emp.identify_number;
            lbDegree.Text = emp.degree;
            lbSalaryType.Text = emp.salary_type;
            lbHouseHold.Text = emp.household;
            lbConnector.Text = emp.family_connector;
            lbConnectorPhone.Text = emp.family_phone;
            lbPhone.Text = emp.phone;
            lbComment.Text = emp.comment;
            lbAddr.Text = emp.family_address;
            lbDepartment.Text = emp.Department1.name;
            tbIdentify.Text = emp.identify_info;
            if (emp.picture != null && emp.picture.Length > 1)
            {
                byte[] imageByte = emp.picture.ToArray();
                using (MemoryStream ms = new MemoryStream(imageByte))
                {
                    Bitmap bm = new Bitmap(ms);
                    pbHead.Image = bm;
                }
            }
            else {
                pbHead.Image = Properties.Resources.DefaultImage;
            }

            //填充住宿信息
            DataTable dt = new DataTable();
            dt.Columns.Add("区号", typeof(string));
            dt.Columns.Add("房号", typeof(string));
            dt.Columns.Add("宿舍类型", typeof(string));
            dt.Columns.Add("租金", typeof(string));
            dt.Columns.Add("管理费", typeof(string));
            dt.Columns.Add("押金", typeof(string));
            dt.Columns.Add("入住日期", typeof(string));
            dt.Columns.Add("退宿日期", typeof(string));

            foreach (Lodging lodg in (from lod in db.Lodging
                                      where lod.emp_id == emp.id
                                      orderby lod.in_date descending
                                      select lod))
            {
                dt.Rows.Add(lodg.Dorm.Area.name,
                            lodg.Dorm.number,
                            lodg.Dorm.DormType.name,
                            lodg.real_rent.ToString(),
                            lodg.real_manage.ToString(),
                            lodg.guarantee.ToString(),
                            ((DateTime)lodg.in_date).ToShortDateString(),
                            lodg.out_date==null?null : ((DateTime)lodg.out_date).ToShortDateString());
            }

            foreach (AutoQuit qu in db.AutoQuit.Where(au => au.emp_id == emp.id)) {
                dt.Rows.Add(qu.Dorm.Area.name,
                            qu.Dorm.number,
                            qu.Dorm.DormType.name,
                            "-",
                            "-",
                            "-",
                            ((DateTime)qu.in_date).ToShortDateString(),
                            qu.out_date==null?null:((DateTime)qu.out_date).ToShortDateString());
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["区号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //填充费用信息
            dataGridView2.DataSource = from fee in db.OtherFee
                                       where fee.emp_id==emp.id
                                       orderby fee.year_month descending
                                       select new
                                       {
                                           年月份 = fee.year_month,
                                           宿舍编号 = fee.Dorm.number,
                                           维修费 = fee.repair_cost,
                                           罚款 = fee.fine,
                                           招待所费用 = fee.guesthouse_cost,
                                           其他费用 = fee.other_cost,
                                           分摊电费 = fee.out_share_eletricity,
                                           分摊水费 = fee.out_share_water,
                                           费用备注 = fee.comment
                                       };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (empId == null)
            {
                MessageBox.Show("请先查询员工信息");
                return;
            }
            else {
                var emp = db.Employee.Single(em => em.id == empId);
                emp.identify_info = tbIdentify.Text;
                db.SubmitChanges();
                MessageBox.Show("保存成功！");
                MyUtil.WriteEventLog("查询入住员工", "", emp.name, "保存办证信息成功");
            }
        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
        }
    }
}
