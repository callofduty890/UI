using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Models;
using DAL;
using DAL.Helper;

namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {
        private ScoreListService objScoreService = new ScoreListService();
        public FrmScoreManage()
        {
            InitializeComponent();
            //断开下拉框事件
            this.cboClass.SelectedIndexChanged -= new EventHandler(this.cboClass_SelectedIndexChanged);
            //初始化班级列表
            this.cboClass.DataSource = objScoreService.GetAllClasses();
            //设置下拉框显示内容
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;
            //恢复下拉框事件
            this.cboClass.SelectedIndexChanged += new EventHandler(this.cboClass_SelectedIndexChanged);

        }     
        //根据班级查询      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 验证数据
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要查询的班级", "查询提示");
                return;
            }
            #endregion

            
            this.dgvScoreList.AutoGenerateColumns = false;

            //获取班级全部成绩
            this.dgvScoreList.DataSource = objScoreService.GetSCoreList(this.cboClass.Text.Trim());
            //调整显示格式
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);

            //显示班级考试信息
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]考试成绩统计";

            //查询参加人数与考试成绩 未参加考试的人员名单
            Dictionary<string, string> dic =
                objScoreService.GetScoreInfoByClassId(this.cboClass.SelectedValue.ToString());
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absentCount"];
            //显示缺考人员姓名
            List<string> list =
                objScoreService.GetAbsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            if (list.Count == 0)
            {
                this.lblList.Items.Add("没有缺考");
            }
            else
            {
                lblList.Items.AddRange(list.ToArray());
                //   lblList.DataSource = list;
            }
        }
        //关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //统计全校考试成绩
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "全校考试成绩统计";
            //获取全部参考人数
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreService.GetSCoreList("");
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);
            //查询并显示成绩统计
            Dictionary<string, string> dic = objScoreService.GetScoreInfo();
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absectCount"];
            //显示缺考人员姓名
            List<string> list = objScoreService.GetAbsentList();
            lblList.Items.Clear();
            lblList.Items.AddRange(list.ToArray());
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //选择框选择改变处理
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
         
        }

       
    }
}