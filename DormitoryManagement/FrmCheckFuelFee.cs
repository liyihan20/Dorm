using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmCheckFuelFee : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmCheckFuelFee()
        {
            InitializeComponent();
        }

        private void FrmCheckFuelFee_Load(object sender, EventArgs e)
        {
            //年月份列表
            cbMonth.DataSource = from ver in db.VerifyOrder
                                 where ver.can_export == 1
                                 orderby ver.year_and_month descending
                                 select ver.year_and_month;
            //宿舍区列表
            List<string> list = (from are in db.Area
                                 select are.name).ToList();
            cbArea.DataSource = list;
            //宿舍列表
            List<string> listDorm = (from dor in db.Dorm
                                     where dor.Area.name == list[0]
                                     select dor.number).ToList();
            listDorm.Insert(0, "所有房");
            cbDorm.DataSource = listDorm;

            //单位费用
            UnitFee unit = db.UnitFee.Single(un => un.name == "冷水");
            lbUnitCold.Text = string.Format("{0}({1})", unit.price, unit.units);

            unit = db.UnitFee.Single(un => un.name == "热水");
            lbUnitHot.Text = string.Format("{0}({1})", unit.price, unit.units);

            unit = db.UnitFee.Single(un => un.name == "电");
            lbUnitElec.Text = string.Format("{0}({1})", unit.price, unit.units);
        }

        private void cbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = (from dor in db.Dorm
                                 where dor.Area.name == cbArea.Text
                                 select dor.number).ToList();
            list.Insert(0, "所有房");
            cbDorm.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yearMonth = cbMonth.Text;
            string area = cbArea.Text;
            string dorm = cbDorm.Text;
            dataGridView1.DataSource = db.check_fuels_used(area, dorm, YearAndMonth.previous(yearMonth), yearMonth);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置列宽
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 70;
            dataGridView1.Columns[9].Width = 70;
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //计算合计
            decimal totalCold = 0, totalHot = 0, totalElec = 0;
            decimal coldQty = 0, hotQty = 0, elecQty = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["冷水费"].Value != DBNull.Value)
                {
                    totalCold += Convert.ToDecimal(row.Cells["冷水费"].Value);
                    if (Convert.ToDecimal(row.Cells["本月冷水"].Value) - Convert.ToDecimal(row.Cells["上月冷水"].Value) >= 0)
                    {
                        coldQty += Convert.ToDecimal(row.Cells["本月冷水"].Value) - Convert.ToDecimal(row.Cells["上月冷水"].Value);
                    }
                    else
                    {
                        coldQty += Convert.ToDecimal(row.Cells["本月冷水"].Value) - Convert.ToDecimal(row.Cells["上月冷水"].Value) + 10000;
                    }
                }
                if (row.Cells["热水费"].Value != DBNull.Value)
                {
                    totalHot += Convert.ToDecimal(row.Cells["热水费"].Value);
                    if (Convert.ToDecimal(row.Cells["本月热水"].Value) - Convert.ToDecimal(row.Cells["上月热水"].Value) >= 0)
                    {
                        hotQty += Convert.ToDecimal(row.Cells["本月热水"].Value) - Convert.ToDecimal(row.Cells["上月热水"].Value);
                    }
                    else
                    {
                        hotQty += Convert.ToDecimal(row.Cells["本月热水"].Value) - Convert.ToDecimal(row.Cells["上月热水"].Value) + 10000;
                    }
                }
                if (row.Cells["电费"].Value != DBNull.Value)
                {
                    totalElec += Convert.ToDecimal(row.Cells["电费"].Value);
                    if (Convert.ToDecimal(row.Cells["本月用电"].Value) - Convert.ToDecimal(row.Cells["上月用电"].Value) >= 0)
                    {
                        elecQty += Convert.ToDecimal(row.Cells["本月用电"].Value) - Convert.ToDecimal(row.Cells["上月用电"].Value);
                    }
                    else
                    {
                        elecQty += Convert.ToDecimal(row.Cells["本月用电"].Value) - Convert.ToDecimal(row.Cells["上月用电"].Value) + 10000;
                    }
                }
            }
            lbTotalCold.Text = totalCold + "(元)";
            lbTotalHot.Text = totalHot + "(元)";
            lbTotalElec.Text = totalElec + "(元)";
            lbColdQty.Text = coldQty + "(吨)";
            lbHotQty.Text = hotQty + "(吨)";
            lbEleQty.Text = elecQty + "(度)";
        }

        private void FrmCheckFuelFee_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
        }


    }
}
