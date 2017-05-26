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
    public partial class FrmGuestCheck : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        int SelectedRowIndex = -1;
        Color selectedRowColor = Color.White;
        public FrmGuestCheck()
        {
            InitializeComponent();
            this.Load += new EventHandler(button5_Click);
        }

        private void FrmGuestCheck_Load(object sender, EventArgs e)
        {
            cbBus.Text = "全部";
            cbBalance.Text = "未结算";
            button2.Enabled = button3.Enabled = button4.Enabled = false;
        }

        //查询按钮事件
        private void button5_Click(object sender, EventArgs e)
        {
            bool isFinish=cbBalance.Text=="未结算"?false:true;
            bool isBus = cbBus.Text == "公事" ? true : false;
            string content = tbContent.Text.Trim();
            var result = from i in db.GuestInfos
                         where !i.is_deleted
                         select i;
            if (!string.IsNullOrEmpty(content)) {
                result = result.Where(r => r.dorm_number.Contains(content) || r.living_people.Contains(content) || r.dep.Contains(content));
            }

            if (!cbBus.Text.Equals("全部")) {
                result = result.Where(r => r.business == isBus);
            }

            if (!cbBalance.Text.Equals("全部")) {
                result = result.Where(r => r.is_finish == isFinish);
            }

            dataGridView1.DataSource = from r in result
                                       orderby r.out_date
                                       select new
                                       {
                                           id = r.id,
                                           房号 = r.dorm_number,
                                           姓名 = r.living_people,
                                           性别 = r.sex,
                                           部门 = r.dep,
                                           性质 = r.business ? "公事" : "私事",
                                           入住日期 = r.in_date == null ? null : ((DateTime)r.in_date).ToShortDateString(),
                                           责任人 = r.charger,
                                           退房人 = r.checkout,
                                           退房日期 = r.real_out_date == null ? (r.out_date == null ? null : ((DateTime)r.out_date).ToShortDateString()) : ((DateTime)r.real_out_date).ToShortDateString(),
                                           单价 = r.price,
                                           金额= r.sum,
                                           结算标志 = r.is_finish,
                                           备注 = r.comment
                                       };
            button2.Enabled = button3.Enabled = button4.Enabled = false;
        }

        //设置列宽与隐藏列
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["房号"].Width = 70;
            dataGridView1.Columns["姓名"].Width = 70;
            dataGridView1.Columns["责任人"].Width = 70;
            dataGridView1.Columns["退房人"].Width = 70;
            dataGridView1.Columns["性别"].Width = 55;
            dataGridView1.Columns["性质"].Width = 60;
            dataGridView1.Columns["单价"].Width = 55;
            dataGridView1.Columns["金额"].Width = 60;
            dataGridView1.Columns["姓名"].Width = 70;
            dataGridView1.Columns["入住日期"].Width = 80;
            dataGridView1.Columns["退房日期"].Width = 80;
            dataGridView1.Columns["结算标志"].Width = 70;
            dataGridView1.Columns["备注"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 使用不同背景色区分则将到期或已过期的住宿者
            DateTime today = DateTime.Now.Date;
            DateTime shouldOutDay;
            if (!cbBalance.Text.Equals("已结算")) {
                foreach (DataGridViewRow row in dataGridView1.Rows) {
                    if (!Convert.ToBoolean(row.Cells["结算标志"].Value)) {
                        shouldOutDay = Convert.ToDateTime(row.Cells["退房日期"].Value);
                        if (shouldOutDay < today) {
                            dataGridView1.Rows[row.Index].DefaultCellStyle.BackColor = Color.Orange;
                        }
                        else if ((shouldOutDay - today).Days <= 1) {
                            dataGridView1.Rows[row.Index].DefaultCellStyle.BackColor = Color.Gold;
                        }                        
                    }
                }
            }
        }

        //单击单元格行变色,并设置结算、修改和删除按钮是否可用
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //使上一次选择行恢复默认背景色
            if (SelectedRowIndex >= 0 && SelectedRowIndex < dataGridView1.Rows.Count) {
                dataGridView1.Rows[SelectedRowIndex].DefaultCellStyle.BackColor = selectedRowColor;
            }
            if (e.RowIndex >= 0)
            {
                SelectedRowIndex = e.RowIndex;
                selectedRowColor = dataGridView1.CurrentRow.DefaultCellStyle.BackColor;
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.SkyBlue;                

                if ((bool)dataGridView1.Rows[e.RowIndex].Cells["结算标志"].Value)
                {
                    button2.Enabled = button3.Enabled = button4.Enabled = false;
                }
                else {
                    button2.Enabled = button3.Enabled = button4.Enabled = true;
                }
            }
        }        

        //修改记录
        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedRowIndex >= 0 && SelectedRowIndex < dataGridView1.Rows.Count)
            {
                int updateId = (int)dataGridView1.Rows[SelectedRowIndex].Cells["id"].Value;
                FrmGuestIn guest = new FrmGuestIn();
                guest.updateId = updateId;
                guest.ShowDialog();
                
                //刷新列表
                button5.PerformClick();
            }            
            

        }

        //删除记录
        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectedRowIndex >= 0 && SelectedRowIndex < dataGridView1.Rows.Count)
            {
                if (MessageBox.Show("确定要删除这条记录吗？", "删除警告", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    int id = (int)dataGridView1.Rows[SelectedRowIndex].Cells["id"].Value;
                    var deletedItem = db.GuestInfos.Single(d => d.id == id);
                    deletedItem.is_deleted = true;
                    db.SubmitChanges();
                    MessageBox.Show("删除成功！");
                    button5.PerformClick();                    
                }                
            }
        }

        //新增记录
        private void button1_Click(object sender, EventArgs e)
        {
            FrmGuestIn guestIn = new FrmGuestIn();
            guestIn.ShowDialog();

            //刷新列表
            button5.PerformClick();
        }

        //进行结算
        private void button3_Click(object sender, EventArgs e)
        {
            if (SelectedRowIndex >= 0 && SelectedRowIndex < dataGridView1.Rows.Count)
            {
                int balanceId = (int)dataGridView1.Rows[SelectedRowIndex].Cells["id"].Value;
                FrmGuestBalance balance = new FrmGuestBalance();
                balance.balanceId = balanceId;
                balance.ShowDialog();

                //刷新列表
                button5.PerformClick();
            }
        }
    }
}
