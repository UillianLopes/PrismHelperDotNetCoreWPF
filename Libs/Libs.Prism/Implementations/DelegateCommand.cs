using System;
using System.Windows.Input;

namespace Libs.Prism.Implementations
{
    public class DelegateCommand<T> : ICommand where T : class
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _action;

        public DelegateCommand(Action<T> action, Func<T, bool> canExecute = null)
        {
            _action = action;

            if (canExecute != null)
                return;

            _canExecute = canExecute;
            RaiseCanExecuteChanged();
        }

        private void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter as T);

        public void Execute(object parameter) => _action(parameter as T);
    }
    
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;
        private readonly Action<object> _actionWithObject;
        private readonly Func<object, bool> _canExecute;

        private DelegateCommand(Func<object, bool> canExecute = null)
        {
            if (canExecute == null)
                return;

            _canExecute = canExecute;
            RaiseCanExecuteChanged();
        }

        public DelegateCommand(Action action, Func<object, bool> canExecute = null) : this(canExecute) => _action = action;

        public DelegateCommand(Action<object> actionWithObject, Func<object, bool> canExecute = null) : this(canExecute) => _actionWithObject = actionWithObject;

        private void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter)
        {
            if (_actionWithObject != null)
                _actionWithObject(parameter);
            else
                _action();
        }
    }

}
