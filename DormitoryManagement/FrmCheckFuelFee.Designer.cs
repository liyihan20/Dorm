namespace DormitoryManagement
{
    partial class FrmCheckFuelFee
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbEleQty = new System.Windows.Forms.Label();
            this.lbHotQty = new System.Windows.Forms.Label();
            this.lbColdQty = new System.Windows.Forms.Label();
            this.lbTotalCold = new System.Windows.Forms.Label();
            this.lbTotalHot = new System.Windows.Forms.Label();
            this.lbTotalElec = new System.Windows.Forms.Label();
            this.lbUnitElec = new System.Windows.Forms.Label();
            this.lbUnitHot = new System.Windows.Forms.Label();
            this.lbUnitCold = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDorm = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbDorm);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbArea);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbMonth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 670);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "水电费用查询";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lbEleQty);
            this.groupBox2.Controls.Add(this.lbHotQty);
            this.groupBox2.Controls.Add(this.lbColdQty);
            this.groupBox2.Controls.Add(this.lbTotalCold);
            this.groupBox2.Controls.Add(this.lbTotalHot);
            this.groupBox2.Controls.Add(this.lbTotalElec);
            this.groupBox2.Controls.Add(this.lbUnitElec);
            this.groupBox2.Controls.Add(this.lbUnitHot);
            this.groupBox2.Controls.Add(this.lbUnitCold);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(16, 543);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(864, 121);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计信息";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label12.Location = new System.Drawing.Point(478, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "电费用量:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label11.Location = new System.Drawing.Point(318, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "热水用量:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label10.Location = new System.Drawing.Point(150, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "冷水用量:";
            // 
            // lbEleQty
            // 
            this.lbEleQty.AutoSize = true;
            this.lbEleQty.Location = new System.Drawing.Point(543, 55);
            this.lbEleQty.Name = "lbEleQty";
            this.lbEleQty.Size = new System.Drawing.Size(23, 12);
            this.lbEleQty.TabIndex = 6;
            this.lbEleQty.Text = "   ";
            // 
            // lbHotQty
            // 
            this.lbHotQty.AutoSize = true;
            this.lbHotQty.Location = new System.Drawing.Point(383, 55);
            this.lbHotQty.Name = "lbHotQty";
            this.lbHotQty.Size = new System.Drawing.Size(23, 12);
            this.lbHotQty.TabIndex = 6;
            this.lbHotQty.Text = "   ";
            // 
            // lbColdQty
            // 
            this.lbColdQty.AutoSize = true;
            this.lbColdQty.Location = new System.Drawing.Point(209, 55);
            this.lbColdQty.Name = "lbColdQty";
            this.lbColdQty.Size = new System.Drawing.Size(23, 12);
            this.lbColdQty.TabIndex = 6;
            this.lbColdQty.Text = "   ";
            // 
            // lbTotalCold
            // 
            this.lbTotalCold.AutoSize = true;
            this.lbTotalCold.Location = new System.Drawing.Point(209, 87);
            this.lbTotalCold.Name = "lbTotalCold";
            this.lbTotalCold.Size = new System.Drawing.Size(23, 12);
            this.lbTotalCold.TabIndex = 6;
            this.lbTotalCold.Text = "   ";
            // 
            // lbTotalHot
            // 
            this.lbTotalHot.AutoSize = true;
            this.lbTotalHot.Location = new System.Drawing.Point(383, 87);
            this.lbTotalHot.Name = "lbTotalHot";
            this.lbTotalHot.Size = new System.Drawing.Size(23, 12);
            this.lbTotalHot.TabIndex = 6;
            this.lbTotalHot.Text = "   ";
            // 
            // lbTotalElec
            // 
            this.lbTotalElec.AutoSize = true;
            this.lbTotalElec.Location = new System.Drawing.Point(543, 87);
            this.lbTotalElec.Name = "lbTotalElec";
            this.lbTotalElec.Size = new System.Drawing.Size(23, 12);
            this.lbTotalElec.TabIndex = 6;
            this.lbTotalElec.Text = "   ";
            // 
            // lbUnitElec
            // 
            this.lbUnitElec.AutoSize = true;
            this.lbUnitElec.Location = new System.Drawing.Point(543, 21);
            this.lbUnitElec.Name = "lbUnitElec";
            this.lbUnitElec.Size = new System.Drawing.Size(23, 12);
            this.lbUnitElec.TabIndex = 6;
            this.lbUnitElec.Text = "   ";
            // 
            // lbUnitHot
            // 
            this.lbUnitHot.AutoSize = true;
            this.lbUnitHot.Location = new System.Drawing.Point(383, 21);
            this.lbUnitHot.Name = "lbUnitHot";
            this.lbUnitHot.Size = new System.Drawing.Size(23, 12);
            this.lbUnitHot.TabIndex = 6;
            this.lbUnitHot.Text = "   ";
            // 
            // lbUnitCold
            // 
            this.lbUnitCold.AutoSize = true;
            this.lbUnitCold.Location = new System.Drawing.Point(209, 21);
            this.lbUnitCold.Name = "lbUnitCold";
            this.lbUnitCold.Size = new System.Drawing.Size(23, 12);
            this.lbUnitCold.TabIndex = 6;
            this.lbUnitCold.Text = "   ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label9.Location = new System.Drawing.Point(478, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "电费合计:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label8.Location = new System.Drawing.Point(318, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "热水合计:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label7.Location = new System.Drawing.Point(150, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "冷水合计:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label6.Location = new System.Drawing.Point(478, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "电费单价:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label5.Location = new System.Drawing.Point(318, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "热水单价:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(227)))));
            this.label4.Location = new System.Drawing.Point(150, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "冷水单价:";
            // 
            // cbDorm
            // 
            this.cbDorm.FormattingEnabled = true;
            this.cbDorm.Location = new System.Drawing.Point(496, 27);
            this.cbDorm.Name = "cbDorm";
            this.cbDorm.Size = new System.Drawing.Size(77, 20);
            this.cbDorm.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "房号:";
            // 
            // cbArea
            // 
            this.cbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Location = new System.Drawing.Point(336, 27);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(82, 20);
            this.cbArea.TabIndex = 2;
            this.cbArea.SelectedIndexChanged += new System.EventHandler(this.cbArea_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "宿舍区:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(864, 479);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(579, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(180, 27);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(72, 20);
            this.cbMonth.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "年月份:";
            // 
            // FrmCheckFuelFee
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 700);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCheckFuelFee";
            this.Text = "月水电费用查询";
            this.Load += new System.EventHandler(this.FrmCheckFuelFee_Load);
            this.Resize += new System.EventHandler(this.FrmCheckFuelFee_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDorm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbTotalCold;
        private System.Windows.Forms.Label lbTotalHot;
        private System.Windows.Forms.Label lbTotalElec;
        private System.Windows.Forms.Label lbUnitElec;
        private System.Windows.Forms.Label lbUnitHot;
        private System.Windows.Forms.Label lbUnitCold;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbEleQty;
        private System.Windows.Forms.Label lbHotQty;
        private System.Windows.Forms.Label lbColdQty;
    }
}