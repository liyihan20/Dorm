namespace DormitoryManagement
{
    partial class FrmOtherFeeInput
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbComment = new System.Windows.Forms.ComboBox();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOther = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHouse = new System.Windows.Forms.TextBox();
            this.tbFine = new System.Windows.Forms.TextBox();
            this.tbRepair = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbDormNumber = new System.Windows.Forms.Label();
            this.lbDormComent = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbMaxNum = new System.Windows.Forms.Label();
            this.lbDormType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.cbKey = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbComment);
            this.groupBox1.Controls.Add(this.rtbDescription);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbOther);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbHouse);
            this.groupBox1.Controls.Add(this.tbFine);
            this.groupBox1.Controls.Add(this.tbRepair);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lbDormNumber);
            this.groupBox1.Controls.Add(this.lbDormComent);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbMaxNum);
            this.groupBox1.Controls.Add(this.lbDormType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tbContent);
            this.groupBox1.Controls.Add(this.cbKey);
            this.groupBox1.Location = new System.Drawing.Point(24, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(817, 466);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "其他费用录入";
            // 
            // cbComment
            // 
            this.cbComment.FormattingEnabled = true;
            this.cbComment.Items.AddRange(new object[] {
            "退宿维修",
            "罚款",
            "补扣上月房租"});
            this.cbComment.Location = new System.Drawing.Point(97, 381);
            this.cbComment.Name = "cbComment";
            this.cbComment.Size = new System.Drawing.Size(655, 20);
            this.cbComment.TabIndex = 27;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Location = new System.Drawing.Point(97, 407);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(655, 24);
            this.rtbDescription.TabIndex = 23;
            this.rtbDescription.Text = "";
            this.rtbDescription.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Crimson;
            this.label9.Location = new System.Drawing.Point(18, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(557, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "说明:浅灰色的记录表示的是一周之内刚入住的新员工,下面输入的各项费用将被平均分摊到所选的员工上";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.Black;
            this.dateTimePicker1.Location = new System.Drawing.Point(505, 25);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(113, 21);
            this.dateTimePicker1.TabIndex = 25;
            this.dateTimePicker1.Value = new System.DateTime(2011, 11, 8, 0, 0, 0, 0);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(356, 437);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 386);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "费用说明";
            // 
            // tbOther
            // 
            this.tbOther.Location = new System.Drawing.Point(656, 331);
            this.tbOther.Name = "tbOther";
            this.tbOther.Size = new System.Drawing.Size(100, 21);
            this.tbOther.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(597, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "其他费用";
            // 
            // tbHouse
            // 
            this.tbHouse.Location = new System.Drawing.Point(458, 331);
            this.tbHouse.Name = "tbHouse";
            this.tbHouse.Size = new System.Drawing.Size(100, 21);
            this.tbHouse.TabIndex = 19;
            // 
            // tbFine
            // 
            this.tbFine.Location = new System.Drawing.Point(250, 331);
            this.tbFine.Name = "tbFine";
            this.tbFine.Size = new System.Drawing.Size(100, 21);
            this.tbFine.TabIndex = 18;
            // 
            // tbRepair
            // 
            this.tbRepair.Location = new System.Drawing.Point(101, 331);
            this.tbRepair.Name = "tbRepair";
            this.tbRepair.Size = new System.Drawing.Size(94, 21);
            this.tbRepair.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(6, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 178);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "宿舍成员列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择});
            this.dataGridView1.Location = new System.Drawing.Point(10, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(736, 152);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // 选择
            // 
            this.选择.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            this.选择.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.选择.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.选择.Width = 54;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(387, 334);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "招待所费用";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(165, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "宿舍类型";
            // 
            // lbDormNumber
            // 
            this.lbDormNumber.AutoSize = true;
            this.lbDormNumber.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDormNumber.Location = new System.Drawing.Point(81, 74);
            this.lbDormNumber.Name = "lbDormNumber";
            this.lbDormNumber.Size = new System.Drawing.Size(0, 12);
            this.lbDormNumber.TabIndex = 13;
            // 
            // lbDormComent
            // 
            this.lbDormComent.AutoSize = true;
            this.lbDormComent.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDormComent.Location = new System.Drawing.Point(480, 74);
            this.lbDormComent.Name = "lbDormComent";
            this.lbDormComent.Size = new System.Drawing.Size(0, 12);
            this.lbDormComent.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(445, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "收费日期";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "扣分";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "备注";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "可住人数";
            // 
            // lbMaxNum
            // 
            this.lbMaxNum.AutoSize = true;
            this.lbMaxNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbMaxNum.Location = new System.Drawing.Point(382, 74);
            this.lbMaxNum.Name = "lbMaxNum";
            this.lbMaxNum.Size = new System.Drawing.Size(0, 12);
            this.lbMaxNum.TabIndex = 6;
            // 
            // lbDormType
            // 
            this.lbDormType.AutoSize = true;
            this.lbDormType.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDormType.Location = new System.Drawing.Point(224, 74);
            this.lbDormType.Name = "lbDormType";
            this.lbDormType.Size = new System.Drawing.Size(0, 12);
            this.lbDormType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 334);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "维修费";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "宿舍编号";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(242, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(136, 26);
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(100, 21);
            this.tbContent.TabIndex = 1;
            // 
            // cbKey
            // 
            this.cbKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKey.FormattingEnabled = true;
            this.cbKey.Items.AddRange(new object[] {
            "宿舍编号",
            "员工厂牌编号",
            "账号"});
            this.cbKey.Location = new System.Drawing.Point(24, 26);
            this.cbKey.Name = "cbKey";
            this.cbKey.Size = new System.Drawing.Size(106, 20);
            this.cbKey.TabIndex = 0;
            // 
            // FrmOtherFeeInput
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 592);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOtherFeeInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其他费用录入";
            this.Load += new System.EventHandler(this.FrmOtherFeeInput_Load);
            this.Resize += new System.EventHandler(this.FrmOtherFeeInput_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbKey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbDormNumber;
        private System.Windows.Forms.Label lbDormComent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbMaxNum;
        private System.Windows.Forms.Label lbDormType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.TextBox tbHouse;
        private System.Windows.Forms.TextBox tbFine;
        private System.Windows.Forms.TextBox tbRepair;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOther;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbComment;
    }
}