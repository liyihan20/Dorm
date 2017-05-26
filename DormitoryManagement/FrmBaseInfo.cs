using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmBaseInfo : Form
    {
        DormDBDataContext db=new DormDBDataContext();
        public FrmBaseInfo()
        {
            InitializeComponent();
        }

        private void FrmBaseInfo_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dormTypeDataSet.dormitory_type”中。您可以根据需要移动或删除它。
            this.dormitory_typeTableAdapter.Fill(this.dormTypeDataSet.dormitory_type);
            tbName.Enabled = false;
            cbNumber.Enabled = false;
            FillFees();
            
        }

        //单位费用，填充datagridview2
        private void FillFees() {            
            var fees = db.UnitFee;
            using (DataTable dt = new DataTable())
            {
                dt.Columns.Add("费用名称", typeof(string));
                dt.Columns.Add("单位价格", typeof(decimal));
                dt.Columns.Add("单位", typeof(string));

                foreach (UnitFee fee in fees)
                {
                    dt.Rows.Add(fee.name, fee.price, fee.units);
                }
                dataGridView2.DataSource = dt;
            }
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            tbName.Enabled = true;
            tbName.Clear();
            tbRent.Clear();
            tbManage.Clear();
            cbCharge.Text = "";
            cbNumber.Text = "";
            cbNumber.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //验证用户输入
            if(string.IsNullOrEmpty(tbName.Text.Trim())){
                MessageBox.Show("名称不能为空");
                tbName.Focus();
                return;
            }
            try{
                Convert.ToDecimal(tbRent.Text);
            }catch(Exception){
                MessageBox.Show("租金必须是一个数字");
                tbRent.Focus();
                return;
            }
            try
            {
                Convert.ToDecimal(tbManage.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("管理费必须是一个数字");
                tbManage.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbCharge.Text)) {
                MessageBox.Show("请选择收费方式");
                cbCharge.Focus();
                return;
            }
            if (cbCharge.Text == "分摊到人")
            {
                if (string.IsNullOrEmpty(cbNumber.Text))
                {
                    MessageBox.Show("请选择住宿人数");
                    cbNumber.Focus();
                    return;
                }
            }

            //新增或者修改宿舍类型
            if (tbName.Enabled)
            {
                DormType typ = new DormType();
                typ.name = tbName.Text;
                typ.rent = Convert.ToDecimal(tbRent.Text);
                typ.manage_cost = Convert.ToDecimal(tbManage.Text);
                typ.charge_mode = cbCharge.Text;
                if (cbCharge.Text == "分摊到人")
                {
                    typ.max_number = Convert.ToInt16(cbNumber.Text);
                }
                else {
                    typ.max_number = 1;
                }
                db.DormType.InsertOnSubmit(typ);
                db.SubmitChanges();
            }
            else {
                DormType typ = db.DormType.Single(ty => ty.name == Convert.ToString(tbName.Text));
                typ.rent = Convert.ToDecimal(tbRent.Text);
                typ.manage_cost = Convert.ToDecimal(tbManage.Text);
                typ.charge_mode = cbCharge.Text;
                if (cbCharge.Text == "分摊到人")
                {
                    typ.max_number = Convert.ToInt16(cbNumber.Text);
                }
                else
                {
                    typ.max_number = 1;
                }
                db.SubmitChanges();
            }
            //更新datagridview1
            this.dormitory_typeTableAdapter.Fill(this.dormTypeDataSet.dormitory_type);
            tbName.Enabled = false;
            tbName.Clear();
            tbRent.Clear();
            tbManage.Clear();
            cbCharge.Text = "";
            cbNumber.Text = "";
            cbNumber.Enabled = false;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tbName.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            tbRent.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            tbManage.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
            cbCharge.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            cbNumber.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            tbName.Enabled = false;
            if (cbCharge.Text == "分摊到人")
            {
                cbNumber.Enabled = true;
            }
        }

        private void cbCharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCharge.Text == "分摊到人")
                cbNumber.Enabled = true;
            else 
                cbNumber.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal? price;
            try
            {
                price = Convert.ToDecimal(tbFee.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("单价输入不合法");
                tbFee.Focus();
                return;
            }
            var fee = db.UnitFee.Where(fe => fe.name == lbFeeName.Text).First();           
            fee.price = price;
            db.SubmitChanges();
            FillFees();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lbFeeName.Text = Convert.ToString(dataGridView2.CurrentRow.Cells[0].Value);
            tbFee.Text = Convert.ToString(dataGridView2.CurrentRow.Cells[1].Value);
            lbUnit.Text = Convert.ToString(dataGridView2.CurrentRow.Cells[2].Value);
        }


    }
}
