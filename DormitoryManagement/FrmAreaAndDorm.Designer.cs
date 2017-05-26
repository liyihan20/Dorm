namespace DormitoryManagement
{
    partial class FrmAreaAndDorm
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
            this.button1 = new System.Windows.Forms.Button();
            this.rtbAreaComment = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAreaName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dormitoryareaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.areaDataSet = new DormitoryManagement.AreaDataSet();
            this.dormitory_areaTableAdapter = new DormitoryManagement.AreaDataSetTableAdapters.dormitory_areaTableAdapter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbManage = new System.Windows.Forms.TextBox();
            this.tbRent = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbDormSex = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.rtbDormComment = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbAva = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAreaNum = new System.Windows.Forms.ComboBox();
            this.cbDormType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNumberTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNumberFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormitoryareaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaDataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.rtbAreaComment);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbAreaName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBox1.Location = new System.Drawing.Point(24, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "宿舍区信息管理";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(192, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbAreaComment
            // 
            this.rtbAreaComment.Location = new System.Drawing.Point(112, 93);
            this.rtbAreaComment.Name = "rtbAreaComment";
            this.rtbAreaComment.Size = new System.Drawing.Size(173, 54);
            this.rtbAreaComment.TabIndex = 4;
            this.rtbAreaComment.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label2.Location = new System.Drawing.Point(63, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "备注:";
            // 
            // tbAreaName
            // 
            this.tbAreaName.Location = new System.Drawing.Point(112, 45);
            this.tbAreaName.Name = "tbAreaName";
            this.tbAreaName.Size = new System.Drawing.Size(122, 21);
            this.tbAreaName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label1.Location = new System.Drawing.Point(39, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "宿舍区名:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dormitoryareaBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(327, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(459, 192);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "序号";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "备注";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dormitoryareaBindingSource
            // 
            this.dormitoryareaBindingSource.DataMember = "dormitory_area";
            this.dormitoryareaBindingSource.DataSource = this.areaDataSet;
            // 
            // areaDataSet
            // 
            this.areaDataSet.DataSetName = "AreaDataSet";
            this.areaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dormitory_areaTableAdapter
            // 
            this.dormitory_areaTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbManage);
            this.groupBox2.Controls.Add(this.tbRent);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cbDormSex);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.rtbDormComment);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbAva);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbAreaNum);
            this.groupBox2.Controls.Add(this.cbDormType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbNumberTo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbNumberFrom);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBox2.Location = new System.Drawing.Point(24, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(805, 423);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "宿舍管理";
            // 
            // tbManage
            // 
            this.tbManage.Location = new System.Drawing.Point(74, 290);
            this.tbManage.Name = "tbManage";
            this.tbManage.Size = new System.Drawing.Size(100, 21);
            this.tbManage.TabIndex = 25;
            // 
            // tbRent
            // 
            this.tbRent.Location = new System.Drawing.Point(74, 254);
            this.tbRent.Name = "tbRent";
            this.tbRent.Size = new System.Drawing.Size(100, 21);
            this.tbRent.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label12.Location = new System.Drawing.Point(29, 257);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "租金:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label11.Location = new System.Drawing.Point(17, 293);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "管理费:";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.OrangeRed;
            this.label10.Location = new System.Drawing.Point(15, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(207, 26);
            this.label10.TabIndex = 21;
            this.label10.Text = "例如:11201到11210，批量新增10间同类型的宿舍";
            // 
            // cbDormSex
            // 
            this.cbDormSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDormSex.FormattingEnabled = true;
            this.cbDormSex.Items.AddRange(new object[] {
            "男",
            "女",
            "不限"});
            this.cbDormSex.Location = new System.Drawing.Point(74, 184);
            this.cbDormSex.Name = "cbDormSex";
            this.cbDormSex.Size = new System.Drawing.Size(100, 20);
            this.cbDormSex.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label7.Location = new System.Drawing.Point(5, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "入住性别:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(601, 387);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 18;
            this.button5.Text = "查看全部";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button4.Location = new System.Drawing.Point(457, 387);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "保存/修改";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.button3.Location = new System.Drawing.Point(327, 387);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "查看预览";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(254, 29);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(532, 348);
            this.dataGridView2.TabIndex = 15;
            this.dataGridView2.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView2_DataBindingComplete);
            // 
            // rtbDormComment
            // 
            this.rtbDormComment.Location = new System.Drawing.Point(74, 326);
            this.rtbDormComment.Name = "rtbDormComment";
            this.rtbDormComment.Size = new System.Drawing.Size(132, 51);
            this.rtbDormComment.TabIndex = 14;
            this.rtbDormComment.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label9.Location = new System.Drawing.Point(29, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "备注:";
            // 
            // cbAva
            // 
            this.cbAva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAva.FormattingEnabled = true;
            this.cbAva.Items.AddRange(new object[] {
            "可用",
            "禁用"});
            this.cbAva.Location = new System.Drawing.Point(74, 219);
            this.cbAva.Name = "cbAva";
            this.cbAva.Size = new System.Drawing.Size(100, 20);
            this.cbAva.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label8.Location = new System.Drawing.Point(5, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "是否可用:";
            // 
            // cbAreaNum
            // 
            this.cbAreaNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAreaNum.FormattingEnabled = true;
            this.cbAreaNum.Location = new System.Drawing.Point(74, 105);
            this.cbAreaNum.Name = "cbAreaNum";
            this.cbAreaNum.Size = new System.Drawing.Size(100, 20);
            this.cbAreaNum.TabIndex = 8;
            // 
            // cbDormType
            // 
            this.cbDormType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDormType.FormattingEnabled = true;
            this.cbDormType.Location = new System.Drawing.Point(74, 146);
            this.cbDormType.Name = "cbDormType";
            this.cbDormType.Size = new System.Drawing.Size(100, 20);
            this.cbDormType.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label6.Location = new System.Drawing.Point(5, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "宿舍类型:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label5.Location = new System.Drawing.Point(29, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "区号:";
            // 
            // tbNumberTo
            // 
            this.tbNumberTo.Location = new System.Drawing.Point(162, 29);
            this.tbNumberTo.Name = "tbNumberTo";
            this.tbNumberTo.Size = new System.Drawing.Size(60, 21);
            this.tbNumberTo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label4.Location = new System.Drawing.Point(139, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "到";
            // 
            // tbNumberFrom
            // 
            this.tbNumberFrom.Location = new System.Drawing.Point(74, 29);
            this.tbNumberFrom.Name = "tbNumberFrom";
            this.tbNumberFrom.Size = new System.Drawing.Size(59, 21);
            this.tbNumberFrom.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label3.Location = new System.Drawing.Point(15, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "宿舍编号:";
            // 
            // FrmAreaAndDorm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(903, 714);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAreaAndDorm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "宿舍区/宿舍管理";
            this.Load += new System.EventHandler(this.FrmAreaAndDorm_Load);
            this.Resize += new System.EventHandler(this.FrmAreaAndDorm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dormitoryareaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaDataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbAreaComment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAreaName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private AreaDataSet areaDataSet;
        private System.Windows.Forms.BindingSource dormitoryareaBindingSource;
        private AreaDataSetTableAdapters.dormitory_areaTableAdapter dormitory_areaTableAdapter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.RichTextBox rtbDormComment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbAva;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbAreaNum;
        private System.Windows.Forms.ComboBox cbDormType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNumberTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNumberFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDormSex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbManage;
        private System.Windows.Forms.TextBox tbRent;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}