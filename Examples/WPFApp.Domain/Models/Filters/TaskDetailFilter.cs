using Libs.Prism.Abstracts;
using System;
using System.Linq;

using WPFApp.Domain.Contracts;
using WPFApp.Domain.Entities;

namespace WPFApp.Domain.Models.Filters
{
    public class TaskDetailFilter : ValidableModel, IFilter<Task, TaskModel>
    {
        public TaskDetailFilter(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public IQueryable<TaskModel> Apply(IQueryable<Task> query) =>
            query.Where(v => v.Id == Id)
                .OrderBy(tsk => tsk.Deadline)
                .Select(tsk => new TaskModel
                {
                    Id = tsk.Id,
                    CurrentState = tsk
                        .Logs
                        .OrderBy(log => log.Date)
                        .Select(log => log.State)
                        .LastOrDefault(),
                    Description = tsk.Description,
                    Name = tsk.Name,
                    Deadline = tsk.Deadline
                });
    }
}
