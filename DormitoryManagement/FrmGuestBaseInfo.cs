using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmGuestBaseInfo : Form
    {
        public FrmGuestBaseInfo()
        {
            InitializeComponent();
        }

        private void FrmGuestBaseInfo_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“guestRoomDataSet.guest_room”中。您可以根据需要移动或删除它。
            this.guest_roomTableAdapter.Fill(this.guestRoomDataSet.guest_room);
            // TODO: 这行代码将数据加载到表“guestDepDataSet.guest_deps”中。您可以根据需要移动或删除它。
            this.guest_depsTableAdapter.Fill(this.guestDepDataSet.guest_deps);

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dataGridView1.CurrentRow.ErrorText = "";
            if (dataGridView1.CurrentRow.IsNewRow) { return; }

            if (e.ColumnIndex == 1)
            {
                string name = e.FormattedValue.ToString();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (i != e.RowIndex)
                    {
                        if (name.Equals(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value)))
                        {
                            e.Cancel = true;
                            dataGridView1.CurrentRow.ErrorText = "部门名已经存在";
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guest_depsTableAdapter.Update(guestDepDataSet);
            MessageBox.Show("保存成功");
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dataGridView2.CurrentRow.ErrorText = "";
            if (dataGridView2.CurrentRow.IsNewRow) { return; }

            if (e.ColumnIndex == 2)
            {
                decimal cellValue;
                if (!Decimal.TryParse(e.FormattedValue.ToString(), out cellValue))
                {
                    e.Cancel = true;
                    dataGridView2.CurrentRow.ErrorText = "租金必须为数字";
                }
            }
        }
    }
}
