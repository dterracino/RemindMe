using System;
using System.Linq;
using System.Collections.Generic;

namespace RemindMe
{
    class TaskManager
    {
        List<RemindTask> taskList = new List<RemindTask>();
        object lockObj = new object();

        public IEnumerator<RemindMessage> GetEnumerator()
        {
            foreach (var task in taskList)
            {
                var timeSpan = task.TimeSpan2Now();
                if (timeSpan != null)
                {
                    if (task is OneTimeTask || task is SpecifyTimeTask)
                        task.Invalidation = false;
                    yield return new RemindMessage() { Message = task.Message, TimeSpan = timeSpan.Value };
                }
                else
                    continue;
            }
            yield break;
        }

        public void Test()
        {
            for (int i = 1; i < 30; ++i)
            {
                taskList.Add(new OneTimeTask(string.Format("+00:00:{0} | 提醒我{0}次！", i, i)));
            }
            
            //taskList.Add(new SpecifyTimeTask("2017.5.21 19:05:00 | 提醒我二次！"));
        }

        public void Add(RemindTask task)
        {
            lock (lockObj)
            {
                taskList.Add(task);
            }
        }

        // 清理过期任务
        public void ClearTasks()
        {
            taskList = taskList.FindAll(task => task.Invalidation == true);
        }

        public void Remove(RemindTask task)
        {
            lock (lockObj)
            {
                taskList.Remove(task);
            }
        }

        public void Read(string tasks)
        {
        }

        public string Write()
        {
            return string.Empty;
        }
    }
}
