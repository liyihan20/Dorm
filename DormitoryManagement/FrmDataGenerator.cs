using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace DormitoryManagement
{
    public partial class FrmDataGenerator : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string yearMonth;
        string modelName = "生成数据";
        public FrmDataGenerator()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            yearMonth = lbGenDate.Text;
            //yearMonth = "201611";
            MyUtil.WriteEventLog(modelName, "", "", yearMonth + ":准备生成月末数据");
            //加入账号审核和导入模块
            MyUtil.WriteEventLog(modelName, "", "", "进入账号审核和导入模块");
            if (!validateAccountNumber())
            {
                return;
            }

            //检测数据是否完整，如完整则生成数据并导出
            MyUtil.WriteEventLog(modelName, "", "", "检测水电度数数据是否完整");
            if (!can_export())
                return;
        }

        private void FrmDataGenerator_Load(object sender, EventArgs e)
        {
            List<string> list = (from veri in db.VerifyOrder
                                 where veri.can_export == 1
                                 orderby veri.year_and_month descending
                                 select veri.year_and_month).ToList();
            if (list.Count() > 0)
                lbGenDate.Text = YearAndMonth.next(list[0]);
            else
                lbGenDate.Text = YearAndMonth.previous((DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()));

            if (list.Count() > 0)
                lbDelYearMonth.Text = list[0];
            else
                lbDelYearMonth.Text = "不存在";
        }

        private bool can_export()
        {
            string preYearMonth = YearAndMonth.previous(yearMonth);

            //否则根据区名，利用存储过程查看水电费读数是否全部录入
            foreach (Area area in db.Area)
            {
                if (db.take_input_fuels(area.name, preYearMonth, yearMonth).Where(inp => (inp.cur_cold == null || inp.cur_elec == null)).Count() > 0)
                {
                    MyUtil.WriteEventLog(modelName, "", "", area.name + "的宿舍水电费录入未完整，数据不能导出");
                    MessageBox.Show(area.name + "的宿舍水电费录入未完整，数据不能导出");
                    return false;
                }
            }

            button6.Enabled = false;
            button1.Enabled = false;
            progressBar6.Value = 0;
            progressBar6.Maximum = db.Lodging.Where(lod => (lod.in_date <= YearAndMonth.lastDayInMonth(yearMonth) && (lod.out_date >= YearAndMonth.firstDayInMonth(yearMonth) || lod.out_date == null))).Count();
            MyUtil.WriteEventLog(modelName, "", "", yearMonth + ":开始生成。。。");
            bgw.RunWorkerAsync();
            return true;
        }

        //private string dataGenerate()
        //{
        //    {
        //        //当月第一天
        //        DateTime firstDayOfYearMonth = YearAndMonth.firstDayInMonth(yearMonth);
        //        //当月总天数
        //        int totalDayOfYearMonth = DateTime.DaysInMonth(Int32.Parse(yearMonth.Substring(0, 4)), Int32.Parse(yearMonth.Substring(4, 2)));
        //        //当月最后一天
        //        DateTime lastDayOfYearMonth = YearAndMonth.lastDayInMonth(yearMonth);
        //        List<Charge> chargeList = new List<Charge>();
        //        List<Lodging> lodgingList = db.Lodging.Where(lod => (lod.in_date <= lastDayOfYearMonth && (lod.out_date >= firstDayOfYearMonth || lod.out_date == null))).ToList();

        //        //保存进度
        //        int progress = 0;
        //        Charge cha;
        //        foreach (Lodging lodg in lodgingList)
        //        {
        //            cha = new Charge();
        //            cha.year_month = yearMonth;
        //            cha.area = lodg.Dorm.Area.name;
        //            cha.department = lodg.Employee.Department1 == null ? null : lodg.Employee.Department1.name;
        //            cha.Loding_id = lodg.id;
        //            cha.property = lodg.Employee.Department1 == null ? null : lodg.Employee.Department1.property;
        //            cha.Area1 = lodg.Dorm.Area;
        //            cha.Department1 = lodg.Employee.Department1;
        //            cha.classify_property = lodg.classify_property;
        //            cha.dorm_number = lodg.Dorm.number;
        //            cha.dorm_order = lodg.Dorm.forOrder;
        //            cha.employee = lodg.Employee.name;
        //            cha.account = lodg.Employee.account_number;

        //            //计算该宿舍所有员工的住宿时间之和，用于按比例分摊水电费
        //            int totalDaysOfDorm = 0;
        //            var lodgingsInDorm = from lod in db.Lodging
        //                                 where lod.Dorm == lodg.Dorm
        //                                 && lod.in_date <= lastDayOfYearMonth
        //                                 && (lod.out_date >= firstDayOfYearMonth
        //                                 || lod.out_date == null)
        //                                 select lod;

        //            //宿舍本月的水电费用
        //            var fuelInDorm = db.get_fuel_used_of_dorm(lodg.dorm_id, YearAndMonth.previous(yearMonth), yearMonth).First();
        //            decimal? elec = (decimal)fuelInDorm.elec * BaseInfo.electricity;
        //            decimal? hot = (decimal)fuelInDorm.hot * BaseInfo.hotWater;
        //            decimal? cold = (decimal)fuelInDorm.cold * BaseInfo.coldWater;
        //            decimal? water = hot + cold;
        //            //该员工当月实际住的天数
        //            int livingdays = 0;

        //            foreach (Lodging lo in lodgingsInDorm)
        //            {
        //                //当月住宿并且当月退宿
        //                if (lo.in_date >= firstDayOfYearMonth && lo.out_date <= lastDayOfYearMonth)
        //                {
        //                    //本月退宿的要把水电费先结算，并在宿舍的总费用中扣除
        //                    var outFuelFee = from ot in lo.Employee.OtherFee
        //                                      where ot.year_month == yearMonth
        //                                      && ot.dorm_id==lo.dorm_id
        //                                      && (ot.out_share_water != null || ot.out_share_eletricity!=null)
        //                                      select new
        //                                      {
        //                                          water = (ot.out_share_water == null ? 0 : ot.out_share_water),
        //                                          ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity)
        //                                      };
        //                    if (outFuelFee.Count() > 0)
        //                    {
        //                        elec -= outFuelFee.First().ele;
        //                        water -= outFuelFee.First().water;
        //                    }
        //                }
        //                //当月住宿
        //                else if (lo.in_date >= firstDayOfYearMonth)
        //                {
        //                    totalDaysOfDorm += totalDayOfYearMonth - ((DateTime)lo.in_date).Day + 1;
        //                }
        //                //当月退宿
        //                else if (lo.out_date <= lastDayOfYearMonth)
        //                {
        //                    //本月退宿的要把水电费先结算，并在宿舍的总费用中扣除
        //                    var outFuelFee = from ot in lo.Employee.OtherFee
        //                                      where ot.year_month == yearMonth
        //                                      && ot.dorm_id == lo.dorm_id
        //                                      && (ot.out_share_water != null || ot.out_share_eletricity != null)
        //                                      select new
        //                                      {
        //                                          water = (ot.out_share_water == null ? 0 : ot.out_share_water),
        //                                          ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity)
        //                                      };
        //                    if (outFuelFee.Count() > 0)
        //                    {
        //                        elec -= outFuelFee.First().ele;
        //                        water -= outFuelFee.First().water;
        //                    }
        //                }
        //                else
        //                {
        //                    totalDaysOfDorm += totalDayOfYearMonth;
        //                }
        //            }

        //            //本月入住并退宿员工处理
        //            if (lodg.in_date >= firstDayOfYearMonth && lodg.out_date <= lastDayOfYearMonth)
        //            {
        //                livingdays = ((DateTime)lodg.out_date).Day - ((DateTime)lodg.in_date).Day;
        //                //房租与押金
        //                cha.rent = Math.Round((decimal)lodg.real_rent * livingdays / totalDayOfYearMonth, 1);
        //                cha.management = Math.Round((decimal)lodg.real_manage * livingdays / totalDayOfYearMonth, 1);
        //                cha.guarantee = 0;

        //                //水电费
        //                var outFuelFee = from ot in lodg.Employee.OtherFee
        //                                 where ot.year_month == yearMonth
        //                                 && ot.dorm_id == lodg.dorm_id
        //                                 && (ot.out_share_water != null || ot.out_share_eletricity != null)
        //                                 select new
        //                                 {
        //                                     water = (ot.out_share_water == null ? 0 : ot.out_share_water),
        //                                     ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity)
        //                                 };
        //                if (outFuelFee.Count() > 0)
        //                {
        //                    cha.electricity = outFuelFee.First().ele;
        //                    cha.water = outFuelFee.First().water;
        //                }
        //                else {
        //                    cha.electricity = 0;
        //                    cha.water = 0;
        //                }
        //            }
        //            //本月入住员工处理
        //            else if (lodg.in_date >= firstDayOfYearMonth)
        //            {
        //                livingdays = totalDayOfYearMonth - ((DateTime)lodg.in_date).Day + 1;                        
        //                //房租与押金
        //                cha.rent = Math.Round((decimal)lodg.real_rent * livingdays / totalDayOfYearMonth, 1);
        //                cha.management = Math.Round((decimal)lodg.real_manage * livingdays / totalDayOfYearMonth, 1);
        //                cha.guarantee = lodg.guarantee;

        //                //水电费
        //                cha.electricity = Math.Round((decimal)elec * livingdays / totalDaysOfDorm, 1);
        //                cha.water = Math.Round((decimal)(water) * livingdays / totalDaysOfDorm, 1);

        //            }
        //            //本月退宿员工处理
        //            else if (lodg.out_date <= lastDayOfYearMonth)
        //            {
        //                livingdays = ((DateTime)lodg.out_date).Day;
        //                //房租与押金
        //                cha.guarantee = -lodg.guarantee;
        //                cha.rent = Math.Round((decimal)lodg.real_rent * ((DateTime)lodg.out_date).Day / totalDayOfYearMonth, 1);
        //                cha.management = Math.Round((decimal)lodg.real_manage * ((DateTime)lodg.out_date).Day / totalDayOfYearMonth, 1);

        //                //水电费
        //                var outFuelFee = from ot in lodg.Employee.OtherFee
        //                                  where ot.year_month == yearMonth
        //                                  && ot.dorm_id == lodg.dorm_id
        //                                  && (ot.out_share_water != null || ot.out_share_eletricity != null)
        //                                  select new
        //                                  {
        //                                      water = (ot.out_share_water == null ? 0 : ot.out_share_water),
        //                                      ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity),
        //                                  };
        //                if (outFuelFee.Count() > 0)
        //                {
        //                    cha.electricity = outFuelFee.First().ele;
        //                    cha.water = outFuelFee.First().water;
        //                }
        //                else {
        //                    cha.electricity = 0;
        //                    cha.water = 0;
        //                }
        //                //cha.comment += "员工退宿:" + ((DateTime)lodg.out_date).ToShortDateString();
        //            }
        //            //正常员工的房租与水电费
        //            else
        //            {
        //                livingdays = totalDayOfYearMonth;
        //                cha.rent = lodg.real_rent;
        //                cha.management = lodg.real_manage;
        //                cha.electricity = Math.Round((decimal)elec * totalDayOfYearMonth / totalDaysOfDorm, 1);
        //                cha.water = Math.Round((decimal)(water) * totalDayOfYearMonth / totalDaysOfDorm, 1);
        //            }

        //            //其他费用用叠加处理
        //            foreach (OtherFee other in lodg.Employee.OtherFee.Where(ot => ot.year_month == yearMonth && ot.dorm_id==lodg.dorm_id))
        //            {
        //                cha.repair = (cha.repair == null ? 0 : cha.repair) + (other.repair_cost == null ? 0 : other.repair_cost);
        //                cha.fine = (cha.fine == null ? 0 : cha.fine) + (other.fine == null ? 0 : other.fine);
        //                cha.guesthouse = (cha.guesthouse == null ? 0 : cha.guesthouse) + (other.guesthouse_cost == null ? 0 : other.guesthouse_cost);
        //                cha.others = (cha.others == null ? 0 : cha.others) + (other.other_cost == null ? 0 : other.other_cost);
        //                cha.comment += other.comment + "  ";
        //            }

        //            //新员工4人间以上前三个月免住宿费
        //            if (lodg.Dorm.DormType.max_number >= 4)
        //            {
        //                string cardNumber = lodg.Employee.card_number;
        //                if (!("0").Equals(cardNumber) &&!string.IsNullOrEmpty(cardNumber))
        //                {
        //                    int leftDay = YearAndMonth.leftDays(Convert.ToDateTime(string.Format("20{0}-{1}-{2}", cardNumber.Substring(0, 2), cardNumber.Substring(2, 2), cardNumber.Substring(4, 2))), lastDayOfYearMonth);
        //                    //加入需要结算的天数小于当月入住的天数，则重新计算租金
        //                    if (leftDay < livingdays)
        //                    {
        //                        cha.rent = Math.Round(Convert.ToDecimal(lodg.real_rent) * leftDay / totalDayOfYearMonth, 2);
        //                        cha.comment += lodg.Employee.card_number;
        //                    }
        //                }
        //            }

        //            //总费用
        //            cha.total = cha.rent + cha.water + cha.electricity + cha.management
        //            + (cha.repair == null ? 0 : cha.repair) + (cha.fine == null ? 0 : cha.fine) + (cha.guesthouse == null ? 0 : cha.guesthouse) + (cha.guarantee == null ? 0 : cha.guarantee) + (cha.others == null ? 0 : cha.others);

        //            //将费用为0的设为空
        //            if (cha.repair == 0) {
        //                cha.repair = null;
        //            }
        //            if (cha.fine == 0) {
        //                cha.fine = null;
        //            }
        //            if (cha.guesthouse == 0) {
        //                cha.guesthouse = null;
        //            }
        //            if (cha.others == 0) {
        //                cha.others = null;
        //            }

        //            //插入新增list
        //            chargeList.Add(cha);
        //            //汇报进度
        //            bgw.ReportProgress(++progress);
        //        }
        //        db.Charge.InsertAllOnSubmit(chargeList);

        //        //若水电费全部录入，则添加记录到审核时序表
        //        VerifyOrder verify = new VerifyOrder()
        //        {
        //            year_and_month = yearMonth,
        //            is_verify = 1,
        //            can_export = 1
        //        };
        //        db.VerifyOrder.InsertOnSubmit(verify);
        //        db.SubmitChanges();
        //        chargeList = null;
        //        System.GC.Collect();
        //        return "S";
        //    }
        //}


        private string dataGenerate()
        {
            //2016-12-05 缩短生成时间，减少数据库查询次数，先将数据放在内存再检索

            //当月第一天
            DateTime firstDayOfYearMonth = YearAndMonth.firstDayInMonth(yearMonth);
            //当月总天数
            int totalDayOfYearMonth = DateTime.DaysInMonth(Int32.Parse(yearMonth.Substring(0, 4)), Int32.Parse(yearMonth.Substring(4, 2)));
            //当月最后一天
            DateTime lastDayOfYearMonth = YearAndMonth.lastDayInMonth(yearMonth);
            List<Charge> chargeList = new List<Charge>();
            List<Lodging> lodgingList = db.Lodging.Where(lod => (lod.in_date <= lastDayOfYearMonth && (lod.out_date >= firstDayOfYearMonth || lod.out_date == null))).ToList();

            //保存在内存中的list，替代频繁从数据库中查询的变量
            List<Lodging> LodgingCkList = new List<Lodging>(lodgingList.ToArray());
            List<OtherFee> otherFeeCkList = db.OtherFee.Where(o => o.year_month == yearMonth).ToList();

            //保存进度
            int progress = 0;
            Charge cha;
            foreach (Lodging lodg in lodgingList)
            //System.Threading.Tasks.Parallel.ForEach(lodgingList, (lodg) =>   //并行处理，有待测试
            {
                cha = new Charge();
                cha.year_month = yearMonth;
                cha.area = lodg.Dorm.Area.name;
                cha.department = lodg.Employee.Department1 == null ? null : lodg.Employee.Department1.name;
                cha.Loding_id = lodg.id;
                cha.property = lodg.Employee.Department1 == null ? null : lodg.Employee.Department1.property;
                cha.Area1 = lodg.Dorm.Area;
                cha.Department1 = lodg.Employee.Department1;
                cha.classify_property = lodg.classify_property;
                cha.dorm_number = lodg.Dorm.number;
                cha.dorm_order = lodg.Dorm.forOrder;
                cha.employee = lodg.Employee.name;
                cha.account = lodg.Employee.account_number;

                //计算该宿舍所有员工的住宿时间之和，用于按比例分摊水电费
                int totalDaysOfDorm = 0;
                var lodgingsInDorm = LodgingCkList.Where(l => l.dorm_id == lodg.dorm_id).ToList();

                //宿舍本月的水电费用
                var fuelInDorm = db.get_fuel_used_of_dorm(lodg.dorm_id, YearAndMonth.previous(yearMonth), yearMonth).First();
                decimal? elec = (decimal)fuelInDorm.elec * BaseInfo.electricity;
                decimal? hot = (decimal)fuelInDorm.hot * BaseInfo.hotWater;
                decimal? cold = (decimal)fuelInDorm.cold * BaseInfo.coldWater;
                decimal? water = hot + cold;
                //该员工当月实际住的天数
                int livingdays = 0;

                foreach (Lodging lo in lodgingsInDorm)
                {
                    //当月住宿并且当月退宿
                    if (lo.in_date >= firstDayOfYearMonth && lo.out_date <= lastDayOfYearMonth)
                    {
                        //本月退宿的要把水电费先结算，并在宿舍的总费用中扣除
                        var outFuelFee = (from ot in otherFeeCkList
                                          where ot.emp_id == lo.emp_id
                                          && ot.Dorm == lo.Dorm
                                          && (ot.out_share_water != null || ot.out_share_eletricity != null)
                                          select new
                                          {
                                              water = (ot.out_share_water == null ? 0 : ot.out_share_water),
                                              ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity)
                                          }).ToList();
                        if (outFuelFee.Count() > 0)
                        {
                            elec -= outFuelFee.First().ele;
                            water -= outFuelFee.First().water;
                        }
                    }
                    //当月住宿
                    else if (lo.in_date >= firstDayOfYearMonth)
                    {
                        totalDaysOfDorm += totalDayOfYearMonth - ((DateTime)lo.in_date).Day + 1;
                    }
                    //当月退宿
                    else if (lo.out_date <= lastDayOfYearMonth)
                    {
                        //本月退宿的要把水电费先结算，并在宿舍的总费用中扣除
                        var outFuelFee = (from ot in otherFeeCkList
                                          where ot.emp_id == lo.emp_id
                                          && ot.Dorm == lo.Dorm
                                          && (ot.out_share_water != null || ot.out_share_eletricity != null)
                                          select new
                                          {
                                              water = (ot.out_share_water == null ? 0 : ot.out_share_water),
                                              ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity)
                                          }).ToList();
                        if (outFuelFee.Count() > 0)
                        {
                            elec -= outFuelFee.First().ele;
                            water -= outFuelFee.First().water;
                        }
                    }
                    else
                    {
                        totalDaysOfDorm += totalDayOfYearMonth;
                    }
                }

                //本月入住并退宿员工处理
                if (lodg.in_date >= firstDayOfYearMonth && lodg.out_date <= lastDayOfYearMonth)
                {
                    livingdays = ((DateTime)lodg.out_date).Day - ((DateTime)lodg.in_date).Day;
                    //房租与押金
                    cha.rent = Math.Round((decimal)lodg.real_rent * livingdays / totalDayOfYearMonth, 1);
                    cha.management = Math.Round((decimal)lodg.real_manage * livingdays / totalDayOfYearMonth, 1);
                    cha.guarantee = 0;

                    //水电费
                    var outFuelFee = from ot in otherFeeCkList
                                     where ot.emp_id == lodg.emp_id
                                        && ot.dorm_id == lodg.dorm_id
                                        && (ot.out_share_water != null || ot.out_share_eletricity != null)
                                     select new
                                     {
                                         water = (ot.out_share_water == null ? 0 : ot.out_share_water),
                                         ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity)
                                     };
                    if (outFuelFee.Count() > 0)
                    {
                        cha.electricity = outFuelFee.First().ele;
                        cha.water = outFuelFee.First().water;
                    }
                    else
                    {
                        cha.electricity = 0;
                        cha.water = 0;
                    }
                }
                //本月入住员工处理
                else if (lodg.in_date >= firstDayOfYearMonth)
                {
                    livingdays = totalDayOfYearMonth - ((DateTime)lodg.in_date).Day + 1;
                    //房租与押金
                    cha.rent = Math.Round((decimal)lodg.real_rent * livingdays / totalDayOfYearMonth, 1);
                    cha.management = Math.Round((decimal)lodg.real_manage * livingdays / totalDayOfYearMonth, 1);
                    cha.guarantee = lodg.guarantee;

                    //水电费
                    cha.electricity = Math.Round((decimal)elec * livingdays / totalDaysOfDorm, 1);
                    cha.water = Math.Round((decimal)(water) * livingdays / totalDaysOfDorm, 1);

                }
                //本月退宿员工处理
                else if (lodg.out_date <= lastDayOfYearMonth)
                {
                    livingdays = ((DateTime)lodg.out_date).Day;
                    //房租与押金
                    cha.guarantee = -lodg.guarantee;
                    cha.rent = Math.Round((decimal)lodg.real_rent * ((DateTime)lodg.out_date).Day / totalDayOfYearMonth, 1);
                    cha.management = Math.Round((decimal)lodg.real_manage * ((DateTime)lodg.out_date).Day / totalDayOfYearMonth, 1);

                    //水电费
                    var outFuelFee = from ot in otherFeeCkList
                                     where ot.emp_id == lodg.emp_id
                                        && ot.dorm_id == lodg.dorm_id
                                        && (ot.out_share_water != null || ot.out_share_eletricity != null)
                                     select new
                                     {
                                         water = (ot.out_share_water == null ? 0 : ot.out_share_water),
                                         ele = (ot.out_share_eletricity == null ? 0 : ot.out_share_eletricity),
                                     };
                    if (outFuelFee.Count() > 0)
                    {
                        cha.electricity = outFuelFee.First().ele;
                        cha.water = outFuelFee.First().water;
                    }
                    else
                    {
                        cha.electricity = 0;
                        cha.water = 0;
                    }
                    //cha.comment += "员工退宿:" + ((DateTime)lodg.out_date).ToShortDateString();
                }
                //正常员工的房租与水电费
                else
                {
                    livingdays = totalDayOfYearMonth;
                    cha.rent = lodg.real_rent;
                    cha.management = lodg.real_manage;
                    cha.electricity = Math.Round((decimal)elec * totalDayOfYearMonth / totalDaysOfDorm, 1);
                    cha.water = Math.Round((decimal)(water) * totalDayOfYearMonth / totalDaysOfDorm, 1);
                }

                //其他费用用叠加处理
                foreach (OtherFee other in otherFeeCkList.Where(o => o.dorm_id == lodg.dorm_id && o.emp_id == lodg.emp_id).ToList())
                {
                    cha.repair = (cha.repair == null ? 0 : cha.repair) + (other.repair_cost == null ? 0 : other.repair_cost);
                    cha.fine = (cha.fine == null ? 0 : cha.fine) + (other.fine == null ? 0 : other.fine);
                    cha.guesthouse = (cha.guesthouse == null ? 0 : cha.guesthouse) + (other.guesthouse_cost == null ? 0 : other.guesthouse_cost);
                    cha.others = (cha.others == null ? 0 : cha.others) + (other.other_cost == null ? 0 : other.other_cost);
                    cha.comment += other.comment + "  ";
                }

                //新员工4人间以上前三个月免住宿费
                if (lodg.Dorm.DormType.max_number >= 4)
                {
                    string cardNumber = lodg.Employee.card_number;
                    if (!("0").Equals(cardNumber) && !string.IsNullOrEmpty(cardNumber))
                    {
                        int leftDay = YearAndMonth.leftDays(Convert.ToDateTime(string.Format("20{0}-{1}-{2}", cardNumber.Substring(0, 2), cardNumber.Substring(2, 2), cardNumber.Substring(4, 2))), lastDayOfYearMonth);
                        //加入需要结算的天数小于当月入住的天数，则重新计算租金
                        if (leftDay < livingdays)
                        {
                            cha.rent = Math.Round(Convert.ToDecimal(lodg.real_rent) * leftDay / totalDayOfYearMonth, 2);
                            cha.comment += lodg.Employee.card_number;
                        }
                    }
                }

                //总费用
                cha.total = cha.rent + cha.water + cha.electricity + cha.management
                + (cha.repair == null ? 0 : cha.repair) + (cha.fine == null ? 0 : cha.fine) + (cha.guesthouse == null ? 0 : cha.guesthouse) + (cha.guarantee == null ? 0 : cha.guarantee) + (cha.others == null ? 0 : cha.others);

                //将费用为0的设为空
                if (cha.repair == 0)
                {
                    cha.repair = null;
                }
                if (cha.fine == 0)
                {
                    cha.fine = null;
                }
                if (cha.guesthouse == 0)
                {
                    cha.guesthouse = null;
                }
                if (cha.others == 0)
                {
                    cha.others = null;
                }

                //插入新增list
                chargeList.Add(cha);
                //汇报进度
                bgw.ReportProgress(++progress);
            }            
            db.Charge.InsertAllOnSubmit(chargeList);

            //若水电费全部录入，则添加记录到审核时序表
            VerifyOrder verify = new VerifyOrder()
            {
                year_and_month = yearMonth,
                is_verify = 1,
                can_export = 1
            };
            db.VerifyOrder.InsertOnSubmit(verify);
            db.SubmitChanges();
            chargeList = null;
            System.GC.Collect();
            return "S";

        }


        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = dataGenerate();
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar6.Value = e.ProgressPercentage;
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button6.Enabled = true;
            button1.Enabled = true;
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
                MyUtil.WriteEventLog(modelName, "", "", "发生未知错误", false);
            }
            switch (e.Result.ToString())
            {
                case "S":
                    MessageBox.Show(yearMonth + ":数据生成成功!");
                    MyUtil.WriteEventLog(modelName, "", "", "数据生成成功");
                    this.Close();
                    break;
                case "NODATA":
                    MessageBox.Show("需要导出的数据不存在");
                    MyUtil.WriteEventLog(modelName, "", "", "需要导出的数据不存在", false);
                    break;
                case "NOEXCEL":
                    MessageBox.Show("客户机可能没有安装OFFICE excel软件，导出失败");
                    MyUtil.WriteEventLog(modelName, "", "", "客户机可能没有安装OFFICE excel软件，导出失败", false);
                    break;
                case "UNCLEAR":
                    MessageBox.Show("操作失败……");
                    MyUtil.WriteEventLog(modelName, "", "", "操作失败", false);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbDelYearMonth.Text.Equals("不存在"))
            {
                MessageBox.Show("不存在要反核算的月份");
                return;
            }
            if (MessageBox.Show("确定要删除" + lbDelYearMonth.Text + "月份所生成的数据吗", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            db.deleteCharge(lbDelYearMonth.Text);
            MessageBox.Show("反核算成功！请重新生成数据");
            MyUtil.WriteEventLog(modelName, "", "", "反核算成功:" + lbDelYearMonth.Text);
            lbGenDate.Text = lbDelYearMonth.Text;
            lbDelYearMonth.Text = YearAndMonth.previous(lbDelYearMonth.Text);
        }

        private void FrmDataGenerator_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox6.Left = this.Width / 2 - groupBox6.Width / 2;
        }

        //员工入住的时候，有些情况下当时账号还没有分配，或者和人事的不匹配，或者账号没有维护为空，需要处理。所以在结算的时候先要把厂内和光电的账号再次带过来。
        private bool validateAccountNumber()
        {
            DateTime firstDayOfYearMonth = YearAndMonth.firstDayInMonth(yearMonth);
            var blanks = db.getBlankAccount(firstDayOfYearMonth.ToShortDateString()).ToList();
            if (blanks.Count() > 0)
            {
                string eventLog = string.Format("厂牌为{0},姓名是{1}的员工账号为空，生成失败", blanks.First().card_number, blanks.First().name);
                MessageBox.Show(eventLog);
                MyUtil.WriteEventLog(modelName, "", "", eventLog, false);
                return false;
            }

            try
            {
                db.updateAccountthroughDJB(firstDayOfYearMonth.ToShortDateString());
            }
            catch (Exception)
            {
                MessageBox.Show("账号更新过程中出现异常，生成失败");
                MyUtil.WriteEventLog(modelName, "", "", "账号更新过程中出现异常，生成失败", false);
                return false;
            }
            return true;
        }
    }
}
