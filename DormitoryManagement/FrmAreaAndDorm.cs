using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmAreaAndDorm : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmAreaAndDorm()
        {
            InitializeComponent();
            cbAreaNum.DataSource = from are in db.Area
                                   select are.name;
            cbDormType.DataSource = from dtp in db.DormType
                                    select dtp.name;
            cbAva.Text = "可用";
            button4.Enabled = false;


        }

        private void FrmAreaAndDorm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“areaDataSet.dormitory_area”中。您可以根据需要移动或删除它。
            this.dormitory_areaTableAdapter.Fill(this.areaDataSet.dormitory_area);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string areaName = tbAreaName.Text.ToString().Trim();
            if (string.IsNullOrEmpty(areaName))
            {
                MessageBox.Show("请先输入宿舍区名");
                return;
            }
            string areaComment = rtbAreaComment.Text.ToString().Trim();
            var areas = from ar in db.Area
                        where ar.name == areaName
                        select ar;
            if (areas.Count() > 0)
            {
                var area = areas.First();
                area.comment = areaComment;
                db.SubmitChanges();
            }
            else
            {
                Area area = new Area()
                {
                    name = areaName,
                    comment = areaComment
                };
                db.Area.InsertOnSubmit(area);
                db.SubmitChanges();
            }
            this.dormitory_areaTableAdapter.Fill(this.areaDataSet.dormitory_area);
            cbAreaNum.DataSource = from are in db.Area
                                   select are.name;
            tbAreaName.Clear();
            rtbAreaComment.Clear();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tbAreaName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            rtbAreaComment.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string areaName = tbAreaName.Text.ToString().Trim();
            if (string.IsNullOrEmpty(areaName))
            {
                MessageBox.Show("请先选择需要删除的区号");
                return;
            }
            var areas = from ar in db.Area
                        where ar.name == areaName
                        select ar;
            if (areas.Count() < 1)
            {
                MessageBox.Show("不存在这条记录");
                return;
            }
            var area = areas.First();
            if (area.Dorm.Count() > 0)
            {
                MessageBox.Show("该区号与宿舍有关联，不能删除！");
                return;
            }
            if (MessageBox.Show("确定要删除这条记录吗?", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                db.Area.DeleteOnSubmit(area);
                db.SubmitChanges();
                this.dormitory_areaTableAdapter.Fill(this.areaDataSet.dormitory_area);
                cbAreaNum.DataSource = from are in db.Area
                                       select are.name;
                tbAreaName.Clear();
                rtbAreaComment.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbDormSex.Text)) {
                MessageBox.Show("请选择入住性别");
                cbDormSex.Focus();
                return;
            }
            string dormStringFrom, dormStringTo;
            dormStringFrom = tbNumberFrom.Text.Trim().ToUpper();
            dormStringTo = tbNumberTo.Text.Trim().ToUpper();
            //suffix表示栋数，一区有a、b栋，其它的都是数字的
            string suffix = dormStringFrom.Substring(0, dormStringFrom.Length - 3);
            int dormFrom;
            int dormTo;
            try
            {
                dormFrom = int.Parse(dormStringFrom.Substring(dormStringFrom.Length - 3, 3));
                if (string.IsNullOrEmpty(tbNumberTo.Text.ToString().Trim()))
                    dormTo = dormFrom;
                else
                    dormTo = int.Parse(dormStringTo.Substring(dormStringTo.Length - 3, 3));
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入正确的宿舍编号");
                return;
            }
            if (dormTo < dormFrom)
            {
                MessageBox.Show("第一个宿舍编号必须比第二个小");
                tbNumberFrom.Clear();
                tbNumberTo.Clear();
                tbNumberFrom.Focus();
                return;
            }

            float rent, manage;
            if (!float.TryParse(tbRent.Text, out rent)) {
                MessageBox.Show("租金输入不正确");
                tbRent.Focus();
                return;
            }
            if (!float.TryParse(tbManage.Text, out manage))
            {
                MessageBox.Show("管理费输入不正确");
                tbManage.Focus();
                return;
            }

            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add(new DataColumn("宿舍编号", typeof(string)));
                dt.Columns.Add(new DataColumn("区号", typeof(string)));
                dt.Columns.Add(new DataColumn("类型", typeof(string)));
                dt.Columns.Add(new DataColumn("入住性别", typeof(string)));
                dt.Columns.Add(new DataColumn("租金", typeof(string)));
                dt.Columns.Add(new DataColumn("管理费", typeof(string)));
                dt.Columns.Add(new DataColumn("是否可用", typeof(string)));
                dt.Columns.Add(new DataColumn("备注", typeof(string)));
                for (int i = dormFrom; i <= dormTo; i++)
                {
                    dt.Rows.Add(new object[] { suffix+i, cbAreaNum.Text, cbDormType.Text, cbDormSex.Text,rent.ToString(),manage.ToString(), cbAva.Text, rtbDormComment.Text });
                }
                dataGridView2.DataSource = dt;
            }
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Dorm> insertList=new List<Dorm>();
            string number,orderNumber;
            int insertnum = 0;
            int updatenum = 0;

            if (dataGridView2.Rows.Count < 1)
            {
                MessageBox.Show("不存在可以保存的数据");
                return;
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                number = row.Cells["宿舍编号"].Value.ToString();
                if (number.Substring(0, 1).Equals("A") || number.Substring(0, 1).Equals("B"))
                {
                    orderNumber = number.Substring(0, 1) + number;
                }
                else {
                    orderNumber = number;
                }

                var dorms = db.Dorm.Where(dor => dor.number == number);
                if (dorms.Count() > 0)
                {
                    updatenum++;
                    var dorm = dorms.First();
                    dorm.Area = db.Area.Where(ar => ar.name == row.Cells["区号"].Value.ToString()).First();
                    dorm.DormType = db.DormType.Where(dot => dot.name == row.Cells["类型"].Value.ToString()).First();
                    dorm.dormSex = Convert.ToString(row.Cells["入住性别"].Value);

                    //将新租金和管理费应用到该宿舍下的在住员工
                    if (dorm.rent != Convert.ToDecimal(row.Cells["租金"].Value)) {
                        var lodgs = dorm.Lodging.Where(lo => lo.out_date == null);
                        if (lodgs.Count() > 0) {
                            if (MessageBox.Show(string.Format("要将{0}房在住员工的房租和管理费更新吗?", dorm.number), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                                foreach (var lod in lodgs) {
                                    lod.real_manage = dorm.manageCost = Convert.ToDecimal(row.Cells["管理费"].Value);
                                    lod.real_rent = Convert.ToDecimal(row.Cells["租金"].Value);
                                }
                            }
                        }
                    }
                    dorm.rent = Convert.ToDecimal(row.Cells["租金"].Value);
                    dorm.manageCost = Convert.ToDecimal(row.Cells["管理费"].Value);
                    dorm.available = row.Cells["是否可用"].Value.ToString().Equals("可用") ? (short)0 : (short)1;
                    dorm.comment = row.Cells["备注"].Value.ToString();
                    db.SubmitChanges();
                }
                else {
                    insertnum++;
                    insertList.Add(new Dorm()
                    {
                        number = number,
                        forOrder=orderNumber,
                        Area = db.Area.Where(ar => ar.name == row.Cells["区号"].Value.ToString()).First(),
                        DormType = db.DormType.Where(dot => dot.name == row.Cells["类型"].Value.ToString()).First(),
                        dormSex = Convert.ToString(row.Cells["入住性别"].Value),
                        rent = Convert.ToDecimal(row.Cells["租金"].Value),
                        manageCost = Convert.ToDecimal(row.Cells["管理费"].Value),
                        available = row.Cells["是否可用"].Value.ToString().Equals("可用") ? (short)0 : (short)1,
                        comment = row.Cells["备注"].Value.ToString()
                    });
                }
            }
            if (insertnum > 0) {
                db.Dorm.InsertAllOnSubmit(insertList);
                db.SubmitChanges();
            }
            MessageBox.Show("成功更新" + updatenum + "条记录，插入" + insertnum + "条记录");
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = from dom in db.Dorm
                                       orderby dom.Area.id, dom.forOrder.Length, dom.forOrder
                                       select new
                                       {
                                           宿舍编号 = dom.number,
                                           区号 = dom.Area.name,
                                           类型 = dom.DormType.name, 
                                           入住性别=dom.dormSex,
                                           租金=dom.rent,
                                           管理费=dom.manageCost,
                                           是否可用 = dom.available == 0 ? "可用" : "禁用",
                                           备注 = dom.comment
                                       };
            button4.Enabled = false;
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.Columns["区号"].Width = 60;
            dataGridView2.Columns["类型"].Width = 80;
            dataGridView2.Columns["入住性别"].Width = 80;
            dataGridView2.Columns["是否可用"].Width = 80;
        }

        private void FrmAreaAndDorm_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox2.Left = this.Width / 2 - groupBox2.Width / 2;
        }
       
    }
}
