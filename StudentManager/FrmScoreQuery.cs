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
using DAL.Helper;

namespace StudentManager
{
    public partial class FrmScoreQuery : Form
    {
        //实例化成绩操作类
        private ScoreListService objListService = new ScoreListService();
        //保存全部查询结果的数据集
        private DataSet ds = null;
        public FrmScoreQuery()
        {
            InitializeComponent();
            //班级下拉列表-获取全部的班级信息
            DataTable dt = new StudentClassService().GetAllClass().Tables[0];
            //绑定
            this.cboClass.DataSource = dt;
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.SelectedIndex = -1;
            //显示全部查询成绩
            ds = objListService.GetAllScoreList();
            this.dgvScoreList.DataSource = ds.Tables[0];
            //调整显示风格
            new Common.DataGridViewStyle().DgvStyle3(this.dgvScoreList);
        }     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //根据班级名称动态筛选
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ds==null)
            {
                return;
            }
            this.ds.Tables[0].DefaultView.RowFilter = "ClassName='"+this.cboClass.Text.Trim()+"'";
        }
        //显示全部成绩
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            this.ds.Tables[0].DefaultView.RowFilter = "ClassName like '%%'";
        }
        //根据C#成绩动态筛选
        private void txtScore_TextChanged(object sender, EventArgs e)
        {
            //输入的内容不能为空
            if (this.txtScore.Text.Trim().Length==0)
            {
                return;
            }
            //输入的必须是字符串
            if (!Common.DataValidate.IsInteger(this.txtScore.Text.Trim()))
            {
                return;
            }
            else
            {
                this.ds.Tables[0].DefaultView.RowFilter = "CSharp>" + this.txtScore.Text.Trim();
                //筛选后保存进入新的表格
                //新表进行筛选后赋值给窗体控件
                //this.ds.Tables[0].DefaultView.RowFilter = "ClassName='" + this.cboClass.Text.Trim() + "'";
            }

            
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
           // Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

        //打印当前的成绩信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }
    }
}
