namespace DormitoryManagement
{
    partial class FrmMovingOutFee
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
            this.lbCap = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbDorm = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.tbOther = new System.Windows.Forms.TextBox();
            this.tbHouse = new System.Windows.Forms.TextBox();
            this.tbFine = new System.Windows.Forms.TextBox();
            this.tbRepair = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbEle = new System.Windows.Forms.TextBox();
            this.tbWater = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCap
            // 
            this.lbCap.AutoSize = true;
            this.lbCap.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.lbCap.Location = new System.Drawing.Point(364, 24);
            this.lbCap.Name = "lbCap";
            this.lbCap.Size = new System.Drawing.Size(88, 28);
            this.lbCap.TabIndex = 0;
            this.lbCap.Text = "caption";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "宿舍号/姓名:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(91, 107);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(862, 466);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(192, 80);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(89, 21);
            this.tbSearch.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "定位";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbWater);
            this.groupBox1.Controls.Add(this.tbEle);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lbName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lbDorm);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbID);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.tbComment);
            this.groupBox1.Controls.Add(this.tbOther);
            this.groupBox1.Controls.Add(this.tbHouse);
            this.groupBox1.Controls.Add(this.tbFine);
            this.groupBox1.Controls.Add(this.tbRepair);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(91, 579);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(862, 95);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "费用修改";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(145, 20);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(23, 12);
            this.lbName.TabIndex = 15;
            this.lbName.Text = "   ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "姓名:";
            // 
            // lbDorm
            // 
            this.lbDorm.AutoSize = true;
            this.lbDorm.Location = new System.Drawing.Point(59, 20);
            this.lbDorm.Name = "lbDorm";
            this.lbDorm.Size = new System.Drawing.Size(23, 12);
            this.lbDorm.TabIndex = 13;
            this.lbDorm.Text = "   ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "宿舍:";
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(789, 58);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(17, 12);
            this.lbID.TabIndex = 11;
            this.lbID.Text = "ID";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(680, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(397, 54);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(277, 21);
            this.tbComment.TabIndex = 9;
            // 
            // tbOther
            // 
            this.tbOther.Location = new System.Drawing.Point(680, 16);
            this.tbOther.Name = "tbOther";
            this.tbOther.Size = new System.Drawing.Size(61, 21);
            this.tbOther.TabIndex = 8;
            // 
            // tbHouse
            // 
            this.tbHouse.Location = new System.Drawing.Point(529, 16);
            this.tbHouse.Name = "tbHouse";
            this.tbHouse.Size = new System.Drawing.Size(65, 21);
            this.tbHouse.TabIndex = 7;
            // 
            // tbFine
            // 
            this.tbFine.Location = new System.Drawing.Point(397, 16);
            this.tbFine.Name = "tbFine";
            this.tbFine.Size = new System.Drawing.Size(62, 21);
            this.tbFine.TabIndex = 6;
            // 
            // tbRepair
            // 
            this.tbRepair.Location = new System.Drawing.Point(267, 16);
            this.tbRepair.Name = "tbRepair";
            this.tbRepair.Size = new System.Drawing.Size(59, 21);
            this.tbRepair.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "费用说明:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(621, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "其它费用:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "招待所:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "罚款:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "维修费:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "分摊水费:";            
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(202, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "分摊电费:";
            // 
            // tbEle
            // 
            this.tbEle.Location = new System.Drawing.Point(267, 54);
            this.tbEle.Name = "tbEle";
            this.tbEle.Size = new System.Drawing.Size(59, 21);
            this.tbEle.TabIndex = 18;
            // 
            // tbWater
            // 
            this.tbWater.Location = new System.Drawing.Point(91, 54);
            this.tbWater.Name = "tbWater";
            this.tbWater.Size = new System.Drawing.Size(70, 21);
            this.tbWater.TabIndex = 19;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(868, 79);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "反退宿";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FrmMovingOutFee
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1058, 700);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbCap);
            this.Name = "FrmMovingOutFee";
            this.Text = "员工退房费用";
            this.Load += new System.EventHandler(this.FrmMovingOutFee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.TextBox tbOther;
        private System.Windows.Forms.TextBox tbHouse;
        private System.Windows.Forms.TextBox tbFine;
        private System.Windows.Forms.TextBox tbRepair;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbDorm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbWater;
        private System.Windows.Forms.TextBox tbEle;
    }
}