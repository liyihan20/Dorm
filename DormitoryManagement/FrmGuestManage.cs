using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmGuestManage : Form
    {
        FrmGuestBaseInfo info;
        FrmGuestCheck check;
        FrmGuestExport export;
        public FrmGuestManage()
        {
            InitializeComponent();
        }
        
        //导出报表
        private void button2_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (export == null || export.IsDisposed)
            {
                export = new FrmGuestExport();
            }
            else
            {
                export.Activate();
            }
            export.TopLevel = false;
            export.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Add(export);
            export.Show();
        }

        //基础资料维护
        private void button4_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (info == null || info.IsDisposed)
            {
                info = new FrmGuestBaseInfo();
            }
            else
            {
                info.Activate();
            }
            info.TopLevel = false;
            info.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Add(info);
            info.Show();
        }

        //入住资料维护
        private void button3_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (check == null || check.IsDisposed)
            {
                check = new FrmGuestCheck();
            }
            else
            {
                check.Activate();
            }
            check.TopLevel = false;
            check.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Add(check);
            check.Show();
        }
    }
}
