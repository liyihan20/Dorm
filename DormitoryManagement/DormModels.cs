using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormitoryManagement
{
    /*
        objData[0, 0] = "序号";
        objData[0, 1] = "部门";
        objData[0, 2] = "账号";
        objData[0, 3] = "姓名";
        objData[0, 4] = "合计";
        objData[0, 5] = "房租";
        objData[0, 6] = "管理费";
        objData[0, 7] = "电费";
        objData[0, 8] = "水费";
        objData[0, 9] = "其他";
        objData[0, 10] = "维修";
        objData[0, 11] = "罚款";
        objData[0, 12] = "招待所";
        objData[0, 13] = "押金";
        objData[0, 14] = "区别";
        objData[0, 15] = "房号";
        objData[0, 16] = "性质";
        objData[0, 17] = "备注";
         */
    public class ExportCharge {

        //在住：true，退宿：false        
        public bool living { get; set; }
        public int num { get; set; }
        public string dep { get; set; }
        public string salaryDep { get; set; }
        public string account { get; set; }
        public string name { get; set; }
        public decimal total { get; set; }
        public decimal rent { get; set; }
        public decimal manage { get; set; }
        public decimal water { get; set; }
        public decimal ele { get; set; }
        public decimal? other { get; set; }
        public decimal? repair { get; set; }
        public decimal? fine { get; set; }
        public decimal? house { get; set; }
        public decimal? guarantee { get; set; }
        public string area { get; set; }
        public string dorm { get; set; }
        public string property { get; set; }
        public string classify { get; set; }
        public string comment { get; set; }
    }
}
