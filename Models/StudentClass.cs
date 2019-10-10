using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{

    [Serializable]
    public class StudentClass
    {
       //班级 ID
        public int ClassId { get; set; }
        //班级名称
        public string ClassName { get; set; }
    }
}
