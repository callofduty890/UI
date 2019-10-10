using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using DAL.Helper;
using Models;

namespace DAL
{

    public class StudentService
    {
        //查询考勤卡号是否存在
        public bool IsCardNoExisted(string cardNo)
        {
            //构建sql语句
            string sql = string.Format("select count(*) from Students where CardNo='{0}'", cardNo);
            //执行SQL语句
            int result = Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            //判断返回的结果
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //查询身份证卡号是否存在
        public bool IsIdNoExisted(string cardNo)
        {
            //构建sql语句
            string sql = string.Format("select count(*) from Students where StudentIdNo='{0}'", cardNo);
            //执行SQL语句
            int result = Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            //判断返回的结果
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //添加学员
        public int AddStudent(Student objStudent)
        {
            //构建SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Students(studentName,Gender,Birthday,StudentIdNo,Age,PhoneNumber,StudentAddress,CardNo,ClassId,StuImage)");
            sqlBuilder.Append(" values('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}',{8},'{9}');select @@Identity");
            //把值传入其中
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName,objStudent.Gender, objStudent.Birthday.ToString("yyyy-MM-dd"),
        objStudent.StudentIdNo, objStudent.Age,objStudent.PhoneNumber, objStudent.StudentAddress, objStudent.CardNo,objStudent.ClassId, objStudent.StuImage);

            //异常判断
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql)); //【3】执行SQL语句，返回结果
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
