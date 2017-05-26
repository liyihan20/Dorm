using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmOperator : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmOperator()
        {
            InitializeComponent();
        }

        private void FrmOperator_Load(object sender, EventArgs e)
        {

            //动态生成宿舍区的checkbox
            var areas = (from are in db.Area
                        select are).ToList();
            CheckBox[] cb = new CheckBox[areas.Count()];
            for (int i = 0; i < cb.Count(); i++)
            {
                cb[i] = new CheckBox();
                cb[i].Name = "checkbox" + (i + 1).ToString();
                cb[i].Text = areas[i].name;
                cb[i].Width = 60;
                cb[i].Location = new Point(70*i, 0);
            }
            panel1.Controls.AddRange(cb);

            //填充datagridview1
            setGridDatas();
            comboBox2.Text = "可用";
            comboBox1.Text = "否";
        }

        private void setGridDatas() {
            dataGridView1.DataSource = from us in db.User
                                       where us.name!="liyh"
                                       select new
                                       {
                                           姓名 = us.name,
                                           可操作宿舍区 = us.Authority.First().operate_area,
                                           是否管理员 = us.Authority.First().is_admin == 0 ? "不是" : "是",                                           
                                           是否可用 = us.available == 1 ? "禁用" : "可用",
                                           注册日期 = us.register_date,
                                           备注 = us.comment,
                                       };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearContent();
            textBox1.Enabled = true;
            lbdate.Text = DateTime.Now.ToShortDateString();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string opreAreas = "";
            foreach (Control ct in panel1.Controls)
            {
                if (((CheckBox)ct).Checked == true)
                    opreAreas += "|"+((CheckBox)ct).Text+"|";
            }
            if (textBox1.Enabled == false)
            {
                var user = db.User.Single(us => us.name == textBox1.Text);
                user.available = comboBox2.Text == "可用" ? (short)0 : (short)1;
                user.comment = richTextBox1.Text;
                user.Authority.First().operate_area = opreAreas;
                user.Authority.First().is_admin = comboBox1.Text == "否" ? (short)0 : (short)1;                
                db.SubmitChanges();
                setGridDatas();
                MessageBox.Show("修改成功");
            }
            else {
                if (db.User.Where(u => u.name == textBox1.Text.Trim()).Count() > 0) {
                    MessageBox.Show("该用户名已有人使用，请修改");
                    textBox1.Focus();
                    return;
                }
                User user = new User();
                Authority auth = new Authority();
                user.name = textBox1.Text.Trim();
                user.available = comboBox2.Text == "可用" ? (short)0 : (short)1;
                user.comment = richTextBox1.Text;
                user.password = "000000";
                user.register_date = DateTime.Parse(lbdate.Text);
                auth.is_admin = comboBox1.Text == "否" ? (short)0 : (short)1;
                auth.operate_area = opreAreas;                
                auth.User = user;
                db.Authority.InsertOnSubmit(auth);
                db.SubmitChanges();
                textBox1.Enabled = false;
                setGridDatas();
                MessageBox.Show("插入成功");                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;            
            clearContent();
        }

        private void clearContent() {
            textBox1.Clear();
            lbdate.Text = "";
            comboBox1.Text = "否";
            comboBox2.Text = "可用";            
            richTextBox1.Clear();
            foreach (Control ct in panel1.Controls)
            {
                ((CheckBox)ct).Checked = false;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            string operateAreas = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            comboBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);            
            comboBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            lbdate.Text =Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            richTextBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value); 
            if (!string.IsNullOrEmpty(operateAreas))
            {
                string areas = (string)operateAreas;
                foreach (Control ct in panel1.Controls)
                {
                    CheckBox tb = (CheckBox)ct;
                    if (areas.IndexOf(tb.Text) >= 0)
                    {
                        tb.Checked = true;
                    }
                    else {
                        tb.Checked = false;
                    }
                }
            }
            textBox1.Enabled = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
