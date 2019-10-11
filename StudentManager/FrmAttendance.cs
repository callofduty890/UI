using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DAL;
using Models;


namespace StudentManager
{
    public partial class FrmAttendance : Form
    {
        //初始化考勤对象
        private AttendanceService objAttendanceService = new AttendanceService();

        public FrmAttendance()
        {
            InitializeComponent();
            //获取考勤人数
            this.lblCount.Text = objAttendanceService.GetAllStudent();
            //启动时间
            timer1_Tick(null, null);
            //初始化显示人数
            ShowStat();
        }
        private void ShowStat()
        {
            //显示实际的出勤人数
            this.lblReal.Text = objAttendanceService.GetAttendStudents(DateTime.Now, true);
            //显示缺勤人数
            this.lblAbsenceCount.Text = (Convert.ToInt32(this.lblCount.Text.Trim()) - Convert.ToInt32(this.lblReal.Text.Trim())).ToString();


        }
        //显示当前时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取年
            this.lblYear.Text = DateTime.Now.Year.ToString();
            //获取月
            this.lblMonth.Text = DateTime.Now.Month.ToString();
            //获取今天日期
            this.lblDay.Text = DateTime.Now.Day.ToString();
            //获取当前时间
            this.lblTime.Text = DateTime.Now.ToLongTimeString();

            //获取当前时间
            DayOfWeek week = DateTime.Now.DayOfWeek;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    this.lblWeek.Text = "一";
                    break;
                case DayOfWeek.Tuesday:
                    this.lblWeek.Text = "二";
                    break;
                case DayOfWeek.Wednesday:
                    this.lblWeek.Text = "三";
                    break;
                case DayOfWeek.Thursday:
                    this.lblWeek.Text = "四";
                    break;
                case DayOfWeek.Friday:
                    this.lblWeek.Text = "五";
                    break;
                case DayOfWeek.Saturday:
                    this.lblWeek.Text = "六";
                    break;
                case DayOfWeek.Sunday:
                    this.lblWeek.Text = "日";
                    break;
            }
        }
        //学员打卡        
        private void txtStuCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            //判断是否有输入内容,是否按下回车键
            if (this.txtStuCardNo.Text.Trim().Length != 0 && e.KeyValue == 13)
            {
                //显示学员信息
                Student objStu = new StudentService().GetStudentByCardNo(this.txtStuCardNo.Text.Trim());
                //没有查找到的情况
                if (objStu==null)
                {
                    MessageBox.Show("卡号不正确！", "信息提示");
                    this.lblInfo.Text = "打卡失败！";
                    this.txtStuCardNo.SelectAll();
                    this.lblStuName.Text = "";
                    this.lblStuClass.Text = "";
                    this.lblStuId.Text = "";
                    this.pbStu.Image = null;
                    return;
                }
                //查找到的显示学生姓名 班级  学生ID
                this.lblStuName.Text = objStu.StudentName;
                this.lblStuClass.Text = objStu.ClassName;
                this.lblStuId.Text = objStu.StudentId.ToString();
                this.pbStu.Image = Image.FromFile("default.png");

                //添加打卡信息
                string result = objAttendanceService.AddRecord(this.txtStuCardNo.Text.Trim());
                if (result != "success")
                {
                    this.lblInfo.Text = "打卡失败！";
                    MessageBox.Show(result, "错误提示");
                }
                else
                {
                    this.lblInfo.Text = "打卡成功！";
                    ShowStat();
                    this.txtStuCardNo.Text = ""; //等待下一个打卡
                    this.txtStuCardNo.Focus();
                }
            }
        }
    
        //结束打卡
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
