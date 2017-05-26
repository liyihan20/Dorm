namespace DormitoryManagement
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作员ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变更密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.住宿登记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退宿登记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.员工ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.黑名单管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.费用核算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他费用录入ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.水电费录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.每月固定费用维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.月末核算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.转换工资部门ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关联工资系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报表管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空房统计报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.厂内水电费ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在住员工报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.宿舍查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入住员工查询ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.退房员工费用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.月水电费用查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调房信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部门管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.宿舍区宿舍管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基本资料维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.宿舍与员工ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部门批量修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入上月房租ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLable1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbAreaName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统管理ToolStripMenuItem,
            this.开始ToolStripMenuItem,
            this.费用核算ToolStripMenuItem,
            this.报表管理ToolStripMenuItem,
            this.查询信息ToolStripMenuItem,
            this.系统维护ToolStripMenuItem,
            this.数据导入ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1099, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统管理ToolStripMenuItem
            // 
            this.系统管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作员ToolStripMenuItem,
            this.变更密码ToolStripMenuItem,
            this.重新登录ToolStripMenuItem,
            this.退出系统ToolStripMenuItem});
            this.系统管理ToolStripMenuItem.Name = "系统管理ToolStripMenuItem";
            this.系统管理ToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.系统管理ToolStripMenuItem.Text = "系统管理(&S)";
            // 
            // 操作员ToolStripMenuItem
            // 
            this.操作员ToolStripMenuItem.Name = "操作员ToolStripMenuItem";
            this.操作员ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.操作员ToolStripMenuItem.Text = "操作员";
            this.操作员ToolStripMenuItem.Click += new System.EventHandler(this.操作员ToolStripMenuItem_Click);
            // 
            // 变更密码ToolStripMenuItem
            // 
            this.变更密码ToolStripMenuItem.Name = "变更密码ToolStripMenuItem";
            this.变更密码ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.变更密码ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.变更密码ToolStripMenuItem.Text = "变更密码";
            this.变更密码ToolStripMenuItem.Click += new System.EventHandler(this.变更密码ToolStripMenuItem_Click);
            // 
            // 重新登录ToolStripMenuItem
            // 
            this.重新登录ToolStripMenuItem.Name = "重新登录ToolStripMenuItem";
            this.重新登录ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.重新登录ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.重新登录ToolStripMenuItem.Text = "重新登录";
            this.重新登录ToolStripMenuItem.Click += new System.EventHandler(this.重新登录ToolStripMenuItem_Click);
            // 
            // 退出系统ToolStripMenuItem
            // 
            this.退出系统ToolStripMenuItem.Name = "退出系统ToolStripMenuItem";
            this.退出系统ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.退出系统ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.退出系统ToolStripMenuItem.Text = "退出系统";
            this.退出系统ToolStripMenuItem.Click += new System.EventHandler(this.退出系统ToolStripMenuItem_Click);
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.住宿登记ToolStripMenuItem,
            this.退宿登记ToolStripMenuItem,
            this.员工ToolStripMenuItem,
            this.黑名单管理ToolStripMenuItem});
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.开始ToolStripMenuItem.Text = "住宿维护(&L)";
            // 
            // 住宿登记ToolStripMenuItem
            // 
            this.住宿登记ToolStripMenuItem.Name = "住宿登记ToolStripMenuItem";
            this.住宿登记ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.住宿登记ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.住宿登记ToolStripMenuItem.Text = "住宿登记";
            this.住宿登记ToolStripMenuItem.Click += new System.EventHandler(this.住宿登记ToolStripMenuItem_Click);
            // 
            // 退宿登记ToolStripMenuItem
            // 
            this.退宿登记ToolStripMenuItem.Name = "退宿登记ToolStripMenuItem";
            this.退宿登记ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.退宿登记ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.退宿登记ToolStripMenuItem.Text = "退宿登记";
            this.退宿登记ToolStripMenuItem.Click += new System.EventHandler(this.退宿登记ToolStripMenuItem_Click);
            // 
            // 员工ToolStripMenuItem
            // 
            this.员工ToolStripMenuItem.Name = "员工ToolStripMenuItem";
            this.员工ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.员工ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.员工ToolStripMenuItem.Text = "员工部门修改";
            this.员工ToolStripMenuItem.Visible = false;
            // 
            // 黑名单管理ToolStripMenuItem
            // 
            this.黑名单管理ToolStripMenuItem.Name = "黑名单管理ToolStripMenuItem";
            this.黑名单管理ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.黑名单管理ToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.黑名单管理ToolStripMenuItem.Text = "黑名单管理";
            this.黑名单管理ToolStripMenuItem.Click += new System.EventHandler(this.黑名单管理ToolStripMenuItem_Click);
            // 
            // 费用核算ToolStripMenuItem
            // 
            this.费用核算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.其他费用录入ToolStripMenuItem1,
            this.水电费录入ToolStripMenuItem,
            this.每月固定费用维护ToolStripMenuItem,
            this.月末核算ToolStripMenuItem,
            this.转换工资部门ToolStripMenuItem,
            this.关联工资系统ToolStripMenuItem});
            this.费用核算ToolStripMenuItem.Name = "费用核算ToolStripMenuItem";
            this.费用核算ToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.费用核算ToolStripMenuItem.Text = "费用核算(&F)";
            // 
            // 其他费用录入ToolStripMenuItem1
            // 
            this.其他费用录入ToolStripMenuItem1.Name = "其他费用录入ToolStripMenuItem1";
            this.其他费用录入ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.其他费用录入ToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.其他费用录入ToolStripMenuItem1.Text = "其他费用录入";
            this.其他费用录入ToolStripMenuItem1.Click += new System.EventHandler(this.其他费用录入ToolStripMenuItem1_Click);
            // 
            // 水电费录入ToolStripMenuItem
            // 
            this.水电费录入ToolStripMenuItem.Name = "水电费录入ToolStripMenuItem";
            this.水电费录入ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.水电费录入ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.水电费录入ToolStripMenuItem.Text = "月末水电费录入";
            this.水电费录入ToolStripMenuItem.Click += new System.EventHandler(this.水电费录入ToolStripMenuItem_Click);
            // 
            // 每月固定费用维护ToolStripMenuItem
            // 
            this.每月固定费用维护ToolStripMenuItem.Name = "每月固定费用维护ToolStripMenuItem";
            this.每月固定费用维护ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.每月固定费用维护ToolStripMenuItem.Text = "每月固定费用维护";
            this.每月固定费用维护ToolStripMenuItem.Click += new System.EventHandler(this.每月固定费用维护ToolStripMenuItem_Click);
            // 
            // 月末核算ToolStripMenuItem
            // 
            this.月末核算ToolStripMenuItem.Name = "月末核算ToolStripMenuItem";
            this.月末核算ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.月末核算ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.月末核算ToolStripMenuItem.Text = "费用核算与生成";
            this.月末核算ToolStripMenuItem.Click += new System.EventHandler(this.月末核算ToolStripMenuItem_Click);
            // 
            // 转换工资部门ToolStripMenuItem
            // 
            this.转换工资部门ToolStripMenuItem.Name = "转换工资部门ToolStripMenuItem";
            this.转换工资部门ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.转换工资部门ToolStripMenuItem.Text = "转换工资部门";
            this.转换工资部门ToolStripMenuItem.Click += new System.EventHandler(this.转换工资部门ToolStripMenuItem_Click);
            // 
            // 关联工资系统ToolStripMenuItem
            // 
            this.关联工资系统ToolStripMenuItem.Name = "关联工资系统ToolStripMenuItem";
            this.关联工资系统ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.关联工资系统ToolStripMenuItem.Text = "关联工资系统";
            this.关联工资系统ToolStripMenuItem.Click += new System.EventHandler(this.关联工资系统ToolStripMenuItem_Click);
            // 
            // 报表管理ToolStripMenuItem
            // 
            this.报表管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.空房统计报表ToolStripMenuItem,
            this.厂内水电费ToolStripMenuItem,
            this.在住员工报表ToolStripMenuItem});
            this.报表管理ToolStripMenuItem.Name = "报表管理ToolStripMenuItem";
            this.报表管理ToolStripMenuItem.Size = new System.Drawing.Size(88, 21);
            this.报表管理ToolStripMenuItem.Text = "Excel导出(&E)";
            // 
            // 空房统计报表ToolStripMenuItem
            // 
            this.空房统计报表ToolStripMenuItem.Name = "空房统计报表ToolStripMenuItem";
            this.空房统计报表ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.空房统计报表ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.空房统计报表ToolStripMenuItem.Text = "空房统计报表";
            this.空房统计报表ToolStripMenuItem.Click += new System.EventHandler(this.空房统计报表ToolStripMenuItem_Click);
            // 
            // 厂内水电费ToolStripMenuItem
            // 
            this.厂内水电费ToolStripMenuItem.Name = "厂内水电费ToolStripMenuItem";
            this.厂内水电费ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.厂内水电费ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.厂内水电费ToolStripMenuItem.Text = "水电费报表";
            this.厂内水电费ToolStripMenuItem.Click += new System.EventHandler(this.厂内水电费ToolStripMenuItem_Click);
            // 
            // 在住员工报表ToolStripMenuItem
            // 
            this.在住员工报表ToolStripMenuItem.Name = "在住员工报表ToolStripMenuItem";
            this.在住员工报表ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.在住员工报表ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.在住员工报表ToolStripMenuItem.Text = "在住员工报表";
            this.在住员工报表ToolStripMenuItem.Click += new System.EventHandler(this.在住员工报表ToolStripMenuItem_Click);
            // 
            // 查询信息ToolStripMenuItem
            // 
            this.查询信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.宿舍查询ToolStripMenuItem,
            this.入住员工查询ToolStripMenuItem1,
            this.退房员工费用ToolStripMenuItem,
            this.月水电费用查询ToolStripMenuItem,
            this.调房信息查询ToolStripMenuItem});
            this.查询信息ToolStripMenuItem.Name = "查询信息ToolStripMenuItem";
            this.查询信息ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.查询信息ToolStripMenuItem.Text = "查询信息(&C)";
            // 
            // 宿舍查询ToolStripMenuItem
            // 
            this.宿舍查询ToolStripMenuItem.Name = "宿舍查询ToolStripMenuItem";
            this.宿舍查询ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.宿舍查询ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.宿舍查询ToolStripMenuItem.Text = "宿舍查询";
            this.宿舍查询ToolStripMenuItem.Click += new System.EventHandler(this.宿舍查询ToolStripMenuItem_Click);
            // 
            // 入住员工查询ToolStripMenuItem1
            // 
            this.入住员工查询ToolStripMenuItem1.Name = "入住员工查询ToolStripMenuItem1";
            this.入住员工查询ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.入住员工查询ToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.入住员工查询ToolStripMenuItem1.Text = "入住员工查询";
            this.入住员工查询ToolStripMenuItem1.Click += new System.EventHandler(this.入住员工查询ToolStripMenuItem1_Click);
            // 
            // 退房员工费用ToolStripMenuItem
            // 
            this.退房员工费用ToolStripMenuItem.Name = "退房员工费用ToolStripMenuItem";
            this.退房员工费用ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.退房员工费用ToolStripMenuItem.Text = "退房员工费用修改";
            this.退房员工费用ToolStripMenuItem.Click += new System.EventHandler(this.退房员工费用ToolStripMenuItem_Click);
            // 
            // 月水电费用查询ToolStripMenuItem
            // 
            this.月水电费用查询ToolStripMenuItem.Name = "月水电费用查询ToolStripMenuItem";
            this.月水电费用查询ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.月水电费用查询ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.月水电费用查询ToolStripMenuItem.Text = "月水电费用查询";
            this.月水电费用查询ToolStripMenuItem.Click += new System.EventHandler(this.月水电费用查询ToolStripMenuItem_Click);
            // 
            // 调房信息查询ToolStripMenuItem
            // 
            this.调房信息查询ToolStripMenuItem.Name = "调房信息查询ToolStripMenuItem";
            this.调房信息查询ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.调房信息查询ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.调房信息查询ToolStripMenuItem.Text = "调房信息查询";
            this.调房信息查询ToolStripMenuItem.Click += new System.EventHandler(this.调房信息查询ToolStripMenuItem_Click);
            // 
            // 系统维护ToolStripMenuItem
            // 
            this.系统维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.部门管理ToolStripMenuItem,
            this.宿舍区宿舍管理ToolStripMenuItem,
            this.基本资料维护ToolStripMenuItem});
            this.系统维护ToolStripMenuItem.Name = "系统维护ToolStripMenuItem";
            this.系统维护ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.系统维护ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.系统维护ToolStripMenuItem.Text = "基础资料(&B)";
            // 
            // 部门管理ToolStripMenuItem
            // 
            this.部门管理ToolStripMenuItem.Name = "部门管理ToolStripMenuItem";
            this.部门管理ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.部门管理ToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.部门管理ToolStripMenuItem.Text = "部门管理";
            this.部门管理ToolStripMenuItem.Click += new System.EventHandler(this.部门管理ToolStripMenuItem_Click);
            // 
            // 宿舍区宿舍管理ToolStripMenuItem
            // 
            this.宿舍区宿舍管理ToolStripMenuItem.Name = "宿舍区宿舍管理ToolStripMenuItem";
            this.宿舍区宿舍管理ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.宿舍区宿舍管理ToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.宿舍区宿舍管理ToolStripMenuItem.Text = "宿舍区/宿舍管理";
            this.宿舍区宿舍管理ToolStripMenuItem.Click += new System.EventHandler(this.宿舍区宿舍管理ToolStripMenuItem_Click);
            // 
            // 基本资料维护ToolStripMenuItem
            // 
            this.基本资料维护ToolStripMenuItem.Name = "基本资料维护ToolStripMenuItem";
            this.基本资料维护ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.基本资料维护ToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.基本资料维护ToolStripMenuItem.Text = "基本资料维护";
            this.基本资料维护ToolStripMenuItem.Click += new System.EventHandler(this.基本资料维护ToolStripMenuItem_Click);
            // 
            // 数据导入ToolStripMenuItem
            // 
            this.数据导入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.宿舍与员工ToolStripMenuItem,
            this.部门批量修改ToolStripMenuItem,
            this.导入上月房租ToolStripMenuItem});
            this.数据导入ToolStripMenuItem.Name = "数据导入ToolStripMenuItem";
            this.数据导入ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.数据导入ToolStripMenuItem.Text = "数据导入";
            // 
            // 宿舍与员工ToolStripMenuItem
            // 
            this.宿舍与员工ToolStripMenuItem.Name = "宿舍与员工ToolStripMenuItem";
            this.宿舍与员工ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.宿舍与员工ToolStripMenuItem.Text = "宿舍与员工";
            this.宿舍与员工ToolStripMenuItem.Visible = false;
            this.宿舍与员工ToolStripMenuItem.Click += new System.EventHandler(this.宿舍与员工ToolStripMenuItem_Click);
            // 
            // 部门批量修改ToolStripMenuItem
            // 
            this.部门批量修改ToolStripMenuItem.Name = "部门批量修改ToolStripMenuItem";
            this.部门批量修改ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.部门批量修改ToolStripMenuItem.Text = "部门批量修改";
            this.部门批量修改ToolStripMenuItem.Visible = false;
            this.部门批量修改ToolStripMenuItem.Click += new System.EventHandler(this.部门批量修改ToolStripMenuItem_Click);
            // 
            // 导入上月房租ToolStripMenuItem
            // 
            this.导入上月房租ToolStripMenuItem.Name = "导入上月房租ToolStripMenuItem";
            this.导入上月房租ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.导入上月房租ToolStripMenuItem.Text = "导入上月房租";
            this.导入上月房租ToolStripMenuItem.Click += new System.EventHandler(this.导入上月房租ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLable1,
            this.lbName,
            this.toolStripStatusLabel1,
            this.lbAreaName,
            this.toolStripStatusLabel3,
            this.lbTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 497);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1099, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ToolStripStatusLable1
            // 
            this.ToolStripStatusLable1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripStatusLable1.Image")));
            this.ToolStripStatusLable1.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.ToolStripStatusLable1.Name = "ToolStripStatusLable1";
            this.ToolStripStatusLable1.Size = new System.Drawing.Size(28, 17);
            this.ToolStripStatusLable1.Text = " ";
            // 
            // lbName
            // 
            this.lbName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(40, 17);
            this.lbName.Text = "name";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel1.Image")));
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(250, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // lbAreaName
            // 
            this.lbAreaName.Name = "lbAreaName";
            this.lbAreaName.Size = new System.Drawing.Size(32, 17);
            this.lbAreaName.Text = "区别";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel3.Image")));
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(250, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel3.Text = " ";
            // 
            // lbTime
            // 
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(33, 17);
            this.lbTime.Text = "time";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1099, 519);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "后勤部宿舍管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 住宿登记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退宿登记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 宿舍与员工ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLable1;
        public System.Windows.Forms.ToolStripStatusLabel lbName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbAreaName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 系统管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作员ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变更密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 费用核算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他费用录入ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 水电费录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 月末核算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报表管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 宿舍查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入住员工查询ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 系统维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部门管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 宿舍区宿舍管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基本资料维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 厂内水电费ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空房统计报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在住员工报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 员工ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 黑名单管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 月水电费用查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调房信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部门批量修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退房员工费用ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关联工资系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 转换工资部门ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入上月房租ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 每月固定费用维护ToolStripMenuItem;
    }
}