using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace DormitoryManagement
{
    public partial class FrmGuestExport : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string yearMonth;
        DateTime firstDayInMonth;
        DateTime lastDayInMonth;
        string fileRoute;

        public FrmGuestExport()
        {
            InitializeComponent();
        }

        private void FrmGuestExport_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = YearAndMonth.monthSpan(DateTime.Parse("2012-05-01"), DateTime.Now);
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            yearMonth = comboBox1.Text;
            firstDayInMonth = DateTime.Parse(yearMonth + "-01");
            lastDayInMonth = YearAndMonth.lastDayInMonth(yearMonth.Substring(0, 4) + yearMonth.Substring(5, 2));

            fileRoute = Utils.FileNameDialog(string.Format("信利招待所报表_{0}",yearMonth));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }
            button1.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = startToExportGuest();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            progressBar1.Style = ProgressBarStyle.Blocks;
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
            }

            switch (e.Result.ToString())
            {
                case "NODATA":
                    MessageBox.Show("需要导出的数据不存在");
                    break;
                case "NOEXCEL":
                    MessageBox.Show("客户机可能没有安装OFFICE excel软件，导出失败");
                    break;
                case "NORMAL":
                    MessageBox.Show("数据已成功导出");
                    break;
                case "UNCLEAR":
                    MessageBox.Show("操作失败……");
                    break;
            }
        }

        private string startToExportGuest() {
            string result = "NORMAL";
            int fieldNum = 13;
            //string sheetCaption = "";//sheet 标题
            List<GuestInfos> list=null;

            // 创建Excel对象
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            if (xlApp == null)
            {
                return "NOEXCEL";
            }
            try
            {
                // 创建Excel工作薄
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                //添加两个工作表
                for (int i = 1; i <= 2; i++)
                {
                    xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                for (int k = 1; k <= 2; k++)
                {
                    Worksheet xlSheet = (Worksheet)xlBook.Worksheets[k];
                    //关键：设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[k]).Activate();
                    if (k == 1)
                    {
                        xlSheet.Name = yearMonth + "(公)";
                        //sheetCaption = string.Format("{0}年{1}月份厂内在住员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        list=(db.GuestInfos.Where(g=>g.business && ((g.is_finish && g.real_out_date >= firstDayInMonth && g.in_date <=lastDayInMonth) || (!g.is_finish && g.out_date > lastDayInMonth && g.in_date <= lastDayInMonth) ))).ToList();
                    }
                    else if (k == 2)
                    {
                        xlSheet.Name = yearMonth + "(私)";
                        //sheetCaption = string.Format("{0}年{1}月份厂内退宿员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        list = (db.GuestInfos.Where(g => g.is_finish && !g.business && g.real_out_date >= firstDayInMonth && g.real_out_date <= lastDayInMonth)).ToList();
                    }

                    //// 设置标题
                    //Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    //range.MergeCells = true;
                    //xlApp.ActiveCell.FormulaR1C1 = sheetCaption; //设置标题
                    //xlApp.ActiveCell.Font.Size = 20;
                    //xlApp.ActiveCell.Font.Bold = true;
                    //xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    // 创建缓存数据
                    object[,] objData = new object[list.Count() + 3, fieldNum];
                    decimal allTotal = 0;

                    //设置列标题
                    objData[0, 0] = "房号";
                    objData[0, 1] = "租住人";
                    objData[0, 2] = "性别";
                    objData[0, 3] = "申请部门";
                    objData[0, 4] = "性质";
                    objData[0, 5] = "入住日期";
                    objData[0, 6] = "责任人";
                    objData[0, 7] = "退房人";
                    objData[0, 8] = "退房日期";
                    objData[0, 9] = "实住";
                    objData[0, 10] = "单价";
                    objData[0, 11] = "金额";
                    objData[0, 12] = "备注";

                    //写入表中数据
                    int i = 0;
                    int days=0;
                    string inDays = null;
                    string outDays = null;
                    decimal? sum = null;
                    foreach (GuestInfos info in list)
                    {                        
                        i++;                        
                        //私事
                        if (!info.business)
                        {
                            days = ((TimeSpan)(info.real_out_date - info.in_date)).Days;
                            inDays = ((DateTime)info.in_date).ToShortDateString();
                            outDays = ((DateTime)info.real_out_date).ToShortDateString();
                            sum = info.sum;
                        }
                        else {  //公事
                            if (info.is_finish) //已结算
                            {
                                if (info.in_date < firstDayInMonth && info.real_out_date <= lastDayInMonth) //之前入住，当月退宿
                                {
                                    days = ((TimeSpan)(info.real_out_date - firstDayInMonth)).Days;
                                    inDays = firstDayInMonth.ToShortDateString();
                                    outDays = ((DateTime)info.real_out_date).ToShortDateString();
                                }
                                else if(info.in_date >= firstDayInMonth && info.real_out_date <= lastDayInMonth)    //当月入住，当月退宿
                                {
                                    days = ((TimeSpan)(info.real_out_date - info.in_date)).Days;
                                    inDays = ((DateTime)info.in_date).ToShortDateString();
                                    outDays = ((DateTime)info.real_out_date).ToShortDateString();
                                    sum = info.sum;
                                }
                                else if (info.in_date < firstDayInMonth && info.real_out_date > lastDayInMonth) //之前入住，之后退宿
                                {
                                    days = (lastDayInMonth - firstDayInMonth).Days;
                                    inDays = firstDayInMonth.ToShortDateString();
                                    outDays = lastDayInMonth.ToShortDateString();
                                }
                                else {
                                    days = ((TimeSpan)(lastDayInMonth - info.in_date)).Days;    //当月入住，之后退宿
                                    inDays = ((DateTime)info.in_date).ToShortDateString();
                                    outDays = lastDayInMonth.ToShortDateString();
                                }
                            }
                            else {  //未结算
                                if (info.in_date < firstDayInMonth)
                                {
                                    days = ((TimeSpan)(lastDayInMonth - firstDayInMonth)).Days;
                                    inDays = firstDayInMonth.ToShortDateString();
                                }
                                else
                                {
                                    days = ((TimeSpan)(lastDayInMonth - info.in_date)).Days;
                                    inDays = ((DateTime)info.in_date).ToShortDateString();
                                }
                                outDays = lastDayInMonth.ToShortDateString();
                            }                            
                        }
                        //如果sum没有被赋值，则用天数乘单价进行计算。只有私事或者当月入住当月退宿的公事sum值才直接使用数据库的sum
                        if (sum == null) {
                            sum = days * info.price;
                        }
                        objData[i, 0] = info.dorm_number;
                        objData[i, 1] = info.living_people;
                        objData[i, 2] = info.sex;
                        objData[i, 3] = info.dep;
                        objData[i, 4] = info.business?"公事":"私事";
                        objData[i, 5] = inDays;
                        objData[i, 6] = info.charger;
                        objData[i, 7] = info.checkout;
                        objData[i, 8] = outDays;
                        objData[i, 9] = days;
                        objData[i, 10] = info.price;
                        objData[i, 11] = sum;
                        objData[i, 12] = info.comment;

                        //合计
                        allTotal += (decimal)sum;
                        
                    }

                    //写入合计行
                    objData[++i, 0] = "合计:";
                    objData[i, 11] = allTotal;

                    //写入最后一行
                    objData[++i, 0] = "统计员:";
                    objData[i, 5] = "后勤部审核：";
                    objData[i, 10] = "总裁办：";
                    // 写入Excel0
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[list.Count() + 3, fieldNum]);
                    range.Value2 = objData;
                    ////写入合计
                    //range = xlSheet.get_Range(xlApp.Cells[rowNum + 3, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                    //range.Font.Bold = true;
                    //列宽自适应
                    range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]);
                    range.EntireColumn.AutoFit();
                }
                //保存
                xlBook.Saved = true;
                xlBook.SaveCopyAs(fileRoute);

            }
            catch (Exception)
            {
                result = "UNCLEAR";
            }
            finally
            {
                xlApp.Quit();
                list = null;
                GC.Collect(); //强制回收
            }
            return result;
        }
    }
}
