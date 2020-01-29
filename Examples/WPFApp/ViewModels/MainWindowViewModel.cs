using Libs.Prism.Implementations;
using System;
using System.Windows;
using System.Windows.Input;
using WPFApp.Domain.Services;
using WPFApp.Extras.Abstracts;

namespace WPFApp.ViewModels
{
    public class MainWindowViewModel : AbstractViewModel
    {
        private readonly AuthenticationService _authService;

        public ICommand Logout => new DelegateCommand<Window>((window) =>
        {
            _authService.Clear();

            Success("Deslogado com sucesso!", areaName: "main");

            window.Close();
        });

        public ICommand ContentRendered => new DelegateCommand<Window>((window) =>
        {
            if (!_authService.HasValidAuthentication)
                Login();
        });

        public MainWindowViewModel(IServiceProvider provider, AuthenticationService service) : base(provider)
        {
            _authService = service;
        }

        private void Login()
        {
            Navigate("MainArea", "login");
        }
    }
}
