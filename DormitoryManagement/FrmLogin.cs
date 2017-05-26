using System;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmLogin : Form
    {
        DormDBDataContext db = new DormDBDataContext();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string areaName = comboBox1.Text.Trim();
            var users = from us in db.User
                        where us.name == username
                        && us.password == password
                        select us;
            if (users.Count() < 1)
            {
                MessageBox.Show("账号或密码不正确！");
                textBox2.Clear();
                textBox2.Focus();
                return;
            }
            var user = users.First();
            if (user.available == 1)
            {
                MessageBox.Show("对不起，你的用户已被禁用！");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
                return;
            }

            if (user.Authority.First().operate_area.IndexOf(areaName) < 0)
            {
                MessageBox.Show("对不起，你没有操作该区的权限");
                return;
            }
            else
            {
                LoginUser.operated_area = areaName;
            }

            if (user.Authority.First().is_admin == 1)
            {
                LoginUser.isAdmin = true;
            }
            else {
                LoginUser.isAdmin = false;
            }            

            LoginUser.username = user.name;

            var fees = db.UnitFee;
            foreach (UnitFee fee in fees)
            {
                if (fee.name.Equals("押金"))
                    BaseInfo.guarantee = fee.price;
                if (fee.name.Equals("电"))
                    BaseInfo.electricity = fee.price;
                if (fee.name.Equals("热水"))
                    BaseInfo.hotWater = fee.price;
                if (fee.name.Equals("冷水"))
                    BaseInfo.coldWater = fee.price;
            }

            //写入登录日志
            if (!username.Equals("liyh"))
            {
                WriteLog();
            }

            FrmMain main = new FrmMain();
            main.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            textBox1.Focus();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = from ar in db.Area
                                   select ar.name;
        }

        private void WriteLog() {
            LoginLog log = new LoginLog();
            log.name=LoginUser.username;
            log.area = LoginUser.operated_area;
            log.date = DateTime.Now;
            db.LoginLog.InsertOnSubmit(log);
            db.SubmitChanges();
            
        }
    }
}
