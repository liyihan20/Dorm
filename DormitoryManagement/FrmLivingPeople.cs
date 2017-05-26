using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using org.in2bits.MyXls;

namespace DormitoryManagement
{
    public partial class FrmLivingPeople : Form
    {
        int doWhat = 0;
        DormDBDataContext db = new DormDBDataContext();
        string fileRoute;
        public FrmLivingPeople()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (doWhat == 1)
            {
                //e.Result = exportByArea();
                e.Result = exportByAreaUseMyxls();
            }
            else {
                //e.Result = exportByDepartment();
                e.Result = exportByDepUseMyxls();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            if (e.Error != null)
            {
                MessageBox.Show("发生未知错误");
            }

            switch (e.Result.ToString())
            {
                case "NODATA":
                    MessageBox.Show("需要导出的数据不存在");
                    MyUtil.WriteEventLog("导出在住人员报表", "", "", "需要导出的数据不存在",false);
                    break;
                case "NOEXCEL":
                    MessageBox.Show("客户机可能没有安装OFFICE excel软件，导出失败");
                    MyUtil.WriteEventLog("导出在住人员报表", "", "", "客户机可能没有安装OFFICE excel软件，导出失败", false);
                    break;
                case "NORMAL":
                    MessageBox.Show("数据已成功导出");
                    MyUtil.WriteEventLog("导出在住人员报表", "", "", "成功导出");
                    break;
                case "UNCLEAR":
                    MessageBox.Show("操作失败……");
                    MyUtil.WriteEventLog("导出在住人员报表", "", "", "操作失败,原因不明", false);
                    break;
            }
        }

        private void FrmLivingPeople_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = this.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = this.Height / 2 - groupBox1.Height / 2 - 20;
        }

        private void FrmLivingPeople_Load(object sender, EventArgs e)
        {
            lbNow.Text = DateTime.Now.ToShortDateString();
            cbType.Text = "按宿舍区";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var people = db.Lodging.Where(p => p.out_date == null);
            progressBar1.Maximum = people.Count();
            progressBar1.Minimum = 0;

            string fileName = string.Format("{0}在住员工统计表（{1}）", lbNow.Text, cbType.Text);
            if (cbType.Text.Equals("按宿舍区"))
            {
                doWhat = 1;
            }
            else {
                doWhat = 2;
            }

            fileRoute = Utils.FileNameDialog(fileName);
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

            progressBar1.Value = 0;
            button1.Enabled = false;
            MyUtil.WriteEventLog("导出在住人员报表", "", "", fileName+",开始导出。。。");
            backgroundWorker1.RunWorkerAsync();

        }

        #region 依赖Excel客户端的导出
        /*private string exportByArea()
        {
            string result = "NORMAL";
            int fieldNum = 9;

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
                    //关键：设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[areaNum]).Activate();
                    xlSheet.Name = area.name;
                    // 设置标题
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}在住员工统计表", DateTime.Now.ToShortDateString()); //设置标题
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    //读出数据库的数据,姓名、帐号、厂牌号、部门、入住时间、住房标准、户籍、区别、房号
                    var peoples = from lod in db.Lodging
                                  where lod.out_date == null
                                  && lod.Dorm.Area == area
                                  orderby lod.Dorm.forOrder.Length, lod.Dorm.forOrder
                                  select new
                                  {
                                      dormNum = lod.Dorm.number,
                                      dormType = lod.Dorm.DormType.name,
                                      emp = lod.Employee.name,
                                      sex = lod.Employee.sex,
                                      dep = lod.Employee.Department1.name,
                                      card = lod.Employee.card_number,
                                      account = lod.Employee.account_number,
                                      inDate = lod.in_date,
                                      home = lod.Employee.household
                                  };
                    int rowNum = peoples.Count();
                    // 创建缓存数据
                    object[,] objData = new object[rowNum + 1, fieldNum];
                    //设置列标题
                    objData[0, 0] = "房号";
                    objData[0, 1] = "姓名";
                    objData[0, 2] = "性别";
                    objData[0, 3] = "部门";
                    objData[0, 4] = "厂牌号";
                    objData[0, 5] = "账号";
                    objData[0, 6] = "户籍";
                    objData[0, 7] = "入住时间";
                    objData[0, 8] = "住房标准";

                    //写入表中数据
                    int i = 0;
                    foreach (var peo in peoples)
                    {
                        i++;
                        progress++;
                        objData[i, 0] = peo.dormNum;
                        objData[i, 1] = peo.emp;
                        objData[i, 2] = peo.sex;
                        objData[i, 3] = peo.dep;
                        objData[i, 4] = peo.card;
                        objData[i, 5] = peo.account;
                        objData[i, 6] = peo.home;
                        objData[i, 7] = ((DateTime)peo.inDate).ToShortDateString();
                        objData[i, 8] = peo.dormType;

                        backgroundWorker1.ReportProgress(progress);
                    }

                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 2, fieldNum]);
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
                throw;
            }
            finally
            {
                xlApp.Quit();
                GC.Collect(); //强制回收
            }
            return result;
        } */
        #endregion

