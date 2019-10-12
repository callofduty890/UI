using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using Models;
using Models.Ext;
using DAL.Helper;
using System.Data.SqlClient;

namespace DAL
{
    //学习成绩类
    public class ScoreListService
    {
        /// <summary>
        /// 获取所有的学员成绩列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllScoreList()
        {
            string sql = "select Students.StudentId,StudentName,ClassName,Gender,PhoneNumber,CSharp,SQLServerDB";
            sql += " from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId ";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId ";
            return SQLHelper.GetDataSet(sql);
        }


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


        //获取指定班级的学员成绩
        public List<StudentExt> GetSCoreList(string className)
        {
            string sql = "select Students.StudentId,StudentName,ClassName,Gender,PhoneNumber,CSharp,SQLServerDB";
            sql += " from Students";
            sql += " inner join StudentClass on StudentClass.ClassId=Students.ClassId ";
            sql += " inner join ScoreList on ScoreList.StudentId=Students.StudentId ";
            //判断输入条件
            if (className!=null&&className.Length!=0)
            {
                sql += " where ClassName='" + className + "'";
            }
            //构建查询
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<StudentExt> list = new List<StudentExt>();
            while (objReader.Read())
            {
                list.Add(new StudentExt()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    ClassName = objReader["ClassName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    CSharp = objReader["CSharp"].ToString(),
                    SQLServerDB = objReader["SQLServerDB"].ToString(),
                    cc = false
                });
            }
            objReader.Close();
            return list;

        }

        #region 按照班级统计考试信息
        // 参考人数 CShape平均分 数据库平均分
        public Dictionary<string, string> GetScoreInfoByClassId(string classId)
        {
            string sql = "select stuCount=count(*),avgCSharp=avg(CSharp),avgDB=avg(SQLServerDB) from ScoreList ";
            sql += "inner join Students on Students.StudentId=ScoreList.StudentId where ClassId={0};";
            sql += "select absentCount=count(*) from Students where StudentId not in";
            sql += "(select StudentId from ScoreList) and ClassId={1}";
            sql = string.Format(sql, classId, classId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Dictionary<string, string> scoreInfo = null;
            if (objReader.Read())//读取考试成绩统计结果
            {
                scoreInfo = new Dictionary<string, string>();
                scoreInfo.Add("stuCount", objReader["stucount"].ToString());
                scoreInfo.Add("avgCSharp", objReader["avgCSharp"].ToString());
                scoreInfo.Add("avgDB", objReader["avgDB"].ToString());
            }
            if (objReader.NextResult())//读取缺考人数列表
            {
                if (objReader.Read())
                {
                    scoreInfo.Add("absentCount", objReader["absentCount"].ToString());
                }
            }
            objReader.Close();
            return scoreInfo;
        }
        /// <summary>
        /// 查询未参加考试的学生名单
        /// </summary>
        /// <returns></returns>
        public List<string> GetAbsentListByClassId(string classId)
        {
            string sql = "select StudentName from Students where StudentId not in ";
            sql += "(select StudentId from ScoreList) and ClassId={0}";
            sql = string.Format(sql, classId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<string> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["StudentName"].ToString());
            }
            objReader.Close();
            return list;
        }

        #endregion

        #region 获取全部考试的考试综合信息

        /// <summary>
        /// 获取全部考试的考试综合信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetScoreInfo()
        {
            string sql = "select stuCount=count(*),avgCSharp=avg(CSharp),avgDB=avg(SQLServerDB) from ScoreList;";
            sql += "select absectCount=count(*) from Students where StudentId not in(select StudentId from ScoreList)";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Dictionary<string, string> scoreInfo = null;
            if (objReader.Read())
            {
                scoreInfo = new Dictionary<string, string>();
                scoreInfo.Add("stuCount", objReader["stucount"].ToString());
                scoreInfo.Add("avgCSharp", objReader["avgCSharp"].ToString());
                scoreInfo.Add("avgDB", objReader["avgDB"].ToString());
            }
            if (objReader.NextResult())
            {
                if (objReader.Read())
                {
                    scoreInfo.Add("absectCount", objReader["absectCount"].ToString());
                }
            }
            objReader.Close();
            return scoreInfo;
        }
        /// <summary>
        /// 获取未参加考试的学生名单
        /// </summary>
        /// <returns></returns>
        public List<string> GetAbsentList()
        {
            string sql = "select StudentName from Students where StudentId not in(select StudentId from ScoreList)";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<string> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["StudentName"].ToString());
            }
            objReader.Close();
            return list;
        }

        #endregion
    }
}
