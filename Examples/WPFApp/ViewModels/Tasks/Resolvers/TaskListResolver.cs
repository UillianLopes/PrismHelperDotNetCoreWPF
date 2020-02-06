using Libs.Prism.Navigation.Interfaces;
using Libs.Prism.Navigation.Options;
using System.Threading.Tasks;
using WPFApp.Domain.Contracts;
using WPFApp.Domain.Models.Filters;

namespace WPFApp.ViewModels.Tasks.Resolvers
{
    public class TaskListResolver : INavigationResolver
    {
        public string Key => "TaskList";

        private readonly IGenericRepository _repository;

        public TaskListResolver(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Resolve(NavigationSnapshot snapshot) => await _repository
            .GetList(new TaskListFilter());


    }
}
