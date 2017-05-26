using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmFuelInput : Form
    {
        string modelName = "水电录入";
        DormDBDataContext db = new DormDBDataContext();
        string newYearAndMonth;
        string oldYearAndMonth;
        FuelFee fuelForModify;
        Color usedColor = Color.FromArgb(224, 224, 224);

        public FrmFuelInput()
        {
            InitializeComponent();
        }

        private void FrmFuelInput_Load(object sender, EventArgs e)
        {
            oldYearAndMonth = (from veri in db.VerifyOrder
                               where veri.is_verify == 1
                               orderby veri.year_and_month descending
                               select veri.year_and_month).First();
            newYearAndMonth = YearAndMonth.next(oldYearAndMonth);
            lbNow.Text = string.Format("{0}年{1}月", newYearAndMonth.Substring(0, 4), newYearAndMonth.Substring(4, 2));
            lbLastYearMonth.Text = string.Format("{0}年{1}月", oldYearAndMonth.Substring(0, 4), oldYearAndMonth.Substring(4, 2));
            bindDatas();

            if ("一区".Equals(LoginUser.operated_area)) {
                button4.Visible = true;
            }
            else if ("二区".Equals(LoginUser.operated_area)) {
                button6.Visible = true;
            }
        }

        private void bindDatas()
        {
            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("区号", typeof(string));
                dt.Columns.Add("宿舍ID", typeof(Int32));
                dt.Columns.Add("宿舍号", typeof(string));
                dt.Columns.Add("上月用电读数", typeof(string));
                dt.Columns.Add("上月冷水读数", typeof(string));
                dt.Columns.Add("上月热水读数", typeof(string));
                dt.Columns.Add("本月用电读数", typeof(string));
                dt.Columns.Add("本月冷水读数", typeof(string));
                dt.Columns.Add("本月热水读数", typeof(string));
                dt.Columns.Add("本月用电量", typeof(string));
                dt.Columns.Add("本月冷水量", typeof(string));
                dt.Columns.Add("本月热水量", typeof(string));

                //将数据填充到datagridview
                foreach (var fuel in db.take_input_fuels(LoginUser.operated_area, oldYearAndMonth, newYearAndMonth))
                {
                    //计算出本月使用量
                    double? coldUsed, hotUsed, eleUsed;
                    coldUsed = hotUsed = eleUsed = null;
                    if (fuel.cur_cold != null)
                    {
                        coldUsed = fuel.cur_cold - (fuel.pre_cold == null ? 0 : fuel.pre_cold);
                        if (coldUsed < 0) {
                            coldUsed += 10000;
                        }
                    }
                    if (fuel.cur_elec != null)
                    {
                        eleUsed = fuel.cur_elec - (fuel.pre_elec == null ? 0 : fuel.pre_elec);
                        if (eleUsed < 0) {
                            eleUsed += 10000;
                        }
                    }
                    if (fuel.cur_hot != null)
                    {
                        hotUsed = fuel.cur_hot - (fuel.pre_hot == null ? 0 : fuel.pre_hot);
                        if (hotUsed < 0) {
                            hotUsed += 10000;
                        }
                    }

                    dt.Rows.Add(fuel.areaName,
                        fuel.dormId,
                        fuel.dormNumber,
                        fuel.pre_elec == null ? 0 : fuel.pre_elec,
                        fuel.pre_cold == null ? 0 : fuel.pre_cold,
                        fuel.pre_hot == null ? 0 : fuel.pre_hot,
                        fuel.cur_elec,
                        fuel.cur_cold,
                        fuel.cur_hot,
                        eleUsed,
                        coldUsed,
                        hotUsed);
                }
                dataGridView1.DataSource = dt;
            }

            //统计信息grid
            using (DataTable dt2 = new DataTable())
            {
                dt2.Columns.Add("区号", typeof(string));
                dt2.Columns.Add("宿舍总数", typeof(Int32));
                dt2.Columns.Add("已录入", typeof(string));
                dt2.Columns.Add("待录入", typeof(string));
                dt2.Columns.Add("电费合计", typeof(double));
                dt2.Columns.Add("冷水合计", typeof(double));
                dt2.Columns.Add("热水合计", typeof(double));

                int totalDormCount = 0;
                int totalHasInput = 0;
                int totalNotInput = 0;
                double? totalElec = 0;
                double? totalCold = 0;
                double? totalHot = 0;
                foreach (Area area in db.Area)
                {
                    int dormCount = area.Dorm.Where(d=>d.available==0).Count();
                    var fuels = from fuel in db.FuelFee
                                where fuel.Dorm.Area == area
                                && fuel.year_month == newYearAndMonth                                
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
                dt2.Rows.Add("合计", totalDormCount, totalHasInput, totalNotInput, totalElec, totalCold, totalHot);
                dataGridView2.DataSource = dt2;
            }
        }

        //单元格输入验证，首先可以为空，然后必须是数字
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex > 5 && dataGridView1.CurrentCell.ColumnIndex < 9)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                float newValue, preValue;

                // Don't try to validate the 'new row' until finished 
                // editing since there 
                // is not any point in validating its initial value.
                if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
                if (string.IsNullOrEmpty(e.FormattedValue.ToString().Trim())) { return; }
                if (!float.TryParse(e.FormattedValue.ToString(),
                    out newValue) || newValue < 0)
                {
                    e.Cancel = true;
                    dataGridView1.Rows[e.RowIndex].ErrorText = "格式不对，必须输入数字！";
                    return;
                }

                preValue = float.Parse(dataGridView1[e.ColumnIndex - 3, e.RowIndex].Value.ToString());
                if (newValue < preValue)
                {
                    newValue += 10000;
                    dataGridView1[e.ColumnIndex + 3, e.RowIndex].Style.ForeColor = Color.Red;
                    //考虑电表到达10000归0的情况
                    //e.Cancel = true;
                    //dataGridView1.Rows[e.RowIndex].ErrorText = "本月度数不得小于上月度数！";
                }
                else {
                    dataGridView1[e.ColumnIndex + 3, e.RowIndex].Style.ForeColor = Color.Black;
                }
                //本月使用量
                dataGridView1[e.ColumnIndex + 3, e.RowIndex].Value = newValue - preValue;
            }

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            dataGridView1.Columns["宿舍ID"].Visible = false;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].Width = 60;
                if (i < 6 || i > 8)
                    dataGridView1.Columns[i].ReadOnly = true;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows) {
                if (row.Cells["本月用电读数"].Value != DBNull.Value)
                {
                    if (Convert.ToDecimal(row.Cells["本月用电读数"].Value) < Convert.ToDecimal(row.Cells["上月用电读数"].Value))
                    {
                        row.Cells["本月用电量"].Style.ForeColor = Color.Red;
                    }
                }
                if (row.Cells["本月冷水读数"].Value != DBNull.Value)
                {
                    if (Convert.ToDecimal(row.Cells["本月冷水读数"].Value) < Convert.ToDecimal(row.Cells["上月冷水读数"].Value))
                    {
                        row.Cells["本月冷水量"].Style.ForeColor = Color.Red;
                    }
                }
                if (row.Cells["本月热水读数"].Value != DBNull.Value)
                {
                    if (Convert.ToDecimal(row.Cells["本月热水读数"].Value) < Convert.ToDecimal(row.Cells["上月热水读数"].Value))
                    {
                        row.Cells["本月热水量"].Style.ForeColor = Color.Red;
                    }
                }
            }

            //数据绑定完成后设置列的只读属性以及不可排序                                
            dataGridView1.Columns["本月热水量"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns["本月用电量"].DefaultCellStyle.BackColor = usedColor;
            dataGridView1.Columns["本月冷水量"].DefaultCellStyle.BackColor = usedColor;
            dataGridView1.Columns["本月热水量"].DefaultCellStyle.BackColor = usedColor;
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
                dataGridView1["本月用电量", e.RowIndex].Style.BackColor = usedColor;
                dataGridView1["本月冷水量", e.RowIndex].Style.BackColor = usedColor;
                dataGridView1["本月热水量", e.RowIndex].Style.BackColor = usedColor;
            }
        }

        //编辑状态行字体变色
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        //编辑结束行字体还原
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        //保存输入数据        
        private void button1_Click(object sender, EventArgs e)
        {
            List<FuelFee> list = new List<FuelFee>();
            string elecInput, hotInput, coldInput;
            float? elec, hot, cold;
            int dormId;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["本月用电读数"].Value == DBNull.Value && row.Cells["本月冷水读数"].Value == DBNull.Value && row.Cells["本月热水读数"].Value == DBNull.Value)
                {
                    continue;
                }

                elecInput = Convert.ToString(row.Cells["本月用电读数"].Value).Trim();
                hotInput = Convert.ToString(row.Cells["本月热水读数"].Value).Trim();
                coldInput = Convert.ToString(row.Cells["本月冷水读数"].Value).Trim();

                if (!string.IsNullOrEmpty(elecInput))
                {
                    elec = float.Parse(elecInput);
                }
                else
                {
                    elec = null;
                }
                if (!string.IsNullOrEmpty(coldInput))
                {
                    cold = float.Parse(coldInput);
                }
                else
                {
                    cold = null;
                }
                if (!string.IsNullOrEmpty(hotInput))
                {
                    hot = float.Parse(hotInput);
                }
                else
                {
                    hot = float.Parse(row.Cells["上月热水读数"].Value.ToString());
                }

                dormId = Convert.ToInt32(row.Cells["宿舍ID"].Value);
                var updateFees = db.FuelFee.Where(fu => fu.dorm_id == dormId).Where(fue => fue.year_month == newYearAndMonth);
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
                        year_month = newYearAndMonth,
                    });
                }

            }
            db.FuelFee.InsertAllOnSubmit(list);
            db.SubmitChanges();
            MessageBox.Show("保存成功");
            bindDatas();
            MyUtil.WriteEventLog(modelName, "", "", "保存成功");
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.Width = 90;
            }
        }

        #region Modify The last Month's reading number

        private void tbDormNumber_Leave(object sender, EventArgs e)
        {
            string dormNum = tbDormNumber.Text.Trim();
            if (string.IsNullOrEmpty(dormNum))
                return;
            dormNum = dormNum.ToUpper();
            var dorms = db.Dorm.Where(dor => dor.number == dormNum);
            if (dorms.Count() < 1)
            {
                MessageBox.Show("该宿舍编号不存在");
                fuelForModify = null;
                return;
            }
            var fuels = from fu in db.FuelFee
                        where fu.Dorm == dorms.First()
                        && fu.year_month == oldYearAndMonth
                        select fu;
            if (fuels.Count() == 0)
            {
                fuelForModify = new FuelFee()
                {
                    cold_water = 0,
                    hot_water = 0,
                    electricity = 0,
                    year_month = oldYearAndMonth,
                    Dorm = dorms.First(),
                };
            }
            else
            {
                fuelForModify = fuels.First();
            }


            if (radEle.Checked)
            {
                lbLastRead.Text = fuelForModify.electricity.ToString();
            }
            if (radCold.Checked)
            {
                lbLastRead.Text = fuelForModify.cold_water.ToString();
            }
            if (radHot.Checked)
            {
                lbLastRead.Text = fuelForModify.hot_water.ToString();
            }

        }

        private void radEle_CheckedChanged(object sender, EventArgs e)
        {
            if (radEle.Checked && fuelForModify!=null)
            {
                lbLastRead.Text = fuelForModify.electricity.ToString();
            }
        }

        private void radCold_CheckedChanged(object sender, EventArgs e)
        {
            if (radCold.Checked && fuelForModify != null)
            {
                lbLastRead.Text = fuelForModify.cold_water.ToString();
            }
        }

        private void radHot_CheckedChanged(object sender, EventArgs e)
        {
            if (radHot.Checked && fuelForModify != null)
            {
                lbLastRead.Text = fuelForModify.hot_water.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要保存吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                double newReading;
                if (double.TryParse(tbNewRading.Text.Trim(), out newReading))
                {
                    //这里需要将之前的月份一并修改
                    foreach (var ff in db.FuelFee.Where(f => f.dorm_id == fuelForModify.dorm_id && f.year_month.CompareTo(fuelForModify.year_month) <= 0))
                    {
                        double newValue = 0;
                        if (string.IsNullOrEmpty(ff.comment)) {
                            ff.comment = "";
                        }
                        if (radEle.Checked)
                        {
                            newValue = (double)ff.electricity + newReading - (double)fuelForModify.electricity;
                            ff.comment += string.Format(" [电：{0} => {1}] ", ff.electricity, newValue);
                            ff.electricity = newValue;
                        }
                        else if (radCold.Checked)
                        {
                            newValue = (double)ff.cold_water + newReading - (double)fuelForModify.cold_water;
                            ff.comment += string.Format(" [冷：{0} => {1}] ", ff.cold_water, newValue);
                            ff.cold_water = newValue;
                        }
                        else if (radHot.Checked)
                        {
                            newValue = (double)ff.hot_water + newReading - (double)fuelForModify.hot_water;
                            ff.comment += string.Format(" [热：{0} => {1}] ", ff.hot_water, newValue);
                            ff.hot_water = newValue;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("新读数格式不正确，请重新输入。");
                    return;
                }
                db.SubmitChanges();
                MessageBox.Show("保存成功！");
                MyUtil.WriteEventLog(modelName, tbDormNumber.Text, "", string.Format("[{0}]{1}:{2}--->{3}", lbLastYearMonth.Text, radEle.Checked ? "热水" : (radCold.Checked ? "冷水" : "热水"), lbLastRead.Text, newReading));
                lbLastRead.Text = newReading.ToString();
            }
        }

        #endregion Modify The last Month's reading number

        private void button3_Click(object sender, EventArgs e)
        {
            string search = tbSearchedNumber.Text.Trim();
            if (string.IsNullOrEmpty(search))
                return;
            search = search.ToUpper();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["宿舍号"].Value.ToString().Equals(search))
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells["本月用电读数"];
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要将A栋上月冷水读数覆盖本月份的吗？", "等待确认", MessageBoxButtons.YesNo) == DialogResult.No) {
                return;
            }
            db.copyColdWater(oldYearAndMonth, newYearAndMonth);
            MessageBox.Show("操作成功");
            bindDatas();
            MyUtil.WriteEventLog(modelName, "", "", "将A栋上月冷水读数覆盖本月份冷水度数");
        }

        private void FrmFuelInput_Resize(object sender, EventArgs e)
        {
            tabControl1.Left = this.Width / 2 - groupBox1.Width / 2 - 40;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dorms="";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["本月用电量"].Value != DBNull.Value)
                {
                    if (double.Parse(row.Cells["本月用电量"].Value.ToString()) > 2000) {
                        dorms += row.Cells["宿舍号"].Value.ToString()+"  ";
                        continue;
                    }
                }
                if (row.Cells["本月冷水量"].Value != DBNull.Value) {
                    if (double.Parse(row.Cells["本月冷水量"].Value.ToString()) > 2000)
                    {
                        dorms += row.Cells["宿舍号"].Value.ToString() + "  ";
                        continue;
                    }
                }
                if (row.Cells["本月热水量"].Value != DBNull.Value)
                {
                    if (double.Parse(row.Cells["本月热水量"].Value.ToString()) > 2000)
                    {
                        dorms += row.Cells["宿舍号"].Value.ToString() + "  ";
                        continue;
                    }
                }
            }
            if (!string.IsNullOrEmpty(dorms))
            {
                MessageBox.Show("本月使用水电量超过2000度的宿舍有： " + dorms);
                if (dorms.Length > 400) {
                    dorms = dorms.Substring(0, 400) + "...";
                }
                MyUtil.WriteEventLog(modelName, "", "", "本月使用水电量超过2000度的宿舍有： " + dorms);
            }
            else {
                MessageBox.Show("本月使用水电量没有超过2000度的宿舍");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要将37/38栋上月冷水读数覆盖本月份的吗？", "等待确认", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            db.copy2areaWater(oldYearAndMonth, newYearAndMonth);
            MessageBox.Show("操作成功");
            bindDatas();
            MyUtil.WriteEventLog(modelName, "", "", "将A栋上月冷水读数覆盖本月份冷水度数");
        }
        
    }
}
