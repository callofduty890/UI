using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Models;
using DAL.Helper;
using System.Data;
using System.Data.SqlClient;

using Models;

namespace DAL
{
    public class StudentClassService
    {
        //获取全部的班级
        public List<StudentClass> GetAllClasses()
        {
            //SQL语句
            string sql = "select className,classId from StudentClass";
            //接受返回值
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //构建接受泛型列表
            List<StudentClass> list = new List<StudentClass>();
            //循环接受全部列表信息
            while (objReader.Read())
            {
                list.Add(new StudentClass()
                {
                    ClassId = Convert.ToInt32(objReader["ClassId"]),
                    ClassName = objReader["ClassName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        //按班级获取全部学生信息
        public List<Student> GetStudentsByClass(string className)
        {
            //构建SQL语句
            string sql = "select StudentId,StudentName,Gender,PhoneNumber,StudentIdNo,Birthday,ClassName from Students";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId ";
            sql += " where ClassName='{0}'";
            sql = string.Format(sql, className);
            //接受返回值
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //构建接受泛型列表
            List<Student> list = new List<Student>();
            //循环接收
            while (objReader.Read())
            {
                list.Add
                    (
                    new Student()
                    {
                        StudentId=Convert.ToInt32(objReader["StudentId"]),
                        StudentName = objReader["StudentName"].ToString(),
                        Gender = objReader["Gender"].ToString(),
                        PhoneNumber = objReader["PhoneNumber"].ToString(),
                        Birthday = Convert.ToDateTime(objReader["Birthday"]),
                        StudentIdNo = objReader["StudentIdNo"].ToString(),
                        ClassName = objReader["ClassName"].ToString()
                    }
                    );
            }
            //关闭连接-返回全部的值
            objReader.Close();
            return list;
        }
    }
}
