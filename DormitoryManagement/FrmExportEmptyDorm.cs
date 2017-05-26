using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using org.in2bits.MyXls;

namespace DormitoryManagement
{
    public partial class FrmExportEmptyDorm : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        string fileRoute;
        string modelName = "Excel导出";
        public FrmExportEmptyDorm()
        {
            InitializeComponent();
        }

        private void FrmExportEmptyDorm_Load(object sender, EventArgs e)
        {
            lbDate.Text = DateTime.Now.ToShortDateString();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            //e.Result = exportEmptyDorm();
            e.Result = exportEmptyDormUseMyxls();
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar3.Value = e.ProgressPercentage;
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button3.Enabled = true;
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
                MyUtil.WriteEventLog(modelName, "", "", "数据导出失败:" + e.Error);
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
                    MyUtil.WriteEventLog(modelName, "", "", "数据已成功导出:"+fileRoute);
                    break;
                case "UNCLEAR":
                    MessageBox.Show("操作失败……");
                    break;
            }
        }

        
        #region 依赖 microsoft excel 导出
        /*private string exportEmptyDorm()
        {
            string result = "NORMAL";
            int fieldNum = 7;

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
                for (int i = 1; i < areas.Count(); i++)
                {
                    xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                int areaNum = 0;
                int progress = 0;
                foreach (Area area in areas)
                {
                    //设置当前sheet
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[++areaNum];
                    //关键：如果有多张sheet，必须设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[areaNum]).Activate();
                    xlSheet.Name = area.name;
                    // 设置标题
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}宿舍空房统计", DateTime.Now.ToShortDateString()); //设置标题
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    //读出数据库的数据
                    var dorms = from dor in db.Dorm
                                where dor.Area == area
                                && dor.available == 0
                                orderby dor.forOrder.Length, dor.forOrder
                                select dor;
                    int rowNum = dorms.Count();
                    // 创建缓存数据
                    object[,] objData = new object[rowNum + 2, fieldNum];
                    //设置列标题
                    objData[0, 0] = "房号";
                    objData[0, 1] = "宿舍类型";
                    objData[0, 2] = "入住性别";
                    objData[0, 3] = "最多可住人数";
                    objData[0, 4] = "现有人数";
                    objData[0, 5] = "剩余可住人数";
                    objData[0, 6] = "区别";

                    //写入表中数据
                    int i = 0;
                    int maxMax = 0;
                    int maxNow = 0;
                    int maxLeft = 0;
                    foreach (Dorm dorm in dorms)
                    {
                        i++;
                        progress++;
                        objData[i, 0] = dorm.number;
                        objData[i, 1] = dorm.DormType.name;
                        objData[i, 2] = dorm.dormSex;
                        objData[i, 3] = dorm.DormType.max_number;
                        int nowLivingNum = dorm.Lodging.Where(lod => lod.out_date == null).Count();
                        objData[i, 4] = nowLivingNum;
                        objData[i, 5] = dorm.DormType.max_number - nowLivingNum;
                        objData[i, 6] = area.name;

                        maxMax += (int)dorm.DormType.max_number;
                        maxNow += nowLivingNum;
                        maxLeft += (int)(dorm.DormType.max_number - nowLivingNum);
                        bgw.ReportProgress(progress);
                    }

                    objData[++i, 0] = "合计";
                    objData[i, 3] = maxMax;
                    objData[i, 4] = maxNow;
                    objData[i, 5] = maxLeft;

                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 3, fieldNum]);
                    range.Value2 = objData;
                    //列宽自适应
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, fieldNum]);
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
                GC.Collect(); //强制回收
            }
            return result;
        } */
        #endregion

        //使用myxls导出
        private string exportEmptyDormUseMyxls() {            

            string fileName = fileRoute.Substring(fileRoute.LastIndexOf(@"\") + 1);
            string folderName = fileRoute.Substring(0, fileRoute.Length - fileName.Length - 1);

            XlsDocument xls = new XlsDocument();
            xls.FileName = fileName;
            
            //普通文字样式
            XF dataXF = xls.NewXF();            
            dataXF.Font.FontName = "宋体";


            ushort[] colWidth = new ushort[] { 8, 12, 12, 16, 12, 16, 8 };
            string[] colName = new string[] { "房号", "宿舍类型", "入住性别", "最多可住人数", "现有人数", "剩余可住人数", "区别" };

            int proc = 0;//汇报进度
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
                var dorms = from dor in db.Dorm
                            where dor.Area == area
                            && dor.available == 0
                            orderby dor.forOrder.Length, dor.forOrder
                            select dor;

                int allMax = 0;
                int allNow = 0;
                
                //"房号", "宿舍类型", "入住性别", "最多可住人数", "现有人数", "剩余可住人数", "区别"
                foreach (var dorm in dorms)
                {
                    colIndex = 0;
                    rowIndex++;
                    cell = cells.Add(rowIndex, ++colIndex, dorm.number, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, dorm.DormType.name, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, dorm.dormSex, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, dorm.DormType.max_number, dataXF);
                    int nowLivingNum = dorm.Lodging.Where(lod => lod.out_date == null).Count();
                    cell = cells.Add(rowIndex, ++colIndex, nowLivingNum, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, dorm.DormType.max_number - nowLivingNum, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, area.name, dataXF);
                    allMax += (int)dorm.DormType.max_number;
                    allNow += nowLivingNum;
                    bgw.ReportProgress(++proc);
                }

                cell = cells.Add(++rowIndex, 1, "合计", dataXF);
                cell = cells.Add(rowIndex, 4, allMax, dataXF);
                cell = cells.Add(rowIndex, 5, allNow, dataXF);
                cell = cells.Add(rowIndex, 6, allMax - allNow, dataXF);
            }

            xls.Save(folderName, true);
            return "NORMAL";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar3.Value = 0;
            progressBar3.Maximum = db.Dorm.Where(d => d.available == 0).Count();
            string fileName = string.Format("{0}宿舍空房统计", DateTime.Now.ToShortDateString());
            fileRoute = Utils.FileNameDialog(fileName);
            if (fileRoute.Equals("cancel"))
            {
                return;
            }      
            button3.Enabled = false;
            MyUtil.WriteEventLog(modelName, "", "", "导出空房报表："+fileName);
            bgw.RunWorkerAsync();
        }

        private void FrmExportEmptyDorm_Resize(object sender, EventArgs e)
        {
            groupBox3.Left = this.Width / 2 - groupBox3.Width / 2;
            groupBox3.Top = this.Height / 2 - groupBox3.Height / 2 -20;
        }
    }
}
