using Libs.Prism.Implementations;
using Libs.Prism.Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFApp.Domain.Contracts;
using WPFApp.Domain.Entities;
using WPFApp.Domain.Models;
using WPFApp.Extras.Abstracts;
using WPFApp.Extras.Constants;

namespace WPFApp.ViewModels.Tasks.Pages
{
    public class DetailViewModel : AbstractViewModel, IResolvableNavigation
    {
        private TaskModel _model;
        public TaskModel Model
        {
            get { return _model; }
            set { _model = value; RaisePropertyChanged(() => Model); }
        }
        private readonly IGenericRepository _repository;

        public DetailViewModel(IServiceProvider provider, IGenericRepository repository) : base(provider)
        {
            _repository = repository;
        }

        public ICommand Save => new DelegateCommand(async () =>
        {
            var task = await RunAsync(() => _repository.Get<Task>(Model.Id));

            task.Alterar(Model.Name, Model.Description, Model.Deadline);

            await RunAsync(() => _repository.Update(task));

            await CommitAsync();

            PopAndNavigate(NavigationAreas.MAIN_AREA, NavigationRoutes.TASK_LIST);
        });


        public bool OnResolved(IDictionary<string, object> resolved)
        {
            if (resolved["Task"] is TaskModel task)
                Model = task;

            return true;
        }
    }
}
