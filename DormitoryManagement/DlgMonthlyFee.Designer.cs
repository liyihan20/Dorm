namespace DormitoryManagement
{
    partial class DlgMonthlyFee
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEmpName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudFee = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbComment = new System.Windows.Forms.ComboBox();
            this.tbDorm = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFee)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tbDorm);
            this.groupBox1.Controls.Add(this.cbComment);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nudFee);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbEmpName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "每月固定扣款信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "宿舍号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "姓名";
            // 
            // cbEmpName
            // 
            this.cbEmpName.FormattingEnabled = true;
            this.cbEmpName.Location = new System.Drawing.Point(240, 44);
            this.cbEmpName.Name = "cbEmpName";
            this.cbEmpName.Size = new System.Drawing.Size(99, 20);
            this.cbEmpName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(370, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "费用(元/月)";
            // 
            // nudFee
            // 
            this.nudFee.DecimalPlaces = 1;
            this.nudFee.Location = new System.Drawing.Point(447, 45);
            this.nudFee.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFee.Name = "nudFee";
            this.nudFee.Size = new System.Drawing.Size(74, 21);
            this.nudFee.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "扣费项目";
            // 
            // cbComment
            // 
            this.cbComment.FormattingEnabled = true;
            this.cbComment.Items.AddRange(new object[] {
            "加装有线电视",
            "电动车充电"});
            this.cbComment.Location = new System.Drawing.Point(90, 85);
            this.cbComment.Name = "cbComment";
            this.cbComment.Size = new System.Drawing.Size(431, 20);
            this.cbComment.TabIndex = 7;
            // 
            // tbDorm
            // 
            this.tbDorm.Location = new System.Drawing.Point(78, 43);
            this.tbDorm.Name = "tbDorm";
            this.tbDorm.Size = new System.Drawing.Size(85, 21);
            this.tbDorm.TabIndex = 8;
            this.tbDorm.Leave += new System.EventHandler(this.tbDorm_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(305, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DlgMonthlyFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 207);
            this.Controls.Add(this.groupBox1);
            this.Name = "DlgMonthlyFee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DlgMonthlyFee";
            this.Load += new System.EventHandler(this.DlgMonthlyFee_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudFee;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEmpName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDorm;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}