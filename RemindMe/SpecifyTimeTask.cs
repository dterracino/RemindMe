using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindMe
{
    class SpecifyTimeTask : RemindTask
    {
        public SpecifyTimeTask(string timeDescription) : base(timeDescription)
        {
        }

        public override TimeSpan? TimeSpan2Now()
        {
            return SpecifyTimeMode(TimeDescription.Trim());
        }

        private TimeSpan? SpecifyTimeMode(string strTime)
        {
            DateTime dateTime;
            if (DateTime.TryParse(strTime, out dateTime))
            {
                return dateTime - DateTime.Now;
            }
            return null;
        }
    }
}
