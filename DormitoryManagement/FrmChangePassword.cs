using System;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmChangePassword : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string old = tbOld.Text.Trim();
            string new1=tbNew1.Text.Trim();
            string new2 = tbNew2.Text.Trim();
            if (string.IsNullOrEmpty(new1)) {
                MessageBox.Show("新密码不能为空");
                tbNew1.Focus();
                return;
            }
            if (!new1.Equals(new2)) {
                MessageBox.Show("两次输入的密码不一致");
                tbNew2.Focus();
                return;
            }
            var user = db.User.Single(u => u.name == LoginUser.username);
            if (!old.Equals(user.password)) {
                MessageBox.Show("旧密码不正确");
                tbOld.Focus();
                return;
            }
            user.password = new1;
            db.SubmitChanges();
            MessageBox.Show("密码修改成功！");
            this.Close();
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            tbOld.Focus();
        }

        private void FrmChangePassword_Resize(object sender, EventArgs e)
        {
            groupBox1.Top = (this.Height - groupBox1.Height) / 2 - 20;
            groupBox1.Left = (this.Width - groupBox1.Width) / 2;
        }

    }
}
