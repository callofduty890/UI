using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using DAL;
using Models;

namespace StudentManager
{
    public partial class FrmStudentManage : Form
    {

        //班级操作类
        private StudentClassService objClassService = new StudentClassService();
        //学生操作类
        private StudentService objStuService = new StudentService();

        //接收数据
        private List<Student> list = null;

        public FrmStudentManage()
        {
            InitializeComponent();
            //初始化班级下拉框
            this.cboClass.DataSource = objClassService.GetAllClasses();
            //
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
        }
        //按照班级查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("请选则班级！", "提示");
                return;
            }
            #endregion

            #region 数据交互

            //接收查询数据
            list = objClassService.GetStudentsByClass(this.cboClass.Text);
            //不显示尚未封装属性
            this.dgvStudentList.AutoGenerateColumns = false;    
            //绑定数据源
            this.dgvStudentList.DataSource = list;
            //修改样式显示样式
            new Common.DataGridViewStyle().DgvStyle1(this.dgvStudentList);

            #endregion
        }
        //根据学号查询
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //学号是否为空
            if (this.txtStudentId.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入学号！", "提示信息");
                this.txtStudentId.Focus();
                return;
            }
            //学号是否为整数
            if (!Common.DataValidate.IsInteger(this.txtStudentId.Text.Trim()))
            {
                MessageBox.Show("学号必须是正整数！", "提示信息");
                this.txtStudentId.SelectAll();
                this.txtStudentId.Focus();
                return;
            }
            //学号是否存在-查找数据是否存在学号
            Student objStudent = objStuService.GetStudentsById(this.txtStudentId.Text.Trim());
            //判断是否为空
            if (objStudent==null)
            {
                MessageBox.Show("学员信息不存在！", "提示信息");
                this.txtStudentId.Focus();
                return;
            }
            #endregion



            #region 数据交互
            FrmStudentInfo frmStudentInfo = new FrmStudentInfo(objStudent);
            frmStudentInfo.Show();
            #endregion

        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        //双击选中的学员对象并显示详细信息
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        //修改学员对象
        private void btnEidt_Click(object sender, EventArgs e)
        {
            #region 验证数据
            //是否有学员需要修改
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("没有任何需要修改的学员信息！", "提示");
                return;
            }
            //是否有选中
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的学员信息！", "提示");
                return;
            }
            #endregion

            //获取学号
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();

            //学号是否存在-查找数据是否存在学号
            Student objStudent = objStuService.GetStudentsById(studentId);

            //显示窗体
            //显示修改学员信息窗口
            FrmEditStudent objEditStudent = new FrmEditStudent(objStudent);
            objEditStudent.ShowDialog();
            //同步刷新
            btnQuery_Click(null, null);

        }
        //删除学员对象
        private void btnDel_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //有无学员
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("没有任何需要删除的学员！", "提示");
                return;
            }
            //是否选中
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的学员！", "提示");
                return;
            }
            #endregion



            #region 数据交互
                //提示信息
                DialogResult result = MessageBox.Show("确实要删除吗？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result==DialogResult.Cancel)
                {
                    return;
                }
                //获取学号
                string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
                if (objStuService.DeleteStudentById(studentId)==1)
                {
                    btnQuery_Click(null, null);
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            #endregion

        }
        //姓名降序
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
         
        }
        //学号降序
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
         
        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }
        //打印当前学员信息
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //导出到Excel
        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }

   
}