using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//数据库使用的类引用
using System.Data.SqlClient;
using System.Data;

namespace DAL.Helper
{
    //访问SQLserver数据的通用类
    public class SQLHelper
    {
        //连接语句
        private static readonly string connString = @"Server=DESKTOP-CTV4ATU\SQLSERVER;DataBase=SMDB;Uid=sa;Pwd=123456";

        ////执行多结果查询
        public static SqlDataReader GetReader(string sql)
        {
            //创建数据库连接对象
            SqlConnection conn = new SqlConnection(connString);
        //创建数据库操作对象
        SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                //打开数据库
                conn.Open();
                //读取返回的对象
                SqlDataReader objReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //返回值
                return objReader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
            //关闭连接
            //conn.Close();

        }

        //单一查询结果，如果查询到了返回第一行第一列，其他结果忽略
        public static object GetSingleResult(string sql)
        {
            //创建连接数据库对象
            SqlConnection conn = new SqlConnection(connString);

            //创建数据库操作对象
            SqlCommand cmd = new SqlCommand(sql, conn);

            //查找结果
            try
            {
                //打开数据库
                conn.Open();
                //返回执行结果
                object result = cmd.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                //可以进行错误日志的记录
                throw ex;
            }
            finally
            {
                //关闭连接
                conn.Close();
            }

        }

        //更新数据库操作 增删改
        public static int Update(string sql)
        {
            //创建连接数据库对象
            SqlConnection conn = new SqlConnection(connString);

            //创建数据库操作对象
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                //打开数据库
                conn.Open();
                //执行SQL语句并返回受影响的行数
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //可以进行错误日志的记录
                throw ex;
            }
            finally
            {
                //关闭连接
                conn.Close();
            }




        }

        /// <summary>
        /// 获取服务器的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            //转换成时间
            return Convert.ToDateTime(GetSingleResult("select getdate()"));
        }
    }
}