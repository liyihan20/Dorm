using System;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmMain : Form
    {
        FrmLivingIn wel;
        FrmAdvanceDep dep;
        FrmAreaAndDorm areaAndDorm;
        FrmOperator opera;
        FrmFuelInput fuel;
        FrmBaseInfo baseInfo;
        FrmImportData import;
        //FrmSettle settle;
        FrmMovingOut moving;
        FrDormInfop dormInfo;
        public FrmEmployeeInfo emp;
        FrmOtherFeeInput other;
        FrmChangePassword pass;
        FrmDataGenerator data;
        FrmExportByInside inside;
        FrmExportEmptyDorm empty;
        //FrmChangeEmpDep empDep;
        FrmBlackList black;
        FrmCheckFuelFee chFuel;
        FrmChangedDormInfos changeDorm;
        FrmFirstFuelInput firstFuel;
        FrmMultiModifyDepartment modifyDep;
        FrmMovingOutFee outFee;
        FrmLivingPeople livingPeople;
        FrmImportIntoSalary importSalary;
        FrmChangeToSalaryDep salaryDep;
        FrmImportFeeLastMonth importFee;
        FrmMonthlyFee monthlyFee;
        DormDBDataContext db = new DormDBDataContext();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToLongDateString() +" "+DateTime.Now.ToLongTimeString();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            操作员ToolStripMenuItem.Visible = false;
            //数据导入ToolStripMenuItem.Visible = false;
            lbName.Text = LoginUser.username;
            lbAreaName.Text = LoginUser.operated_area;
            lbTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            if (LoginUser.isAdmin) {
                操作员ToolStripMenuItem.Visible = true;
            }
            //if (LoginUser.username.Equals("liyh")) {
            //    数据导入ToolStripMenuItem.Visible = true;
            //}
            //曾玉凤的权限：下载报表，转换部门
            if (LoginUser.username.Equals("zyf")) {
                开始ToolStripMenuItem.Visible = false;
                查询信息ToolStripMenuItem.Visible = false;
                系统维护ToolStripMenuItem.Visible = false;
                空房统计报表ToolStripMenuItem.Visible = false;
                其他费用录入ToolStripMenuItem1.Visible = false;
                水电费录入ToolStripMenuItem.Visible = false;
                月末核算ToolStripMenuItem.Visible = false;
                关联工资系统ToolStripMenuItem.Visible = false;
                在住员工报表ToolStripMenuItem.Visible = false;
            }               

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if (MessageBox.Show("确定要退出系统吗？", "警告", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (!LoginUser.username.Equals("liyh"))
                    {
                        LoginLog log = new LoginLog()
                        {
                            name = LoginUser.username + " >>",
                            area = LoginUser.operated_area,
                            date = DateTime.Now
                        };
                        db.LoginLog.InsertOnSubmit(log);
                        db.SubmitChanges();
                    }
                    Application.Exit();
                }
                else
                    e.Cancel = true;
            }
        }

        private void 操作员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opera == null || opera.IsDisposed)
            {
                opera = new FrmOperator();
                opera.MdiParent = this;
                opera.WindowState = FormWindowState.Maximized;
                opera.Show();
                opera.AutoScroll = true;
            }
            else
                opera.Activate();
        }

        private void 变更密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pass == null || pass.IsDisposed)
            {
                pass = new FrmChangePassword();
                pass.MdiParent = this;
                pass.WindowState = FormWindowState.Normal;
                pass.Show();
                pass.AutoScroll = true;
            }
            else
                pass.Activate();
        }

        private void 重新登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重新登录吗？", "重新登录", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FrmLogin login = new FrmLogin();
                this.Hide();
                login.Show();
            }
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 住宿登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wel == null || wel.IsDisposed)
            {
                wel = new FrmLivingIn();
                wel.MdiParent = this;
                wel.WindowState = FormWindowState.Maximized;
                wel.Show();
                wel.AutoScroll = true;
            }
            else
                wel.Activate();
        }

        //private void 住宿安排ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (settle == null || settle.IsDisposed)
        //    {
        //        settle = new FrmSettle();
        //        settle.MdiParent = this;
        //        settle.WindowState = FormWindowState.Maximized;
        //        settle.Show();
        //    }
        //    else
        //        settle.Activate();
        //}

        private void 退宿登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (moving == null || moving.IsDisposed)
            {
                moving = new FrmMovingOut();
                moving.MdiParent = this;
                moving.WindowState = FormWindowState.Maximized;
                moving.Show();
                moving.AutoScroll = true;
            }
            else
                moving.Activate();
        }

        private void 其他费用录入ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (other == null || other.IsDisposed)
            {
                other = new FrmOtherFeeInput();
                other.MdiParent = this;
                other.WindowState = FormWindowState.Maximized;
                other.Show();
                other.AutoScroll = true;
            }
            else
                other.Activate();
        }

        private void 水电费录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (db.VerifyOrder.Count() > 0)
            {

                if (fuel == null || fuel.IsDisposed)
                {
                    fuel = new FrmFuelInput();
                    fuel.MdiParent = this;
                    fuel.WindowState = FormWindowState.Maximized;
                    fuel.Show();
                    fuel.AutoScroll = true;
                }
                else
                    fuel.Activate();
            }
            else
            {
                if (firstFuel == null || firstFuel.IsDisposed)
                {
                    firstFuel = new FrmFirstFuelInput();
                    firstFuel.MdiParent = this;
                    firstFuel.WindowState = FormWindowState.Maximized;
                    firstFuel.Show();
                    firstFuel.AutoScroll = true;
                }
                else
                    firstFuel.Activate();
            }
        }

        private void 月末核算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (data == null || data.IsDisposed)
            {
                data = new FrmDataGenerator();
                data.MdiParent = this;
                data.Show();
                data.AutoScroll = true;
            }
            else
                data.Activate();
        }

        private void 宿舍查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dormInfo == null || dormInfo.IsDisposed)
            {
                dormInfo = new FrDormInfop();
                dormInfo.MdiParent = this;
                dormInfo.WindowState = FormWindowState.Maximized;
                dormInfo.Show();
                dormInfo.AutoScroll = true;
            }
            else
                dormInfo.Activate();
        }

        private void 入住员工查询ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (emp == null || emp.IsDisposed)
            {
                emp = new FrmEmployeeInfo();
                emp.MdiParent = this;
                emp.WindowState = FormWindowState.Maximized;
                emp.Show();
                emp.AutoScroll = true;
            }
            else
                emp.Activate();
        }

        private void 部门管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dep == null || dep.IsDisposed)
            {
                dep = new FrmAdvanceDep();
                dep.MdiParent = this;
                dep.WindowState = FormWindowState.Maximized;
                dep.Show();
                dep.AutoScroll = true;
            }
            else
                dep.Activate();
        }

        private void 宿舍区宿舍管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (areaAndDorm == null || areaAndDorm.IsDisposed)
            {
                areaAndDorm = new FrmAreaAndDorm();
                areaAndDorm.MdiParent = this;
                areaAndDorm.WindowState = FormWindowState.Maximized;
                areaAndDorm.Show();
                areaAndDorm.AutoScroll = true;
            }
            else
                areaAndDorm.Activate();
        }

        private void 基本资料维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseInfo == null || baseInfo.IsDisposed)
            {
                baseInfo = new FrmBaseInfo();
                baseInfo.MdiParent = this;
                baseInfo.WindowState = FormWindowState.Maximized;
                baseInfo.Show();
                baseInfo.AutoScroll = true;
            }
            else
                baseInfo.Activate();
        }

        private void 宿舍与员工ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (import == null || import.IsDisposed)
            {
                import = new FrmImportData();
                import.MdiParent = this;
                import.WindowState = FormWindowState.Maximized;
                import.Show();
                import.AutoScroll = true;
            }
            else
                import.Activate();
        }

        private void 空房统计报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (empty == null || empty.IsDisposed)
            {
                empty = new FrmExportEmptyDorm();
                empty.MdiParent = this;
                empty.Show();
                empty.AutoScroll = true;
            }
            else
                empty.Activate();
        }

        private void 厂内水电费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inside == null || inside.IsDisposed)
            {
                inside = new FrmExportByInside();
                inside.MdiParent = this;
                inside.Show();
                inside.AutoScroll = true;
            }
            else
                inside.Activate();
        }                

        //private void 员工ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (empDep == null || empDep.IsDisposed)
        //    {
        //        empDep = new FrmChangeEmpDep();
        //        empDep.MdiParent = this;
        //        empDep.Show();
        //        empDep.AutoScroll = true;
        //    }
        //    else
        //        empDep.Activate();
        //}

        private void 黑名单管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (black == null || black.IsDisposed)
            {
                black = new FrmBlackList();
                black.MdiParent = this;
                black.WindowState = FormWindowState.Maximized;
                black.Show();
                black.AutoScroll = true;
            }
            else
                black.Activate();
        }

        private void 月水电费用查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chFuel == null || chFuel.IsDisposed)
            {
                chFuel = new FrmCheckFuelFee();
                chFuel.MdiParent = this;
                chFuel.WindowState = FormWindowState.Maximized;
                chFuel.Show();
                chFuel.AutoScroll = true;
            }
            else
                chFuel.Activate();
        }

        private void 调房信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeDorm == null || changeDorm.IsDisposed)
            {
                changeDorm = new FrmChangedDormInfos();
                changeDorm.MdiParent = this;
                changeDorm.WindowState = FormWindowState.Maximized;
                changeDorm.Show();
                changeDorm.AutoScroll = true;
            }
            else
                changeDorm.Activate();
        }

        private void 部门批量修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (modifyDep == null || modifyDep.IsDisposed)
            {
                modifyDep = new FrmMultiModifyDepartment();
                modifyDep.MdiParent = this;
                modifyDep.WindowState = FormWindowState.Maximized;
                modifyDep.Show();
                modifyDep.AutoScroll = true;
            }
            else
                modifyDep.Activate();
        }

        private void 退房员工费用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outFee == null || outFee.IsDisposed)
            {
                outFee = new FrmMovingOutFee();
                outFee.MdiParent = this;
                outFee.WindowState = FormWindowState.Maximized;
                outFee.Show();
                outFee.AutoScroll = true;
            }
            else
                outFee.Activate();
        }
        
        private void 在住员工报表ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (livingPeople == null || livingPeople.IsDisposed)
             {
                 livingPeople = new FrmLivingPeople();
                 livingPeople.MdiParent = this;
                 livingPeople.WindowState = FormWindowState.Maximized;
                 livingPeople.Show();
                 livingPeople.AutoScroll = true;
             }
             else
                 livingPeople.Activate();
         }

        private void 关联工资系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (importSalary == null || importSalary.IsDisposed)
             {
                 importSalary = new FrmImportIntoSalary();
                 importSalary.MdiParent = this;
                 importSalary.Show();
                 importSalary.AutoScroll = true;
             }
             else
                 importSalary.Activate();
         }

        private void 转换工资部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (salaryDep == null || salaryDep.IsDisposed)
            {
                salaryDep = new FrmChangeToSalaryDep();
                salaryDep.MdiParent = this;
                salaryDep.Show();
                salaryDep.AutoScroll = true;
            }
            else
                salaryDep.Activate();
        }

        private void 导入上月房租ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (importFee == null || importFee.IsDisposed)
            {
                importFee = new FrmImportFeeLastMonth();
                importFee.MdiParent = this;
                importFee.Show();
                importFee.AutoScroll = true;
            }
            else
                importFee.Activate();
        }

        private void 每月固定费用维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (monthlyFee == null || monthlyFee.IsDisposed)
            {
                monthlyFee = new FrmMonthlyFee();
                monthlyFee.MdiParent = this;
                monthlyFee.WindowState = FormWindowState.Maximized;
                monthlyFee.Show();
                monthlyFee.AutoScroll = true;
            }
            else
                monthlyFee.Activate();
        }
        
        
        
    }
}
