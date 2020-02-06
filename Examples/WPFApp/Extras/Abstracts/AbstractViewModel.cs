using Libs.Prism.Abstracts;
using Libs.Prism.Navigation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Wpf.Core;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace WPFApp.Extras.Abstracts
{
    public abstract class AbstractViewModel : BindableModel
    {
        private readonly IServiceProvider _provider;
        private readonly INavigationService _service;

        private bool _buzy;
        public bool Buzy
        {
            get => _buzy;
            set 
            {
                _buzy = value;
                RaisePropertyChanged(() => Buzy);
            }
        }

        private readonly NotificationManager _manager;

        protected AbstractViewModel(IServiceProvider provider)
        {
            _provider = provider;
            _service = Resolve<INavigationService>();
            _manager = Resolve<NotificationManager>();
        }


        protected T Dispatch<T>(Func<T> func) => Application.Current.Dispatcher.Invoke(func);

        protected void Dispatch(Action action) => Application.Current.Dispatcher.Invoke(action);

        protected void Shutdown() => Application.Current.Shutdown();

        protected async void Success(string message, string title = "Sucesso", string areaName = null)
        {
            var content = new NotificationContent
            {
                Message = message,
                Title = title,
                Type = NotificationType.Success
            };

            await _manager.ShowAsync(content, areaName);
        }

        protected async void Warning(string message, string title = "Aviso", string areaName = null)
        {
            var content = new NotificationContent
            {
                Message = message,
                Title = title,
                Type = NotificationType.Warning
            };

            await _manager.ShowAsync(content, areaName);
        }

        protected async void Error(string message, string title = "Erro", string areaName = null)
        {
            var content = new NotificationContent
            {
                Message = message,
                Title = title,
                Type = NotificationType.Error
            };

            await _manager.ShowAsync(content, areaName);
        }

        protected async void Information(string message, string title = "Informação", string areaName = null)
        {
            var content = new NotificationContent
            {
                Message = message,
                Title = title,
                Type = NotificationType.Information
            };

            await _manager.ShowAsync(content, areaName);
        }

        protected T Resolve<T>() => _provider.GetRequiredService<T>();

        protected Task<T> RunAsync<T>(Func<Task<T>> func) => Task.Run(async () => await func());

        protected Task RunAsync(Func<Task> func) => Task.Run(async () => await func());

        protected Task<T> RunAsync<T>(Func<T> func) => Task.Run(() => func());

        protected Task RunAsync<T>(Action action) => Task.Run(() => action());

        protected void Navigate(string areaName, string route, object param = null) =>
            Dispatch(() => _service.Navigate(areaName, route, useHistory: false, param));

    }
}
