using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helper;

namespace DAL
{
    //操作考勤类
    public class AttendanceService
    {
        //获取全部学生数量
        public string GetAllStudent()
        {
            //构建SQL语句
            string sql = "select COUNT(*) from Students";

            return SQLHelper.GetSingleResult(sql).ToString();

        }

        //按照指定日期查询当天已签到的学员总数
        public string GetAttendStudents(DateTime dt, bool isToday)
        {
            //时间
            DateTime dt1;
            //如果是当天，直接获取服务器的时间
            if (isToday)
            {
                dt1 = Convert.ToDateTime(SQLHelper.GetServerTime().ToShortDateString());
            }
            else
            {
                dt1 = dt;
            }
            //当天加上一天
            DateTime dt2 = dt1.AddDays(1.0);

            string sql = "select count(CardNo)  from Attendance where DTime between '{0}' and '{1}'";
            sql = string.Format(sql, dt1, dt2);

            return SQLHelper.GetSingleResult(sql).ToString();
        }

        //开始打卡
        public string AddRecord(string cardNo)
        {
            string sql = "insert into Attendance (CardNo) values('{0}')";
            sql = string.Format(sql, cardNo);
            try
            {
                SQLHelper.Update(sql);
                return "success";
            }
            catch (Exception ex)
            {
                return "打卡失败！系统出现问题，请联系管理员！" + ex.Message;
            }
        }
    }
}
