using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

// it is not working - just an overall representation of API
namespace AsyncInternals
{
    public class MyTaskScheduler : TaskScheduler
    {
        private readonly BlockingCollection<Task> _tasks = new();

        protected override IEnumerable<Task> GetScheduledTasks() => _tasks;
    
        protected override void QueueTask(Task task)
        {
            _tasks.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return TryExecuteTask(task);
        }
    }
}