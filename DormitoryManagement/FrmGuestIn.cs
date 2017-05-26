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
    public partial class FrmGuestIn : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        DateTime? inDate = null;
        DateTime? outDate = null;
        public int? updateId;
        GuestInfos updateInfos;

        public FrmGuestIn()
        {
            InitializeComponent();
            button1.Click += new EventHandler(FrmGuestIn_Load);
        }

        private void FrmGuestIn_Load(object sender, EventArgs e)
        {
            clearForm();

            int days;
            TimeSpan span = dtpOut.Value - dtpIn.Value;
            days=span.Days;
            tbDays.Text = days.ToString();
            //tbSum.Text = (days * nbRent.Value).ToString();

        }

        //初始化界面
        private void clearForm()
        {
            //部门列表
            cbDep.DataSource = from d in db.GuestDeps
                               orderby d.department
                               select d.department;            
            
            if (updateId == null)
            {
                rdbMale.Checked = true;
                rdbBus.Checked = true;
                dtpIn.Value = DateTime.Now;
                dtpOut.Value = DateTime.Now.AddDays(1);

                tbDays.Text = (dtpOut.Value - dtpIn.Value).Days.ToString();

                //房间列表
                cbRoom.DataSource = from r in db.GuestRoom
                                    where !((from i in db.GuestInfos
                                             where !i.is_finish
                                             && !i.is_deleted
                                             select i.dorm_number).Contains(r.number))
                                    orderby r.typ, r.number.Length, r.number
                                    select r.number;
            }
            else {
                groupBox1.Text = "入住信息修改";
                button1.Text = "确认修改";
                updateInfos = db.GuestInfos.Single(g => g.id == (int)updateId);
                cbComment.Text = updateInfos.comment;
                tbName.Text = updateInfos.living_people;
                tbCharge.Text = updateInfos.charger;
                tbCheckOut.Text = updateInfos.checkout;
                cbDep.Text = updateInfos.dep;                                          
                dtpIn.Value = (DateTime)updateInfos.in_date;
                dtpOut.Value = (DateTime)updateInfos.out_date;
                tbDays.Text = (dtpOut.Value - dtpIn.Value).Days.ToString();
                     
                if (updateInfos.sex.Equals("男"))
                {
                    rdbMale.Checked = true;
                }
                else {
                    rdbFemale.Checked = true;
                }
                if (updateInfos.business)
                {
                    rdbBus.Checked = true;
                }
                else {
                    rdbSelf.Checked = true;
                }
                //房间列表
                cbRoom.DataSource = from r in db.GuestRoom
                                    where !((from i in db.GuestInfos
                                             where !i.is_finish
                                             && !i.is_deleted
                                             && i.dorm_number != updateInfos.dorm_number
                                             select i.dorm_number).Contains(r.number))
                                    orderby r.typ, r.number.Length, r.number
                                    select r.number;

                cbRoom.Text = updateInfos.dorm_number;
                nbRent.Value = (decimal)updateInfos.price; 
            } 

        }

        //负责人文本框和退房人文本框的值随着部门的选择而改变
        private void cbDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbDep.Text;
            var dep = db.GuestDeps.Single(g => g.department == selected);
            tbCharge.Text = dep.charger;
            tbCheckOut.Text = dep.checkout;
            
        }

        //如果部门不存在数据库中，而要自己输入，则清空负责人文本框和退房人文本框
        private void cbDep_TextChanged(object sender, EventArgs e)
        {
            tbCheckOut.Text = tbCharge.Text = "";
        }

        private void tbCharge_TextChanged(object sender, EventArgs e)
        {
            tbCheckOut.Text = tbCharge.Text;
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            var room = db.GuestRoom.Single(r => r.number == cbRoom.Text);
            nbRent.Value = (decimal)room.rent;
        }

        private void cbRoom_TextChanged(object sender, EventArgs e)
        {
            nbRent.Value = 0.0m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(tbName.Text)) {
                MessageBox.Show("姓名不能为空");
                tbName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cbRoom.Text)) {
                MessageBox.Show("房间不能为空");
                cbRoom.Focus();
                return;
            }
            
            inDate = dtpIn.Value.Date;
            outDate = dtpOut.Value.Date;

            if (inDate > outDate) {
                MessageBox.Show("退宿日期不能早于入住日期");
                dtpOut.Focus();
                return;
            }

            if (updateInfos == null)
            {
                addLiving();
            }
            else {
                updateLiving();
            }
            this.Close();
        }

        private void addLiving()
        {
            if ((from g in db.GuestInfos
                 where !g.is_finish
                 && !g.is_deleted
                 && g.dorm_number == cbRoom.Text
                 select g
                 ).Count() > 0)
            {
                MessageBox.Show("此房间已有人住！");
                cbRoom.Focus();
                return;
            }

            GuestInfos info = new GuestInfos();
            info.living_people = tbName.Text;
            info.dorm_number = cbRoom.Text;
            info.dep = cbDep.Text;
            info.business = rdbBus.Checked;
            info.sex = rdbMale.Checked ? "男" : "女";
            info.charger = tbCharge.Text;
            info.checkout = tbCheckOut.Text;
            info.comment = cbComment.Text;
            info.price = nbRent.Value;            
            info.in_date = inDate;
            info.out_date = outDate;
            info.is_finish = false;
            info.is_deleted = false;
            db.GuestInfos.InsertOnSubmit(info);
            db.SubmitChanges();

            MessageBox.Show("登记成功");
        }

        private void updateLiving() {
            if ((from g in db.GuestInfos
                 where !g.is_finish
                 && !g.is_deleted
                 && g.dorm_number == cbRoom.Text
                 && g.dorm_number != updateInfos.dorm_number  //更新信息的宿舍号
                 select g
                 ).Count() > 0)
            {
                MessageBox.Show("此房间已有人住！");
                cbRoom.Focus();
                return;
            }
            
            updateInfos.living_people = tbName.Text;
            updateInfos.dorm_number = cbRoom.Text;
            updateInfos.dep = cbDep.Text;
            updateInfos.business = rdbBus.Checked;
            updateInfos.sex = rdbMale.Checked ? "男" : "女";
            updateInfos.charger = tbCharge.Text;
            updateInfos.checkout = tbCheckOut.Text;
            updateInfos.comment = cbComment.Text;
            updateInfos.price = nbRent.Value;            
            updateInfos.in_date = inDate;
            updateInfos.out_date = outDate;

            db.SubmitChanges();

            MessageBox.Show("信息更新成功");
        }

        private void dtpIn_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan span = dtpOut.Value - dtpIn.Value;
            tbDays.Text = span.Days.ToString();
        }

        private void nbRent_ValueChanged(object sender, EventArgs e)
        {
            tbSum.Text = (nbRent.Value * Convert.ToInt32(tbDays.Text)).ToString();
        }
    }
}
