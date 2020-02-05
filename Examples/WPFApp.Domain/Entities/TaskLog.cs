using System;
using WPFApp.Domain.Abstracts;
using WPFApp.Domain.Enums;

namespace WPFApp.Domain.Entities
{
    public class TaskLog : Entity
    {
        protected TaskLog() : base() { }

        public TaskLog(Task task, TaskState state, DateTime date) : this()
        {
            Task = task;
            State = state;
            Date = date;
        }

        public TaskLog(Task task, string message, TaskState state, DateTime date) : this(task, state, date)
        {
            Message = message;
        }

        public string Message { get; protected set; }
        public TaskState State { get; protected set; }
        public DateTime Date { get; protected set; }
        public Task Task { get; protected set; }

        public static TaskLog Started(Task task, string message) => new TaskLog(task, message, TaskState.Started, DateTime.Now);
        
        public static TaskLog Completed(Task task, string message) => new TaskLog(task, message, TaskState.Completed, DateTime.Now);
        
        public static TaskLog Paused(Task task, string message) => new TaskLog(task, message, TaskState.Paused, DateTime.Now);

        public static TaskLog Created(Task task) => new TaskLog(task, TaskState.Created, DateTime.Now);

    }
}
