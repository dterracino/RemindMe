using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindMe
{
    abstract class RemindTask
    {
        public RemindTask(string timeDescription)
        {
            var str = timeDescription.Trim().Split('|');
            if (str.Length == 2)
            {
                TimeDescription = str[0];
                Message = str[1];
            }

            Invalidation = true;
        }


        public bool Invalidation { get; set; }

        public abstract TimeSpan? TimeSpan2Now();

        public string Message { get; set; }

        public string TimeDescription { get; private set; }
    }
}
