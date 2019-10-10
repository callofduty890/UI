using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    [Serializable]
    public class Student
    {
        private int studentId;
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        private string studentName;
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }

        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        //将数据库中的18位整数转换成字符串
        public string StudentIdNo { get; set; }

        public int Age { get; set; }
        public string StuImage { get; set; }
        public string PhoneNumber { get; set; }
        public string StudentAddress { get; set; }
        public string CardNo { get; set; }
        public int ClassId { get; set; }

        public string ClassName { get; set; }
    }
}