        #region 依赖Excel客户单的导出
        /*private string exportByDepartment()
        {
            string result = "NORMAL";
            int fieldNum = 10;

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
                var depTypes = from d in db.Department
                               select d.property;

                depTypes = depTypes.Distinct();
                //创建多个sheet表
                for (int i = 1; i < depTypes.Count(); i++)
                {
                    xlBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                int typeNum = 0;
                int progress = 0;
                foreach (string depType in depTypes)
                {
                    //设置当前sheet
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[++typeNum];
                    //关键：设置为活动sheet
                    ((_Worksheet)xlApp.Worksheets[typeNum]).Activate();
                    xlSheet.Name = depType.Equals("特殊部门")?"巴黎酒店":depType;
                    // 设置标题
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, fieldNum]); //标题所占的单元格数与表中的列数相同
                    range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = string.Format("{0}在住员工统计表", DateTime.Now.ToShortDateString()); //设置标题
                    xlApp.ActiveCell.Font.Size = 20;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    //读出数据库的数据,姓名、帐号、厂牌号、部门、入住时间、住房标准、户籍、区别、房号
                    var peoples = from lod in db.Lodging
                                  where lod.out_date == null
                                  && lod.Employee.Department1.property==depType
                                  orderby lod.Employee.Department1.number, lod.Employee.account_number
                                  select new
                                  {
                                      dormNum = lod.Dorm.number,
                                      dormType = lod.Dorm.DormType.name,
                                      emp = lod.Employee.name,
                                      dep = lod.Employee.Department1.name,
                                      sex=lod.Employee.sex,
                                      card = lod.Employee.card_number,
                                      account = lod.Employee.account_number,
                                      inDate = lod.in_date,
                                      home = lod.Employee.household,
                                      area = lod.Dorm.Area.name
                                  };
                    int rowNum = peoples.Count();
                    // 创建缓存数据
                    object[,] objData = new object[rowNum + 1, fieldNum];
                    //设置列标题
                    objData[0, 0] = "部门";
                    objData[0, 1] = "姓名";
                    objData[0, 2] = "性别";
                    objData[0, 3] = "厂牌号";
                    objData[0, 4] = "账号";
                    objData[0, 5] = "户籍";
                    objData[0, 6] = "入住时间";
                    objData[0, 7] = "宿舍编号";
                    objData[0, 8] = "住房标准";
                    objData[0, 9] = "区号";

                    //写入表中数据
                    int i = 0;
                    foreach (var peo in peoples)
                    {
                        i++;
                        progress++;
                        objData[i, 0] = peo.dep;
                        objData[i, 1] = peo.emp;
                        objData[i, 2] = peo.sex;
                        objData[i, 3] = peo.card;
                        objData[i, 4] = peo.account;
                        objData[i, 5] = peo.home;
                        objData[i, 6] = ((DateTime)peo.inDate).ToShortDateString();
                        objData[i, 7] = peo.dormNum;
                        objData[i, 8] = peo.dormType;
                        objData[i, 9] = peo.area;

                        backgroundWorker1.ReportProgress(progress);
                    }

                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[rowNum + 2, fieldNum]);
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

        //以下两个方法使用myxls插件导出
        private string exportByAreaUseMyxls() {
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
            dataXF.Font.Height = 12 * 20;
            dataXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 8, 12, 8, 24, 12, 8, 36, 12, 12 };
            string[] colName = new string[] { "房号", "姓名", "性别", "部门", "厂牌号", "账号", "户籍", "入住时间", "住房标准" };
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

                //标题
                MergeArea ma = new MergeArea(rowIndex, rowIndex, colIndex, colName.Length);
                sheet.AddMergeArea(ma);
                Cell cell = cells.Add(rowIndex, colIndex, string.Format("{0}在住员工统计表", DateTime.Now.ToShortDateString()), titleXF);
                for (int i = colIndex + 1; i <= colName.Length; i++)
                {
                    cell = cells.Add(rowIndex, i, "", titleXF);
                }
                rowIndex++;

                foreach (var name in colName)
                {
                    cell = cells.Add(rowIndex, colIndex++, name, dataXF);
                }

                var peoples = from lod in db.Lodging
                              where lod.out_date == null
                              && lod.Dorm.Area == area
                              orderby lod.Dorm.forOrder.Length, lod.Dorm.forOrder
                              select new
                              {
                                  dormNum = lod.Dorm.number,
                                  dormType = lod.Dorm.DormType.name,
                                  emp = lod.Employee.name,
                                  sex = lod.Employee.sex,
                                  dep = lod.Employee.Department1.name,
                                  card = lod.Employee.card_number,
                                  account = lod.Employee.account_number,
                                  inDate = lod.in_date,
                                  home = lod.Employee.household
                              };

                //"房号", "姓名", "性别", "部门", "厂牌号", "账号", "户籍", "入住时间", "住房标准"
                foreach (var people in peoples)
                {
                    colIndex = 0;
                    rowIndex++;
                    cell = cells.Add(rowIndex, ++colIndex, people.dormNum, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.emp, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.sex, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.dep, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.card, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.account, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.home, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, ((DateTime)people.inDate).ToShortDateString(), dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.dormType, dataXF);

                    backgroundWorker1.ReportProgress(++proc);
                }

            }

            xls.Save(folderName, true);
            return "NORMAL";
        }

        private string exportByDepUseMyxls() {
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
            dataXF.Font.Height = 12 * 20;
            dataXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 24, 12, 8, 12, 8, 36, 12, 8, 12, 8 };
            string[] colName = new string[] { "部门", "姓名", "性别", "厂牌号", "账号", "户籍", "入住时间", "宿舍编号", "住房标准", "区号" };
            int proc = 0;
            var depTypes = (from d in db.Department
                            select d.property).Distinct();
            foreach (var depType in depTypes)
            {
                int rowIndex = 1;
                int colIndex = 1;
                org.in2bits.MyXls.Worksheet sheet = xls.Workbook.Worksheets.Add(depType.Equals("特殊部门") ? "巴黎酒店" : depType);
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
                Cell cell = cells.Add(rowIndex, colIndex, string.Format("{0}在住员工统计表", DateTime.Now.ToShortDateString()), titleXF);
                for (int i = colIndex + 1; i <= colName.Length; i++)
                {
                    cell = cells.Add(rowIndex, i, "", titleXF);
                }
                rowIndex++;

                foreach (var name in colName)
                {
                    cell = cells.Add(rowIndex, colIndex++, name, dataXF);
                }

                var peoples = from lod in db.Lodging
                              where lod.out_date == null
                              && lod.Employee.Department1.property == depType
                              orderby lod.Employee.Department1.number, lod.Employee.account_number
                              select new
                              {
                                  dormNum = lod.Dorm.number,
                                  dormType = lod.Dorm.DormType.name,
                                  emp = lod.Employee.name,
                                  dep = lod.Employee.Department1.name,
                                  sex = lod.Employee.sex,
                                  card = lod.Employee.card_number,
                                  account = lod.Employee.account_number,
                                  inDate = lod.in_date,
                                  home = lod.Employee.household,
                                  area = lod.Dorm.Area.name
                              };

                //"部门", "姓名", "性别", "厂牌号", "账号", "户籍", "入住时间", "宿舍编号", "住房标准","区号"
                foreach (var people in peoples)
                {
                    colIndex = 0;
                    rowIndex++;
                    cell = cells.Add(rowIndex, ++colIndex, people.dep, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.emp, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.sex, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.card, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.account, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.home, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, ((DateTime)people.inDate).ToShortDateString(), dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.dormNum, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.dormType, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, people.area, dataXF);
                    backgroundWorker1.ReportProgress(++proc);
                }
            }

            xls.Save(folderName, true);
            return "NORMAL";
        }
    }
}
