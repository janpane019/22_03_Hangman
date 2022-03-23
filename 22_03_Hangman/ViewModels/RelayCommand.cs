using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace _22_03_Hangman.ViewModels
{
    internal class RelayCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null) return true;
            if (parameter == default) return _canExecute(default);
            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    internal class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _execute;
        private Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}