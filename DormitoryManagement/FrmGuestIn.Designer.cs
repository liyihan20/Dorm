namespace DormitoryManagement
{
    partial class FrmGuestIn
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
            this.tbDays = new System.Windows.Forms.TextBox();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbRoom = new System.Windows.Forms.ComboBox();
            this.nbRent = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbSelf = new System.Windows.Forms.RadioButton();
            this.rdbBus = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbFemale = new System.Windows.Forms.RadioButton();
            this.rdbMale = new System.Windows.Forms.RadioButton();
            this.cbDep = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpOut = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpIn = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCheckOut = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCharge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbComment = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbRent)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbComment);
            this.groupBox1.Controls.Add(this.tbDays);
            this.groupBox1.Controls.Add(this.tbSum);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cbRoom);
            this.groupBox1.Controls.Add(this.nbRent);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.cbDep);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dtpOut);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.dtpIn);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbCheckOut);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbCharge);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 375);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "招待所入住登记";
            // 
            // tbDays
            // 
            this.tbDays.Location = new System.Drawing.Point(97, 251);
            this.tbDays.Name = "tbDays";
            this.tbDays.ReadOnly = true;
            this.tbDays.Size = new System.Drawing.Size(100, 21);
            this.tbDays.TabIndex = 13;
            this.tbDays.TextChanged += new System.EventHandler(this.nbRent_ValueChanged);
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(410, 251);
            this.tbSum.Name = "tbSum";
            this.tbSum.ReadOnly = true;
            this.tbSum.Size = new System.Drawing.Size(106, 21);
            this.tbSum.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label13.Location = new System.Drawing.Point(354, 255);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 33;
            this.label13.Text = "总金额:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label11.Location = new System.Drawing.Point(32, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 31;
            this.label11.Text = "住宿天数:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Location = new System.Drawing.Point(57, 298);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "备注:";
            // 
            // cbRoom
            // 
            this.cbRoom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbRoom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbRoom.FormattingEnabled = true;
            this.cbRoom.Location = new System.Drawing.Point(97, 170);
            this.cbRoom.Name = "cbRoom";
            this.cbRoom.Size = new System.Drawing.Size(100, 20);
            this.cbRoom.TabIndex = 9;
            this.cbRoom.SelectedIndexChanged += new System.EventHandler(this.cbRoom_SelectedIndexChanged);
            this.cbRoom.TextChanged += new System.EventHandler(this.cbRoom_TextChanged);
            // 
            // nbRent
            // 
            this.nbRent.DecimalPlaces = 1;
            this.nbRent.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nbRent.Location = new System.Drawing.Point(415, 169);
            this.nbRent.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nbRent.Name = "nbRent";
            this.nbRent.Size = new System.Drawing.Size(101, 21);
            this.nbRent.TabIndex = 27;
            this.nbRent.ValueChanged += new System.EventHandler(this.nbRent_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbSelf);
            this.panel2.Controls.Add(this.rdbBus);
            this.panel2.Location = new System.Drawing.Point(410, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(123, 35);
            this.panel2.TabIndex = 4;
            // 
            // rdbSelf
            // 
            this.rdbSelf.AutoSize = true;
            this.rdbSelf.Location = new System.Drawing.Point(70, 10);
            this.rdbSelf.Name = "rdbSelf";
            this.rdbSelf.Size = new System.Drawing.Size(47, 16);
            this.rdbSelf.TabIndex = 5;
            this.rdbSelf.Text = "私事";
            this.rdbSelf.UseVisualStyleBackColor = true;
            // 
            // rdbBus
            // 
            this.rdbBus.AutoSize = true;
            this.rdbBus.Checked = true;
            this.rdbBus.Location = new System.Drawing.Point(3, 9);
            this.rdbBus.Name = "rdbBus";
            this.rdbBus.Size = new System.Drawing.Size(47, 16);
            this.rdbBus.TabIndex = 4;
            this.rdbBus.TabStop = true;
            this.rdbBus.Text = "公事";
            this.rdbBus.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbFemale);
            this.panel1.Controls.Add(this.rdbMale);
            this.panel1.Location = new System.Drawing.Point(410, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 30);
            this.panel1.TabIndex = 2;
            // 
            // rdbFemale
            // 
            this.rdbFemale.AutoSize = true;
            this.rdbFemale.Location = new System.Drawing.Point(70, 7);
            this.rdbFemale.Name = "rdbFemale";
            this.rdbFemale.Size = new System.Drawing.Size(35, 16);
            this.rdbFemale.TabIndex = 3;
            this.rdbFemale.Text = "女";
            this.rdbFemale.UseVisualStyleBackColor = true;
            // 
            // rdbMale
            // 
            this.rdbMale.AutoSize = true;
            this.rdbMale.Checked = true;
            this.rdbMale.Location = new System.Drawing.Point(5, 7);
            this.rdbMale.Name = "rdbMale";
            this.rdbMale.Size = new System.Drawing.Size(35, 16);
            this.rdbMale.TabIndex = 2;
            this.rdbMale.TabStop = true;
            this.rdbMale.Text = "男";
            this.rdbMale.UseVisualStyleBackColor = true;
            // 
            // cbDep
            // 
            this.cbDep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDep.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDep.FormattingEnabled = true;
            this.cbDep.Location = new System.Drawing.Point(97, 84);
            this.cbDep.Name = "cbDep";
            this.cbDep.Size = new System.Drawing.Size(124, 20);
            this.cbDep.TabIndex = 3;
            this.cbDep.SelectedIndexChanged += new System.EventHandler(this.cbDep_SelectedIndexChanged);
            this.cbDep.TextChanged += new System.EventHandler(this.cbDep_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 29);
            this.button1.TabIndex = 26;
            this.button1.Text = "登记";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpOut
            // 
            this.dtpOut.CustomFormat = "yyyy-MM-dd";
            this.dtpOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOut.Location = new System.Drawing.Point(416, 211);
            this.dtpOut.Name = "dtpOut";
            this.dtpOut.Size = new System.Drawing.Size(100, 21);
            this.dtpOut.TabIndex = 12;
            this.dtpOut.Value = new System.DateTime(2012, 5, 11, 0, 0, 0, 0);
            this.dtpOut.ValueChanged += new System.EventHandler(this.dtpIn_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Location = new System.Drawing.Point(318, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "预计退宿日期:";
            // 
            // dtpIn
            // 
            this.dtpIn.CustomFormat = "yyyy-MM-dd";
            this.dtpIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIn.Location = new System.Drawing.Point(97, 213);
            this.dtpIn.Name = "dtpIn";
            this.dtpIn.Size = new System.Drawing.Size(100, 21);
            this.dtpIn.TabIndex = 11;
            this.dtpIn.Value = new System.DateTime(2012, 5, 11, 0, 0, 0, 0);
            this.dtpIn.ValueChanged += new System.EventHandler(this.dtpIn_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(32, 217);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "入住日期:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(366, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "性别:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(324, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "房租(元/天):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(56, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "房间:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(354, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "退房人:";
            // 
            // tbCheckOut
            // 
            this.tbCheckOut.Location = new System.Drawing.Point(416, 125);
            this.tbCheckOut.Name = "tbCheckOut";
            this.tbCheckOut.Size = new System.Drawing.Size(100, 21);
            this.tbCheckOut.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(44, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "责任人:";
            // 
            // tbCharge
            // 
            this.tbCharge.Location = new System.Drawing.Point(97, 127);
            this.tbCharge.Name = "tbCharge";
            this.tbCharge.Size = new System.Drawing.Size(100, 21);
            this.tbCharge.TabIndex = 7;
            this.tbCharge.TextChanged += new System.EventHandler(this.tbCharge_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(366, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "性质:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(56, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "部门:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(97, 37);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 21);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(56, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名:";
            // 
            // cbComment
            // 
            this.cbComment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbComment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbComment.FormattingEnabled = true;
            this.cbComment.Items.AddRange(new object[] {
            "客户来访",
            "回厂处理事务",
            "顾问入住",
            "报到",
            "面试"});
            this.cbComment.Location = new System.Drawing.Point(97, 293);
            this.cbComment.Name = "cbComment";
            this.cbComment.Size = new System.Drawing.Size(416, 20);
            this.cbComment.TabIndex = 34;
            // 
            // FrmGuestIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 398);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGuestIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmGuestIn";
            this.Load += new System.EventHandler(this.FrmGuestIn_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbRent)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpOut;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpIn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCheckOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCharge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDep;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdbSelf;
        private System.Windows.Forms.RadioButton rdbBus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbFemale;
        private System.Windows.Forms.RadioButton rdbMale;
        private System.Windows.Forms.NumericUpDown nbRent;
        private System.Windows.Forms.ComboBox cbRoom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbDays;
        private System.Windows.Forms.ComboBox cbComment;
    }
}