using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DormitoryManagement
{
    public partial class FrmImportData : Form
    {
        string Connstring;
        DormDBDataContext db = new DormDBDataContext();
        int doWhat = 0;
        int areaCode = 0;

        public FrmImportData()
        {
            InitializeComponent();
        }

        //执行导入宿舍方法之后很可能会出现宿舍编号重复的记录存在，原因在于同个宿舍在旧数据库中有多个标准，或者同个宿舍的分类性质不同，或者字段为空的情况。查出重复宿舍的sql语句是：select number,count(number) from dormitory_dorm group by number having count(number)>1
        private string importDorms()
        {
            OleDbConnection conn = null;
            OleDbDataAdapter adap = null;
            DataSet ds = null;
            List<Dorm> list = new List<Dorm>();

            using (conn = new OleDbConnection(Connstring))
            {
                conn.Open();
                using (adap = new OleDbDataAdapter("SELECT DISTINCT 房号, 房间标准, 性别, 标准月租, 卫生费 FROM JB", conn))
                {
                    ds = new DataSet();
                    adap.Fill(ds);
                }
            }
            string number, dormType, dormSex;
            decimal rent, manageCost;
            Area area = null;
            DormType type;
            int count = ds.Tables[0].Rows.Count;
            int i = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                number = row[0].ToString();
                dormType = Convert.ToString(row[1]);
                dormSex = Convert.ToString(row[2]);
                rent = decimal.Parse(row[3].ToString());
                manageCost = decimal.Parse(row[4].ToString());
                if (string.IsNullOrEmpty(dormType))
                    dormType = "肆人间";
                if (dormType.Equals("双人"))
                    dormType = "双人间";
                type = db.DormType.Single(dt => dt.name == dormType);
                if (areaCode == 35)
                {
                    if (number.StartsWith("3"))
                        area = db.Area.Single(ar => ar.name == "三区");
                    else
                        area = db.Area.Single(ar => ar.name == "五区");
                }
                else if (areaCode == 1)
                {
                    area = db.Area.Single(ar => ar.name == "一区");
                }
                else if (areaCode == 2)
                {
                    area = db.Area.Single(ar => ar.name == "二区");
                }
                list.Add(new Dorm()
                {
                    number = number,
                    DormType = type,
                    Area = area,
                    dormSex = dormSex,
                    available = (short)0,
                    rent = rent,
                    manageCost = manageCost
                });
                backgroundWorker1.ReportProgress((++i) * 100 / count);
            }
            db.Dorm.InsertAllOnSubmit(list);
            db.SubmitChanges();
            ds.Dispose();
            return "success";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doWhat = 1;
            switch (cbArea.Text.Trim())
            {
                case "一区":
                    areaCode = 1;
                    break;
                case "二区":
                    areaCode = 2;
                    break;
                case "三五区":
                    areaCode = 35;
                    break;
                default:
                    areaCode = 0;
                    break;
            }
            if (areaCode == 0)
            {
                MessageBox.Show("请选择宿舍区");
                return;
            }
            backgroundWorker1.RunWorkerAsync();

        }

        //查出重复的宿舍，放进datagridview中
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            button6.Visible = true;
            dataGridView1.DataSource = db.take_duplicate_record().ToList();
        }

        //宿舍导入完成后，再导入员工基本资料，通过账号到HR数据库中取到其他资料，然后一并导入新数据库。发现在宿舍管理系统旧数据库中经常出现厂牌号码或者名字打错的情况，所以通过账号拿数据。如果账号拿不到数据，则通过厂牌号码。因为账号数字位数较小，出现录入错误的几率也较小些
        private string importEmployees()
        {
            OleDbConnection conn = null;
            OleDbDataAdapter adap = null;
            DataSet ds = null;
            Department dept = null;
            using (conn = new OleDbConnection(Connstring))
            {
                conn.Open();
                using (adap = new OleDbDataAdapter("SELECT ID, 部门, 帐号, 姓名, 身份证号, 籍贯, 性别, 学历, 住宿期间情况 FROM JB WHERE (押金情况 <> '退')", conn))
                {
                    ds = new DataSet();
                    adap.Fill(ds);
                }
            }

            //HR数据库的连接
            string connectionString = "Data Source=192.168.168.168;Initial Catalog=DJB;User ID=k3;password=k35790";
            SqlConnection conn2 = null;
            SqlDataAdapter adap2 = null;
            DataTable ds2 = new DataTable();
            DataRow row2;
            Employee employee;
            conn2 = new SqlConnection(connectionString);
            conn2.Open();

            int count = ds.Tables[0].Rows.Count;
            int i = 0;
            //循环更新入住员工的信息。。。
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(row["部门"].ToString().Trim()))
                {
                    dept = null;
                }
                else
                {
                    var depts = db.Department.Where(de => de.name == row["部门"].ToString().Trim());
                    if (depts.Count() > 0)
                        dept = depts.First();
                    else {
                        dept = db.Department.Single(de => de.name == "待确认");
                    }
                }

                //如果账号为空，代表是厂外部门的员工，不从人事数据库获取信息，而是直接从旧数据库中拿。
                if (string.IsNullOrEmpty(row["帐号"].ToString().Trim()))
                {
                    employee = new Employee()
                    {
                        name = Convert.ToString(row["姓名"]),
                        sex = Convert.ToString(row["性别"]),
                        card_number = Convert.ToString(row["身份证号"]),
                        household = Convert.ToString(row["籍贯"]),
                        degree = Convert.ToString(row["学历"]),
                        Department1 = dept,
                        old_id = Convert.ToInt32(row["ID"])
                    };
                }
                else
                {
                    int accountInt = Int32.Parse(row["帐号"].ToString());
                    string accountString = accountInt.ToString();
                    if (accountString.Length == 4) { accountString = "0" + accountString; };
                    using (adap2 = new SqlDataAdapter("select emp_no,emp_name,sex,gzlb,family_addr,txm,jsrxm,id_code,tp,jsrdh,native_place,study_level,ZP from rsemp where txm='" + accountString + "'", conn2))
                    {
                        adap2.Fill(ds2);
                    }
                    if (ds2.Rows.Count != 1)
                    {
                        ds2.Clear();
                        using (adap2 = new SqlDataAdapter("select emp_no,emp_name,sex,gzlb,family_addr,txm,jsrxm,id_code,tp,jsrdh,native_place,study_level,ZP from rsemp where emp_no='" + Convert.ToString(row["身份证号"]) + "'", conn2))
                        {
                            adap2.Fill(ds2);
                        }
                    }
                    row2 = ds2.Rows[0];
                    employee = new Employee()
                    {
                        card_number = Convert.ToString(row2["emp_no"]),
                        name = Convert.ToString(row2["emp_name"]),
                        sex = Convert.ToString(row2["sex"]),
                        salary_type = Convert.ToString(row2["gzlb"]),
                        family_address = Convert.ToString(row2["family_addr"]),
                        account_number = Convert.ToString(row2["txm"]),
                        family_connector = Convert.ToString(row2["jsrxm"]),
                        identify_number = Convert.ToString(row2["id_code"]),
                        phone = Convert.ToString(row2["tp"]),
                        family_phone = Convert.ToString(row2["jsrdh"]),
                        household = Convert.ToString(row2["native_place"]),
                        degree = Convert.ToString(row2["study_level"]),
                        picture = Convert.IsDBNull(row2["Zp"]) ? null : (byte[])row2["Zp"],
                        Department1 = dept,
                        old_id=Convert.ToInt32(row["ID"]),
                        comment=Convert.ToString(row["住宿期间情况"])
                    };
                }
                db.Employee.InsertOnSubmit(employee);
                db.SubmitChanges();
                ds2.Clear();
                System.GC.Collect();
                backgroundWorker1.ReportProgress((++i) * 100 / count);
            }
            conn2.Close();
            conn2.Dispose();
            System.GC.Collect();
            return "success";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doWhat = 3;
            backgroundWorker1.RunWorkerAsync();
        }

        //最后通过该方法导入住宿表，则员工与宿舍之间的关系表。
        private string importLodging()
        {
            OleDbConnection conn = null;
            OleDbDataAdapter adap = null;
            DataTable dt = new DataTable();
            DataRow row;
            List<Lodging> list = new List<Lodging>();
            var emps = db.Employee.Where(em => em.id > 21139);
            conn = new OleDbConnection(Connstring);
            conn.Open();
            int count = emps.Count();
            int i = 0;
            foreach (Employee emp in emps)
            {
                
                adap = new OleDbDataAdapter("SELECT 姓名, 帐号, 标准月租, 卫生费, 房号, 原交押金, 入住时间, 身份证号, 性质 FROM JB where ID=" +emp.old_id, conn);
                adap.Fill(dt);
                
                row = dt.Rows[0];
                Lodging lodge = new Lodging();
                int dormInt=Int32.Parse(row["房号"].ToString());
                    lodge.Dorm = db.Dorm.Where(dor => dor.number == dormInt.ToString()).Where(dor=>dor.Area.name=="一区").First();
                    lodge.Employee = emp;
                    lodge.in_date = Convert.ToDateTime(row["入住时间"]);
                    lodge.real_rent = Convert.ToDecimal(row["标准月租"]);
                    lodge.real_manage = Convert.ToDecimal(row["卫生费"]);
                    lodge.guarantee = Convert.ToDecimal(row["原交押金"]);
                    lodge.classify_property = Convert.ToString(row["性质"]);                    
                
                list.Add(lodge);
                dt.Clear();
                adap.Dispose();
                System.GC.Collect();
                backgroundWorker1.ReportProgress((++i) * 100 / count);
            }
            conn.Close();
            conn.Dispose();
            db.Lodging.InsertAllOnSubmit(list);
            db.SubmitChanges();
            list = null;
            System.GC.Collect();
            return "success";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            doWhat = 4;
            backgroundWorker1.RunWorkerAsync();
        }

        private void FrmImportData_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string path = dataPath.Text.Trim();
            string pass = dataPass.Text.Trim();
            OleDbConnection conn = null;
            path = path.Replace("\\", "\\\\");
            Connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=true;Data Source=" + path + ";Jet OLEDB:Database Password=" + pass;
            try
            {
                conn = new OleDbConnection(Connstring);
                conn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("连接失败");
                return;
            }
            finally
            {
                conn.Close();
            }

            MessageBox.Show("连接成功");
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lbID.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbID.Text))
            {
                MessageBox.Show("请先选择需要删除的记录");
                return;
            }
            DialogResult res = MessageBox.Show("确定要删除ID为" + lbID.Text + "的记录吗？", "提示", MessageBoxButtons.OKCancel);
            if (res == DialogResult.Cancel)
                return;
            try
            {
                var dorm = db.Dorm.Single(dor => dor.id == Convert.ToInt32(lbID.Text));
                db.Dorm.DeleteOnSubmit(dorm);
                db.SubmitChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("有外键关联该记录，不能删除。");
                return;
            }
            lbID.Text = "";
            dataGridView1.DataSource = db.take_duplicate_record().ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "mdf files(*.mdb)|*.mdb";
            openFileDialog1.InitialDirectory = "D:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dataPath.Text = openFileDialog1.FileName;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (doWhat)
            {
                case 1:
                    e.Result = importDorms();
                    break;
                case 3:
                    e.Result = importEmployees();
                    break;
                case 4:
                    e.Result = importLodging();
                    break;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            if (e.Result.Equals("success"))
            {
                MessageBox.Show("success");
            }
        }


    }
}
