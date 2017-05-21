using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemindMe
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string s) { Message = s; }
        public string Message { get; private set; }
    }

    class TaskScheduler
    {
        readonly int PollingTime = 30;
        CancellationTokenSource cts;
        TaskManager taskManager;

        //public delegate void MessageHandler(object sender, Eve)

        public event EventHandler MessageEvent;

        public TaskScheduler(TaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        public void Start()
        {
            // TODO:
            // 启动轮询线程，每PollingTime秒轮询一次任务列表，如果距离任务开始时间小于PollingTime
            // 则启动一个子线程，sleep (PollingTime - startTime)ms 之后，向主界面发送消息
            // 直到任务轮询完毕，进入sleep并等待下次轮询
            cts = new CancellationTokenSource();
            var task = Task.Run(new Action(() => { Polling(cts.Token); }));
        }

        public void Polling()
        {
            Polling(CancellationToken.None);
        }

        public void Polling(CancellationToken cancellationToken)
        {
            int second = 0;
            ScanTask();
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (second == PollingTime)
                {
                    ScanTask();
                    second = 0;
                }
                else
                {
                    Thread.Sleep(1000);
                    second++;
                }
            }
        }

        public void Stop()
        {
            cts.Cancel();
        }

        private void ScanTask()
        {
            foreach (var task in taskManager)
            {
                if (task.TimeSpan.TotalSeconds < PollingTime)
                {
                    Task.Run(new Action(() =>
                    {
                        Thread.Sleep((int)(task.TimeSpan.TotalSeconds * 1000));
                        if (MessageEvent != null)
                        {
                            MessageEvent(this, new MessageEventArgs(task.Message));
                        }
                    }));
                }
            }

            taskManager.ClearTasks();
        }
    }
}
