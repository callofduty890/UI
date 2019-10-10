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

        //执行多结果查询
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


    }
}
