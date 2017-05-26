using System;
using System.Windows.Forms;

namespace DormitoryManagement
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
            //Application.Run(new DlgMonthlyFee());
            //Application.Run(new FrmExportDatas());
            //Application.Run(new FrmExportByInside());
        }
    }
}
