using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Models;
using DAL.Helper;
using System.Data;
using System.Data.SqlClient;

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
    }
}
