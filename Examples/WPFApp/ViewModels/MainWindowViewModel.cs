using Libs.Prism.Implementations;
using System;
using System.Windows;
using System.Windows.Input;
using WPFApp.Extras.Abstracts;

namespace WPFApp.ViewModels
{
    public class MainWindowViewModel : AbstractViewModel
    {

        public ICommand ContentRendered => new DelegateCommand<Window>((window) =>
        {

        });

        public MainWindowViewModel(IServiceProvider provider) : base(provider)
        {
        }

        private void Login()
        {
            Navigate("MainArea", "login");
        }
    }
}
