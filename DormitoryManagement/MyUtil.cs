using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormitoryManagement
{
    public class MyUtil
    {
        public void WriteChangeDepLog(string empName,string userAccount,string oldDep,string newDep) {
            DormDBDataContext db = new DormDBDataContext();
            ChangeDepLog log = new ChangeDepLog();
            log.modify_date = DateTime.Now;
            log.modify_user = LoginUser.username;
            log.emp_account = userAccount;
            log.emp_name = empName;
            log.old_dep = oldDep;
            log.new_dep = newDep;
            db.ChangeDepLog.InsertOnSubmit(log);
            db.SubmitChanges();            
        }

        //记录操作日志
        public static void WriteEventLog(string model,string dormNumber, string emp, string log, bool isNomal = true)
        {
            DormDBDataContext db = new DormDBDataContext();
            db.EventLog.InsertOnSubmit(new EventLog()
            {
                model = model,
                area = LoginUser.operated_area,
                operate_time = DateTime.Now,
                @operator = LoginUser.username,
                dorm = dormNumber,
                emp = emp,
                @event = log,
                is_normal = isNomal
            });
            db.SubmitChanges();
        }
    }
}
