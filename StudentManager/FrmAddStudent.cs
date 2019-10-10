using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DAL;
using Models;
using Common;

namespace StudentManager
{
    public partial class FrmAddStudent : Form
    {
        //�������ݷ��ʶ���
        private StudentClassService objClassService = new StudentClassService();
        private StudentService objStudentService = new StudentService();
        //ѧ�����ͼ���
        private List<Student> stuList = new List<Student>();

        public FrmAddStudent()
        {
            InitializeComponent();
            //��ʼ���༶������
            this.cboClassName.DataSource = objClassService.GetAllClasses();
            this.cboClassName.DisplayMember = "ClassName";//�������������ʾ�ı�
            this.cboClassName.ValueMember = "ClassId";//������������ʾ�ı���Ӧ��value    
            this.dgvStudentList.AutoGenerateColumns = false; //��ֹ�Զ�������
        }
        //�����ѧԱ
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region  ��֤����

            if (this.txtStudentName.Text.Trim().Length == 0)
            {
                MessageBox.Show("ѧ����������Ϊ�գ�", "��ʾ��Ϣ");
                this.txtStudentName.Focus();
                return;
            }
            if (this.txtCardNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("���ڿ��Ų���Ϊ�գ�", "��ʾ��Ϣ");
                this.txtCardNo.Focus();
                return;
            }
            //��֤�Ա�
            if (!this.rdoFemale.Checked && !this.rdoMale.Checked)
            {
                MessageBox.Show("��ѡ��ѧ���Ա�", "��ʾ��Ϣ");
                return;
            }
            //��֤�༶
            if (this.cboClassName.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��༶��", "��ʾ��Ϣ");
                return;
            }
            //��֤����
            int age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year;
            if (age > 35 && age < 18)
            {
                MessageBox.Show("���������28-35��֮�䣡", "��ʾ��Ϣ");
                return;
            }
            //��֤���֤���Ƿ����Ҫ��
            if (!Common.DataValidate.IsIdentityCard(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤�Ų�����Ҫ��", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                return;
            }


            if (!this.txtStudentIdNo.Text.Trim().Contains(this.dtpBirthday.Value.ToString("yyyyMMdd")))
            {
                MessageBox.Show("���֤�źͳ������ڲ�ƥ�䣡", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //��֤���֤���Ƿ��ظ�
            if (objStudentService.IsIdNoExisted(this.txtStudentIdNo.Text.Trim()))
            {
                MessageBox.Show("���֤�Ų��ܺ�����ѧԱ���֤���ظ���", "��֤��ʾ");
                this.txtStudentIdNo.Focus();
                this.txtStudentIdNo.SelectAll();
                return;
            }
            //��֤�����Ƿ��ظ�
            if (objStudentService.IsCardNoExisted(this.txtCardNo.Text.Trim()))
            {
                MessageBox.Show("��ǰ�����Ѿ����ڣ�", "��֤��ʾ");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            #endregion

            #region ��װѧ������
            Student objStudent = new Student()
            {
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rdoMale.Checked ? "��" : "Ů",
                Birthday = Convert.ToDateTime(this.dtpBirthday.Text),
                StudentIdNo = this.txtStudentIdNo.Text.Trim(),
                PhoneNumber = this.txtPhoneNumber.Text.Trim(),
                ClassName = this.cboClassName.Text,
                StudentAddress = this.txtAddress.Text.Trim() == "" ? "��ַ����" : this.txtAddress.Text.Trim(),
                CardNo = this.txtCardNo.Text.Trim(),
                ClassId = Convert.ToInt32(this.cboClassName.SelectedValue),//��ȡѡ��༶��ӦclassId
                Age = DateTime.Now.Year - Convert.ToDateTime(this.dtpBirthday.Text).Year,
                StuImage = this.pbStu.Image != null ? new SerializeObjectToString().SerializeObject(this.pbStu.Image) : ""
            };
            #endregion

            #region ���ú�̨����-�������ݿ⵱��
            int studentId = objStudentService.AddStudent(objStudent);
            if (studentId>1)
            {
                //ͬ����ʾ��ӵ�ѧԱ
                objStudent.StudentId = studentId;
                //��ӽ��뷺�ͼ��ϵ���
                this.stuList.Add(objStudent);
                //����б�����
                this.dgvStudentList.DataSource = null;
                //ָ������Դ
                this.dgvStudentList.DataSource = this.stuList;

                //ѯ���Ƿ�������ѧԱ��Ϣ
                DialogResult result = MessageBox.Show("��ѧԱ��ӳɹ�!�Ƿ������ӣ�", "��ʾ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //����û��������Ϣ
                    foreach (Control item in this.gbstuinfo.Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = "";
                        } 
                    }
                    //����������
                    this.cboClassName.SelectedIndex = -1;
                    //������ѡ��
                    this.rdoFemale.Checked = false;
                    this.rdoMale.Checked = false;
                    this.txtStudentName.Focus();
                    //�������뽹��
                    this.pbStu.Image = null;
                }
            }
            else
            {
                MessageBox.Show("����ʧ��");
            }
            #endregion
        }
        //�رմ���
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmAddStudent_KeyDown(object sender, KeyEventArgs e)
        {
       

        }
        //ѡ������Ƭ
        private void btnChoseImage_Click(object sender, EventArgs e)
        {
            //���´�����
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            //�ж��Ƿ������ַ
            if (result==DialogResult.OK)
            {
                this.pbStu.Image = Image.FromFile(openFileDialog.FileName);
            }


        }
        //��������ͷ
        private void btnStartVideo_Click(object sender, EventArgs e)
        {
         
        }
        //����
        private void btnTake_Click(object sender, EventArgs e)
        {
        
        }
        //�����Ƭ
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.pbStu.Image = null;
        }

     
    }
}