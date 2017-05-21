using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindMe
{
    public class RemindMessage : INotifyPropertyChanged
    {
        private string message;


        public TimeSpan TimeSpan { get; set; }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
