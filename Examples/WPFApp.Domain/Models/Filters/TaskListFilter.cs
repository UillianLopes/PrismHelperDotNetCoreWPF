using Libs.Prism.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using WPFApp.Domain.Contracts;
using WPFApp.Domain.Entities;

namespace WPFApp.Domain.Models.Filters
{
    public class TaskListFilter : ValidableModel, IFilter<Task, TaskListItemModel>
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; RaisePropertyChanged(() => Text); }
        }


        public IQueryable<TaskListItemModel> Apply(IQueryable<Task> query) => 
            query.Where(v => Text == null || EF.Functions.Like(v.Name, $"%{Text}"))
                .OrderBy(tsk => tsk.Deadline)
                .Select(tsk => new TaskListItemModel 
                { 
                    Id = tsk.Id,
                    CurrentState = tsk
                        .Logs
                        .OrderBy(log => log.Date)
                        .Select(log => log.State)
                        .LastOrDefault(),
                    Description = tsk.Description,
                    Name = tsk.Name
                });
    }
}
