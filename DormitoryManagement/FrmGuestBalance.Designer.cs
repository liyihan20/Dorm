namespace DormitoryManagement
{
    partial class FrmGuestBalance
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
            this.button1 = new System.Windows.Forms.Button();
            this.cbContinue = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.dtpRealOutDate = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.lbOutDate = new System.Windows.Forms.Label();
            this.lbSex = new System.Windows.Forms.Label();
            this.lbRent = new System.Windows.Forms.Label();
            this.lbCheckOut = new System.Windows.Forms.Label();
            this.lbPro = new System.Windows.Forms.Label();
            this.lbInDate = new System.Windows.Forms.Label();
            this.lbRoom = new System.Windows.Forms.Label();
            this.lbCharger = new System.Windows.Forms.Label();
            this.lbDep = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbContinue);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.tbSum);
            this.groupBox1.Controls.Add(this.nudDays);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.dtpRealOutDate);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.lbOutDate);
            this.groupBox1.Controls.Add(this.lbSex);
            this.groupBox1.Controls.Add(this.lbRent);
            this.groupBox1.Controls.Add(this.lbCheckOut);
            this.groupBox1.Controls.Add(this.lbPro);
            this.groupBox1.Controls.Add(this.lbInDate);
            this.groupBox1.Controls.Add(this.lbRoom);
            this.groupBox1.Controls.Add(this.lbCharger);
            this.groupBox1.Controls.Add(this.lbDep);
            this.groupBox1.Controls.Add(this.lbName);
            this.groupBox1.Controls.Add(this.tbComment);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 337);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入住用户结算信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(233, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 71;
            this.button1.Text = "结 算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbContinue
            // 
            this.cbContinue.AutoSize = true;
            this.cbContinue.Location = new System.Drawing.Point(449, 204);
            this.cbContinue.Name = "cbContinue";
            this.cbContinue.Size = new System.Drawing.Size(15, 14);
            this.cbContinue.TabIndex = 70;
            this.cbContinue.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(384, 205);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(59, 12);
            this.label25.TabIndex = 69;
            this.label25.Text = "继续入住:";
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(256, 201);
            this.tbSum.Name = "tbSum";
            this.tbSum.Size = new System.Drawing.Size(85, 21);
            this.tbSum.TabIndex = 68;
            // 
            // nudDays
            // 
            this.nudDays.Location = new System.Drawing.Point(117, 201);
            this.nudDays.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDays.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(68, 21);
            this.nudDays.TabIndex = 67;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(202, 205);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(47, 12);
            this.label24.TabIndex = 66;
            this.label24.Text = "总金额:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(44, 205);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 12);
            this.label23.TabIndex = 65;
            this.label23.Text = "实住天数:";
            // 
            // dtpRealOutDate
            // 
            this.dtpRealOutDate.CustomFormat = "yyyy-MM-dd";
            this.dtpRealOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRealOutDate.Location = new System.Drawing.Point(343, 159);
            this.dtpRealOutDate.Name = "dtpRealOutDate";
            this.dtpRealOutDate.Size = new System.Drawing.Size(98, 21);
            this.dtpRealOutDate.TabIndex = 64;
            this.dtpRealOutDate.Value = new System.DateTime(2012, 5, 18, 0, 0, 0, 0);
            this.dtpRealOutDate.ValueChanged += new System.EventHandler(this.dtpRealOutDate_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(254, 163);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(83, 12);
            this.label22.TabIndex = 63;
            this.label22.Text = "实际退宿日期:";
            // 
            // lbOutDate
            // 
            this.lbOutDate.AutoSize = true;
            this.lbOutDate.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbOutDate.Location = new System.Drawing.Point(133, 163);
            this.lbOutDate.Name = "lbOutDate";
            this.lbOutDate.Size = new System.Drawing.Size(17, 12);
            this.lbOutDate.TabIndex = 62;
            this.lbOutDate.Text = "  ";
            // 
            // lbSex
            // 
            this.lbSex.AutoSize = true;
            this.lbSex.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbSex.Location = new System.Drawing.Point(231, 41);
            this.lbSex.Name = "lbSex";
            this.lbSex.Size = new System.Drawing.Size(17, 12);
            this.lbSex.TabIndex = 61;
            this.lbSex.Text = "  ";
            // 
            // lbRent
            // 
            this.lbRent.AutoSize = true;
            this.lbRent.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbRent.Location = new System.Drawing.Point(231, 126);
            this.lbRent.Name = "lbRent";
            this.lbRent.Size = new System.Drawing.Size(17, 12);
            this.lbRent.TabIndex = 60;
            this.lbRent.Text = "  ";
            // 
            // lbCheckOut
            // 
            this.lbCheckOut.AutoSize = true;
            this.lbCheckOut.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbCheckOut.Location = new System.Drawing.Point(382, 78);
            this.lbCheckOut.Name = "lbCheckOut";
            this.lbCheckOut.Size = new System.Drawing.Size(17, 12);
            this.lbCheckOut.TabIndex = 59;
            this.lbCheckOut.Text = "  ";
            // 
            // lbPro
            // 
            this.lbPro.AutoSize = true;
            this.lbPro.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbPro.Location = new System.Drawing.Point(86, 78);
            this.lbPro.Name = "lbPro";
            this.lbPro.Size = new System.Drawing.Size(17, 12);
            this.lbPro.TabIndex = 58;
            this.lbPro.Text = "  ";
            // 
            // lbInDate
            // 
            this.lbInDate.AutoSize = true;
            this.lbInDate.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbInDate.Location = new System.Drawing.Point(382, 126);
            this.lbInDate.Name = "lbInDate";
            this.lbInDate.Size = new System.Drawing.Size(17, 12);
            this.lbInDate.TabIndex = 57;
            this.lbInDate.Text = "  ";
            // 
            // lbRoom
            // 
            this.lbRoom.AutoSize = true;
            this.lbRoom.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbRoom.Location = new System.Drawing.Point(86, 126);
            this.lbRoom.Name = "lbRoom";
            this.lbRoom.Size = new System.Drawing.Size(17, 12);
            this.lbRoom.TabIndex = 56;
            this.lbRoom.Text = "  ";
            // 
            // lbCharger
            // 
            this.lbCharger.AutoSize = true;
            this.lbCharger.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbCharger.Location = new System.Drawing.Point(231, 78);
            this.lbCharger.Name = "lbCharger";
            this.lbCharger.Size = new System.Drawing.Size(17, 12);
            this.lbCharger.TabIndex = 55;
            this.lbCharger.Text = "  ";
            // 
            // lbDep
            // 
            this.lbDep.AutoSize = true;
            this.lbDep.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lbDep.Location = new System.Drawing.Point(382, 41);
            this.lbDep.Name = "lbDep";
            this.lbDep.Size = new System.Drawing.Size(17, 12);
            this.lbDep.TabIndex = 54;
            this.lbDep.Text = "  ";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.ForeColor = System.Drawing.Color.Red;
            this.lbName.Location = new System.Drawing.Point(88, 41);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(17, 12);
            this.lbName.TabIndex = 53;
            this.lbName.Text = "  ";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(88, 247);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(355, 21);
            this.tbComment.TabIndex = 47;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(46, 249);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 12);
            this.label12.TabIndex = 52;
            this.label12.Text = "备注:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(44, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 50;
            this.label10.Text = "预计退宿日期:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(317, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 49;
            this.label9.Text = "入住日期:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(190, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 48;
            this.label8.Text = "性别:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(148, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 46;
            this.label7.Text = "房租(元/天):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(45, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 43;
            this.label6.Text = "房间:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(329, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "退房人:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(178, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 38;
            this.label4.Text = "责任人:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(45, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "性质:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(341, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "部门:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(47, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "姓名:";
            // 
            // FrmGuestBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 368);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGuestBalance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "进行结算";
            this.Load += new System.EventHandler(this.FrmGuestBalance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbContinue;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.DateTimePicker dtpRealOutDate;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbOutDate;
        private System.Windows.Forms.Label lbSex;
        private System.Windows.Forms.Label lbRent;
        private System.Windows.Forms.Label lbCheckOut;
        private System.Windows.Forms.Label lbPro;
        private System.Windows.Forms.Label lbInDate;
        private System.Windows.Forms.Label lbRoom;
        private System.Windows.Forms.Label lbCharger;
        private System.Windows.Forms.Label lbDep;
        private System.Windows.Forms.Label lbName;
    }
}