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
            //�Ͽ��������¼�
            this.cboClass.SelectedIndexChanged -= new EventHandler(this.cboClass_SelectedIndexChanged);
            //��ʼ���༶�б�
            this.cboClass.DataSource = objScoreService.GetAllClasses();
            //������������ʾ����
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
            this.cboClass.SelectedIndex = -1;
            //�ָ��������¼�
            this.cboClass.SelectedIndexChanged += new EventHandler(this.cboClass_SelectedIndexChanged);

        }     
        //���ݰ༶��ѯ      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ��֤����
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��Ҫ��ѯ�İ༶", "��ѯ��ʾ");
                return;
            }
            #endregion

            
            this.dgvScoreList.AutoGenerateColumns = false;

            //��ȡ�༶ȫ���ɼ�
            this.dgvScoreList.DataSource = objScoreService.GetSCoreList(this.cboClass.Text.Trim());
            //������ʾ��ʽ
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);

            //��ʾ�༶������Ϣ
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]���Գɼ�ͳ��";

            //��ѯ�μ������뿼�Գɼ� δ�μӿ��Ե���Ա����
            Dictionary<string, string> dic =
                objScoreService.GetScoreInfoByClassId(this.cboClass.SelectedValue.ToString());
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absentCount"];
            //��ʾȱ����Ա����
            List<string> list =
                objScoreService.GetAbsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            if (list.Count == 0)
            {
                this.lblList.Items.Add("û��ȱ��");
            }
            else
            {
                lblList.Items.AddRange(list.ToArray());
                //   lblList.DataSource = list;
            }
        }
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //ͳ��ȫУ���Գɼ�
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "ȫУ���Գɼ�ͳ��";
            //��ȡȫ���ο�����
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreService.GetSCoreList("");
            new Common.DataGridViewStyle().DgvStyle1(this.dgvScoreList);
            //��ѯ����ʾ�ɼ�ͳ��
            Dictionary<string, string> dic = objScoreService.GetScoreInfo();
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absectCount"];
            //��ʾȱ����Ա����
            List<string> list = objScoreService.GetAbsentList();
            lblList.Items.Clear();
            lblList.Items.AddRange(list.ToArray());
        }

        private void dgvScoreList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvScoreList, e);
        }

    
     
        //ѡ���ѡ��ı䴦��
        private void dgvScoreList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
         
        }

       
    }
}