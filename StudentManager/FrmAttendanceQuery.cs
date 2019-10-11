using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//引入数据库
using Models;
using DAL;


namespace StudentManager
{
    public partial class FrmAttendanceQuery : Form
    {
        //实例化考勤操作类
        private AttendanceService objAService = new AttendanceService();


        public FrmAttendanceQuery()
        {
            InitializeComponent();
        }
        //查询结果
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //出现结果
            //获取当前的时间- 时间选择 开始时间
            DateTime dt1 = Convert.ToDateTime(this.dtpTime.Text);
            //结束时间
            DateTime dt2 = dt1.AddDays(1.0);

            this.dgvStudentList.AutoGenerateColumns = false;
            //按时间查询获取全部的考勤结果
            this.dgvStudentList.DataSource = objAService.GetStuByDate(dt1, dt2, this.txtName.Text.Trim());
            //调试显示风格
            new Common.DataGridViewStyle().DgvStyle3(this.dgvStudentList);

            //获取考勤学生总数
            this.lblCount.Text = objAService.GetAllStudent();
            //实际到的人数
            this.lblReal.Text = objAService.GetAttendStudents(Convert.ToDateTime(this.dtpTime.Text), false);
            //显示缺勤
            this.lblAbsenceCount.Text = (Convert.ToInt32(this.lblCount.Text.Trim()) - Convert.ToInt32(this.lblReal.Text.Trim())).ToString();
        }
        //添加行号
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
