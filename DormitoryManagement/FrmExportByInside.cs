using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using org.in2bits.MyXls;

namespace DormitoryManagement
{
    public partial class FrmExportByInside : Form
    {
        string modelName = "Excel导出";
        DormDBDataContext db = new DormDBDataContext();
        string yearMonth;
        int doWhat;
        string fileRoute;
        string exportType;
        DateTime lastDayOfMonth;
        DateTime firstDayOfMonth;
        IQueryable<Charge> charges;
        List<ExportCharge> aList;

        public FrmExportByInside()
        {
            InitializeComponent();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (doWhat)
            {
                case 1:
                    //e.Result = exportDataOfInsideByDepartment();
                    e.Result = exportHotelByMyxls();
                    break;
                case 2:
                    //e.Result = exportDataOfInsideByArea();
                    e.Result = exportAreaDataByMyxsl();
                    break;
                case 3:
                    //e.Result = exportDataOfDep();
                    e.Result = exportDepDataByMyxls();
                    break;
                case 4:
                    //e.Result = exportChangeDorm();
                    e.Result = exportChangeDormByMyxls();
                    break;
                case 5:
                    //e.Result = exportDataOfInAndOut();
                    e.Result = exportInAndOutDataByMyxls();
                    break;
            }
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
                MyUtil.WriteEventLog(modelName, "", "", "导出Excel发生错误：" + e.Error.Message, false);
            }

            switch (e.Result.ToString())
            {
                case "NODATA":
                    MessageBox.Show("需要导出的数据不存在");
                    MyUtil.WriteEventLog(modelName, "", "", "需要导出的数据不存在", false);
                    break;
                case "NOEXCEL":
                    MessageBox.Show("客户机可能没有安装OFFICE excel软件，导出失败");
                    MyUtil.WriteEventLog(modelName, "", "", "客户机可能没有安装OFFICE excel软件，导出失败", false);
                    break;
                case "NORMAL":
                    progressBar1.Value = progressBar1.Maximum;
                    MessageBox.Show("数据已成功导出");
                    MyUtil.WriteEventLog(modelName, "", "", "数据已成功导出:" + fileRoute);
                    break;
                case "UNCLEAR":
                    MessageBox.Show("操作失败……");
                    MyUtil.WriteEventLog(modelName, "", "", "操作失败……错误未知", false);
                    break;
            }
        }

