using System;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmDepartment : Form
    {
        DormDBDataContext dorm = new DormDBDataContext();

        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            showDatas();
            cbProperty.Text = "厂内";
            disableInput();
        }

        private void showDatas() {
            dataGridView1.DataSource = from dep in dorm.Department
                                       orderby dep.number
                                       select new
                                       {
                                           序号 = dep.number,
                                           名称 = dep.name,
                                           部门属性 = dep.property,
                                           备注 = dep.comment,
                                           ID = dep.id
                                       };
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            string name = tbName.Text.Trim();
            string property = cbProperty.Text.Trim();
            string number = tbNumber.Text.Trim();
            string comment = richTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(name)) {
                MessageBox.Show("名称不能为空");
                tbName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(number)) {
                MessageBox.Show("序号不能为空");
                tbNumber.Focus();
                return;
            }
            int num = Int32.Parse(number);
            if (button1.Text == "更新")
            {
                int id = Int32.Parse(lbID.Text);
                var department = dorm.Department.Single(de => de.id == id);
                    department.number = num;
                    department.name = name;
                    department.property = property;
                    department.comment = comment;
                    dorm.SubmitChanges();                
            }
            else
            {
                Department department = new Department()
                {
                    number = num,
                    name = name,
                    property = property,
                    comment = comment
                };
                dorm.Department.InsertOnSubmit(department);
                dorm.SubmitChanges();
            }
            clearInput();
            disableInput();
            showDatas();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lbID.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            tbNumber.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            tbName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            cbProperty.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
            richTextBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            button1.Text = "更新";
            enableInput();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearInput();
            enableInput();
            button1.Text = "保存";
            tbNumber.Focus();
        }

        //使控件接受用户输入
        private void enableInput() {
            tbName.Enabled = true;
            tbNumber.Enabled =true;
            cbProperty.Enabled = true;
            richTextBox1.Enabled = true;
        }

        //使控件不接受用户输入
        private void disableInput() {
            tbName.Enabled = false;
            tbNumber.Enabled = false;
            cbProperty.Enabled = false;
            richTextBox1.Enabled = false;
        }

        //还原控件内容
        private void clearInput() {
            tbNumber.Clear();
            tbName.Clear();
            tbNumber.Focus();
            cbProperty.Text = "产内";
            richTextBox1.Clear();
        }
        //private void button2_Click(object sender, EventArgs e)
        //{
            //string Connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=true;Data Source=C:\\Documents and Settings\\1104280101\\桌面\\后勤部宿舍管理系统\\后勤部数据库3区5区(新）.mdb;Jet OLEDB:Database Password=1112";
            //OleDbConnection conn = null;
            //OleDbDataAdapter adap = null;
            //DataSet ds = null;
            //List<Department> list = new List<Department>();

            //conn = new OleDbConnection(Connstring);
            //conn.Open();
            //adap = new OleDbDataAdapter("select * from BM", conn);
            //ds = new DataSet();
            //adap.Fill(ds);

            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    list.Add(new Department()
            //    {
            //        name = row[1].ToString()
            //    });
            //}
            //dorm.Department.InsertAllOnSubmit(list);
            //dorm.SubmitChanges();
            //MessageBox.Show("success.");
        //}
    }
}
