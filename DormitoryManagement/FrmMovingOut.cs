using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace DormitoryManagement
{
    public partial class FrmMovingOut : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        int? empID, dormID, lodgingID;
        Employee emp;
        String newYearAndMonth;
        DateTime FirstDayInMonth, LastDayInMonth;
        string modelName = "退宿管理";

        public FrmMovingOut()
        {
            InitializeComponent();

        }

        private void FrmMovingOut_Load(object sender, EventArgs e)
        {
            //lbDateNow.Text = DateTime.Now.ToShortDateString();
            //lbDateNow.ForeColor = Color.Red;
            pictureBox1.Image = Properties.Resources.DefaultImage;

            foreach (Control ct in groupBox1.Controls)
            {
                if (ct.Name.StartsWith("lb"))
                {
                    ct.ForeColor = Color.Blue;
                }
            }
            
            var oldYearAndMonth = (from veri in db.VerifyOrder
                               where veri.is_verify == 1
                               orderby veri.year_and_month descending
                               select veri.year_and_month).First();
            newYearAndMonth = YearAndMonth.next(oldYearAndMonth);
            dateTimePicker1.Value = YearAndMonth.firstDayInMonth(newYearAndMonth);
            cbSearch.Text = "宿舍编号";
            FirstDayInMonth = YearAndMonth.firstDayInMonth(newYearAndMonth);
            LastDayInMonth = YearAndMonth.lastDayInMonth(newYearAndMonth);

            dateTimePicker1.MinDate = FirstDayInMonth;
            dateTimePicker1.MaxDate = LastDayInMonth;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Text = "费用核算";
            if (cbSearch.Text.Equals("厂牌编号"))
            {
                var emps = db.Employee.Where(em => em.card_number == tbSearchContent.Text.Trim());
                if (emps.Count() < 1)
                {
                    MessageBox.Show("不存在该员工信息");
                    return;
                }
                emp = emps.First();
                displayInfo();
            }
            else if (cbSearch.Text.Equals("姓名")) {
                var emps = db.Employee.Where(em => em.name == tbSearchContent.Text.Trim()).Where(em=>em.Lodging.Where(l=>l.out_date==null).Count()>0);
                if (emps.Count() < 1)
                {
                    MessageBox.Show("不存在该员工信息");
                    return;
                }
                else if (emps.Count() == 1)
                {
                    emp = emps.First();
                    displayInfo();
                }
                else {
                    DlgMovingOut dlg = new DlgMovingOut();
                    dlg.Owner = this;
                    dlg.emps = emps;
                    dlg.ShowDialog();
                }
            }
            else
            {
                var dorms = db.Dorm.Where(dor => dor.number == tbSearchContent.Text.Trim());
                if (dorms.Count() < 1)
                {
                    MessageBox.Show("不存在该宿舍");
                    return;
                }
                Dorm dorm = dorms.First();
                var livings = dorm.Lodging.Where(lod => lod.out_date == null);
                if (livings.Count() < 1)
                {
                    MessageBox.Show("该宿舍没有在住员工");
                    return;
                }
                else if (livings.Count() == 1)
                {
                    emp = livings.First().Employee;
                    displayInfo();
                }
                else
                {
                    DlgMovingOut dlg = new DlgMovingOut();
                    dlg.Owner = this;
                    dlg.dorm = dorm;
                    dlg.ShowDialog();
                }
            }

        }

        private void displayInfo()
        {

            if (emp.Lodging.Where(lo => lo.out_date == null).Count() == 0)
            {
                MessageBox.Show("该员工已退宿或还没安排住宿");
                return;
            }
            Lodging lodg = emp.Lodging.Where(log => log.out_date == null).First();
            if (!lodg.Dorm.Area.name.Equals(LoginUser.operated_area))
            {
                MessageBox.Show("该员工所属宿舍区属于" + lodg.Dorm.Area.name + ",你没有权限对其进行退宿处理");
                MyUtil.WriteEventLog(modelName, lodg.Dorm.number, emp.name, "你没有权限对其进行退宿处理", false);
                return;
            }
            string preMonth = YearAndMonth.previous(newYearAndMonth);
            var fuels = from fu in lodg.Dorm.FuelFee
                        where fu.year_month == preMonth
                        select fu;

            FuelFee fuel = null;
            if (fuels.Count() > 0)
            {
                fuel = fuels.First();
            }

            //显示员工基本信息
            empID = emp.id;
            lbName.Text = emp.name;
            lbAccount.Text = emp.account_number;
            lbIdentify.Text = emp.identify_number;
            lbSex.Text = emp.sex;
            lbDepartment.Text = emp.Department1.name;
            lbCardNum.Text = emp.card_number;
            lbPhone.Text = emp.phone;
            if (emp.picture != null && emp.picture.Length > 1)
            {
                byte[] imageByte = emp.picture.ToArray();
                using (MemoryStream ms = new MemoryStream(imageByte))
                {
                    Bitmap bm = new Bitmap(ms);
                    pictureBox1.Image = bm;
                }
            }
            else {
                pictureBox1.Image = Properties.Resources.DefaultImage;
            }
            //显示住宿宿舍信息
            dormID = lodg.dorm_id;
            lodgingID = lodg.id;
            lbDormNumber.Text = lodg.Dorm.number;
            lbAreaName.Text = lodg.Dorm.Area.name;
            lbDormType.Text = lodg.Dorm.DormType.name;
            lbPeopleCount.Text = (lodg.Dorm.Lodging.Where(lodge => lodge.out_date == null).Count()).ToString();
            tbsituation.Text = emp.comment;
            lbRent.Text = lodg.real_rent.ToString();
            lbManage.Text = lodg.real_manage.ToString();
            lbGuarantee.Text = lodg.guarantee.ToString();
            lbInDate.Text = ((DateTime)lodg.in_date).ToShortDateString();

            //显示水电信息
            lbPreElec.Text = fuel != null ? fuel.electricity.ToString() : "0";
            lbPreColdWater.Text = fuel != null ? fuel.cold_water.ToString() : "0";
            lbPreHotWater.Text = fuel != null ? fuel.hot_water.ToString() : "0";

            setFees();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbName.Text))
            {
                MessageBox.Show("请先查询员工信息");
                tbSearchContent.Focus();
                return;
            }

            double elec, coldWater, hotWater;
            decimal repair, fine, guest, otherFee;

            //验证用户输入完整性
            if (!double.TryParse(tbNowElec.Text.Trim(), out elec))
            {
                MessageBox.Show("今日用电读数输入不合法");
                tbNowElec.Focus();
                return;
            }
            else if (elec < Convert.ToDouble(lbPreElec.Text))
            {
                elec += 10000;
            }

            if (!double.TryParse(tbNowColdWater.Text.Trim(), out coldWater))
            {
                MessageBox.Show("今日冷水读数输入不合法");
                tbNowColdWater.Focus();
                return;
            }
            else if (coldWater < Convert.ToDouble(lbPreColdWater.Text))
            {
                coldWater += 10000;
            }

            if (string.IsNullOrEmpty(tbNowHotWater.Text.Trim()))
            {
                hotWater = 0;
            }
            else
            {
                if (!double.TryParse(tbNowHotWater.Text.Trim(), out hotWater))
                {
                    MessageBox.Show("今日热水读数输入不合法");
                    tbNowHotWater.Focus();
                    return;
                }
                else if (hotWater < Convert.ToDouble(lbPreHotWater.Text))
                {
                    hotWater += 10000;
                }
            }

            if (string.IsNullOrEmpty(tbRepair.Text.Trim()))
            {
                repair = 0;
            }
            else
            {
                if (!decimal.TryParse(tbRepair.Text.Trim(), out repair))
                {
                    MessageBox.Show("维修费输入不合法");
                    tbRepair.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbFine.Text.Trim()))
            {
                fine = 0;
            }
            else
            {
                if (!decimal.TryParse(tbFine.Text.Trim(), out fine))
                {
                    MessageBox.Show("罚款输入不合法");
                    tbFine.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbGuestCost.Text.Trim()))
            {
                guest = 0;
            }
            else
            {
                if (!decimal.TryParse(tbGuestCost.Text.Trim(), out guest))
                {
                    MessageBox.Show("招待所费用输入不合法");
                    tbGuestCost.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(tbOtherFee.Text.Trim()))
            {
                otherFee = 0;
            }
            else
            {
                if (!decimal.TryParse(tbOtherFee.Text.Trim(), out otherFee))
                {
                    MessageBox.Show("其他费用输入不合法");
                    tbOtherFee.Focus();
                    return;
                }
            }

            //开始核心逻辑实现。。。
            //如果按钮名称为"费用核算",则开始计算费用

            decimal rent, guarantee;
            string yearMon = YearAndMonth.toLong(newYearAndMonth);

            Lodging lodg = db.Lodging.Single(lo => lo.id == lodgingID);
            int livingDays;
            //本月入住
            if (lodg.in_date >= FirstDayInMonth)
            {
                livingDays = dateTimePicker1.Value.Day - ((DateTime)lodg.in_date).Day;
                guarantee = 0;
            }
            else
            {
                livingDays = dateTimePicker1.Value.Day;
                guarantee = Convert.ToDecimal(lbGuarantee.Text);
            }

            //计算该宿舍所有员工的住宿时间之和，用于按比例分摊水电费
            int totalDaysOfDorm = livingDays;
            decimal outWater = 0;
            decimal outEle = 0;

            //获取该宿舍其它员工的住宿信息
            var lodgingsInDorm = from lod in db.Lodging
                                 where lod.Dorm == lodg.Dorm
                                 && lod.id != lodg.id
                                 && lod.in_date <= dateTimePicker1.Value
                                 && (lod.out_date >= FirstDayInMonth
                                 || lod.out_date == null)
                                 select lod;

            foreach (Lodging lo in lodgingsInDorm)
            {
                //当月住宿并且已经退宿
                if (lo.in_date >= FirstDayInMonth && lo.out_date <= dateTimePicker1.Value)
                {
                    //totalDaysOfDorm += ((DateTime)lo.out_date).Day - ((DateTime)lo.in_date).Day;
                    var outFuelFee = (from ot in db.OtherFee
                                      where ot.year_month == yearMon
                                      && ot.dorm_id == lo.dorm_id
                                      && ot.emp_id == lo.emp_id
                                      select new
                                      {
                                          water = ot.out_share_water,
                                          ele = ot.out_share_eletricity
                                      }).First();
                    outWater += outFuelFee.water == null ? 0 : (decimal)outFuelFee.water;
                    outEle += outFuelFee.ele == null ? 0 : (decimal)outFuelFee.ele;
                }
                //当月住宿
                else if (lo.in_date >= FirstDayInMonth)
                {
                    totalDaysOfDorm += dateTimePicker1.Value.Day - ((DateTime)lo.in_date).Day;
                }
                //已经退宿
                else if (lo.out_date <= dateTimePicker1.Value)
                {
                    //totalDaysOfDorm += ((DateTime)lo.out_date).Day;
                    var outFuelFee = (from ot in db.OtherFee
                                      where ot.year_month == yearMon
                                      && ot.dorm_id == lo.dorm_id
                                      && ot.emp_id == lo.emp_id
                                      select new
                                      {
                                          water = ot.out_share_water,
                                          ele = ot.out_share_eletricity
                                      }).First();
                    outWater += outFuelFee.water == null ? 0 : (decimal)outFuelFee.water;
                    outEle += outFuelFee.ele == null ? 0 : (decimal)outFuelFee.ele;
                }
                else
                {
                    totalDaysOfDorm += dateTimePicker1.Value.Day;
                }
            }

            //按实际的入住天数计算租金管理费以及分摊水电费用
            decimal percent = totalDaysOfDorm == 0 ? 0 : (decimal)livingDays / totalDaysOfDorm;
            rent = Math.Round(Convert.ToDecimal(lbRent.Text) * livingDays / DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month), 1);
            decimal manage = Math.Round(Convert.ToDecimal(lbManage.Text) * livingDays / DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month), 1);
            decimal elecCost = Math.Round(((decimal)(elec - Convert.ToDouble(lbPreElec.Text)) * (decimal)BaseInfo.electricity - outEle) * percent, 1);
            decimal coldCost = (decimal)(coldWater - Convert.ToDouble(lbPreColdWater.Text)) * (decimal)BaseInfo.coldWater;
            decimal hotCost = hotWater != 0 ? (decimal)(hotWater - Convert.ToDouble(lbPreHotWater.Text)) * (decimal)BaseInfo.hotWater : 0;
            decimal waterCost = Math.Round((coldCost + hotCost - outWater) * percent, 1);

            //新员工4人间以上前三个月免住宿费
            if (lodg.Dorm.DormType.max_number >= 4)
            {
                string cardNumber = lodg.Employee.card_number;
                if (!string.IsNullOrEmpty(cardNumber) && !cardNumber.Equals("0"))
                {
                    int leftDay = YearAndMonth.leftDays(Convert.ToDateTime(string.Format("20{0}-{1}-{2}", cardNumber.Substring(0, 2), cardNumber.Substring(2, 2), cardNumber.Substring(4, 2))), dateTimePicker1.Value);
                    rent = Math.Round(Convert.ToDecimal(lbRent.Text) * leftDay / DateTime.DaysInMonth(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month), 2);
                }                
            }

            decimal total = Math.Round(rent + manage + elecCost + waterCost + repair + fine + guest + otherFee - guarantee, 1);
            if (button2.Text.Equals("费用核算"))
            {
                //写入日志
                string eventLog = string.Format("费用核算：电：{7}->{0}，冷水：{8}->{1}，热水：{9}->{2}，罚款：{3}，维修费：{4}，招待所：{5}，其它费用：{6}", tbNowElec.Text, tbNowColdWater.Text, tbNowHotWater.Text, tbFine.Text, tbRepair.Text, tbGuestCost.Text, tbOtherFee.Text, lbPreElec.Text, lbPreColdWater.Text, lbPreHotWater.Text);
                MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, eventLog); 
                rtbResult.Text = string.Format("租金({0}) + 管理费({1}) + 分摊电费({2}) + 分摊水费({3}) + 维修费({4}) + 扣分({5}) + 招待所费用({6}) + 其他费用({7}) - 押金({8}) = {9}(元)", rent, manage, elecCost, waterCost, repair, fine, guest, otherFee, guarantee, total);
                button2.Text = "确认退宿";
            }
            else
            {
                //如果总额大于两千，则弹出警告
                if (total > 2000) {
                    if (MessageBox.Show("该员工本月应支付的总费用是:" + total.ToString() + " ,确定没问题并继续退宿吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) {
                        MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "总费用过多,操作中止：" + total.ToString(),false);
                        MessageBox.Show("退宿失败");
                        button2.Text = "费用核算";
                        return;
                    }
                }
                //新需求之后，必须先将其他费用表中的有关记录删除，然后再保存一次
                var delOthers = from oth in db.OtherFee
                                where oth.emp_id == empID
                                && oth.dorm_id == dormID
                                && oth.date >= FirstDayInMonth
                                && oth.date <= dateTimePicker1.Value.AddDays(1)
                                select oth;
                if (delOthers.Count() > 0)
                {
                    db.OtherFee.DeleteAllOnSubmit(delOthers);
                    db.SubmitChanges();
                }

                //需要处理不按照时间线操作的情况，即先录入费用，后退宿，但是费用时间在退宿时间之后，这时要把费用重新分配（变态需求）
                DateTime tomorrow = DateTime.Parse(dateTimePicker1.Value.AddDays(1).ToShortDateString());
                var faultyFees = from oth in db.OtherFee
                                 where oth.emp_id == empID
                                 && oth.dorm_id == dormID
                                 && oth.date > tomorrow
                                 && oth.date <= LastDayInMonth
                                 && oth.repair_cost != null
                                 select oth;

                if (faultyFees.Count() > 0)
                {
                    foreach (OtherFee faul in faultyFees)
                    {
                        //费用分摊给在住的舍友
                        var otherMembersInDorm = db.Lodging.Where(l => l.dorm_id == faul.dorm_id && (l.out_date == null || l.out_date > faul.date) && l.emp_id!=faul.emp_id);
                        foreach (var mem in otherMembersInDorm) {
                            OtherFee memFee = new OtherFee();
                            memFee.repair_cost =  Math.Round((decimal)(faul.repair_cost / otherMembersInDorm.Count()),1);
                            memFee.dorm_id = mem.dorm_id;
                            memFee.emp_id = mem.emp_id;
                            memFee.date = dateTimePicker1.Value;
                            memFee.year_month = faul.year_month;

                            db.OtherFee.InsertOnSubmit(memFee);
                        }

                        MyUtil.WriteEventLog("退宿", lbDormNumber.Text, lbName.Text, "将维修费用分摊给舍友：" + faul.repair_cost);
                        //将自己的维修费改为0
                        faul.repair_cost = 0;
                    } 
                    
                    db.SubmitChanges();
                }

                //保存信息到其他费用表
                OtherFee other = new OtherFee();
                other.dorm_id = dormID;
                other.emp_id = empID;
                other.repair_cost = repair != 0 ? repair : (decimal?)null;
                other.fine = fine != 0 ? fine : (decimal?)null;
                other.other_cost = otherFee != 0 ? otherFee : (decimal?)null;
                other.out_share_eletricity = elecCost;
                other.out_share_water = waterCost;
                other.year_month = newYearAndMonth;
                other.comment = tbFeeComment.Text;
                other.date = dateTimePicker1.Value;
                db.OtherFee.InsertOnSubmit(other);

                //写入住宿情况到员工表
                var empl = db.Employee.Single(em => em.id == empID);
                empl.comment = tbsituation.Text;

                //将住宿信息反馈到HR系统，2013-3-22更新
                if ("厂内".Equals(empl.Department1.property) || "光电".Equals(empl.Department1.property))
                {
                    if (!string.IsNullOrWhiteSpace(empl.account_number))
                    {
                        try
                        {
                            db.updateHRLivingState(empl.account_number, false);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("连接失败，员工退宿信息无法更新到人事系统。");
                            MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "连接失败，员工退宿信息无法更新到人事系统:"+ex.Message, false);
                        }
                    }
                }

                //在住宿表中对应员工记录写入退宿日期
                var lodge = db.Lodging.Single(lod => lod.id == lodgingID);
                lodge.out_date = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
                db.SubmitChanges();
                MessageBox.Show("退宿信息登记成功");
                MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "退宿信息登记成功");

                //退宿的时候先检测当天录入的水电费，如果当天的水电读数比这个月末还要大，那么用这个读数将月末的覆盖掉。
                #region 

                string dormNumber = db.Dorm.Single(d => d.id == dormID).number;

                
                var ffs = db.FuelFee.Where(f => (f.dorm_id == dormID && f.year_month == yearMon));
                
                if (ffs.Count() > 0)
                {
                    FuelFee ff = ffs.First();
                    if (ff.cold_water == null || ff.cold_water < coldWater)
                    {
                        rtbMes.AppendText(string.Format("[{0}]:冷水[{1}=>{2}]\n", dormNumber, ff.cold_water, coldWater));
                        ff.cold_water = coldWater;
                        MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "冷水比月末度数大，将替代：" + ff.cold_water + "->" + coldWater.ToString());
                    }
                    else {
                        rtbMes.AppendText(string.Format("[{0}]:冷水[{1}=>不变]\n", dormNumber, ff.cold_water));
                    }
                    if (ff.hot_water == null || ff.hot_water < hotWater)
                    {
                        rtbMes.AppendText(string.Format("[{0}]:热水[{1}=>{2}]\n", dormNumber, ff.hot_water, hotWater));
                        MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "热水比月末度数大，将替代：" + ff.hot_water.ToString() + "->" + hotWater.ToString());
                        ff.hot_water = hotWater;
                    }
                    else {
                        rtbMes.AppendText(string.Format("[{0}]:热水[{1}=>不变]\n", dormNumber, ff.hot_water));
                    }
                    if (ff.electricity == null || ff.electricity < elec)
                    {
                        rtbMes.AppendText(string.Format("[{0}]:电表[{1}=>{2}]\n", dormNumber, ff.electricity, elec));
                        MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "电表比月末度数大，将替代：" + ff.electricity.ToString() + "->" + elec.ToString());
                        ff.electricity = elec;
                    }
                    else {
                        rtbMes.AppendText(string.Format("[{0}]:电表[{1}=>不变]\n", dormNumber, ff.electricity));
                    }
                }
                else
                {
                    FuelFee ff = new FuelFee()
                    {
                        cold_water = coldWater,
                        hot_water = hotWater,
                        electricity = elec,
                        dorm_id = dormID,
                        year_month = yearMon
                    };
                    db.FuelFee.InsertOnSubmit(ff);
                    rtbMes.AppendText(string.Format("[{0}]:冷水[未录入=>{1}]\n", dormNumber, coldWater));
                    rtbMes.AppendText(string.Format("[{0}]:热水[未录入=>{1}]\n", dormNumber, hotWater));
                    rtbMes.AppendText(string.Format("[{0}]:电表[未录入=>{1}]\n", dormNumber, elec));
                }
                db.SubmitChanges();

                #endregion

                clearContent();
                button2.Text = "费用核算";
            }
        }

        private void clearContent()
        {
            foreach (Control ct in groupBox1.Controls)
            {
                if (ct.Name.StartsWith("lb"))
                {
                    ((Label)ct).Text = "";
                }
                if (ct.Name.StartsWith("tb"))
                {
                    ((TextBox)ct).Clear();
                }
            }
            rtbResult.Clear();
            pictureBox1.Image = Properties.Resources.DefaultImage;
        }

        private void setFees()
        {
            //新需求,将其他费用带进来
            decimal repair = 0, guestFee = 0, otherFee = 0, fine = 0;
            string feeComment = "";
            //string yearMonth = YearAndMonth.toLong(dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.Month.ToString());
            DateTime tomorrow = Convert.ToDateTime(dateTimePicker1.Value.AddDays(1).ToShortDateString());
            foreach (OtherFee other in emp.OtherFee.Where(ot => (ot.year_month==YearAndMonth.getYearAndMonth(dateTimePicker1.Value) && ot.date >= FirstDayInMonth && ot.date <= tomorrow && ot.dorm_id == dormID)))
            {
                repair += (other.repair_cost == null ? 0 : (decimal)other.repair_cost);
                guestFee += (other.guesthouse_cost == null ? 0 : (decimal)other.guesthouse_cost);
                otherFee += (other.other_cost == null ? 0 : (decimal)other.other_cost);
                fine += (other.fine == null ? 0 : (decimal)other.fine);
                feeComment += other.comment + "  ";
            }
            tbRepair.Text = repair.ToString();
            tbGuestCost.Text = guestFee.ToString();
            tbFine.Text = fine.ToString();
            tbOtherFee.Text = otherFee.ToString();
            tbFeeComment.Text = feeComment;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {            
            button2.Text = "费用核算";
            FirstDayInMonth = YearAndMonth.firstDayInMonth(dateTimePicker1.Value.Year + "" + dateTimePicker1.Value.Month);
            LastDayInMonth = YearAndMonth.lastDayInMonth(dateTimePicker1.Value.Year + "" + dateTimePicker1.Value.Month);
            if (dormID != null)
            {
            string preMonth = YearAndMonth.previous(YearAndMonth.toLong(dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.Month.ToString()));
            
                var dorm = db.Dorm.Single(d => d.id == dormID);
                var fuels = from fu in dorm.FuelFee
                            where fu.year_month == preMonth
                            select fu;

                FuelFee fuel = null;
                if (fuels.Count() > 0)
                {
                    fuel = fuels.First();
                }
                //显示水电信息
                lbPreElec.Text = fuel != null ? fuel.electricity.ToString() : "0";
                lbPreColdWater.Text = fuel != null ? fuel.cold_water.ToString() : "0";
                lbPreHotWater.Text = fuel != null ? fuel.hot_water.ToString() : "0";
            }
            if(emp!=null)
                setFees();
        }

        private void lbEmpNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbEmpNo.Text)) { return; }
            emp = db.Employee.Single(em => em.id == Int32.Parse(lbEmpNo.Text));
            displayInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbName.Text))
            {
                MessageBox.Show("请先查询员工信息");
                tbSearchContent.Focus();
                return;
            }

            if (MessageBox.Show("确定要不结算费用将该员工退宿吗？", "退宿确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            Lodging lodg = db.Lodging.Single(lo => lo.id == lodgingID);
            Employee empl = lodg.Employee;
            AutoQuit quit = new AutoQuit();
            quit.emp_id = empID;
            quit.dorm_id = dormID;
            quit.in_date = lodg.in_date;
            quit.out_date = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
            quit.comment = "不结算费用退宿";
            var emp = db.Employee.Single(em => em.id == empID);
            foreach(var ch in db.Charge.Where(c=>c.Lodging==lodg)){
                ch.Lodging = null;
            }
            BlackList bla = new BlackList();
            bla.emp_id = (int)empID;
            bla.in_date = dateTimePicker1.Value;
            bla.in_operator = LoginUser.username;
            bla.in_reason = tbsituation.Text;
            db.BlackList.InsertOnSubmit(bla);
            db.AutoQuit.InsertOnSubmit(quit);
            db.Lodging.DeleteOnSubmit(lodg);
            db.SubmitChanges();
            MyUtil.WriteEventLog(modelName, lbDormNumber.Text, lbName.Text, "不结算费用退宿,加入黑名单");
            //将住宿信息反馈到HR系统，2013-3-22更新
            if (empl.Department1.property.Equals("厂内") || empl.Department1.property.Equals("光电"))
            {
                if (!string.IsNullOrWhiteSpace(empl.account_number))
                {
                    try
                    {
                        db.updateHRLivingState(empl.account_number, false);
                    }
                    catch
                    {
                        MessageBox.Show("连接失败，员工退宿信息无法更新到人事系统。");
                    }
                }
            }

            rtbMes.AppendText(string.Format("[{0}]-->[黑名单]\n",emp.name));
            MessageBox.Show("操作成功");
        }

        private void FrmMovingOut_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 40;
        }

    }
}
