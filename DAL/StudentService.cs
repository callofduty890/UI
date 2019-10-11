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
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName, objStudent.Gender, objStudent.Birthday.ToString("yyyy-MM-dd"),
        objStudent.StudentIdNo, objStudent.Age, objStudent.PhoneNumber, objStudent.StudentAddress, objStudent.CardNo, objStudent.ClassId, objStudent.StuImage);

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

        //按班级获取全部学生信息
        public Student GetStudentsById(string studentId)
        {
            //构建SQL语句
            string sql = "select StudentId,StudentName,Gender,Birthday,ClassName,StudentIdNo,PhoneNumber,StudentAddress,CardNo,StuImage from Students";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId ";
            sql += " where StudentId='{0}'";
            sql = string.Format(sql, studentId);
            //接受返回值
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //构建接受泛型列表
            Student objStudent = new Student();
            //循环接收
            if (objReader.Read())
            {

                objStudent = new Student()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"]),
                    ClassName = objReader["ClassName"].ToString(),
                    CardNo = objReader["CardNo"].ToString(),
                    StudentIdNo = objReader["StudentIdNo"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    StudentAddress = objReader["StudentAddress"].ToString(),
                    StuImage = objReader["StuImage"] == null ? "" : objReader["StuImage"].ToString()
                };

            }
            //关闭连接-返回全部的值
            objReader.Close();
            return objStudent;
        }

        //按考勤卡号获取全部学生信息
        public Student GetStudentByCardNo(string CardNo)
        {
            //构建SQL语句
            string sql = "select StudentId,StudentName,Gender,Birthday,ClassName,StudentIdNo,PhoneNumber,StudentAddress,CardNo,StuImage from Students";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId ";
            sql += " where CardNo='{0}'";
            sql = string.Format(sql, CardNo);
            //接受返回值
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //构建接受泛型列表
            Student objStudent = new Student();
            //循环接收
            if (objReader.Read())
            {

                objStudent = new Student()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"]),
                    ClassName = objReader["ClassName"].ToString(),
                    CardNo = objReader["CardNo"].ToString(),
                    StudentIdNo = objReader["StudentIdNo"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    StudentAddress = objReader["StudentAddress"].ToString(),
                    StuImage = objReader["StuImage"] == null ? "" : objReader["StuImage"].ToString()
                };

            }
            //关闭连接-返回全部的值
            objReader.Close();
            return objStudent;
        }

        //修改学生信息
        public int ModifyStudent(Student objStudent)
        {   //【1】编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update Students set studentName='{0}',Gender='{1}',Birthday='{2}',");
            sqlBuilder.Append(
                "StudentIdNo={3},Age={4},PhoneNumber='{5}',StudentAddress='{6}',CardNo='{7}',ClassId={8},StuImage='{9}'");
            sqlBuilder.Append(" where StudentId={10}");
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName,
                                         objStudent.Gender, objStudent.Birthday.ToString("yyyy-MM-dd"),
                                        objStudent.StudentIdNo, objStudent.Age,
                                        objStudent.PhoneNumber, objStudent.StudentAddress, objStudent.CardNo,
                                        objStudent.ClassId, objStudent.StuImage, objStudent.StudentId);

            //进行交互
            return Convert.ToInt32(SQLHelper.Update(sql));

        }

        //删除学生信息
        public int DeleteStudentById(string studentId)
        {
            //SQL语句构建
            string sql = "delete from Students where StudentId=" + studentId;
            //进行交互
            return Convert.ToInt32(SQLHelper.Update(sql));
        }   }
}
