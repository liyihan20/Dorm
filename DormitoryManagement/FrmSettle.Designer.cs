namespace DormitoryManagement
{
    partial class FrmSettle
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRent = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbManage = new System.Windows.Forms.TextBox();
            this.tbGuarantee = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.IDdorm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dormName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dormType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salaryType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manageCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guarantee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classifyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbClassifyType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(458, 166);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(18, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 192);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待分配宿舍员工";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Location = new System.Drawing.Point(508, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(469, 191);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "可分配宿舍";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView2.Location = new System.Drawing.Point(9, 20);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(454, 165);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            this.dataGridView2.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView2_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "姓名:";
            // 
            // tbRent
            // 
            this.tbRent.Location = new System.Drawing.Point(495, 252);
            this.tbRent.Name = "tbRent";
            this.tbRent.Size = new System.Drawing.Size(59, 21);
            this.tbRent.TabIndex = 4;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(80, 258);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(0, 12);
            this.lbName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "宿舍编号:";
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(215, 256);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(0, 12);
            this.lbNumber.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "实际租金:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(586, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "管理费:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(728, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "押金:";
            // 
            // tbManage
            // 
            this.tbManage.Location = new System.Drawing.Point(639, 252);
            this.tbManage.Name = "tbManage";
            this.tbManage.Size = new System.Drawing.Size(58, 21);
            this.tbManage.TabIndex = 11;
            // 
            // tbGuarantee
            // 
            this.tbGuarantee.Location = new System.Drawing.Point(769, 252);
            this.tbGuarantee.Name = "tbGuarantee";
            this.tbGuarantee.Size = new System.Drawing.Size(56, 21);
            this.tbGuarantee.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(853, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 25);
            this.button1.TabIndex = 13;
            this.button1.Text = "加入预览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Location = new System.Drawing.Point(23, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(948, 240);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "宿舍安排预览";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDdorm,
            this.IDemp,
            this.dormName,
            this.dormType,
            this.empName,
            this.sex,
            this.salaryType,
            this.rent,
            this.manageCost,
            this.guarantee,
            this.classifyType,
            this.inDate});
            this.dataGridView3.Location = new System.Drawing.Point(8, 20);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(934, 214);
            this.dataGridView3.TabIndex = 0;
            // 
            // IDdorm
            // 
            this.IDdorm.HeaderText = "宿舍ID";
            this.IDdorm.Name = "IDdorm";
            this.IDdorm.ReadOnly = true;
            this.IDdorm.Visible = false;
            // 
            // IDemp
            // 
            this.IDemp.HeaderText = "员工ID";
            this.IDemp.Name = "IDemp";
            this.IDemp.ReadOnly = true;
            this.IDemp.Visible = false;
            // 
            // dormName
            // 
            this.dormName.HeaderText = "宿舍编号";
            this.dormName.Name = "dormName";
            this.dormName.ReadOnly = true;
            this.dormName.Width = 80;
            // 
            // dormType
            // 
            this.dormType.HeaderText = "宿舍类型";
            this.dormType.Name = "dormType";
            this.dormType.ReadOnly = true;
            // 
            // empName
            // 
            this.empName.HeaderText = "员工姓名";
            this.empName.Name = "empName";
            this.empName.ReadOnly = true;
            this.empName.Width = 80;
            // 
            // sex
            // 
            this.sex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sex.HeaderText = "性别";
            this.sex.Name = "sex";
            this.sex.ReadOnly = true;
            // 
            // salaryType
            // 
            this.salaryType.HeaderText = "工资类型";
            this.salaryType.Name = "salaryType";
            this.salaryType.ReadOnly = true;
            // 
            // rent
            // 
            this.rent.HeaderText = "实际租金";
            this.rent.Name = "rent";
            this.rent.ReadOnly = true;
            this.rent.Width = 90;
            // 
            // manageCost
            // 
            this.manageCost.HeaderText = "管理费";
            this.manageCost.Name = "manageCost";
            this.manageCost.ReadOnly = true;
            this.manageCost.Width = 90;
            // 
            // guarantee
            // 
            this.guarantee.HeaderText = "押金";
            this.guarantee.Name = "guarantee";
            this.guarantee.ReadOnly = true;
            this.guarantee.Width = 90;
            // 
            // classifyType
            // 
            this.classifyType.HeaderText = "分类性质";
            this.classifyType.Name = "classifyType";
            this.classifyType.ReadOnly = true;
            // 
            // inDate
            // 
            this.inDate.HeaderText = "入住日期";
            this.inDate.Name = "inDate";
            this.inDate.ReadOnly = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(375, 546);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 29);
            this.button3.TabIndex = 16;
            this.button3.Text = "全部保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(517, 546);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 29);
            this.button4.TabIndex = 17;
            this.button4.Text = "全部重新分配";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "分类性质:";
            // 
            // cbClassifyType
            // 
            this.cbClassifyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClassifyType.FormattingEnabled = true;
            this.cbClassifyType.Items.AddRange(new object[] {
            "计",
            "公",
            "港",
            "高"});
            this.cbClassifyType.Location = new System.Drawing.Point(356, 252);
            this.cbClassifyType.Name = "cbClassifyType";
            this.cbClassifyType.Size = new System.Drawing.Size(53, 20);
            this.cbClassifyType.TabIndex = 19;
            // 
            // FrmSettle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 587);
            this.Controls.Add(this.cbClassifyType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbGuarantee);
            this.Controls.Add(this.tbManage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbRent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSettle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "住宿安排";
            this.Load += new System.EventHandler(this.FrmSettle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRent;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbManage;
        private System.Windows.Forms.TextBox tbGuarantee;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbClassifyType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDdorm;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dormName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dormType;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn salaryType;
        private System.Windows.Forms.DataGridViewTextBoxColumn rent;
        private System.Windows.Forms.DataGridViewTextBoxColumn manageCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn guarantee;
        private System.Windows.Forms.DataGridViewTextBoxColumn classifyType;
        private System.Windows.Forms.DataGridViewTextBoxColumn inDate;
    }
}