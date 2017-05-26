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
    public partial class FrmMovingOutFee : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string theMonth;
        DateTime firstDayInMonth, lastDayInMonth;
        public FrmMovingOutFee()
        {
            InitializeComponent();
        }

        private void FrmMovingOutFee_Load(object sender, EventArgs e)
        {
            lbID.Visible = false;
            int maxId = db.VerifyOrder.Max(v => v.id);
            theMonth = db.VerifyOrder.Single(v => v.id == maxId).year_and_month;
            theMonth = YearAndMonth.next(theMonth);
            firstDayInMonth = YearAndMonth.firstDayInMonth(theMonth);
            lastDayInMonth = YearAndMonth.lastDayInMonth(theMonth);
            lbCap.Text = string.Format("{0}年{1}月{2}员工退宿费用列表",theMonth.Substring(0,4),theMonth.Substring(4,2),LoginUser.operated_area);
            fetchDatas();
        }

        private void fetchDatas() {
            dataGridView1.DataSource = from f in db.OtherFee
                                       where f.year_month == theMonth //月份
                                       && f.Dorm.Area.name == LoginUser.operated_area  //区号
                                       && f.Employee.Lodging.Where(lo => lo.out_date == null).Where(lo => lo.Dorm == f.Dorm).Count() == 0  //已退宿
                                       select new
                                       {
                                           ID = f.id,
                                           宿舍 = f.Dorm.number,
                                           姓名 = f.Employee.name,
                                           分摊水费 = f.out_share_water,
                                           分摊电费 = f.out_share_eletricity,
                                           维修费 = f.repair_cost,
                                           扣分 = f.fine,
                                           招待所费用 = f.guesthouse_cost,
                                           其他费用 = f.other_cost,
                                           费用说明 = f.comment
                                       };
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["宿舍"].Width = 60;
            dataGridView1.Columns["姓名"].Width = 60;
            dataGridView1.Columns["分摊水费"].Width = 80;
            dataGridView1.Columns["分摊电费"].Width = 80;
            dataGridView1.Columns["维修费"].Width = 80;
            dataGridView1.Columns["扣分"].Width = 60;
            dataGridView1.Columns["招待所费用"].Width = 100;
            dataGridView1.Columns["其他费用"].Width = 80;
            dataGridView1.Columns["费用说明"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            lbID.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
            lbDorm.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["宿舍"].Value);
            lbName.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["姓名"].Value);
            tbRepair.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["维修费"].Value);
            tbFine.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["扣分"].Value);
            tbHouse.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["招待所费用"].Value);
            tbOther.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["其他费用"].Value);
            tbComment.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["费用说明"].Value);
            tbWater.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["分摊水费"].Value);
            tbEle.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["分摊电费"].Value);
        }

        //保存费用
        private void button2_Click(object sender, EventArgs e)
        {
            if ("ID".Equals(lbID.Text))
            {
                MessageBox.Show("请先选择一行在操作");
                return;
            }
            decimal repair = 0m, fine = 0m, house = 0m, other = 0m, water = 0m, ele = 0m;
            if (!string.IsNullOrEmpty(tbRepair.Text))
            {
                if (!decimal.TryParse(tbRepair.Text, out repair))
                {
                    MessageBox.Show("维修费用格式不正确");
                    tbRepair.Focus();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(tbHouse.Text))
            {
                if (!decimal.TryParse(tbHouse.Text, out house))
                {
                    MessageBox.Show("招待所费用格式不正确");
                    tbHouse.Focus();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(tbFine.Text))
            {
                if (!decimal.TryParse(tbFine.Text, out fine))
                {
                    MessageBox.Show("扣分格式不正确");
                    tbFine.Focus();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(tbOther.Text))
            {
                if (!decimal.TryParse(tbOther.Text, out other))
                {
                    MessageBox.Show("其他费用格式不正确");
                    tbOther.Focus();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(tbWater.Text)) {
                if (!decimal.TryParse(tbWater.Text, out water)) {
                    MessageBox.Show("分摊水费格式不正确");
                    tbWater.Focus();
                }
            }
            if (!string.IsNullOrEmpty(tbEle.Text))
            {
                if (!decimal.TryParse(tbEle.Text, out ele))
                {
                    MessageBox.Show("分摊电费格式不正确");
                    tbEle.Focus();
                }
            }
            int id = int.Parse(lbID.Text);
            OtherFee fee = db.OtherFee.Single(o => o.id == id);            
            fee.repair_cost = repair != 0m ? repair : (decimal?)null;
            fee.guesthouse_cost = house != 0m ? house : (decimal?)null;
            fee.fine = fine != 0m ? fine : (decimal?)null;
            fee.other_cost = other != 0m ? other : (decimal?)null;
            fee.out_share_water = water != 0m ? water : (decimal?)null;
            fee.out_share_eletricity = ele != 0m ? ele : (decimal?)null;
            fee.comment = tbComment.Text;
            db.SubmitChanges();
            MessageBox.Show("保存成功");
            fetchDatas();

            MyUtil.WriteEventLog("修改已退宿费用", lbDorm.Text, lbName.Text, "保存成功");
        }

        //搜索定位行
        private void button1_Click(object sender, EventArgs e)
        {
            string search = tbSearch.Text;
            if (string.IsNullOrEmpty(search)) 
                return;
            search = search.ToUpper();
            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (Convert.ToString(row.Cells["宿舍"].Value).Equals(search) || Convert.ToString(row.Cells["姓名"].Value).Equals(search)) {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells["宿舍"];
                    dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(1, row.Index));
                }
            }
        }
         
        //反退宿
        private void button3_Click(object sender, EventArgs e)
        {
            if ("ID".Equals(lbID.Text)) {
                MessageBox.Show("请先选择一行再操作");
                return;
            }
            if (MessageBox.Show(string.Format("确定要反退宿{0}房间的{1}吗？",lbDorm.Text,lbName.Text), "退宿确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                int id = Int32.Parse(lbID.Text);
                var otherFee = db.OtherFee.Single(o => o.id == id);
                var lodg = db.Lodging.Where(l => l.dorm_id == otherFee.dorm_id && l.emp_id == otherFee.emp_id).First();
                string info = "";
                var empl = lodg.Employee;
                var delfee = db.OtherFee.Where(d => d.emp_id == lodg.emp_id && d.dorm_id == lodg.dorm_id && d.out_share_water != null && d.year_month==theMonth).First();
                info += string.Format("反退宿成功！以下费用也将被删除：\n分摊水费：{0}\n分摊电费：{1}\n", delfee.out_share_water, delfee.out_share_eletricity);
                info += delfee.repair_cost > 0 ? "维修费：" + delfee.repair_cost.ToString() + "\n" : "";
                info += delfee.fine > 0 ? "扣分：" + delfee.fine.ToString() + "\n" : "";
                info += delfee.guesthouse_cost > 0 ? "招待所：" + delfee.guesthouse_cost.ToString() + "\n" : "";
                info += delfee.other_cost > 0 ? "其他费用：" + delfee.other_cost.ToString() + "\n" : "";
                info += !string.IsNullOrEmpty(delfee.comment) ? "备注：\n" + delfee.comment : "";

                lodg.out_date = null;
                db.OtherFee.DeleteOnSubmit(delfee);
                db.SubmitChanges();
                fetchDatas();

                //将住宿信息反馈到HR系统，2013-3-22更新

                if (empl.Department1.property.Equals("厂内") || empl.Department1.property.Equals("光电"))
                {
                    if (!string.IsNullOrWhiteSpace(empl.account_number))
                    {
                        try
                        {
                            db.updateHRLivingState(empl.account_number, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("连接失败，员工反退宿信息无法更新到人事系统。");
                            MyUtil.WriteEventLog("反退宿", lbDorm.Text, lbName.Text, "连接失败，员工反退宿信息无法更新到人事系统:" + ex.Message, false);
                        }
                    }
                }

                //写入日志
                MyUtil.WriteEventLog("反退宿", lbDorm.Text, lbName.Text, info);
                MessageBox.Show(info);
            }
        }
    }
}
