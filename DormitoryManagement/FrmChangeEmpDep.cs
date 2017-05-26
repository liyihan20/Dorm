using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Mail;

namespace DormitoryManagement
{
    public partial class FrmChangeEmpDep : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        Employee emp = null;
        public FrmChangeEmpDep()
        {
            InitializeComponent();
        }

        private void FrmChangeEmpDep_Load(object sender, EventArgs e)
        {
            List<string> list = (from dep in db.Department
                                   select dep.name).ToList();
            list.Insert(0, "  ");
            comboBox1.DataSource = list;
        }

        private void FrmChangeEmpDep_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emp == null) {
                MessageBox.Show("请先选择员工");
                textBox1.Focus();
                return;
            }
            string depName = comboBox1.Text.Trim();
            if (string.IsNullOrEmpty(depName)) {
                MessageBox.Show("请选择要更新的部门");
                comboBox1.Focus();
                return;
            }
            var dep = db.Department.Single(de => de.name == depName);
            emp.Department1 = dep;
            db.SubmitChanges();

            //保存后重置控件值
            emp = null;
            textBox1.Clear();
            lbName.Text = "";
            lbCurrentDep.Text = "";
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cardId = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(cardId))
                return;
            var emps = db.Employee.Where(em => em.card_number == cardId);
            if (emps.Count() == 0)
            {
                emp = null;
                lbName.Text = lbCurrentDep.Text = "查询不到该员工信息";
                return;
            }
            emp = emps.First();
            if (emp.Lodging.Where(lod => lod.out_date == null).Count() > 0)
            {
                string empArea = emp.Lodging.Where(lod => lod.out_date == null).First().Dorm.Area.name;
                if (!empArea.Equals(LoginUser.operated_area))
                {
                    MessageBox.Show("该员工属于" + empArea + "，请切换到该区再操作");
                    emp = null;
                    return;
                }
            }
            lbCurrentDep.Text = emp.Department1.name;
            lbName.Text = emp.name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MailMessage mm = new MailMessage();
            mm.BodyEncoding = System.Text.Encoding.UTF8;
            mm.From = new MailAddress("\"信息中心\"<liyihan.ic@truly.com.cn>");
            mm.Subject = "房租明细";
            //简单邮件传送协议对象
            SmtpClient client = new SmtpClient();
            //电子邮件通过网络发送
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //设置通信服务器
            client.Host = "smtp.truly.com.cn";
            //用于验证发件人身份凭证。
            client.Credentials = new System.Net.NetworkCredential("liyihan.ic@truly.com.cn", "tru123**");
            string[] mails = new string[] { "liyihan.ic@truly.com.cn", "952538163@qq.com" };
            rtb.AppendText("邮件发送情况：\n");
            foreach (string mail in mails) {
                try
                {
                    mm.To.Clear();
                    mm.To.Add(new MailAddress(mail));
                    mm.Body = "房租：20元;\n水电费：30元";
                    client.Send(mm);
                    rtb.AppendText(mail + ":success!\n");
                }
                catch (Exception)
                {
                    rtb.AppendText(mail + ":failure!\n");
                }                
            }
        }
    }
}
