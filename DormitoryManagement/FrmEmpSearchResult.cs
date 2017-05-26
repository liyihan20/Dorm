using System;
using System.Linq;
using System.Windows.Forms;

namespace DormitoryManagement
{
    public partial class FrmEmpSearchResult : Form
    {
        IQueryable<Employee> emps;
        FrmEmployeeInfo info;
        FormWithID formWithId;

        public FrmEmpSearchResult()
        {
            InitializeComponent();
        }
        public FrmEmpSearchResult(IQueryable<Employee> ems,FrmEmployeeInfo frmInfo)
        {
            emps = ems;
            info = frmInfo;
            InitializeComponent();
        }

        public FrmEmpSearchResult(IQueryable<Employee> ems, FormWithID withId) {
            formWithId = withId;
            emps = ems;
            InitializeComponent();
        }

        private void FrmEmpSearchResult_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = from emp in emps
                                       select new { 
                                        id=emp.id,
                                        部门=emp.Department1.name,
                                        姓名=emp.name,
                                        性别=emp.sex,
                                        厂牌编号=emp.card_number,
                                        籍贯=emp.household
                                       };      
        }  

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (info != null)
            {
                info.lbID.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["id"].Value);
                info.Activate();
            }
            else {
                formWithId.myId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                formWithId.Activate();
            }
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["籍贯"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }        
    }
}
