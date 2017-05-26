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
    public partial class FrmGuestBalance : Form
    {
        DormDBDataContext db = new DormDBDataContext();
        public int balanceId;
        GuestInfos info;

        public FrmGuestBalance()
        {
            InitializeComponent();
        }

        private void FrmGuestBalance_Load(object sender, EventArgs e)
        {
            info = db.GuestInfos.Single(g => g.id == balanceId);
            lbName.Text = info.living_people;
            lbSex.Text = info.sex;
            lbDep.Text = info.dep;
            lbCharger.Text = info.charger;
            lbCheckOut.Text = info.checkout;
            lbPro.Text = info.business ? "公事" : "私事";

            lbRoom.Text = info.dorm_number;
            lbRent.Text = info.price.ToString();
            lbInDate.Text = ((DateTime)info.in_date).ToShortDateString();
            lbOutDate.Text = ((DateTime)info.out_date).ToShortDateString();

            dtpRealOutDate.Value = (DateTime)info.out_date;
            tbComment.Text = info.comment;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal sum;
            if (decimal.TryParse(tbSum.Text, out sum))
            {
                info.sum = sum;
            }
            else {
                MessageBox.Show("金额不合法，必须为数字！");
                return;
            }
            info.real_out_date = dtpRealOutDate.Value.Date;
            info.comment = tbComment.Text;
            info.is_finish = true;

            //继续入住
            if (cbContinue.Checked) {                
                db.GuestInfos.InsertOnSubmit(new GuestInfos()
                {
                    business = info.business,
                    charger = info.charger,
                    checkout = info.checkout,
                    comment = info.comment,
                    dep = info.dep,
                    dorm_number = info.dorm_number,
                    in_date = info.real_out_date,
                    is_deleted = false,
                    is_finish = false,
                    living_people = info.living_people,
                    out_date = ((DateTime)info.real_out_date).AddMonths(1),
                    price = info.price,
                    sex = info.sex,
                });
            }

            db.SubmitChanges();
            MessageBox.Show("结算成功！");
            this.Close();

        }

        private void dtpRealOutDate_ValueChanged(object sender, EventArgs e)
        {
            nudDays.Value = (dtpRealOutDate.Value - DateTime.Parse(lbInDate.Text)).Days;
            tbSum.Text = (nudDays.Value * decimal.Parse(lbRent.Text)).ToString();
        }
    }
}
