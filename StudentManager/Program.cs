using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


using System.Diagnostics;


namespace StudentManager
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //显示登录窗体
            FrmUserLogin frmLogin = new FrmUserLogin();
            //等待窗体返回值
            DialogResult result = frmLogin.ShowDialog();
            //判断是否登录成功
            if (result==DialogResult.OK)
            {
                //打开主窗体运行
               Application.Run(new FrmMain());
            }
            else
            {
                //输入账号密码不正确退出
                Application.Exit();
            }

        }

    }
}
