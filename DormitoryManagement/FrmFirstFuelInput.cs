using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmFirstFuelInput : Form
    {
        string yearMonthPre;
        DormDBDataContext db = new DormDBDataContext();
        public FrmFirstFuelInput()
        {
            InitializeComponent();
        }

        private void FrmFirstFuelInput_Load(object sender, EventArgs e)
        {
            string yearMonthNow = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
            yearMonthPre = YearAndMonth.previous(yearMonthNow);
            lbYearMonth.Text = yearMonthPre.ToString();

            bindDatas();
        }
        private void bindDatas()
        {
            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("区号", typeof(string));
                dt.Columns.Add("宿舍ID", typeof(Int32));
                dt.Columns.Add("宿舍号", typeof(string));
                dt.Columns.Add("本月用电读数", typeof(string));
                dt.Columns.Add("本月冷水读数", typeof(string));
                dt.Columns.Add("本月热水读数", typeof(string));

                //将数据填充到datagridview 

                foreach (var fuel in db.take_first_fuel_input(LoginUser.operated_area, yearMonthPre))
                {
                    dt.Rows.Add(fuel.areaName,
                        fuel.dormId,
                        fuel.dormNumber,
                        fuel.elec,
                        fuel.cold,
                        fuel.hot);
                }
                dataGridView1.DataSource = dt;
            }

            //统计信息grid

            int totalDormCount = 0;
            int totalHasInput = 0;
            int totalNotInput = 0;
            double? totalElec = 0;
            double? totalCold = 0;
            double? totalHot = 0;
            using (DataTable dt2 = new DataTable())
            {
                dt2.Columns.Add("区号", typeof(string));
                dt2.Columns.Add("宿舍总数", typeof(Int32));
                dt2.Columns.Add("已录入宿舍", typeof(string));
                dt2.Columns.Add("待录入宿舍", typeof(string));
                dt2.Columns.Add("电费合计", typeof(double));
                dt2.Columns.Add("冷水合计", typeof(double));
                dt2.Columns.Add("热水合计", typeof(double));

                foreach (Area area in db.Area)
                {
                    int dormCount = area.Dorm.Count();
                    var fuels = from fuel in db.FuelFee
                                where fuel.Dorm.Area == area
                                && fuel.year_month == yearMonthPre
                                select fuel;
                    int hasInput = fuels.Count();
                    int notInput = dormCount - hasInput;
                    double? elec = (from fuel in fuels
                                  select fuel.electricity).Sum();
                    double? cold = (from fuel in fuels
                                    select fuel.cold_water).Sum();
                    double? hot = (from fuel in fuels
                                   select fuel.hot_water).Sum();
                    dt2.Rows.Add(area.name, dormCount, hasInput, notInput, elec, cold, hot);
                    totalDormCount += dormCount;
                    totalHasInput += hasInput;
                    totalNotInput += notInput;
                    totalElec += elec == null ? 0 : elec;
                    totalCold += cold == null ? 0 : cold;
                    totalHot += hot == null ? 0 : hot;
                }
                dt2.Rows.Add("合计", totalDormCount, totalHasInput, totalNotInput,totalElec,totalCold,totalHot);
                dataGridView2.DataSource = dt2;
            }

            //如果全部宿舍录入完成，verify_order表新增记录
            if (totalNotInput==0)
            {
                VerifyOrder verify = new VerifyOrder()
                {
                    can_export = 0,
                    is_verify = 1,
                    year_and_month = yearMonthPre
                };
                db.VerifyOrder.InsertOnSubmit(verify);
                db.SubmitChanges();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (i < 3)
                    dataGridView1.Columns[i].ReadOnly = true;
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.Columns["宿舍ID"].Visible = false;
            dataGridView1.Columns["本月热水读数"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        //单元格输入验证，首先可以为空，然后必须是数字
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex > 2)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                float newValue;

                // Don't try to validate the 'new row' until finished 
                // editing since there
                // is not any point in validating its initial value.
                if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
                if (string.IsNullOrEmpty(e.FormattedValue.ToString().Trim())) { return; }
                if (!float.TryParse(e.FormattedValue.ToString(),
                    out newValue) || newValue < 0)
                {
                    e.Cancel = true;
                    dataGridView1.Rows[e.RowIndex].ErrorText = "格式不对，必须输入正数！";
                    return;
                }
            }

        }

        //鼠标经过行背景变色
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PowderBlue;
            }
        }

        //鼠标离开行背景变默认
        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<FuelFee> list = new List<FuelFee>();
            float elec, hot, cold;
            int dormId;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //本月用电读数与本月冷水读数不能一个为空一个不为空
                if (row.Cells["本月用电读数"].Value != DBNull.Value && row.Cells["本月冷水读数"].Value == DBNull.Value)
                {
                    MessageBox.Show("宿舍号为" + row.Cells["宿舍号"].Value + "的本月冷水读数不能为空");
                    dataGridView1.CurrentCell = row.Cells["本月冷水读数"];
                    return;
                }
                if (row.Cells["本月用电读数"].Value == DBNull.Value && row.Cells["本月冷水读数"].Value != DBNull.Value)
                {
                    MessageBox.Show("宿舍号为" + row.Cells["宿舍号"].Value + "的本月用电读数不能为空");
                    dataGridView1.CurrentCell = row.Cells["本月用电读数"];
                    return;
                }
                float.TryParse(row.Cells["本月用电读数"].Value.ToString(), out elec);
                float.TryParse(row.Cells["本月热水读数"].Value.ToString(), out hot);
                float.TryParse(row.Cells["本月冷水读数"].Value.ToString(), out cold);

                if (elec != 0 && cold != 0)
                {
                    dormId = Convert.ToInt32(row.Cells["宿舍ID"].Value);
                    var updateFees = db.FuelFee.Where(fu => fu.dorm_id == dormId).Where(fue => fue.year_month == yearMonthPre);
                    if (updateFees.Count() > 0)
                    {
                        FuelFee updateFee = updateFees.First();
                        updateFee.electricity = elec;
                        updateFee.cold_water = cold;
                        updateFee.hot_water = hot;

                    }
                    else
                    {
                        list.Add(new FuelFee()
                        {
                            dorm_id = dormId,
                            electricity = elec,
                            cold_water = cold,
                            hot_water = hot,
                            year_month = yearMonthPre,
                        });
                    }
                }
            }            
            db.FuelFee.InsertAllOnSubmit(list);
            db.SubmitChanges();
            MessageBox.Show("保存成功");            
            bindDatas();

        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.Width = 90;
            }
        }
       
    }
}
