using Libs.Prism.Navigation.Interfaces;
using Libs.Prism.Navigation.Options;
using System;
using System.Threading.Tasks;
using WPFApp.Domain.Contracts;
using WPFApp.Domain.Models.Filters;

namespace WPFApp.ViewModels.Tasks.Resolvers
{
    public class TaskDetailResolver : INavigationResolver
    {
        public string Key => "Task";

        private readonly IGenericRepository _repository;

        public TaskDetailResolver(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Resolve(NavigationSnapshot snapshot) => await _repository
            .Get(new TaskDetailFilter(snapshot.QueryParams["Id"] is Guid id ? id : Guid.Empty));
    }
}
