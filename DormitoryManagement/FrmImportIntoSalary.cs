using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using org.in2bits.MyXls;

namespace DormitoryManagement
{
    public partial class FrmImportIntoSalary : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string yearMonth;
        string fileRoute;
        public FrmImportIntoSalary()
        {
            InitializeComponent();
        }

        private void FrmImportIntoSalary_Load(object sender, EventArgs e)
        {
            //功能说明
            rtb.AppendText("关联工资系统功能说明:\n");
            rtb.AppendText("    一、导入工资系统说明:\n");
            rtb.AppendText("        （1）、该功能是将已结算完的宿舍费用自动导入工资系统中间表，节省手工录入的时间以及避免出错。\n");
            rtb.AppendText("        （2）、只有厂内和光电部门的人员才能自动导入，厂外（包括巴黎酒店）的人员因为没有工资账号，不会导入工资系统。\n");
            rtb.AppendText("        （3）、只有当月结算完成，并且人工核对完没问题之后，才可以导进工资系统。\n");
            rtb.AppendText("        （4）、每月只能导入工资系统一次，所以请谨慎操作！\n");
            rtb.AppendText("    二、导出失败人员信息说明:\n");
            rtb.AppendText("        （1）、该功能是将导入到工资系统失败的人员汇总导出EXCEL。\n");
            rtb.AppendText("        （2）、导入失败的原因可能是该员工自动离职，或者是其它原因。\n");
            rtb.AppendText("        （3）、请对导入失败的员工另行手工处理。\n");

            iniStatus();

        }

        //刷新UI
        private void iniStatus()
        {
            //导入系统月份
            var importLogs = db.ImportLog.OrderByDescending(i => i.id);
            if (importLogs.Count() == 0)
            {
                lbYearMonth.Text = db.VerifyOrder.OrderByDescending(v => v.id).First().year_and_month;
                button1.Enabled = true;
            }
            else
            {
                var nextYearMonth = YearAndMonth.next(importLogs.First().year_month);
                if (db.VerifyOrder.Where(v => v.year_and_month == nextYearMonth).Count() > 0)
                {
                    lbYearMonth.Text = nextYearMonth;
                    button1.Enabled = true;
                }
                else
                {
                    lbYearMonth.Text = nextYearMonth + "月份还没有结算，不能操作";
                    button1.Enabled = false;
                }
            }

            //导出到excel的月份
            var months = from il in db.ImportLog
                         orderby il.id descending
                         select il.year_month;
            if (months.Count() > 0)
            {
                cbExportMonth.DataSource = months;
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        //导入数据到工资系统中间表按钮
        private void button1_Click(object sender, EventArgs e)
        {
            yearMonth = lbYearMonth.Text;
            if (db.VerifyOrder.Where(v => v.year_and_month == yearMonth).Count() < 1)
            {
                MessageBox.Show("该月份还未进行结算，导入失败");
                return;
            }
            if (db.ImportLog.Where(i => i.year_month == yearMonth).Count() > 0)
            {
                MessageBox.Show("该月份的数据已经导入，不能重复操作！");
                return;
            }
            progressBar1.Style = ProgressBarStyle.Marquee;
            button1.Enabled = false;
            MyUtil.WriteEventLog("导入工资系统", "", "", "开始导入到工资系统");
            backgroundWorker1.RunWorkerAsync();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                
                db.import_into_salary(yearMonth, LoginUser.username);
                e.Result = "成功导入数据到工资系统";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
                MyUtil.WriteEventLog("导入工资系统", "", "", "导入失败",false);
            }
            else
            {
                MessageBox.Show(e.Result.ToString());
                MyUtil.WriteEventLog("导入工资系统", "", "", "导入成功！");
            }
            //刷新UI
            iniStatus();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.Result = exportToExcel();
            e.Result = exportToExcelUseMyxls();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
                MyUtil.WriteEventLog("导出失败人员报表", "", "", "导出失败:"+e.Error,false);
            }

            switch (e.Result.ToString())
            {                
                case "NORMAL":
                    MessageBox.Show("数据已成功导出");
                    MyUtil.WriteEventLog("导出失败人员报表", "", "", "成功导出！");
                    break;
                case "UNCLEAR":
                    MessageBox.Show("操作失败……");
                    MyUtil.WriteEventLog("导出失败人员报表", "", "", "操作失败，原因不明", false);
                    break;
            }
            progressBar2.Style = ProgressBarStyle.Blocks;
            iniStatus();
        }

