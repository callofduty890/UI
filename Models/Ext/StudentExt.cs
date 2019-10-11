using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Ext
{
    /// <summary>
    /// 学员信息拓展
    /// </summary>
    [Serializable]
    public class StudentExt:Student
    {
        //签到时间
        public DateTime DTime { get; set; }
    }
}
