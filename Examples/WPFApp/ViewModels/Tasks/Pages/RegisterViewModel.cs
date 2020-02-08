using Libs.Prism.Implementations;
using System;
using System.Windows.Input;
using WPFApp.Domain.Contracts;
using WPFApp.Domain.Entities;
using WPFApp.Domain.Models.Commands;
using WPFApp.Extras.Abstracts;
using WPFApp.Extras.Constants;

namespace WPFApp.ViewModels.Tasks.Pages
{
    public class RegisterViewModel : AbstractViewModel
    {
        private TaskRegisterModel _model = new TaskRegisterModel();
        public TaskRegisterModel Model
        {
            get { return _model; }
            set 
            { 
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        private readonly IGenericRepository _repository;

        public RegisterViewModel(IServiceProvider provider, IGenericRepository repository) : base(provider)
        {
            _repository = repository;
        }

        public ICommand Save => new DelegateCommand(async () =>
        {
            var task = new Task(Model.Name, Model.Description, Model.Deadline.GetValueOrDefault());

            await RunAsync(() => _repository.Insert(task));

            await CommitAsync();

            PopAndNavigate(NavigationAreas.MAIN_AREA, NavigationRoutes.TASK_LIST);
        });
    }
}