        #region 依赖excel客户端生成
        /*private string exportToExcel()
        {
            string result = "NORMAL";
            int fieldNum = 6;

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
                //创建多个sheet表
                xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //设置当前sheet
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                //关键：如果有多张sheet，必须设置为活动sheet
                ((_Worksheet)xlApp.Worksheets[1]).Activate();
                xlSheet.Name = "人员列表";
                // 设置标题
                Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                range.MergeCells = true;
                xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}年{1}月份导入系统失败人员", yearMonth.Substring(0,4),yearMonth.Substring(4,2)); //设置标题
                xlApp.ActiveCell.Font.Size = 20;
                xlApp.ActiveCell.Font.Bold = true;
                xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                //读出数据库的数据
                var datas = db.VwFailImport.Where(v => v.year_month == yearMonth).OrderBy(v=>v.department);
                int rowNum = datas.Count();
                // 创建缓存数据
                object[,] objData = new object[rowNum + 1, fieldNum];
                //设置列标题
                objData[0, 0] = "部门";
                objData[0, 1] = "账号";
                objData[0, 2] = "姓名";
                objData[0, 3] = "宿舍号";
                objData[0, 4] = "合计";
                objData[0, 5] = "在职状态";

                //写入表中数据
                int i = 0;
                foreach (var d in datas)
                {
                    i++;
                    objData[i, 0] = d.department;
                    objData[i, 1] = d.account;
                    objData[i, 2] = d.employee;
                    objData[i, 3] = d.dorm_number;
                    objData[i, 4] = d.total;
                    objData[i, 5] = d.status;
                }

                // 写入Excel
                range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 2, fieldNum]);
                range.Value2 = objData;
                //列宽自适应
                range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
                range.EntireColumn.AutoFit();

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
                GC.Collect(); //强制回收
            }
            return result;
        }*/
        #endregion
        private string exportToExcelUseMyxls() {
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

            ushort[] colWidth = new ushort[] { 24, 8, 12, 8, 8, 12 };
            string[] colName = new string[] { "部门", "账号", "姓名", "宿舍号", "合计", "在职状态" };

            int rowIndex = 1;
            int colIndex = 1;
            org.in2bits.MyXls.Worksheet sheet = xls.Workbook.Worksheets.Add("人员列表");
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

            var datas = db.VwFailImport.Where(v => v.year_month == yearMonth).OrderBy(v => v.department);

            //"部门", "账号", "姓名", "宿舍号", "合计", "在职状态"
            foreach (var data in datas)
            {
                colIndex = 0;
                rowIndex++;
                cell = cells.Add(rowIndex, ++colIndex, data.department, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.account, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.employee, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.dorm_number, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.total, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.status, dataXF);
            }

            xls.Save(folderName, true);
            return "NORMAL";
        }

        //导出excel按钮
        private void button2_Click(object sender, EventArgs e)
        {
            yearMonth = cbExportMonth.Text;
            if (db.VwFailImport.Where(v => v.year_month == yearMonth).Count() < 1)
            {
                MessageBox.Show(yearMonth + "月份的数据不存在");
                return;
            }
            string fileName = string.Format("{0}年{1}月份导入系统失败人员", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2));
            fileRoute = Utils.FileNameDialog(fileName);
            if (fileRoute.Equals("cancel"))
            {
                return;
            }
            button2.Enabled = false;
            progressBar2.Style = ProgressBarStyle.Marquee;
            MyUtil.WriteEventLog("导出失败人员报表", "", "", fileName+",开始导出。。。");
            backgroundWorker2.RunWorkerAsync();
        }

    }
}
