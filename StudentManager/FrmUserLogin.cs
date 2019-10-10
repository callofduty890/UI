using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Models;
using DAL;

namespace StudentManager
{
    public partial class FrmUserLogin : Form
    {
        public FrmUserLogin()
        {
            InitializeComponent();
        }


        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region 用户输入数据验证检查是否正常
            //登录账号
            if (this.txtLoginId.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录账号！", "提示信息");
                this.txtLoginId.Focus();
                return;
            }
            //输入密码不能为空
            if (this.txtLoginPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录密码！", "提示信息");
                this.txtLoginPwd.Focus();
            }
            //验证一下是否是整数
            if (!Common.DataValidate.IsInteger(this.txtLoginId.Text.Trim()))
            {
                MessageBox.Show("登录账号必须是整数！", "提示信息");
                this.txtLoginId.Focus();
                return;
            }
            #endregion
            //三范式   UI-中间层-数据访问层

            #region 接收用户输入的内容
            Admin objAdmin = new Admin()
            {
                //登录账号
                LoginId = Convert.ToInt32(this.txtLoginId.Text.Trim()),
                //登录密码
                LoginPwd = this.txtLoginPwd.Text.Trim()
            };
            #endregion

            #region 调用后台方法查询验证
            //将输入的账号密码进行验证
            Program.currentAdmin = new AdminService().AdminLogin(objAdmin);
            if (Program.currentAdmin !=null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("用户账号密码错误！", "提示信息");
            }

            #endregion

        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
