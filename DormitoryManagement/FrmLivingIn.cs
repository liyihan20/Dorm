using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace DormitoryManagement
{
    public partial class FrmLivingIn : Form
    {
        string modelName = "入住管理";
        string connectionString = "Data Source=192.168.168.168;Initial Catalog=DJB;User ID=k3read;password=99768";
        SqlConnection conn = null;
        SqlDataAdapter adap = null;
        DataSet ds = null;
        string isWorking = null;
        DormDBDataContext db = new DormDBDataContext();
        byte[] imageByte;
        public int selectedRow; //保存选择的行号

        string miDepName;//人事部的部门名称

        public FrmLivingIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            string sql = "";
            if (string.IsNullOrEmpty(tbCard.Text.Trim()) && string.IsNullOrEmpty(tbName.Text.Trim()))
            {
                label15.Text = "请先输入查询内容";
                return;
            }
            else if (!string.IsNullOrEmpty(tbCard.Text.Trim()))
            {
                sql = "select t1.*,t2.gfclassname,t2.cust_name,t2.full_name from rsemp t1 left join rsbm t2 on t1.dept_id=t2.id where t1.emp_no='" + tbCard.Text.Trim() + "'";
            }
            else if (!string.IsNullOrEmpty(tbName.Text.Trim()))
            {
                sql = "select t1.*,t2.gfclassname,t2.cust_name,t2.full_name from rsemp t1 left join rsbm t2 on t1.dept_id=t2.id where t1.emp_name='" + tbName.Text.Trim() + "' order by t1.emp_no";
            }

            //尝试与HR数据库连接
            using (conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    adap = new SqlDataAdapter(sql, conn);
                    ds = new DataSet();
                    adap.Fill(ds);
                }
                catch (Exception)
                {
                    MyUtil.WriteEventLog(modelName, "", "", "与HR数据库连接失败", false);
                    label15.Text = "与HR数据库连接失败";
                    label15.ForeColor = Color.Red;
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }
            if (ds.Tables[0].Rows.Count < 1)
            {
                label15.Text = "不存在该厂牌号码或姓名";
                label15.ForeColor = Color.FromArgb(0, 84, 227);
                return;
            }
            else if (ds.Tables[0].Rows.Count == 1)
            {
                clearGroup1ExcepCard();
                //将HR数据库的员工信息读取出来放进相应控件中                
                showDatas(0);
            }
            else
            {
                clearGroup1ExcepCard();
                //多条记录，显示选择对话框
                DlgLivingIn dlg = new DlgLivingIn();
                dlg.Owner = this;
                dlg.dt = ds.Tables[0];
                dlg.ShowDialog();
            }
            label15.Text = "信息获取成功";
            label15.ForeColor = Color.FromArgb(0, 84, 227);
            cbDepart.Focus();
        }

        private void showDatas(int rowNumber)
        {
            DataRow row = ds.Tables[0].Rows[rowNumber];
            cbDepart.Text = row["gfclassname"].ToString() + row["cust_name"].ToString();
            miDepName = row["gfclassname"].ToString() + row["cust_name"].ToString(); //先保存从人事数据库获取过来的部门名称
            tbCard.Text = row["emp_no"].ToString();
            tbName.Text = row["emp_name"].ToString();
            cbSex.Text = row["sex"].ToString();
            tbSalaryType.Text = row["gzlb"].ToString();
            tbAddr.Text = row["family_addr"].ToString();
            tbAccount.Text = row["txm"].ToString();
            tbConnector.Text = row["jsrxm"].ToString();
            tbIdentify.Text = row["id_code"].ToString();
            tbPhone.Text = row["tp"].ToString();
            tbConnectorPhone.Text = row["jsrdh"].ToString();
            tbHomeTown.Text = row["native_place"].ToString();
            degree.Text = row["study_level"].ToString();
            isWorking =Convert.ToString(row["lzlx"]);
            if (row["Zp"] != DBNull.Value)
            {
                imageByte = (byte[])row["Zp"];
                using (MemoryStream ms = new MemoryStream(imageByte))
                {
                    Bitmap bm = new Bitmap(ms);
                    pictureBox1.Image = bm;
                }
            }
            else {
                pictureBox1.Image = Properties.Resources.DefaultImage;
                imageByte = null;
            }
            //增加入住情况和办证情况
            if (!string.IsNullOrEmpty(tbCard.Text) && tbCard.Text.Length > 6)
            {
                var emps = db.Employee.Where(em => em.card_number == tbCard.Text);
                if (emps.Count() > 0) {
                    var emp = emps.First();
                    tbComment.Text = emp.comment;
                    tbIdentifyInfo.Text = emp.identify_info;
                }
            }
            MyUtil.WriteEventLog(modelName, "", tbName.Text, "信息显示成功，厂牌："+tbCard.Text);
        }

        private void FrmLivingIn_Load(object sender, EventArgs e)
        {
            List<String> dep = (from de in db.Department
                                orderby Convert.ToDouble(de.number) ascending
                                select de.name).ToList();
            dep.Insert(0, "");
            cbDepart.DataSource = dep;
            pictureBox1.Image = Properties.Resources.DefaultImage;

            //dateTimePicker1.MinDate = YearAndMonth.firstDayInMonth(DateTime.Now.Year + "" + DateTime.Now.Month);
            //dateTimePicker1.MaxDate = YearAndMonth.lastDayInMonth(DateTime.Now.Year + "" + DateTime.Now.Month);
            dateTimePicker1.Value = DateTime.Now;
            cbClassify.Text = "计";

            //房号的自动提示
            AutoCompleteStringCollection asc = new AutoCompleteStringCollection();
            foreach (Dorm dorm in db.Area.Single(are => are.name == LoginUser.operated_area).Dorm.Where(d => d.available == 0 && d.DormType.max_number - d.Lodging.Where(l => l.out_date == null).Count() > 0))
            {
                asc.Add(dorm.number);
            }
            tbDorm.AutoCompleteCustomSource = asc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Department dep;
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("请先填写员工信息");
                return;
            }
            if (string.IsNullOrEmpty(cbDepart.Text.Trim()))
            {
                MessageBox.Show("请选择员工所属部门");
                cbDepart.Focus();
                return;
            }
            dep = db.Department.Single(d => d.name == cbDepart.Text.Trim());
            //if (dep.property.Equals("厂内") || dep.property.Equals("光电")) {
            //    if (string.IsNullOrWhiteSpace(tbAccount.Text)) {
            //        MessageBox.Show("厂内或者光电人员的账号不能为空，入住失败。");
            //        return;
            //    }
            //}
            if (string.IsNullOrEmpty(tbDorm.Text.Trim()))
            {
                MessageBox.Show("请输入房号");
                tbDorm.Focus();
                return;
            }
            //判断在职状态
            if (isWorking != null && isWorking != "" && !isWorking.Equals("香港同事"))
            {
                MessageBox.Show("该员工的在职状态是：" + isWorking + ",不允许登记入宿");
                return;
            }            

            int left = Convert.ToInt32(lbLeftNum.Text.Trim());
            if (left < 1)
            {
                MessageBox.Show("该房间所住人数已满");
                return;
            }
            //判断宿舍性别是否对应
            if (!lbDormSex.Text.Equals(cbSex.Text.Trim()) && !lbDormSex.Text.Equals("不限"))
            {
                MessageBox.Show("此宿舍不能男女混住");
                return;
            }

            //验证数据完整性
            //decimal rent, manage, guarantee;
            //if (!decimal.TryParse(tbRent.Text, out rent) || rent < 0)
            //{
            //    MessageBox.Show("实际租金输入不合法");
            //    tbRent.Focus();
            //    return;
            //}
            //if (!decimal.TryParse(tbManage.Text, out manage) || manage < 0)
            //{
            //    MessageBox.Show("管理费输入不合法");
            //    tbManage.Focus();
            //    return;
            //}
            //if (!decimal.TryParse(tbGuarantee.Text, out guarantee) || guarantee < 0)
            //{
            //    MessageBox.Show("押金输入不合法");
            //    tbGuarantee.Focus();
            //    return;
            //}
                        
            Employee emp;

            //查看员工表中是否已有记录，没有就新增,这里要考虑厂外的情况。
            if (string.IsNullOrEmpty(tbAccount.Text))
            {
                //int outsideCount = db.Employee.Where(em => em.name == tbName.Text.Trim()).Count();
                int outsideCount = db.Lodging.Where(l => l.Employee.name == tbName.Text.Trim() && l.out_date == null).Count();
                if (outsideCount < 1)
                {
                    emp = new Employee();
                    emp.name = tbName.Text;
                    emp.card_number = tbCard.Text;
                    emp.comment = tbComment.Text;
                    emp.degree = degree.Text;
                    emp.Department1 = dep;
                    emp.family_address = tbAddr.Text;
                    emp.family_connector = tbConnector.Text;
                    emp.family_phone = tbConnectorPhone.Text;
                    emp.household = tbHomeTown.Text;
                    emp.identify_number = tbIdentify.Text;
                    emp.salary_type = tbSalaryType.Text;
                    emp.sex = cbSex.Text;
                    emp.phone = tbPhone.Text;
                    emp.picture = imageByte;
                    db.Employee.InsertOnSubmit(emp);
                    db.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("该员工姓名已存在！厂外员工的姓名不能够一样，因为无法标识唯一,建议在名字后面加上数字区分");
                    return;
                }
            }
            else
            {
                if (db.Employee.Where(em => (em.account_number == tbAccount.Text)).Count() < 1)
                {
                    emp = new Employee();
                    emp.card_number = tbCard.Text;
                    emp.name = tbName.Text;
                    emp.account_number = tbAccount.Text;
                    emp.comment = tbComment.Text;
                    emp.degree = degree.Text;
                    emp.Department1 = dep;
                    emp.family_address = tbAddr.Text;
                    emp.family_connector = tbConnector.Text;
                    emp.family_phone = tbConnectorPhone.Text;
                    emp.household = tbHomeTown.Text;
                    emp.identify_number = tbIdentify.Text;
                    emp.picture = imageByte;
                    emp.salary_type = tbSalaryType.Text;
                    emp.sex = cbSex.Text;
                    emp.phone = tbPhone.Text;
                    emp.identify_info = tbIdentifyInfo.Text;
                    db.Employee.InsertOnSubmit(emp);
                }
                else
                {
                    emp = db.Employee.Single(emplo => emplo.account_number == tbAccount.Text);
                    emp.comment = tbComment.Text;
                    emp.identify_info = tbIdentifyInfo.Text;
                }
            }

            //判断该员工是否在住宿表中存在退宿日期为null的记录，如果存在表示则不允许再登记入住
            if (emp.Lodging.Where(lodge => lodge.out_date == null).Count() > 0)
            {
                MyUtil.WriteEventLog(modelName, "", emp.name, "cardnumber:" + emp.card_number + ";该员工还没有退宿，不能再登记入住", false);
                MessageBox.Show("该员工还没有退宿，不能再登记入住");
                return;
            }

            //判断该员工是否被列入黑名单
            if (emp.BlackList.Where(bl => bl.out_date == null).Count() > 0)
            {
                MyUtil.WriteEventLog(modelName, "", emp.name, "cardnumber:" + emp.card_number + ";该员工已被列入黑名单，不能再登记入住", false);
                MessageBox.Show("该员工已被列入黑名单，不能再登记入住");
                return;
            }

            //保存到住宿表
            Lodging lodg = new Lodging();
            Dorm dorm = db.Dorm.Single(dor => dor.number == tbDorm.Text.Trim());
            lodg.in_date = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
            lodg.Dorm = dorm;
            lodg.Employee = emp;
            lodg.real_rent = dorm.rent;
            lodg.real_manage = dorm.manageCost;
            lodg.guarantee = db.UnitFee.Where(u=>u.name=="押金").First().price;
            lodg.classify_property = cbClassify.Text;
            
            db.Lodging.InsertOnSubmit(lodg);

            //保存到部门对应表
            //2012-10-8新增功能
            string acDepName = cbDepart.Text;
            if (!string.IsNullOrEmpty(miDepName) && !string.IsNullOrEmpty(acDepName))
            {
                var depMaps = db.DepMap.Where(dm => dm.mi_dep == miDepName.Trim() && dm.ac_dep == acDepName);
                if (depMaps.Count() < 1)
                {
                    DepMap depMap = new DepMap()
                    {
                        mi_dep = miDepName,
                        ac_dep = acDepName,
                        qty = 1
                    };
                    db.DepMap.InsertOnSubmit(depMap);
                }
                else {
                    var depMap = depMaps.First();
                    depMap.qty = depMap.qty + 1;
                }
            }

            //将住宿信息反馈到HR系统，2013-3-22更新
            if (emp.Department1.property.Equals("厂内") || emp.Department1.property.Equals("光电"))
            {
                if (!string.IsNullOrWhiteSpace(emp.account_number))
                {
                    try
                    {
                        db.updateHRLivingState(emp.account_number, true);
                    }
                    catch
                    {
                        MyUtil.WriteEventLog(modelName, "", emp.name, "cardnumber:" + emp.card_number + ";连接失败，员工入住信息无法更新到人事系统。", false);
                        MessageBox.Show("连接失败，员工入住信息无法更新到人事系统。");
                    }
                }
            }

            //提交事务
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                MyUtil.WriteEventLog(modelName, tbDorm.Text, emp.name, "cardnumber:" + emp.card_number + ";入住失败:"+ex.Message,false);
                MessageBox.Show("宿舍分配失败，请稍后重试。");
                return;
            }

            MyUtil.WriteEventLog(modelName, tbDorm.Text, emp.name, "cardnumber:" + emp.card_number + ";入住成功");
            MessageBox.Show("宿舍已成功分配");
            clearAll();

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string dormNumber = tbDorm.Text.Trim();
            if (string.IsNullOrEmpty(dormNumber))
            {
                dormNumErrorToClear();
                return;
            }

            //判断宿舍是否存在
            dormNumber = dormNumber.ToUpper();
            var dorms = db.Dorm.Where(dor => dor.number == dormNumber);
            if (dorms.Count() < 1)
            {
                MessageBox.Show("该宿舍号不存在");
                dormNumErrorToClear();
                return;
            }
            Dorm dorm = dorms.First();

            //判断是否已经禁用
            if (dorm.available == 1) {
                MessageBox.Show("此宿舍已经禁用，不能登记");
                dormNumErrorToClear();
                return;
            }

            //判断宿舍是否属于当前可操作的区
            if (!dorm.Area.name.Equals(LoginUser.operated_area))
            {
                MessageBox.Show("该宿舍属于" + dorm.Area.name + "请切换到该区再操作");
                dormNumErrorToClear();
                return;
            }

            //宿舍基本信息
            lbArea.Text = dorm.Area.name;
            lbDormType.Text = dorm.DormType.name;
            lbDormSex.Text = dorm.dormSex;
            lbMaxNum.Text = dorm.DormType.max_number.ToString();
            lbLeftNum.Text = (dorm.DormType.max_number - dorm.Lodging.Where(lod => lod.out_date == null).Count()).ToString();
            tbRent.Text = dorm.rent.ToString();
            tbManage.Text = dorm.manageCost.ToString();
            tbGuarantee.Text = BaseInfo.guarantee.ToString();
            tbDorm.Text = dormNumber;
        }

        private void dormNumErrorToClear()
        {
            foreach (Control con in groupBox2.Controls)
            {
                if ((con.Name.StartsWith("lb") || con.Name.StartsWith("tb")) && !con.Name.StartsWith("tbDorm"))
                {
                    con.Text = "";
                }
            }
        }

        private void clearGroup1ExcepCard()
        {
            foreach (Control tb in groupBox1.Controls)
            {
                if (tb.GetType().Equals(typeof(TextBox)))
                {
                    if (!tb.Name.Equals("tbCard"))
                    {
                        ((TextBox)tb).Clear();
                    }
                }
            }
            pictureBox1.Image = Properties.Resources.DefaultImage;
            imageByte = null;
            cbDepart.Text = " ";
            cbSex.Text = "";
        }

        private void clearAll()
        {
            clearGroup1ExcepCard();
            dormNumErrorToClear();
            tbDorm.Clear();
            tbCard.Clear();
        }

        private void cbDepart_Leave(object sender, EventArgs e)
        {
            string inputString = cbDepart.Text;
            foreach (string item in cbDepart.Items)
            {
                if (item.ToString().Equals(inputString))
                    return;
            }
            MessageBox.Show("输入的部门必须存在于列表中");
            cbDepart.Focus();
        }

        private void lbRowNum_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbRowNum.Text)) { return; }
            showDatas(Int32.Parse(lbRowNum.Text));
        }

        private void FrmLivingIn_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 40;
        }

    }
}
