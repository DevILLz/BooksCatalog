using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BooksCatalog.ViewModels
{
    public class INPC : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
        protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RaisePropertyChanged(object sender, [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Command : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public Command(Action<object> execute, Predicate<object> canExecute = null)
        {
            _canExecute = canExecute is null ? p => true : canExecute;
            _execute = execute;
        }
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public bool CanExecute(object parameter) => _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}
