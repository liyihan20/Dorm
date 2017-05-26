using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormitoryManagement
{
    public class YearAndMonth
    {
        public static string next(string old) {
            string year, month;            
            year = old.Substring(0, 4);
            if (old.Length == 6)
                month = old.Substring(4, 2);
            else
                month = old.Substring(4, 1);
            if (month.Equals("12"))
            {
                year = (Convert.ToInt32(year) + 1).ToString();
                month = "01";
            }
            else if (Convert.ToInt32(month) < 9) {
                month = "0" + (Convert.ToInt32(month) + 1).ToString();
            }
            else
            {
                month = (Convert.ToInt32(month) + 1).ToString();
            }
            return year + month;
        }

        public static string previous(string str) { 
            string newstr=YearAndMonth.toLong(str);
            string year = newstr.Substring(0, 4);
            string month = newstr.Substring(4, 2);
            if (Int32.Parse(month) == 1) {
                year = (Int32.Parse(year) - 1).ToString();
                month = "12";
            }
            else if (Int32.Parse(month) < 11)
            {
                month = "0" + (Int32.Parse(month) - 1).ToString();
            }
            else {
                month = (Int32.Parse(month) - 1).ToString();
            }
            return year + month;
        }

        public static string toLong(string str) {
            if (str.Length == 5)
                return str.Substring(0, 4) + "0" + str.Substring(4, 1);
            else
                return str;
        }

        public static DateTime firstDayInMonth(string yearMonth){
            yearMonth = YearAndMonth.toLong(yearMonth);
            return DateTime.Parse(yearMonth.Substring(0, 4) + "-" + yearMonth.Substring(4, 2) + "-01");
        }

        public static DateTime lastDayInMonth(string yearMonth) {
            yearMonth = YearAndMonth.toLong(yearMonth);
            //当月总天数
            int totalDayOfYearMonth = DateTime.DaysInMonth(Int32.Parse(yearMonth.Substring(0, 4)), Int32.Parse(yearMonth.Substring(4, 2)));
            
            return DateTime.Parse(yearMonth.Substring(0, 4) + "-" + yearMonth.Substring(4, 2) + "-" + totalDayOfYearMonth.ToString());
        }

        public static string getYearAndMonth(DateTime date) {
            return string.Format("{0}{1:00}", date.Year, date.Month);
        }

        public static int leftDays(DateTime d1, DateTime d2)
        {
            d1 = d1.AddMonths(3);
            //Console.WriteLine("now the d1:" + d1.ToShortDateString());
            if (d2.Year + 1 == d1.Year)
                return 0;
            if (d2.Year == d1.Year)
            {
                if (d2.Month < d1.Month)
                    return 0;
                if (d2.Month == d1.Month)
                {
                    if (d1.Day > d2.Day)
                    {
                        return 0;
                    }
                    else
                    {
                        return d2.Day - d1.Day;
                    }
                }
            }
            return d2.Day;
        }

        public static List<string> monthSpan(DateTime d1, DateTime d2)
        {
            List<string> list = new List<string>();

            while (d2 > d1)
            {
                list.Add(d2.ToString("yyyy-MM"));
                d2 = d2.AddMonths(-1);
            }

            return list;
        }
    }
}
