using System;
using System.Collections.Generic;
using WPFApp.Domain.Abstracts;

namespace WPFApp.Domain.Entities
{
    public class Task : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime Deadline { get; protected set; }
        public ICollection<TaskLog> Logs { get; }

        protected Task() : base() 
        {
            Logs = new List<TaskLog>();
        }

        public Task(string name, string description, DateTime limit) : this()
        {
            Name = name;
            Description = description;
            Deadline = limit;
        }

        public void Alterar(string name, string description, DateTime limit)
        {
            Name = name;
            Description = description;
            Deadline = limit;
        }

        public void Start(string message) => Logs.Add(TaskLog.Started(this, message));

        public void Pause(string message) => Logs.Add(TaskLog.Paused(this, message));

        public void Complete(string message) => Logs.Add(TaskLog.Completed(this, message));

        public void Create() => Logs.Add(TaskLog.Created(this));
    }
}
