using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindMe
{
    /// <summary>
    /// 一次性任务
    /// +hh:mm:ss
    /// +mm:ss
    /// +ss
    /// </summary>
    class OneTimeTask : RemindTask
    {
        public OneTimeTask(string timeDescription) : base(timeDescription)
        {
        }

        public override TimeSpan? TimeSpan2Now()
        {
            return AddMode(TimeDescription.Trim());
        }

        private TimeSpan? AddMode(string strTime)
        {
            if (strTime[0] != '+')
                return null;

            // 去除加号后面的字符串
            strTime = strTime.Substring(1);
            TimeSpan timeSpan;
            if (TimeSpan.TryParse(strTime, out timeSpan))
                return timeSpan;
            else
                return null;
        }
    }
}