        private void FrmExportByInside_Load(object sender, EventArgs e)
        {
            //年月份列表的数据源
            List<string> list = (from veri in db.VerifyOrder
                                 where veri.can_export == 1
                                 orderby veri.year_and_month descending
                                 select veri.year_and_month).ToList();
            cbYearMonth1.DataSource = list;
            cbExportType1.Text = "按部门";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!cbYearMonth1.Text.Equals(yearMonth))
            {
                yearMonth = cbYearMonth1.Text;
                aList = null;
            }
            if (string.IsNullOrEmpty(yearMonth.Trim()))
            {
                MessageBox.Show("请选择年月份");
                return;
            }
            startToExportInside();
        }

        private void startToExportInside()
        {
            string fileName = string.Format("{0}年{1}月份房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
            int processMax = 0;
            exportType = cbExportType1.Text;
            lastDayOfMonth = YearAndMonth.lastDayInMonth(yearMonth);
            firstDayOfMonth = YearAndMonth.firstDayInMonth(yearMonth);
            if (exportType.Equals("按部门"))
            {
                fileName += "(按部门)";
                charges = db.Charge.Where(cha => (cha.year_month == yearMonth && !cha.property.Equals("特殊部门")));
                processMax = charges.Count() * 2;//1倍光电、半导体，1倍数据合并阶段
                doWhat = 3;
            }
            if (exportType.Equals("按宿舍区"))
            {
                processMax = db.Charge.Where(ch => ch.year_month == yearMonth).Count();
                fileName += "(按宿舍区)";
                doWhat = 2;
            }
            if (exportType.Equals("巴黎酒店"))
            {
                exportType = "巴黎酒店";
                fileName += "(巴黎酒店)";
                doWhat = 1;
                processMax = db.Charge.Where(ch => (ch.year_month == yearMonth && ch.property == exportType || ch.property.Equals("特殊部门"))).Count();
            }
            if (exportType.Equals("调房"))
            {
                fileName += "(调房)";
                processMax = db.VwChangeReport.Where(v => v.year_month == yearMonth).Count();
                doWhat = 4;
            } if (exportType.Equals("按在住退宿"))
            {
                fileName += "(按在住退宿)";
                charges = db.Charge.Where(cha => (cha.year_month == yearMonth && !cha.property.Equals("特殊部门") && !cha.property.Equals("巴黎酒店")));
                processMax = charges.Count() * 2;//1倍在住、退宿，1倍数据合并阶段
                doWhat = 5;
            }

            fileRoute = Utils.FileNameDialog(fileName);
            if (fileRoute.Equals("cancel"))
            {
                return;
            }
            button1.Enabled = false;
            progressBar1.Value = 0;
            progressBar1.Maximum = processMax;

            MyUtil.WriteEventLog(modelName, "", "", "开始导出：" + fileName);
            bgw.RunWorkerAsync();
        }

        #region 巴黎酒店：依赖ms excel
        /*private string exportDataOfInsideByDepartment()
        {
            string result = "NORMAL";
            int fieldNum = 17;
            List<Charge> exportingData = (from ch in db.Charge
                                          where ch.year_month == yearMonth
                                          && ch.property == exportType
                                          orderby ch.dorm_number
                                          select ch).ToList();
            int rowNum = exportingData.Count();
            if (rowNum == 0)
            {
                return "NODATA";
            }

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
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                // 设置标题
                Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                range.MergeCells = true;
                xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}年{1}月份房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)); //设置标题
                xlApp.ActiveCell.Font.Size = 20;
                xlApp.ActiveCell.Font.Bold = true;
                xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                // 创建缓存数据
                object[,] objData = new object[rowNum + 3, fieldNum];
                decimal totalAll = 0m;
                decimal totalRent = 0m;
                decimal totalManag = 0m;
                decimal totalEle = 0m;
                decimal totalWater = 0m;
                decimal totalOther = 0m;
                decimal totalRepair = 0m;
                decimal totalFine = 0m;
                decimal totalGuest = 0m;
                decimal totalGuarantee = 0m;
                //设置列标题
                objData[0, 0] = "部门";
                objData[0, 1] = "账号";
                objData[0, 2] = "姓名";
                objData[0, 3] = "合计";
                objData[0, 4] = "房租";
                objData[0, 5] = "管理费";
                objData[0, 6] = "电费";
                objData[0, 7] = "水费";
                objData[0, 8] = "其他";
                objData[0, 9] = "维修";
                objData[0, 10] = "扣分";
                objData[0, 11] = "招待所";
                objData[0, 12] = "押金";
                objData[0, 13] = "区别";
                objData[0, 14] = "房号";
                objData[0, 15] = "性质";
                objData[0, 16] = "备注";

                //写入表中数据
                int i = 0;
                //string acc = "XY";
                foreach (Charge cha in exportingData)
                {
                    //如果在同一个月来退宿又住宿(转宿舍)，即记录合并。                    
                    //if (acc!=null && acc.Equals(cha.account))
                    //{
                    //    objData[i, 3] = Convert.ToDecimal(objData[i, 3]) + (cha.total == null ? 0 : (decimal)cha.total);
                    //    objData[i, 4] = Convert.ToDecimal(objData[i, 4]) + (cha.rent == null ? 0 : (decimal)cha.rent);
                    //    objData[i, 5] = Convert.ToDecimal(objData[i, 5]) + (cha.management == null ? 0 : (decimal)cha.management);
                    //    objData[i, 6] = Convert.ToDecimal(objData[i, 6]) + (cha.electricity == null ? 0 : (decimal)cha.electricity);
                    //    objData[i, 7] = Convert.ToDecimal(objData[i, 7]) + (cha.water == null ? 0 : (decimal)cha.water);
                    //    objData[i, 8] = Convert.ToDecimal(objData[i, 8]) + (cha.others == null ? 0 : (decimal)cha.others);
                    //    objData[i, 9] = Convert.ToDecimal(objData[i, 9]) + (cha.repair == null ? 0 : (decimal)cha.repair);
                    //    objData[i, 10] = Convert.ToDecimal(objData[i, 10]) + (cha.fine == null ? 0 : (decimal)cha.fine);
                    //    objData[i, 11] = Convert.ToDecimal(objData[i, 11]) + (cha.guesthouse == null ? 0 : (decimal)cha.guesthouse);
                    //    objData[i, 12] = Convert.ToDecimal(objData[i, 12]) + (cha.guarantee == null ? 0 : (decimal)cha.guarantee);
                    //    if (cha.guarantee > 0) {
                    //        objData[i, 13] = cha.area;
                    //        objData[i, 14] = cha.dorm_number;
                    //        objData[i, 15] = cha.classify_property;
                    //    }                        
                    //    objData[i, 16] = Convert.ToString(objData[i, 16]) + " " + cha.comment;
                    //}
                    //else
                    //{
                    //acc = cha.account;
                    i++;
                    objData[i, 0] = cha.department;
                    objData[i, 1] = cha.account;
                    objData[i, 2] = cha.employee;
                    objData[i, 3] = cha.total;
                    objData[i, 4] = cha.rent;
                    objData[i, 5] = cha.management;
                    objData[i, 6] = cha.electricity;
                    objData[i, 7] = cha.water;
                    objData[i, 8] = cha.others;
                    objData[i, 9] = cha.repair;
                    objData[i, 10] = cha.fine;
                    objData[i, 11] = cha.guesthouse;
                    objData[i, 12] = cha.guarantee;
                    objData[i, 13] = cha.area;
                    objData[i, 14] = cha.dorm_number;
                    objData[i, 15] = cha.classify_property;
                    objData[i, 16] = cha.comment;
                    //}

                    totalAll += (cha.total == null ? 0 : (decimal)cha.total);
                    totalRent += (cha.rent == null ? 0 : (decimal)cha.rent);
                    totalManag += (cha.management == null ? 0 : (decimal)cha.management);
                    totalEle += (cha.electricity == null ? 0 : (decimal)cha.electricity);
                    totalWater += (cha.water == null ? 0 : (decimal)cha.water);
                    totalOther += (cha.others == null ? 0 : (decimal)cha.others);
                    totalRepair += (cha.repair == null ? 0 : (decimal)cha.repair);
                    totalFine += (cha.fine == null ? 0 : (decimal)cha.fine);
                    totalGuest += (cha.guesthouse == null ? 0 : (decimal)cha.guesthouse);
                    totalGuarantee += (cha.guarantee == null ? 0 : (decimal)cha.guarantee);

                    bgw.ReportProgress(i);
                }

                //写入合计数据
                objData[++i, 0] = "合计";
                objData[i, 3] = Math.Round(totalAll, 1);
                objData[i, 4] = Math.Round(totalRent, 1);
                objData[i, 5] = Math.Round(totalManag, 1);
                objData[i, 6] = Math.Round(totalEle, 1);
                objData[i, 7] = Math.Round(totalWater, 1);
                objData[i, 8] = Math.Round(totalOther, 1);
                objData[i, 9] = Math.Round(totalRepair, 1);
                objData[i, 10] = Math.Round(totalFine, 1);
                objData[i, 11] = Math.Round(totalGuest, 1);
                objData[i, 12] = Math.Round(totalGuarantee, 1);

                //写入最后一行
                objData[++i, 0] = "制表:";
                objData[i, 9] = "审核:";
                // 写入Excel
                range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 4, fieldNum]);
                range.Value2 = objData;
                ////写入合计
                //range = xlSheet.get_Range(xlApp.Cells[rowNum + 3, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                //range.Font.Bold = true;
                //列宽自适应
                range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
                range.EntireColumn.AutoFit();
                //设置金额保留1位小数点，第4列到10列,12列到13列
                range = xlSheet.get_Range(xlApp.Cells[3, 4], xlApp.Cells[rowNum + 3, 10]);
                //range.NumberFormatLocal = "0.0";
                range.NumberFormat = "0.0";
                range = xlSheet.get_Range(xlApp.Cells[3, 12], xlApp.Cells[rowNum + 3, 13]);
                range.NumberFormat = "0.0";
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
                exportingData = null;
                GC.Collect(); //强制回收
            }
            return result;
        }*/
        #endregion

        //巴黎半岛：使用myxls
        private string exportHotelByMyxls()
        {
            string fileName = fileRoute.Substring(fileRoute.LastIndexOf(@"\") + 1);
            string folderName = fileRoute.Substring(0, fileRoute.Length - fileName.Length - 1);

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;

            //普通文字样式
            XF dataXF = xls.NewXF();
            dataXF.Font.FontName = "宋体";

            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 12, 8, 12, 10, 10, 10, 10, 10, 10, 10, 10, 10, 
                //10,
                8, 8, 8, 16 };
            string[] colName = new string[] { "部门", "账号", "姓名", "合计", "房租", "管理费", "电费",
                "水费", "其他", "维修", "扣分", "招待所", 
                //"押金", 
                "区别", "房号", "性质", "备注" };

            int rowIndex = 1;
            int colIndex = 1;
            org.in2bits.MyXls.Worksheet sheet = xls.Workbook.Worksheets.Add("巴黎酒店");
            Cells cells = sheet.Cells;

            //设置列宽
            ColumnInfo col;
            for (ushort i = 0; i < colWidth.Length; i++)
            {
                col = new ColumnInfo(xls, sheet);
                col.ColumnIndexStart = i;
                col.ColumnIndexEnd = i;
                col.Width = (ushort)(colWidth[i] * 256);
                sheet.AddColumnInfo(col);
            }

            Cell cell;
            foreach (var name in colName)
            {
                cell = cells.Add(rowIndex, colIndex++, name, dataXF);
            }

            List<Charge> exportingData = (from ch in db.Charge
                                          where ch.year_month == yearMonth
                                          && (ch.property == exportType || ch.property.Equals("特殊部门"))
                                          orderby ch.dorm_number
                                          select ch).ToList();
            int proc = 0;
            //"部门", "账号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别", "房号", "性质","备注"
            foreach (var data in exportingData)
            {
                colIndex = 0;
                rowIndex++;
                cell = cells.Add(rowIndex, ++colIndex, data.department, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.account, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.employee, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.total, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.rent, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.management, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.electricity, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.water, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.others, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.repair, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.fine, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.guesthouse, decimaXF);
                //cell = cells.Add(rowIndex, ++colIndex, data.guarantee, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.area, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.dorm_number, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.classify_property, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.comment, dataXF);

                bgw.ReportProgress(++proc);
            }

            //合计
            rowIndex++;
            cell = cells.Add(rowIndex, 1, "合计", dataXF);
            cell = cells.Add(rowIndex, 4, exportingData.Sum(d => d.total), decimaXF);
            cell = cells.Add(rowIndex, 5, exportingData.Sum(d => d.rent), decimaXF);
            cell = cells.Add(rowIndex, 6, exportingData.Sum(d => d.management), decimaXF);
            cell = cells.Add(rowIndex, 7, exportingData.Sum(d => d.electricity), decimaXF);
            cell = cells.Add(rowIndex, 8, exportingData.Sum(d => d.water), decimaXF);
            cell = cells.Add(rowIndex, 9, exportingData.Sum(d => d.others), decimaXF);
            cell = cells.Add(rowIndex, 10, exportingData.Sum(d => d.repair), decimaXF);
            cell = cells.Add(rowIndex, 11, exportingData.Sum(d => d.fine), dataXF);
            cell = cells.Add(rowIndex, 12, exportingData.Sum(d => d.guesthouse), decimaXF);
            //cell = cells.Add(rowIndex, 13, exportingData.Sum(d => d.guarantee), decimaXF);

            //最后一行
            cell = cells.Add(++rowIndex, 1, "制表:", dataXF);
            cell = cells.Add(rowIndex, 10, "审核:", dataXF);
            xls.Save(folderName, true);

            return "NORMAL";
        }

        #region 按宿舍区 依赖ms excel
        /*private string exportDataOfInsideByArea()
        {
            string result = "NORMAL";
            int fieldNum = 14;

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
                var areas = db.Area;
                //创建多个sheet表
                for (int i = 1; i < areas.Where(ar => ar.Charge.Count() != 0).Count(); i++)
                {
                    xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                int areaNum = 0;
                int progress = 0;
                foreach (Area area in areas)
                {
                    if (area.Charge.Count() == 0)
                        continue;
                    //设置当前sheet
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[++areaNum];
                    //关键：设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[areaNum]).Activate();
                    xlSheet.Name = area.name;
                    // 设置标题
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}年{1}月份房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)); //设置标题
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    //读出数据库的数据
                    var charges = from cha in db.Charge
                                  where cha.year_month == yearMonth
                                  && cha.area_id == area.id
                                  orderby cha.dorm_order.Length, cha.dorm_order
                                  select cha;
                    int rowNum = charges.Count();
                    // 创建缓存数据
                    object[,] objData = new object[rowNum + 2, fieldNum];
                    //设置列标题
                    objData[0, 0] = "房号";
                    objData[0, 1] = "姓名";
                    objData[0, 2] = "合计";
                    objData[0, 3] = "房租";
                    objData[0, 4] = "管理费";
                    objData[0, 5] = "电费";
                    objData[0, 6] = "水费";
                    objData[0, 7] = "其他";
                    objData[0, 8] = "维修";
                    objData[0, 9] = "扣分";
                    objData[0, 10] = "招待所";
                    objData[0, 11] = "押金";
                    objData[0, 12] = "区别";
                    objData[0, 13] = "备注";

                    //写入表中数据
                    int i = 0;
                    foreach (Charge cha in charges)
                    {
                        i++;
                        progress++;
                        objData[i, 0] = cha.dorm_number;
                        objData[i, 1] = cha.employee;
                        objData[i, 2] = cha.total;
                        objData[i, 3] = cha.rent;
                        objData[i, 4] = cha.management;
                        objData[i, 5] = cha.electricity;
                        objData[i, 6] = cha.water;
                        objData[i, 7] = cha.others;
                        objData[i, 8] = cha.repair;
                        objData[i, 9] = cha.fine;
                        objData[i, 10] = cha.guesthouse;
                        objData[i, 11] = cha.guarantee;
                        objData[i, 12] = cha.area;
                        objData[i, 13] = cha.comment;

                        bgw.ReportProgress(progress);
                    }

                    //写入最后行
                    objData[++i, 0] = "备注:";
                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                    range.Value2 = objData;
                    //提示信息
                    range = xlSheet.get_Range(xlApp.Cells[i + 3, 1], xlApp.Cells[i + 3, fieldNum]);
                    range.MergeCells = true;
                    range.Value2 = "1.核实后，如果有问题，请到后勤部核对";
                    //列宽自适应
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
                    range.EntireColumn.AutoFit();
                    //设置金额保留1位小数点，第3列到9列,11列到12列
                    range = xlSheet.get_Range(xlApp.Cells[3, 3], xlApp.Cells[rowNum + 3, 9]);
                    //range.NumberFormatLocal = "0.0";
                    range.NumberFormat = "0.0";
                    range = xlSheet.get_Range(xlApp.Cells[3, 11], xlApp.Cells[rowNum + 3, 12]);
                    range.NumberFormat = "0.0";
                }
                //保存
                xlBook.Saved = true;
                xlBook.SaveCopyAs(fileRoute);
            }
            catch (Exception)
            {
                result = "UNCLEAR";
                throw;
            }
            finally
            {
                xlApp.Quit();
                GC.Collect(); //强制回收
            }
            return result;
        }*/
        #endregion

        //myxls按宿舍区报表
        private string exportAreaDataByMyxsl()
        {
            string fileName = fileRoute.Substring(fileRoute.LastIndexOf(@"\") + 1);
            string folderName = fileRoute.Substring(0, fileRoute.Length - fileName.Length - 1);

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;

            //普通文字样式
            XF dataXF = xls.NewXF();
            dataXF.Font.FontName = "宋体";

            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 8, 12, 8, 8, 8, 8, 8, 8, 8, 8, 8,
                //8, 
                8, 16 };
            string[] colName = new string[] { "房号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所",
                //"押金",
                "区别", "备注" };

            int proc = 0;
            foreach (Area area in db.Area)
            {
                int rowIndex = 1;
                int colIndex = 1;
                org.in2bits.MyXls.Worksheet sheet = xls.Workbook.Worksheets.Add(area.name);
                Cells cells = sheet.Cells;

                //设置列宽
                ColumnInfo col;
                for (ushort i = 0; i < colWidth.Length; i++)
                {
                    col = new ColumnInfo(xls, sheet);
                    col.ColumnIndexStart = i;
                    col.ColumnIndexEnd = i;
                    col.Width = (ushort)(colWidth[i] * 256);
                    sheet.AddColumnInfo(col);
                }
                Cell cell;
                foreach (var name in colName)
                {
                    cell = cells.Add(rowIndex, colIndex++, name, dataXF);
                }

                //读出数据库的数据
                var charges = from cha in db.Charge
                              where cha.year_month == yearMonth
                              && cha.area_id == area.id
                              orderby cha.dorm_order.Length, cha.dorm_order
                              select cha;

                //"房号", "宿舍类型", "入住性别", "最多可住人数", "现有人数", "剩余可住人数", "区别"
                foreach (var cha in charges)
                {
                    colIndex = 0;
                    rowIndex++;
                    cell = cells.Add(rowIndex, ++colIndex, cha.dorm_number, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.employee, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.total, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.rent, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.management, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.electricity, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.water, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.others, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.repair, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.fine, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.guesthouse, decimaXF);
                    //cell = cells.Add(rowIndex, ++colIndex, cha.guarantee, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.area, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.comment, dataXF);

                    bgw.ReportProgress(++proc);
                }

                //最后两行
                cell = cells.Add(++rowIndex, 1, "备注：", dataXF);
                cell = cells.Add(++rowIndex, 1, "1.核实后，如果有问题，请到后勤部核对", dataXF);
            }

            xls.Save(folderName, true);

            return "NORMAL";
        }

        #region 按部门 依赖ms excel导出数据
        /*private string exportDataOfDep()
        {
            //导出之前先要将相同账号的记录合并，因为有调房，不能够分开
            List<ExportCharge> list;
            int procces = 0;
            if (aList != null)
            {
                list = aList;
                procces = progressBar1.Maximum / 2;
                bgw.ReportProgress(procces);
            }
            else
            {
                list = new List<ExportCharge>();
                string acc = "XX";
                ExportCharge charge = null;
                //var salaryDep = db.VwGetSalaryDepByAccount.ToList();
                foreach (var cha in (from ch in charges where ch.can_import == null orderby ch.Department1.number, ch.account, ch.Lodging.in_date select ch))
                {
                    procces++;
                    if (!string.IsNullOrWhiteSpace(acc) && acc.Equals(cha.account))
                    {
                        charge.total += (decimal)cha.total;
                        charge.rent += (decimal)cha.rent;
                        charge.manage += (decimal)cha.management;
                        charge.ele += (decimal)cha.electricity;
                        charge.water += (decimal)cha.water;
                        charge.other = (charge.other == null ? 0 : (decimal)charge.other) + (cha.others == null ? 0 : (decimal)cha.others);
                        charge.repair = (charge.repair == null ? 0 : (decimal)charge.repair) + (cha.repair == null ? 0 : (decimal)cha.repair);
                        charge.fine = (charge.fine == null ? 0 : (decimal)charge.fine) + (cha.fine == null ? 0 : (decimal)cha.fine);
                        charge.house = (charge.house == null ? 0 : (decimal)charge.house) + (cha.guesthouse == null ? 0 : (decimal)cha.guesthouse);
                        charge.guarantee = (charge.guarantee == null ? 0 : (decimal)charge.guarantee) + (cha.guarantee == null ? 0 : (decimal)cha.guarantee);
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;
                        //因为同一个账号是按时间排序的，所以如果是调房，则取最后时间的状态（入住或退宿），时间晚的覆盖时间早的。
                        if (cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    else
                    {
                        acc = cha.account;
                        if (charge != null)
                        {
                            list.Add(charge);
                        }
                        charge = new ExportCharge();
                        charge.name = cha.employee;
                        charge.num = (int)cha.Department1.number;
                        charge.account = cha.account;
                        charge.dep = cha.department;
                        charge.property = cha.property;
                        charge.total = (decimal)cha.total;
                        charge.rent = (decimal)cha.rent;
                        charge.manage = (decimal)cha.management;
                        charge.ele = (decimal)cha.electricity;
                        charge.water = (decimal)cha.water;
                        charge.other = cha.others;
                        charge.repair = cha.repair;
                        charge.fine = cha.fine;
                        charge.house = cha.guesthouse;
                        charge.guarantee = cha.guarantee;
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;

                        //如果cha.lodging == null ,则表示在结算当月他是在住的，往后因为自离，所以loding被删掉了。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    bgw.ReportProgress(procces);
                }
                list.Add(charge);
                aList = list;
            }
            string result = "NORMAL";
            int fieldNum = 17;
            string sheetCaption = "";//sheet 标题
            List<ExportCharge> SheetCharge = null;

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
                for (int i = 1; i <= 3; i++)
                {
                    xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                for (int k = 1; k <= 3; k++)
                {
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[k];
                    //关键：设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[k]).Activate();
                    if (k == 1)
                    {
                        xlSheet.Name = "厂内";
                        sheetCaption = string.Format("{0}年{1}月份厂内员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where ch.property.Equals("厂内")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }
                    else if (k == 2)
                    {
                        xlSheet.Name = "光电";
                        sheetCaption = string.Format("{0}年{1}月份光电员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where ch.property.Equals("光电")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }
                    else if (k == 3)
                    {
                        xlSheet.Name = "厂外";
                        sheetCaption = string.Format("{0}年{1}月份厂外员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where ch.property.Equals("厂外")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }

                    // 设置标题
                    Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = sheetCaption; //设置标题
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    // 创建缓存数据,200是预留给部门合计的
                    object[,] objData = new object[SheetCharge.Count() + 3 + 200, fieldNum];

                    //设置列标题
                    objData[0, 0] = "序号";
                    objData[0, 1] = "部门";
                    objData[0, 2] = "账号";
                    objData[0, 3] = "姓名";
                    objData[0, 4] = "合计";
                    objData[0, 5] = "房租";
                    objData[0, 6] = "管理费";
                    objData[0, 7] = "电费";
                    objData[0, 8] = "水费";
                    objData[0, 9] = "其他";
                    objData[0, 10] = "维修";
                    objData[0, 11] = "扣分";
                    objData[0, 12] = "招待所";
                    objData[0, 13] = "押金";
                    objData[0, 14] = "区别";
                    objData[0, 15] = "房号";
                    objData[0, 16] = "备注";

                    //写入表中数据
                    int i = 0;
                    string depName = "AA";
                    foreach (ExportCharge cha in SheetCharge)
                    {
                        if (!depName.Equals("AA") && !depName.Equals(cha.dep))
                        {
                            objData[++i, 1] = "部门合计:";
                            objData[i, 4] = SheetCharge.Where(s => s.dep == depName).Select(s => s.total).Sum();
                            objData[i, 5] = SheetCharge.Where(s => s.dep == depName).Select(s => s.rent).Sum();
                            objData[i, 6] = SheetCharge.Where(s => s.dep == depName).Select(s => s.manage).Sum();
                            objData[i, 7] = SheetCharge.Where(s => s.dep == depName).Select(s => s.ele).Sum();
                            objData[i, 8] = SheetCharge.Where(s => s.dep == depName).Select(s => s.water).Sum();
                            objData[i, 9] = SheetCharge.Where(s => s.dep == depName).Where(s => s.other != null).Select(s => s.other).Sum();
                            objData[i, 10] = SheetCharge.Where(s => s.dep == depName).Where(s => s.repair != null).Select(s => s.repair).Sum();
                            objData[i, 11] = SheetCharge.Where(s => s.dep == depName).Where(s => s.fine != null).Select(s => s.fine).Sum();
                            objData[i, 12] = SheetCharge.Where(s => s.dep == depName).Where(s => s.house != null).Select(s => s.house).Sum();
                            objData[i, 13] = SheetCharge.Where(s => s.dep == depName).Where(s => s.guarantee != null).Select(s => s.guarantee).Sum();
                        }
                        depName = cha.dep;
                        procces++;
                        i++;
                        objData[i, 0] = cha.num;
                        objData[i, 1] = cha.dep;
                        objData[i, 2] = cha.account;
                        objData[i, 3] = cha.name;
                        objData[i, 4] = cha.total;
                        objData[i, 5] = cha.rent;
                        objData[i, 6] = cha.manage;
                        objData[i, 7] = cha.ele;
                        objData[i, 8] = cha.water;
                        objData[i, 9] = cha.other;
                        objData[i, 10] = cha.repair;
                        objData[i, 11] = cha.fine;
                        objData[i, 12] = cha.house;
                        objData[i, 13] = cha.guarantee;
                        objData[i, 14] = cha.area;
                        objData[i, 15] = cha.dorm;
                        objData[i, 16] = cha.comment;

                        bgw.ReportProgress(procces);
                    }
                    //最后一个部门也要加上合计
                    objData[++i, 1] = "部门合计:";
                    objData[i, 4] = SheetCharge.Where(s => s.dep == depName).Select(s => s.total).Sum();
                    objData[i, 5] = SheetCharge.Where(s => s.dep == depName).Select(s => s.rent).Sum();
                    objData[i, 6] = SheetCharge.Where(s => s.dep == depName).Select(s => s.manage).Sum();
                    objData[i, 7] = SheetCharge.Where(s => s.dep == depName).Select(s => s.ele).Sum();
                    objData[i, 8] = SheetCharge.Where(s => s.dep == depName).Select(s => s.water).Sum();
                    objData[i, 9] = SheetCharge.Where(s => s.dep == depName).Where(s => s.other != null).Select(s => s.other).Sum();
                    objData[i, 10] = SheetCharge.Where(s => s.dep == depName).Where(s => s.repair != null).Select(s => s.repair).Sum();
                    objData[i, 11] = SheetCharge.Where(s => s.dep == depName).Where(s => s.fine != null).Select(s => s.fine).Sum();
                    objData[i, 12] = SheetCharge.Where(s => s.dep == depName).Where(s => s.house != null).Select(s => s.house).Sum();
                    objData[i, 13] = SheetCharge.Where(s => s.dep == depName).Where(s => s.guarantee != null).Select(s => s.guarantee).Sum();

                    //写入合计行
                    objData[++i, 1] = "合计:";
                    objData[i, 4] = SheetCharge.Select(s => s.total).Sum();
                    objData[i, 5] = SheetCharge.Select(s => s.rent).Sum();
                    objData[i, 6] = SheetCharge.Select(s => s.manage).Sum();
                    objData[i, 7] = SheetCharge.Select(s => s.ele).Sum();
                    objData[i, 8] = SheetCharge.Select(s => s.water).Sum();
                    objData[i, 9] = SheetCharge.Where(s => s.other != null).Select(s => s.other).Sum();
                    objData[i, 10] = SheetCharge.Where(s => s.repair != null).Select(s => s.repair).Sum();
                    objData[i, 11] = SheetCharge.Where(s => s.fine != null).Select(s => s.fine).Sum();
                    objData[i, 12] = SheetCharge.Where(s => s.house != null).Select(s => s.house).Sum();
                    objData[i, 13] = SheetCharge.Where(s => s.guarantee != null).Select(s => s.guarantee).Sum();

                    //写入最后一行
                    objData[++i, 1] = "制表:";
                    objData[i, 5] = "审核:";
                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[SheetCharge.Count() + 4 + 200, fieldNum]);
                    range.Value2 = objData;
                    ////写入合计
                    //range = xlSheet.get_Range(xlApp.Cells[rowNum + 3, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                    //range.Font.Bold = true;
                    //列宽自适应
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
                    range.EntireColumn.AutoFit();

                    //设置金额保留1位小数点，第5列到11列,13列到14列
                    range = xlSheet.get_Range(xlApp.Cells[3, 5], xlApp.Cells[SheetCharge.Count() + 3 + 200, 11]);
                    //range.NumberFormatLocal = "0.0";
                    range.NumberFormat = "0.0";
                    range = xlSheet.get_Range(xlApp.Cells[3, 13], xlApp.Cells[SheetCharge.Count() + 3 + 200, 14]);
                    range.NumberFormat = "0.0";
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
                SheetCharge = null;
                GC.Collect(); //强制回收
            }
            return result;
        }*/
        #endregion

        //Myxls 按部门        
        private string exportDepDataByMyxls()
        {
            #region //导出之前先要将相同账号的记录合并，因为有调房，不能够分开
            List<ExportCharge> list;
            int procces = 0;
            if (aList != null)
            {
                list = aList;
                procces = progressBar1.Maximum / 2;
                bgw.ReportProgress(procces);
            }
            else
            {
                list = new List<ExportCharge>();
                string acc = "XX";
                ExportCharge charge = null;
                //var salaryDep = db.VwGetSalaryDepByAccount.ToList();
                foreach (var cha in (from ch in charges where ch.can_import == null orderby ch.Department1.number, ch.account, ch.Lodging.in_date select ch))
                {
                    if (!string.IsNullOrWhiteSpace(acc) && acc.Equals(cha.account))
                    {
                        charge.total += (decimal)cha.total;
                        charge.rent += (decimal)cha.rent;
                        charge.manage += (decimal)cha.management;
                        charge.ele += (decimal)cha.electricity;
                        charge.water += (decimal)cha.water;
                        charge.other = (charge.other == null ? 0 : (decimal)charge.other) + (cha.others == null ? 0 : (decimal)cha.others);
                        charge.repair = (charge.repair == null ? 0 : (decimal)charge.repair) + (cha.repair == null ? 0 : (decimal)cha.repair);
                        charge.fine = (charge.fine == null ? 0 : (decimal)charge.fine) + (cha.fine == null ? 0 : (decimal)cha.fine);
                        charge.house = (charge.house == null ? 0 : (decimal)charge.house) + (cha.guesthouse == null ? 0 : (decimal)cha.guesthouse);
                        charge.guarantee = (charge.guarantee == null ? 0 : (decimal)charge.guarantee) + (cha.guarantee == null ? 0 : (decimal)cha.guarantee);
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;

                        //因为同一个账号是按时间排序的，所以如果是调房，则取最后时间的状态（入住或退宿），时间晚的覆盖时间早的。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    else
                    {
                        acc = cha.account;
                        if (charge != null)
                        {
                            list.Add(charge);
                        }
                        charge = new ExportCharge();
                        charge.name = cha.employee;
                        charge.num = (int)cha.Department1.number;
                        charge.account = cha.account;
                        charge.dep = cha.department;
                        charge.property = cha.property;
                        charge.total = (decimal)cha.total;
                        charge.rent = (decimal)cha.rent;
                        charge.manage = (decimal)cha.management;
                        charge.ele = (decimal)cha.electricity;
                        charge.water = (decimal)cha.water;
                        charge.other = cha.others;
                        charge.repair = cha.repair;
                        charge.fine = cha.fine;
                        charge.house = cha.guesthouse;
                        charge.guarantee = cha.guarantee;
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;

                        //如果cha.lodging == null ,则表示在结算当月他是在住的，往后因为自离，所以loding被删掉了。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    bgw.ReportProgress(++procces);
                }
                list.Add(charge);
                aList = list;
            }
            #endregion

            List<ExportCharge> SheetCharge = null;
            string fileName = fileRoute.Substring(fileRoute.LastIndexOf(@"\") + 1);
            string folderName = fileRoute.Substring(0, fileRoute.Length - fileName.Length - 1);

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;

            //普通文字样式
            XF dataXF = xls.NewXF();
            dataXF.Font.FontName = "宋体";

            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 6, 24, 6, 8, 10, 10, 8, 8, 8, 8, 8, 8, 8, 
                //8, 
                8, 8, 16 };
            string[] colName = new string[] { "序号", "部门", "账号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所",
                //"押金",
                "区别", "房号", "备注" };

            string[] sheetNames = list.Select(l => l.property).Distinct().ToArray();

            foreach(var sheetName in sheetNames)
            {
                Cells cells;
                org.in2bits.MyXls.Worksheet sheet;
                sheet = xls.Workbook.Worksheets.Add(sheetName);
                cells = sheet.Cells;
                SheetCharge = (from ch in list
                                where ch.property.Equals(sheetName)
                                orderby ch.num, ch.account
                                select ch).ToList();                

                Cell cell;
                int rowIndex = 1;
                int colIndex = 1;

                //设置列宽
                ColumnInfo col;
                for (ushort i = 0; i < colWidth.Length; i++)
                {
                    col = new ColumnInfo(xls, sheet);
                    col.ColumnIndexStart = i;
                    col.ColumnIndexEnd = i;
                    col.Width = (ushort)(colWidth[i] * 256);
                    sheet.AddColumnInfo(col);
                }

                foreach (var name in colName)
                {
                    cell = cells.Add(rowIndex, colIndex++, name, dataXF);
                }

                //写入表中数据                    

                string depName = "AA";
                foreach (ExportCharge cha in SheetCharge)
                {
                    if (!depName.Equals("AA") && !depName.Equals(cha.dep))
                    {
                        cell = cells.Add(++rowIndex, 2, "部门合计:", dataXF);
                        cell = cells.Add(rowIndex, 5, SheetCharge.Where(s => s.dep == depName).Select(s => s.total).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 6, SheetCharge.Where(s => s.dep == depName).Select(s => s.rent).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 7, SheetCharge.Where(s => s.dep == depName).Select(s => s.manage).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 8, SheetCharge.Where(s => s.dep == depName).Select(s => s.ele).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 9, SheetCharge.Where(s => s.dep == depName).Select(s => s.water).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 10, SheetCharge.Where(s => s.dep == depName).Where(s => s.other != null).Select(s => s.other).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 11, SheetCharge.Where(s => s.dep == depName).Where(s => s.repair != null).Select(s => s.repair).Sum(), decimaXF);
                        cell = cells.Add(rowIndex, 12, SheetCharge.Where(s => s.dep == depName).Where(s => s.fine != null).Select(s => s.fine).Sum(), dataXF);
                        cell = cells.Add(rowIndex, 13, SheetCharge.Where(s => s.dep == depName).Where(s => s.house != null).Select(s => s.house).Sum(), decimaXF);
                        //cell = cells.Add(rowIndex, 14, SheetCharge.Where(s => s.dep == depName).Where(s => s.guarantee != null).Select(s => s.guarantee).Sum(), decimaXF);
                    }
                    depName = cha.dep;
                    colIndex = 0;
                    rowIndex++;
                    //"序号", "部门", "账号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别","房号", "备注"
                    cell = cells.Add(rowIndex, ++colIndex, cha.num, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.dep, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.account, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.name, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.total, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.rent, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.manage, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.ele, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.water, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.other, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.repair, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.fine, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.house, decimaXF);
                    //cell = cells.Add(rowIndex, ++colIndex, cha.guarantee, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.area, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.dorm, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.comment, dataXF);

                    bgw.ReportProgress(++procces);
                }
                //最后一个部门也要加上合计
                cell = cells.Add(++rowIndex, 2, "部门合计:", dataXF);
                cell = cells.Add(rowIndex, 5, SheetCharge.Where(s => s.dep == depName).Select(s => s.total).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 6, SheetCharge.Where(s => s.dep == depName).Select(s => s.rent).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 7, SheetCharge.Where(s => s.dep == depName).Select(s => s.manage).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 8, SheetCharge.Where(s => s.dep == depName).Select(s => s.ele).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 9, SheetCharge.Where(s => s.dep == depName).Select(s => s.water).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 10, SheetCharge.Where(s => s.dep == depName).Where(s => s.other != null).Select(s => s.other).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 11, SheetCharge.Where(s => s.dep == depName).Where(s => s.repair != null).Select(s => s.repair).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 12, SheetCharge.Where(s => s.dep == depName).Where(s => s.fine != null).Select(s => s.fine).Sum(), dataXF);
                cell = cells.Add(rowIndex, 13, SheetCharge.Where(s => s.dep == depName).Where(s => s.house != null).Select(s => s.house).Sum(), decimaXF);
                //cell = cells.Add(rowIndex, 14, SheetCharge.Where(s => s.dep == depName).Where(s => s.guarantee != null).Select(s => s.guarantee).Sum(), decimaXF);

                //写入合计行
                cell = cells.Add(++rowIndex, 2, "合计:", dataXF);
                cell = cells.Add(rowIndex, 5, SheetCharge.Select(s => s.total).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 6, SheetCharge.Select(s => s.rent).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 7, SheetCharge.Select(s => s.manage).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 8, SheetCharge.Select(s => s.ele).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 9, SheetCharge.Select(s => s.water).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 10, SheetCharge.Where(s => s.other != null).Select(s => s.other).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 11, SheetCharge.Where(s => s.repair != null).Select(s => s.repair).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 12, SheetCharge.Where(s => s.fine != null).Select(s => s.fine).Sum(), dataXF);
                cell = cells.Add(rowIndex, 13, SheetCharge.Where(s => s.house != null).Select(s => s.house).Sum(), decimaXF);
                //cell = cells.Add(rowIndex, 14, SheetCharge.Where(s => s.guarantee != null).Select(s => s.guarantee).Sum(), decimaXF);


                //写入最后一行
                cell = cells.Add(++rowIndex, 2, "制表:", dataXF);
                cell = cells.Add(rowIndex, 6, "审核:", dataXF);

            }

            xls.Save(folderName, true);
            return "NORMAL";
        }

        #region 按在住退宿 依赖ms excel
        /*private string exportDataOfInAndOut()
        {
            //导出之前先要将相同账号的记录合并，因为有调房，不能够分开
            List<ExportCharge> list;
            int procces = 0;
            if (aList != null)
            {
                list = aList;
                procces = progressBar1.Maximum / 2;
                bgw.ReportProgress(procces);
            }
            else
            {
                list = new List<ExportCharge>();
                string acc = "XX";
                ExportCharge charge = null;
                foreach (var cha in (from ch in charges where ch.can_import == null orderby ch.Department1.number, ch.account, ch.Lodging.in_date select ch))
                {
                    procces++;
                    if (!string.IsNullOrWhiteSpace(acc) && acc.Equals(cha.account))
                    {
                        charge.total += (decimal)cha.total;
                        charge.rent += (decimal)cha.rent;
                        charge.manage += (decimal)cha.management;
                        charge.ele += (decimal)cha.electricity;
                        charge.water += (decimal)cha.water;
                        charge.other = (charge.other == null ? 0 : (decimal)charge.other) + (cha.others == null ? 0 : (decimal)cha.others);
                        charge.repair = (charge.repair == null ? 0 : (decimal)charge.repair) + (cha.repair == null ? 0 : (decimal)cha.repair);
                        charge.fine = (charge.fine == null ? 0 : (decimal)charge.fine) + (cha.fine == null ? 0 : (decimal)cha.fine);
                        charge.house = (charge.house == null ? 0 : (decimal)charge.house) + (cha.guesthouse == null ? 0 : (decimal)cha.guesthouse);
                        charge.guarantee = (charge.guarantee == null ? 0 : (decimal)charge.guarantee) + (cha.guarantee == null ? 0 : (decimal)cha.guarantee);
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;
                        //因为同一个账号是按时间排序的，所以如果是调房，则取最后时间的状态（入住或退宿），时间晚的覆盖时间早的。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    else
                    {
                        acc = cha.account;
                        if (charge != null)
                        {
                            list.Add(charge);
                        }
                        charge = new ExportCharge();
                        charge.name = cha.employee;
                        charge.num = (int)cha.Department1.number;
                        charge.account = cha.account;
                        charge.dep = cha.department;
                        charge.property = cha.property;
                        charge.total = (decimal)cha.total;
                        charge.rent = (decimal)cha.rent;
                        charge.manage = (decimal)cha.management;
                        charge.ele = (decimal)cha.electricity;
                        charge.water = (decimal)cha.water;
                        charge.other = cha.others;
                        charge.repair = cha.repair;
                        charge.fine = cha.fine;
                        charge.house = cha.guesthouse;
                        charge.guarantee = cha.guarantee;
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;

                        //如果cha.lodging == null ,则表示在结算当月他是在住的，往后因为自离，所以loding被删掉了。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    bgw.ReportProgress(procces);
                }
                list.Add(charge);
                aList = list;
            }
            string result = "NORMAL";
            int fieldNum = 17;
            string sheetCaption = "";//sheet 标题
            List<ExportCharge> SheetCharge = null;

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
                for (int i = 1; i <= 4; i++)
                {
                    xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                for (int k = 1; k <= 4; k++)
                {
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[k];
                    //关键：设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[k]).Activate();
                    if (k == 1)
                    {
                        xlSheet.Name = "厂内在住";
                        sheetCaption = string.Format("{0}年{1}月份厂内在住员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where ch.living
                                       && ch.property.Equals("厂内")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }
                    else if (k == 2)
                    {
                        xlSheet.Name = "厂内退宿";
                        sheetCaption = string.Format("{0}年{1}月份厂内退宿员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where !ch.living
                                       && ch.property.Equals("厂内")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }
                    else if (k == 3)
                    {
                        xlSheet.Name = "光电在住";
                        sheetCaption = string.Format("{0}年{1}月份光电入住员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where ch.living
                                       && ch.property.Equals("光电")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }
                    else if (k == 4)
                    {
                        xlSheet.Name = "光电退宿";
                        sheetCaption = string.Format("{0}年{1}月份光电退宿员工房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
                        SheetCharge = (from ch in list
                                       where !ch.living
                                       && ch.property.Equals("光电")
                                       orderby ch.num, ch.account
                                       select ch).ToList();
                    }


                    // 设置标题
                    Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = sheetCaption; //设置标题
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    object[,] objData = new object[SheetCharge.Count() + 3, fieldNum];

                    //设置列标题
                    objData[0, 0] = "序号";
                    objData[0, 1] = "部门";
                    //objData[0, 2] = "会计部门";
                    objData[0, 2] = "账号";
                    objData[0, 3] = "姓名";
                    objData[0, 4] = "合计";
                    objData[0, 5] = "房租";
                    objData[0, 6] = "管理费";
                    objData[0, 7] = "电费";
                    objData[0, 8] = "水费";
                    objData[0, 9] = "其他";
                    objData[0, 10] = "维修";
                    objData[0, 11] = "扣分";
                    objData[0, 12] = "招待所";
                    objData[0, 13] = "押金";
                    objData[0, 14] = "区别";
                    objData[0, 15] = "房号";
                    objData[0, 16] = "备注";

                    //写入表中数据
                    int i = 0;
                    foreach (ExportCharge cha in SheetCharge)
                    {
                        procces++;
                        i++;
                        objData[i, 0] = cha.num;
                        objData[i, 1] = cha.dep;
                        objData[i, 2] = cha.account;
                        objData[i, 3] = cha.name;
                        objData[i, 4] = cha.total;
                        objData[i, 5] = cha.rent;
                        objData[i, 6] = cha.manage;
                        objData[i, 7] = cha.ele;
                        objData[i, 8] = cha.water;
                        objData[i, 9] = cha.other;
                        objData[i, 10] = cha.repair;
                        objData[i, 11] = cha.fine;
                        objData[i, 12] = cha.house;
                        objData[i, 13] = cha.guarantee;
                        objData[i, 14] = cha.area;
                        objData[i, 15] = cha.dorm;
                        objData[i, 16] = cha.comment;

                        bgw.ReportProgress(procces);
                    }

                    //写入合计行
                    objData[++i, 1] = "合计:";
                    objData[i, 4] = SheetCharge.Select(s => s.total).Sum();
                    objData[i, 5] = SheetCharge.Select(s => s.rent).Sum();
                    objData[i, 6] = SheetCharge.Select(s => s.manage).Sum();
                    objData[i, 7] = SheetCharge.Select(s => s.ele).Sum();
                    objData[i, 8] = SheetCharge.Select(s => s.water).Sum();
                    objData[i, 9] = SheetCharge.Where(s => s.other != null).Select(s => s.other).Sum();
                    objData[i, 10] = SheetCharge.Where(s => s.repair != null).Select(s => s.repair).Sum();
                    objData[i, 11] = SheetCharge.Where(s => s.fine != null).Select(s => s.fine).Sum();
                    objData[i, 12] = SheetCharge.Where(s => s.house != null).Select(s => s.house).Sum();
                    objData[i, 13] = SheetCharge.Where(s => s.guarantee != null).Select(s => s.guarantee).Sum();

                    //写入最后一行
                    objData[++i, 1] = "制表:";
                    objData[i, 5] = "审核:";
                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[SheetCharge.Count() + 4, fieldNum]);
                    range.Value2 = objData;
                    ////写入合计
                    //range = xlSheet.get_Range(xlApp.Cells[rowNum + 3, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                    //range.Font.Bold = true;
                    //列宽自适应
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
                    range.EntireColumn.AutoFit();

                    //设置金额保留1位小数点，第5列到11列,13列到14列
                    range = xlSheet.get_Range(xlApp.Cells[3, 5], xlApp.Cells[SheetCharge.Count() + 3, 11]);
                    //range.NumberFormatLocal = "0.0";
                    range.NumberFormat = "0.0";
                    range = xlSheet.get_Range(xlApp.Cells[3, 13], xlApp.Cells[SheetCharge.Count() + 3, 14]);
                    range.NumberFormat = "0.0";
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
                SheetCharge = null;
                GC.Collect(); //强制回收
            }
            return result;
        }*/
        #endregion

        //MyXls 按在住退宿
        private string exportInAndOutDataByMyxls()
        {
            #region //导出之前先要将相同账号的记录合并，因为有调房，不能够分开
            List<ExportCharge> list;
            int procces = 0;
            if (aList != null)
            {
                list = aList;
                procces = progressBar1.Maximum / 2;
                bgw.ReportProgress(procces);
            }
            else
            {
                list = new List<ExportCharge>();
                string acc = "XX";
                ExportCharge charge = null;
                //var salaryDep = db.VwGetSalaryDepByAccount.ToList();
                foreach (var cha in (from ch in charges where ch.can_import == null orderby ch.Department1.number, ch.account, ch.Lodging.in_date select ch))
                {
                    if (!string.IsNullOrWhiteSpace(acc) && acc.Equals(cha.account))
                    {
                        charge.total += (decimal)cha.total;
                        charge.rent += (decimal)cha.rent;
                        charge.manage += (decimal)cha.management;
                        charge.ele += (decimal)cha.electricity;
                        charge.water += (decimal)cha.water;
                        charge.other = (charge.other == null ? 0 : (decimal)charge.other) + (cha.others == null ? 0 : (decimal)cha.others);
                        charge.repair = (charge.repair == null ? 0 : (decimal)charge.repair) + (cha.repair == null ? 0 : (decimal)cha.repair);
                        charge.fine = (charge.fine == null ? 0 : (decimal)charge.fine) + (cha.fine == null ? 0 : (decimal)cha.fine);
                        charge.house = (charge.house == null ? 0 : (decimal)charge.house) + (cha.guesthouse == null ? 0 : (decimal)cha.guesthouse);
                        charge.guarantee = (charge.guarantee == null ? 0 : (decimal)charge.guarantee) + (cha.guarantee == null ? 0 : (decimal)cha.guarantee);
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;

                        //因为同一个账号是按时间排序的，所以如果是调房，则取最后时间的状态（入住或退宿），时间晚的覆盖时间早的。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    else
                    {
                        acc = cha.account;
                        if (charge != null)
                        {
                            list.Add(charge);
                        }
                        charge = new ExportCharge();
                        charge.name = cha.employee;
                        charge.num = (int)cha.Department1.number;
                        charge.account = cha.account;
                        charge.dep = cha.department;
                        charge.property = cha.property;
                        charge.total = (decimal)cha.total;
                        charge.rent = (decimal)cha.rent;
                        charge.manage = (decimal)cha.management;
                        charge.ele = (decimal)cha.electricity;
                        charge.water = (decimal)cha.water;
                        charge.other = cha.others;
                        charge.repair = cha.repair;
                        charge.fine = cha.fine;
                        charge.house = cha.guesthouse;
                        charge.guarantee = cha.guarantee;
                        charge.area = Utils.ConvertAreaName(cha.area);
                        charge.dorm = cha.dorm_number;
                        charge.classify = cha.classify_property;
                        charge.comment += " " + cha.comment;

                        //如果cha.lodging == null ,则表示在结算当月他是在住的，往后因为自离，所以loding被删掉了。
                        if (cha.Lodging == null || cha.Lodging.out_date == null || cha.Lodging.out_date > lastDayOfMonth)
                        {
                            charge.living = true;
                        }
                        else
                        {
                            charge.living = false;
                        }
                    }
                    bgw.ReportProgress(++procces);
                }
                list.Add(charge);
                aList = list;
            }
            #endregion

            List<ExportCharge> SheetCharge = null;
            string fileName = fileRoute.Substring(fileRoute.LastIndexOf(@"\") + 1);
            string folderName = fileRoute.Substring(0, fileRoute.Length - fileName.Length - 1);

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;

            //普通文字样式
            XF dataXF = xls.NewXF();
            dataXF.Font.FontName = "宋体";

            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 6, 24, 6, 8, 10, 10, 8, 8, 8, 8, 8, 8, 8,
                //10, 
                8, 8, 16 };
            string[] colName = new string[] { "序号", "部门", "账号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", 
                //"押金", 
                "区别", "房号", "备注" };

            for (int k = 1; k <= 4; k++)
            {               
                org.in2bits.MyXls.Worksheet sheet;

                if (k == 1)
                {
                    sheet = xls.Workbook.Worksheets.Add("半导体在住");                    
                    SheetCharge = (from ch in list
                                   where ch.living
                                   && ch.property.Equals("半导体")
                                   orderby ch.num, ch.account
                                   select ch).ToList();
                }
                else if (k == 2)
                {
                    sheet = xls.Workbook.Worksheets.Add("半导体退宿");                    
                    SheetCharge = (from ch in list
                                   where !ch.living
                                   && ch.property.Equals("半导体")
                                   orderby ch.num, ch.account
                                   select ch).ToList();
                }
                else if (k == 3)
                {
                    sheet = xls.Workbook.Worksheets.Add("光电在住");                    
                    SheetCharge = (from ch in list
                                   where ch.living
                                   && ch.property.Equals("光电")
                                   orderby ch.num, ch.account
                                   select ch).ToList();
                }
                else {
                    sheet = xls.Workbook.Worksheets.Add("光电退宿");                    
                    SheetCharge = (from ch in list
                                   where !ch.living
                                   && ch.property.Equals("光电")
                                   orderby ch.num, ch.account
                                   select ch).ToList();
                }

                Cells cells = sheet.Cells;
                Cell cell;
                int rowIndex = 1;
                int colIndex = 1;

                //设置列宽
                ColumnInfo col;
                for (ushort i = 0; i < colWidth.Length; i++)
                {
                    col = new ColumnInfo(xls, sheet);
                    col.ColumnIndexStart = i;
                    col.ColumnIndexEnd = i;
                    col.Width = (ushort)(colWidth[i] * 256);
                    sheet.AddColumnInfo(col);
                }

                foreach (var name in colName)
                {
                    cell = cells.Add(rowIndex, colIndex++, name, dataXF);
                }

                //写入表中数据
                
                foreach (ExportCharge cha in SheetCharge)
                {                    
                    colIndex = 0;
                    rowIndex++;
                    //"序号", "部门", "账号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别","房号", "备注"
                    cell = cells.Add(rowIndex, ++colIndex, cha.num, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.dep, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.account, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.name, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.total, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.rent, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.manage, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.ele, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.water, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.other, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.repair, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.fine, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.house, decimaXF);
                    //cell = cells.Add(rowIndex, ++colIndex, cha.guarantee, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.area, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.dorm, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.comment, dataXF);

                    bgw.ReportProgress(++procces);
                }                

                //写入合计行
                cell = cells.Add(++rowIndex, 2, "合计:", dataXF);
                cell = cells.Add(rowIndex, 5, SheetCharge.Select(s => s.total).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 6, SheetCharge.Select(s => s.rent).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 7, SheetCharge.Select(s => s.manage).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 8, SheetCharge.Select(s => s.ele).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 9, SheetCharge.Select(s => s.water).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 10, SheetCharge.Where(s => s.other != null).Select(s => s.other).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 11, SheetCharge.Where(s => s.repair != null).Select(s => s.repair).Sum(), decimaXF);
                cell = cells.Add(rowIndex, 12, SheetCharge.Where(s => s.fine != null).Select(s => s.fine).Sum(), dataXF);
                cell = cells.Add(rowIndex, 13, SheetCharge.Where(s => s.house != null).Select(s => s.house).Sum(), decimaXF);
                //cell = cells.Add(rowIndex, 14, SheetCharge.Where(s => s.guarantee != null).Select(s => s.guarantee).Sum(), decimaXF);


                //写入最后一行
                cell = cells.Add(++rowIndex, 2, "制表:", dataXF);
                cell = cells.Add(rowIndex, 6, "审核:", dataXF);

            }

            xls.Save(folderName, true);
            return "NORMAL";
        }

        #region 调房_依赖ms excel
        /*private string exportChangeDorm()
        {
            string result = "NORMAL";
            int fieldNum = 14;

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
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                // 设置标题
                Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                range.MergeCells = true;
                xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}年{1}月份调房员工原房水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)); //设置标题
                xlApp.ActiveCell.Font.Size = 20;
                xlApp.ActiveCell.Font.Bold = true;
                xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                //读出数据库的数据
                var charges = (from cha in db.VwChangeReport
                               where cha.year_month == yearMonth
                               orderby cha.area_id, cha.dorm_order
                               select cha).ToList();
                int rowNum = charges.Count();
                // 创建缓存数据
                object[,] objData = new object[rowNum + 2, fieldNum];
                //设置列标题
                objData[0, 0] = "房号";
                objData[0, 1] = "姓名";
                objData[0, 2] = "合计";
                objData[0, 3] = "房租";
                objData[0, 4] = "管理费";
                objData[0, 5] = "电费";
                objData[0, 6] = "水费";
                objData[0, 7] = "其他";
                objData[0, 8] = "维修";
                objData[0, 9] = "扣分";
                objData[0, 10] = "招待所";
                objData[0, 11] = "押金";
                objData[0, 12] = "区别";
                objData[0, 13] = "备注";

                //写入表中数据
                int i = 0;
                int progress = 0;
                foreach (var cha in charges)
                {
                    i++;
                    progress++;
                    objData[i, 0] = cha.dorm_number;
                    objData[i, 1] = cha.employee;
                    objData[i, 2] = cha.total;
                    objData[i, 3] = cha.rent;
                    objData[i, 4] = cha.management;
                    objData[i, 5] = cha.electricity;
                    objData[i, 6] = cha.water;
                    objData[i, 7] = cha.others;
                    objData[i, 8] = cha.repair;
                    objData[i, 9] = cha.fine;
                    objData[i, 10] = cha.guesthouse;
                    objData[i, 11] = cha.guarantee;
                    objData[i, 12] = cha.area;
                    objData[i, 13] = cha.comment;

                    bgw.ReportProgress(progress);
                }
                //写入最后行
                objData[++i, 0] = "备注:";
                // 写入Excel
                range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                range.Value2 = objData;
                //提示信息
                range = xlSheet.get_Range(xlApp.Cells[i + 3, 1], xlApp.Cells[i + 3, fieldNum]);
                range.MergeCells = true;
                range.Value2 = "1.核实后，如果有问题，请到后勤部核对";
                //列宽自适应
                range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
                range.EntireColumn.AutoFit();
                //设置金额保留1位小数点，第3列到9列,11列到12列
                range = xlSheet.get_Range(xlApp.Cells[3, 3], xlApp.Cells[charges.Count() + 3, 9]);
                //range.NumberFormatLocal = "0.0";
                range.NumberFormat = "0.0";
                range = xlSheet.get_Range(xlApp.Cells[3, 11], xlApp.Cells[charges.Count() + 3, 12]);
                range.NumberFormat = "0.0";
                //保存
                xlBook.Saved = true;
                xlBook.SaveCopyAs(fileRoute);
            }
            catch (Exception)
            {
                result = "UNCLEAR";
                throw;
            }
            finally
            {
                xlApp.Quit();
                GC.Collect(); //强制回收
            }
            return result;
        }*/
        #endregion

        //Myxls调房
        private string exportChangeDormByMyxls()
        {
            string fileName = fileRoute.Substring(fileRoute.LastIndexOf(@"\") + 1);
            string folderName = fileRoute.Substring(0, fileRoute.Length - fileName.Length - 1);

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;

            //标题样式
            XF titleXF = xls.NewXF();
            titleXF.HorizontalAlignment = HorizontalAlignments.Centered;
            titleXF.Font.Bold = true;
            titleXF.Font.Height = 18 * 20;// 字体大小（字体大小是以 1/20 point 为单位的）
            titleXF.Font.FontName = "宋体";

            //普通文字样式
            XF dataXF = xls.NewXF();
            dataXF.Font.FontName = "宋体";

            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 8, 12, 8, 8, 8, 8, 8, 8, 8, 8, 8, 
                //8, 
                8, 16 };
            string[] colName = new string[] { "房号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", 
                //"押金", 
                "区别", "备注" };

            int rowIndex = 1;
            int colIndex = 1;
            org.in2bits.MyXls.Worksheet sheet = xls.Workbook.Worksheets.Add("原房费用");
            Cells cells = sheet.Cells;

            //设置列宽
            ColumnInfo col;
            for (ushort i = 0; i < colWidth.Length; i++)
            {
                col = new ColumnInfo(xls, sheet);
                col.ColumnIndexStart = i;
                col.ColumnIndexEnd = i;
                col.Width = (ushort)(colWidth[i] * 256);
                sheet.AddColumnInfo(col);
            }

            //标题
            MergeArea ma = new MergeArea(rowIndex, rowIndex, colIndex, colName.Length);
            sheet.AddMergeArea(ma);
            Cell cell = cells.Add(rowIndex, colIndex, string.Format("{0}年{1}月份调房员工原房水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)), titleXF);
            for (int i = colIndex + 1; i <= colName.Length; i++)
            {
                cell = cells.Add(rowIndex, i, "", titleXF);
            }
            rowIndex++;

            foreach (var name in colName)
            {
                cell = cells.Add(rowIndex, colIndex++, name, dataXF);
            }

            var charges = (from cha in db.VwChangeReport
                           where cha.year_month == yearMonth
                           //orderby cha.area_id, cha.dorm_order
                           select cha).ToList();
            
            int proc = 0;
            //"房号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别", "备注"
            foreach (var data in charges.OrderBy(c=>c.area_id).OrderBy(c=>c.dorm_order))
            {
                colIndex = 0;
                rowIndex++;
                cell = cells.Add(rowIndex, ++colIndex, data.dorm_number, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.employee, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.total, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.rent, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.management, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.electricity, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.water, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.others, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.repair, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.fine, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.guesthouse, decimaXF);
                //cell = cells.Add(rowIndex, ++colIndex, data.guarantee, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.area, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.comment, dataXF);
                bgw.ReportProgress(++proc);
            }

            //最后2行
            cell = cells.Add(++rowIndex, 1, "制表:", dataXF);
            cell = cells.Add(++rowIndex, 1, "1.核实后，如果有问题，请到后勤部核对", dataXF);
            xls.Save(folderName, true);

            return "NORMAL";
        }

        private void FrmExportByInside_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 20;
        }

    }
}
