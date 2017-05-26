namespace DormitoryManagement
{
    partial class FrmBaseInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbNumber = new System.Windows.Forms.ComboBox();
            this.cbCharge = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbManage = new System.Windows.Forms.TextBox();
            this.tbRent = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managecostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chargemodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxnumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dormitorytypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dormTypeDataSet = new DormitoryManagement.DormTypeDataSet();
            this.dormitory_typeTableAdapter = new DormitoryManagement.DormTypeDataSetTableAdapters.dormitory_typeTableAdapter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbFee = new System.Windows.Forms.TextBox();
            this.lbFeeName = new System.Windows.Forms.Label();
            this.lbUnit = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormitorytypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormTypeDataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.cbNumber);
            this.groupBox1.Controls.Add(this.cbCharge);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tbManage);
            this.groupBox1.Controls.Add(this.tbRent);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBox1.Location = new System.Drawing.Point(290, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 367);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "宿舍类型信息管理";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(562, 316);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "新增/修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbNumber
            // 
            this.cbNumber.FormattingEnabled = true;
            this.cbNumber.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8"});
            this.cbNumber.Location = new System.Drawing.Point(562, 250);
            this.cbNumber.Name = "cbNumber";
            this.cbNumber.Size = new System.Drawing.Size(100, 20);
            this.cbNumber.TabIndex = 11;
            // 
            // cbCharge
            // 
            this.cbCharge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCharge.FormattingEnabled = true;
            this.cbCharge.Items.AddRange(new object[] {
            "分摊到人",
            "按宿舍"});
            this.cbCharge.Location = new System.Drawing.Point(562, 197);
            this.cbCharge.Name = "cbCharge";
            this.cbCharge.Size = new System.Drawing.Size(100, 20);
            this.cbCharge.TabIndex = 10;
            this.cbCharge.SelectedIndexChanged += new System.EventHandler(this.cbCharge_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(668, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbManage
            // 
            this.tbManage.Location = new System.Drawing.Point(562, 148);
            this.tbManage.Name = "tbManage";
            this.tbManage.Size = new System.Drawing.Size(100, 21);
            this.tbManage.TabIndex = 8;
            // 
            // tbRent
            // 
            this.tbRent.Location = new System.Drawing.Point(562, 93);
            this.tbRent.Name = "tbRent";
            this.tbRent.Size = new System.Drawing.Size(100, 21);
            this.tbRent.TabIndex = 7;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(562, 41);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 21);
            this.tbName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label5.Location = new System.Drawing.Point(497, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "可住人数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label4.Location = new System.Drawing.Point(497, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "收费方式:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label3.Location = new System.Drawing.Point(509, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "管理费:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label2.Location = new System.Drawing.Point(497, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "标准租金:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label1.Location = new System.Drawing.Point(521, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.rentDataGridViewTextBoxColumn,
            this.managecostDataGridViewTextBoxColumn,
            this.chargemodeDataGridViewTextBoxColumn,
            this.maxnumberDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dormitorytypeBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(22, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(443, 307);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 80;
            // 
            // rentDataGridViewTextBoxColumn
            // 
            this.rentDataGridViewTextBoxColumn.DataPropertyName = "rent";
            this.rentDataGridViewTextBoxColumn.HeaderText = "标准租金";
            this.rentDataGridViewTextBoxColumn.Name = "rentDataGridViewTextBoxColumn";
            this.rentDataGridViewTextBoxColumn.ReadOnly = true;
            this.rentDataGridViewTextBoxColumn.Width = 80;
            // 
            // managecostDataGridViewTextBoxColumn
            // 
            this.managecostDataGridViewTextBoxColumn.DataPropertyName = "manage_cost";
            this.managecostDataGridViewTextBoxColumn.HeaderText = "管理费";
            this.managecostDataGridViewTextBoxColumn.Name = "managecostDataGridViewTextBoxColumn";
            this.managecostDataGridViewTextBoxColumn.ReadOnly = true;
            this.managecostDataGridViewTextBoxColumn.Width = 80;
            // 
            // chargemodeDataGridViewTextBoxColumn
            // 
            this.chargemodeDataGridViewTextBoxColumn.DataPropertyName = "charge_mode";
            this.chargemodeDataGridViewTextBoxColumn.HeaderText = "收费方式";
            this.chargemodeDataGridViewTextBoxColumn.Name = "chargemodeDataGridViewTextBoxColumn";
            this.chargemodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.chargemodeDataGridViewTextBoxColumn.Width = 80;
            // 
            // maxnumberDataGridViewTextBoxColumn
            // 
            this.maxnumberDataGridViewTextBoxColumn.DataPropertyName = "max_number";
            this.maxnumberDataGridViewTextBoxColumn.HeaderText = "可住人数";
            this.maxnumberDataGridViewTextBoxColumn.Name = "maxnumberDataGridViewTextBoxColumn";
            this.maxnumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.maxnumberDataGridViewTextBoxColumn.Width = 80;
            // 
            // dormitorytypeBindingSource
            // 
            this.dormitorytypeBindingSource.DataMember = "dormitory_type";
            this.dormitorytypeBindingSource.DataSource = this.dormTypeDataSet;
            // 
            // dormTypeDataSet
            // 
            this.dormTypeDataSet.DataSetName = "DormTypeDataSet";
            this.dormTypeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dormitory_typeTableAdapter
            // 
            this.dormitory_typeTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbFee);
            this.groupBox2.Controls.Add(this.lbFeeName);
            this.groupBox2.Controls.Add(this.lbUnit);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBox2.Location = new System.Drawing.Point(290, 426);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 182);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "水电与押金单位费用";
            // 
            // tbFee
            // 
            this.tbFee.Location = new System.Drawing.Point(578, 81);
            this.tbFee.Name = "tbFee";
            this.tbFee.Size = new System.Drawing.Size(71, 21);
            this.tbFee.TabIndex = 6;
            // 
            // lbFeeName
            // 
            this.lbFeeName.AutoSize = true;
            this.lbFeeName.Location = new System.Drawing.Point(590, 44);
            this.lbFeeName.Name = "lbFeeName";
            this.lbFeeName.Size = new System.Drawing.Size(0, 12);
            this.lbFeeName.TabIndex = 5;
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(662, 84);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(0, 12);
            this.lbUnit.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label7.Location = new System.Drawing.Point(513, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "单位价格:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label6.Location = new System.Drawing.Point(513, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "费用名称:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(22, 31);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(443, 136);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(567, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 630);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBaseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本资料维护";
            this.Load += new System.EventHandler(this.FrmBaseInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormitorytypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormTypeDataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DormTypeDataSet dormTypeDataSet;
        private System.Windows.Forms.BindingSource dormitorytypeBindingSource;
        private DormTypeDataSetTableAdapters.dormitory_typeTableAdapter dormitory_typeTableAdapter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbNumber;
        private System.Windows.Forms.ComboBox cbCharge;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbManage;
        private System.Windows.Forms.TextBox tbRent;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn managecostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chargemodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxnumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbFee;
        private System.Windows.Forms.Label lbFeeName;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}