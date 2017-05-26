using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public class Utils
    {
        //excel 2007 版本的: "导出Excel(*.xlsx)|*.xlsx"
        static string excelVersionFilter = "导出Excel(*.xls)|*.xls";

        public static string FileNameDialog(string fileName)
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = excelVersionFilter;
            saveFileDialog.FileName = fileName;
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.Title = "导出文件保存路径";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return "cancel";
            }            
            return saveFileDialog.FileName;
        }

        public static string FoldDialog() {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择需要保存的文件夹";
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.Cancel) {
                return "cancel";
            }
            return fbd.SelectedPath;
        }

        public static string ConvertAreaName(string areaName) {
            switch (areaName) { 
                case "一区":
                    return "1区";
                case "二区":
                    return "2区";
                case "三区":
                    return "3区";
                case "五区":
                    return "5区";
                case "六区":
                    return "6区";
                default:
                    return areaName;
            }
        }
    }
}
