namespace DormitoryManagement
{
    partial class FrmAdvanceDep
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dormitorydepartmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.advanceDepDataSet = new DormitoryManagement.AdvanceDepDataSet();
            this.dormitory_departmentTableAdapter = new DormitoryManagement.AdvanceDepDataSetTableAdapters.dormitory_departmentTableAdapter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtb2 = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lbIn = new System.Windows.Forms.ListBox();
            this.cbDepIn = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbOut = new System.Windows.Forms.ListBox();
            this.cbDepOut = new System.Windows.Forms.ComboBox();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.del = new System.Windows.Forms.DataGridViewButtonColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormitorydepartmentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advanceDepDataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(35, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 700);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "部门管理";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtb1);
            this.panel1.Location = new System.Drawing.Point(26, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 93);
            this.panel1.TabIndex = 3;
            // 
            // rtb1
            // 
            this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb1.Location = new System.Drawing.Point(0, 0);
            this.rtb1.Name = "rtb1";
            this.rtb1.ReadOnly = true;
            this.rtb1.Size = new System.Drawing.Size(524, 93);
            this.rtb1.TabIndex = 0;
            this.rtb1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(228, 662);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.del,
            this.numberDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.propertyDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dormitorydepartmentBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(26, 136);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(524, 520);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // dormitorydepartmentBindingSource
            // 
            this.dormitorydepartmentBindingSource.DataMember = "dormitory_department";
            this.dormitorydepartmentBindingSource.DataSource = this.advanceDepDataSet;
            // 
            // advanceDepDataSet
            // 
            this.advanceDepDataSet.DataSetName = "AdvanceDepDataSet";
            this.advanceDepDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dormitory_departmentTableAdapter
            // 
            this.dormitory_departmentTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.tbSearch);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.lbIn);
            this.groupBox2.Controls.Add(this.cbDepIn);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.lbOut);
            this.groupBox2.Controls.Add(this.cbDepOut);
            this.groupBox2.Location = new System.Drawing.Point(622, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(616, 700);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "员工部门修改";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 667);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "姓名/账号:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(165, 662);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "搜索";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(77, 662);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(82, 21);
            this.tbSearch.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtb2);
            this.panel2.Location = new System.Drawing.Point(20, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(574, 93);
            this.panel2.TabIndex = 9;
            // 
            // rtb2
            // 
            this.rtb2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb2.Location = new System.Drawing.Point(0, 0);
            this.rtb2.Name = "rtb2";
            this.rtb2.ReadOnly = true;
            this.rtb2.Size = new System.Drawing.Size(574, 93);
            this.rtb2.TabIndex = 0;
            this.rtb2.Text = "";
            this.rtb2.WordWrap = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(259, 363);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "<< 撤销转移";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(372, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "转入部门：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "转出部门：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(270, 662);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbIn
            // 
            this.lbIn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbIn.FormattingEnabled = true;
            this.lbIn.ItemHeight = 14;
            this.lbIn.Location = new System.Drawing.Point(374, 176);
            this.lbIn.Name = "lbIn";
            this.lbIn.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbIn.Size = new System.Drawing.Size(220, 480);
            this.lbIn.TabIndex = 4;
            // 
            // cbDepIn
            // 
            this.cbDepIn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDepIn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDepIn.FormattingEnabled = true;
            this.cbDepIn.Location = new System.Drawing.Point(443, 132);
            this.cbDepIn.Name = "cbDepIn";
            this.cbDepIn.Size = new System.Drawing.Size(151, 20);
            this.cbDepIn.TabIndex = 3;
            this.cbDepIn.Leave += new System.EventHandler(this.cbDepIn_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(259, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "转移部门 >>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbOut
            // 
            this.lbOut.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOut.FormattingEnabled = true;
            this.lbOut.ItemHeight = 14;
            this.lbOut.Location = new System.Drawing.Point(20, 176);
            this.lbOut.Name = "lbOut";
            this.lbOut.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbOut.Size = new System.Drawing.Size(220, 480);
            this.lbOut.TabIndex = 1;
            // 
            // cbDepOut
            // 
            this.cbDepOut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDepOut.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDepOut.FormattingEnabled = true;
            this.cbDepOut.Location = new System.Drawing.Point(89, 131);
            this.cbDepOut.Name = "cbDepOut";
            this.cbDepOut.Size = new System.Drawing.Size(151, 20);
            this.cbDepOut.TabIndex = 0;
            this.cbDepOut.SelectedIndexChanged += new System.EventHandler(this.cbDepOut_SelectedIndexChanged);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // del
            // 
            this.del.HeaderText = "删除";
            this.del.Name = "del";
            this.del.Text = "删除";
            this.del.UseColumnTextForButtonValue = true;
            this.del.Width = 60;
            // 
            // numberDataGridViewTextBoxColumn
            // 
            this.numberDataGridViewTextBoxColumn.DataPropertyName = "number";
            this.numberDataGridViewTextBoxColumn.HeaderText = "编号";
            this.numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            this.numberDataGridViewTextBoxColumn.Width = 60;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // propertyDataGridViewTextBoxColumn
            // 
            this.propertyDataGridViewTextBoxColumn.DataPropertyName = "property";
            this.propertyDataGridViewTextBoxColumn.HeaderText = "属性";
            this.propertyDataGridViewTextBoxColumn.Items.AddRange(new object[] {
            "厂内",
            "厂外",
            "光电",
            "半导体",
            "工业",
            "仪器",
            "电子",
            "惠州",
            "后勤部",
            "巴黎酒店",
            "特殊部门",
            "信元公司"});
            this.propertyDataGridViewTextBoxColumn.Name = "propertyDataGridViewTextBoxColumn";
            this.propertyDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.propertyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "备注";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            // 
            // FrmAdvanceDep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 753);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAdvanceDep";
            this.Text = "部门管理";
            this.Load += new System.EventHandler(this.FrmAdvanceDep_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormitorydepartmentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advanceDepDataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private AdvanceDepDataSet advanceDepDataSet;
        private System.Windows.Forms.BindingSource dormitorydepartmentBindingSource;
        private AdvanceDepDataSetTableAdapters.dormitory_departmentTableAdapter dormitory_departmentTableAdapter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox lbIn;
        private System.Windows.Forms.ComboBox cbDepIn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbOut;
        private System.Windows.Forms.ComboBox cbDepOut;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtb1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtb2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn del;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn propertyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}