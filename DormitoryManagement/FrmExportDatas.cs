using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.in2bits.MyXls;

namespace DormitoryManagement
{
    public partial class FrmExportDatas : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public FrmExportDatas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //空房测试
            //exportEmptyDorm();

            //button1.Text = "在住员工信息导出中……";
            //exportLivingPeopleByArea();

            //button1.Text = "在住员工信息(按部门)导出中……";
            //exportLivingPeopleByDep();

            //button1.Text = "导入失败人员导出……";
            //exportFailureImport();

            //button1.Text = "巴黎半岛……";
            //exportHotel();

            //button1.Text = "调房……";
            //exportChangeDorm();

            button1.Text = "按宿舍区……";
            exportAreaData();
        }

        private void exportEmptyDorm()
        {
            string fileRoute = Utils.FileNameDialog(string.Format("{0}宿舍空房统计", DateTime.Now.ToShortDateString()));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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


            ushort[] colWidth = new ushort[] { 8, 12, 12, 16, 12, 16, 8 };
            string[] colName = new string[] { "房号", "宿舍类型", "入住性别", "最多可住人数", "现有人数", "剩余可住人数", "区别" };

            foreach (Area area in db.Area)
            {
                int rowIndex = 1;
                int colIndex = 1;
                Worksheet sheet = xls.Workbook.Worksheets.Add(area.name);
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
                Cell cell = cells.Add(rowIndex, colIndex, string.Format("{0}宿舍空房统计", DateTime.Now.ToShortDateString()), titleXF);
                for (int i = colIndex + 1; i <= colName.Length; i++)
                {
                    cell = cells.Add(rowIndex, i, "", titleXF);
                }
                rowIndex++;

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
                }

                cell = cells.Add(++rowIndex, 1, "合计", dataXF);
                cell = cells.Add(rowIndex, 4, allMax, dataXF);
                cell = cells.Add(rowIndex, 5, allNow, dataXF);
                cell = cells.Add(rowIndex, 6, allMax - allNow, dataXF);
            }

            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }

        private void exportLivingPeopleByArea()
        {
            string fileRoute = Utils.FileNameDialog(string.Format("{0}在住员工统计表", DateTime.Now.ToShortDateString()));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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

            foreach (Area area in db.Area)
            {
                int rowIndex = 1;
                int colIndex = 1;
                Worksheet sheet = xls.Workbook.Worksheets.Add(area.name);
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
                }

            }

            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }

        private void exportLivingPeopleByDep()
        {
            string fileRoute = Utils.FileNameDialog(string.Format("{0}在住员工统计表", DateTime.Now.ToShortDateString()));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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

            var depTypes = (from d in db.Department
                            select d.property).Distinct();
            foreach (var depType in depTypes)
            {
                int rowIndex = 1;
                int colIndex = 1;
                Worksheet sheet = xls.Workbook.Worksheets.Add(depType.Equals("特殊部门") ? "巴黎酒店" : depType);
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
                }
            }

            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }

        private void exportFailureImport()
        {
            string yearMonth = "201310";
            string fileRoute = Utils.FileNameDialog(string.Format("{0}年{1}月份导入系统失败人员", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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

            ushort[] colWidth = new ushort[] { 24, 8, 12, 8, 8, 12 };
            string[] colName = new string[] { "部门", "账号", "姓名", "宿舍号", "合计", "在职状态" };

            int rowIndex = 1;
            int colIndex = 1;
            Worksheet sheet = xls.Workbook.Worksheets.Add("人员列表");
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
            Cell cell = cells.Add(rowIndex, colIndex, string.Format("{0}年{1}月份导入系统失败人员", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)), titleXF);
            for (int i = colIndex + 1; i <= colName.Length; i++)
            {
                cell = cells.Add(rowIndex, i, "", titleXF);
            }
            rowIndex++;

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
                cell = cells.Add(rowIndex, ++colIndex, data.total, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.status, dataXF);
            }

            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }

        private void exportHotel() {
            string yearMonth = "201310";
            string exportType = "特殊部门";
            string fileRoute = Utils.FileNameDialog(string.Format("{0}年{1}月份房租水电费(巴黎酒店)", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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
            
            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.Height = 12 * 20;
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 12, 8, 12, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 8, 8, 8, 16 };
            string[] colName = new string[] { "部门", "账号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别", "房号", "性质","备注" };

            int rowIndex = 1;
            int colIndex = 1;
            Worksheet sheet = xls.Workbook.Worksheets.Add("巴黎酒店");
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
            Cell cell = cells.Add(rowIndex, colIndex, string.Format("{0}年{1}月份房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)), titleXF);
            for (int i = colIndex + 1; i <= colName.Length; i++)
            {
                cell = cells.Add(rowIndex, i, "", titleXF);
            }
            rowIndex++;

            foreach (var name in colName)
            {
                cell = cells.Add(rowIndex, colIndex++, name, dataXF);
            }

            List<Charge> exportingData = (from ch in db.Charge
                                          where ch.year_month == yearMonth
                                          && ch.property == exportType
                                          orderby ch.dorm_number
                                          select ch).ToList();

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
                cell = cells.Add(rowIndex, ++colIndex, data.fine, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.guesthouse, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.guarantee, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.area, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.dorm_number, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.classify_property, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.comment, dataXF);
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
            cell = cells.Add(rowIndex, 11, exportingData.Sum(d => d.fine), decimaXF);
            cell = cells.Add(rowIndex, 12, exportingData.Sum(d => d.guesthouse), decimaXF);
            cell = cells.Add(rowIndex, 13, exportingData.Sum(d => d.guarantee), decimaXF);

            //最后一行
            cell = cells.Add(++rowIndex, 1, "制表:", dataXF);
            cell = cells.Add(rowIndex, 10, "审核:", dataXF);
            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }

        private void exportChangeDorm()
        {
            string yearMonth = "201310";            
            string fileRoute = Utils.FileNameDialog(string.Format("{0}年{1}月份调房员工原房水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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

            //金额格式，一位小数点
            XF decimaXF = xls.NewXF();
            decimaXF.Format = "0.0";
            decimaXF.Font.Height = 12 * 20;
            decimaXF.Font.FontName = "宋体";

            ushort[] colWidth = new ushort[] { 8, 12, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 16 };
            string[] colName = new string[] { "房号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别", "备注" };

            int rowIndex = 1;
            int colIndex = 1;
            Worksheet sheet = xls.Workbook.Worksheets.Add("原房费用");
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
                           orderby cha.area_id, cha.dorm_order
                           select cha).ToList();

            //"房号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别", "备注"
            foreach (var data in charges)
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
                cell = cells.Add(rowIndex, ++colIndex, data.fine, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.guesthouse, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.guarantee, decimaXF);
                cell = cells.Add(rowIndex, ++colIndex, data.area, dataXF);
                cell = cells.Add(rowIndex, ++colIndex, data.comment, dataXF);
            }
                       
            //最后2行
            cell = cells.Add(++rowIndex, 1, "制表:", dataXF);
            cell = cells.Add(++rowIndex, 1, "1.核实后，如果有问题，请到后勤部核对", dataXF);
            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }

        private void exportAreaData()
        {
            string yearMonth = "201310";
            string fileRoute = Utils.FileNameDialog(string.Format("{0}年{1}月份房租水电费", yearMonth.Substring(0, 4), yearMonth.Substring(4, 2)));
            if (fileRoute.Equals("cancel"))
            {
                return;
            }

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

            ushort[] colWidth = new ushort[] { 8, 12, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 16 };
            string[] colName = new string[] { "房号", "姓名", "合计", "房租", "管理费", "电费", "水费", "其他", "维修", "扣分", "招待所", "押金", "区别", "备注" };

            foreach (Area area in db.Area)
            {
                int rowIndex = 1;
                int colIndex = 1;
                Worksheet sheet = xls.Workbook.Worksheets.Add(area.name);
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
                    cell = cells.Add(rowIndex, ++colIndex, cha.fine, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.guesthouse, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.guarantee, decimaXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.area, dataXF);
                    cell = cells.Add(rowIndex, ++colIndex, cha.comment, dataXF);
                }

                //最后两行
                cell = cells.Add(++rowIndex, 1, "备注：", dataXF);
                cell = cells.Add(++rowIndex, 1, "1.核实后，如果有问题，请到后勤部核对", dataXF);
            }

            xls.Save(folderName, true);
            MessageBox.Show("导出成功");
        }
    }
}
