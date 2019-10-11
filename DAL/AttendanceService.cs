using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helper;

using System.Data;
using System.Data.SqlClient;
using Models;
using Models.Ext;

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

        //根据时间段还有姓名查询考勤
        public List<StudentExt> GetStuByDate(DateTime beginDate, DateTime endDate, string name)
        {
            //构建SQL语句
            string sql = "select StudentId,StudentName,Gender,DTime,ClassName,Attendance.CardNo from Students ";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId ";
            sql += " inner join Attendance on Students.CardNo=Attendance.CardNo";
            sql += " where DTime between '2019-10-11 00:00:00' and '2019-10-12 00:00:00'";

            //判断输入姓名
            if (name!=null&&name.Length!=0)
            {
                sql += string.Format(" and StudentName='{0}'", name);
            }
            //对签到时间进行排序
            sql += " Order By DTime ASC";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<StudentExt> list = new List<StudentExt>();
            while (objReader.Read())
            {
                list.Add(new StudentExt()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    CardNo = objReader["CardNo"].ToString(),
                    ClassName = objReader["ClassName"].ToString(),
                    DTime = Convert.ToDateTime(objReader["DTime"])
                });
            }
            return list;

        }
    }
}
