using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemindMe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public RemindMessage remindMessage = new RemindMessage { Message = "Message Here" };

        public MainWindow()
        {
            InitializeComponent();
            InitializeBinding();            
        }

        private void TaskScheduler_MessageEvent(object sender, EventArgs e)
        {
            remindMessage.Message = (e as MessageEventArgs).Message;
        }

        private void InitializeBinding()
        {
            txtMessage.DataContext = remindMessage;
            //Binding binding = new Binding();
            //binding.Source = message;
            //binding.Path = new PropertyPath("Message");
            //Console.WriteLine(message.Message);
            //txtMessage.SetBinding(TextBox.TextProperty, binding);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var taskManager = new TaskManager();
            taskManager.Test();
            var taskScheduler = new TaskScheduler(taskManager);
            taskScheduler.MessageEvent += TaskScheduler_MessageEvent;
            taskScheduler.Start();

            //Timer timer = new Timer();
        }
    }
}
