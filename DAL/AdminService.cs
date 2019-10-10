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

    public class AdminService
    {
        //根据账号密码登录
        public Admin AdminLogin(Admin objAdmin)
        {
            //构建SQL语句
            string sql = "select LoginId,LoginPwd,AdminName from Admins where loginId={0} and loginPwd='{1}'";
            sql = string.Format(sql, objAdmin.LoginId, objAdmin.LoginPwd);

            //接受查询的返回数据
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            //判断收读取成功
            if (objReader.Read())
            {
                objAdmin.AdminName = objReader["AdminName"].ToString();
                //关闭登录
                objReader.Close();
            }
            else
            {
                objAdmin = null;
            }
            return objAdmin;
        }
    }
}
