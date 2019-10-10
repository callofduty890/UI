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

        //�༶������
        private StudentClassService objClassService = new StudentClassService();
        //ѧ��������
        private StudentService objStuService = new StudentService();

        //��������
        private List<Student> list = null;

        public FrmStudentManage()
        {
            InitializeComponent();
            //��ʼ���༶������
            this.cboClass.DataSource = objClassService.GetAllClasses();
            //
            this.cboClass.DisplayMember = "ClassName";
            this.cboClass.ValueMember = "ClassId";
        }
        //���հ༶��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region ������֤
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��༶��", "��ʾ");
                return;
            }
            #endregion

            #region ���ݽ���

            //���ղ�ѯ����
            list = objClassService.GetStudentsByClass(this.cboClass.Text);
            //����ʾ��δ��װ����
            this.dgvStudentList.AutoGenerateColumns = false;    
            //������Դ
            this.dgvStudentList.DataSource = list;
            //�޸���ʽ��ʾ��ʽ
            new Common.DataGridViewStyle().DgvStyle1(this.dgvStudentList);

            #endregion
        }
        //����ѧ�Ų�ѯ
        private void btnQueryById_Click(object sender, EventArgs e)
        {
            #region ������֤
            //ѧ���Ƿ�Ϊ��
            if (this.txtStudentId.Text.Trim().Length == 0)
            {
                MessageBox.Show("������ѧ�ţ�", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
                return;
            }
            //ѧ���Ƿ�Ϊ����
            if (!Common.DataValidate.IsInteger(this.txtStudentId.Text.Trim()))
            {
                MessageBox.Show("ѧ�ű�������������", "��ʾ��Ϣ");
                this.txtStudentId.SelectAll();
                this.txtStudentId.Focus();
                return;
            }
            //ѧ���Ƿ����-���������Ƿ����ѧ��
            Student objStudent = objStuService.GetStudentsById(this.txtStudentId.Text.Trim());
            //�ж��Ƿ�Ϊ��
            if (objStudent==null)
            {
                MessageBox.Show("ѧԱ��Ϣ�����ڣ�", "��ʾ��Ϣ");
                this.txtStudentId.Focus();
                return;
            }
            #endregion



            #region ���ݽ���
            FrmStudentInfo frmStudentInfo = new FrmStudentInfo(objStudent);
            frmStudentInfo.Show();
            #endregion

        }
        private void txtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        //˫��ѡ�е�ѧԱ������ʾ��ϸ��Ϣ
        private void dgvStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        //�޸�ѧԱ����
        private void btnEidt_Click(object sender, EventArgs e)
        {
            #region ��֤����
            //�Ƿ���ѧԱ��Ҫ�޸�
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("û���κ���Ҫ�޸ĵ�ѧԱ��Ϣ��", "��ʾ");
                return;
            }
            //�Ƿ���ѡ��
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ����Ҫ�޸ĵ�ѧԱ��Ϣ��", "��ʾ");
                return;
            }
            #endregion

            //��ȡѧ��
            string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();

            //ѧ���Ƿ����-���������Ƿ����ѧ��
            Student objStudent = objStuService.GetStudentsById(studentId);

            //��ʾ����
            //��ʾ�޸�ѧԱ��Ϣ����
            FrmEditStudent objEditStudent = new FrmEditStudent(objStudent);
            objEditStudent.ShowDialog();
            //ͬ��ˢ��
            btnQuery_Click(null, null);

        }
        //ɾ��ѧԱ����
        private void btnDel_Click(object sender, EventArgs e)
        {
            #region ������֤
            //����ѧԱ
            if (this.dgvStudentList.RowCount == 0)
            {
                MessageBox.Show("û���κ���Ҫɾ����ѧԱ��", "��ʾ");
                return;
            }
            //�Ƿ�ѡ��
            if (this.dgvStudentList.CurrentRow == null)
            {
                MessageBox.Show("��ѡ����Ҫɾ����ѧԱ��", "��ʾ");
                return;
            }
            #endregion



            #region ���ݽ���
                //��ʾ��Ϣ
                DialogResult result = MessageBox.Show("ȷʵҪɾ����", "ɾ��ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result==DialogResult.Cancel)
                {
                    return;
                }
                //��ȡѧ��
                string studentId = this.dgvStudentList.CurrentRow.Cells["StudentId"].Value.ToString();
                if (objStuService.DeleteStudentById(studentId)==1)
                {
                    btnQuery_Click(null, null);
                }
                else
                {
                    MessageBox.Show("ɾ��ʧ��");
                }
            #endregion

        }
        //��������
        private void btnNameDESC_Click(object sender, EventArgs e)
        {
         
        }
        //ѧ�Ž���
        private void btnStuIdDESC_Click(object sender, EventArgs e)
        {
         
        }
        //����к�
        private void dgvStudentList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvStudentList, e);
        }
        //��ӡ��ǰѧԱ��Ϣ
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //������Excel
        private void btnExport_Click(object sender, EventArgs e)
        {

        }
    }

   
}