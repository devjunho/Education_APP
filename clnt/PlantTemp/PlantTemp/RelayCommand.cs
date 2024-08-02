using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlantTemp
{
    internal class RelayCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Predicate<T> _predicate;

        public RelayCommand(Action<T> execute, Predicate<T> predicate)
        {
            _execute = execute;
            _predicate = predicate;
        }

        public RelayCommand(Action<T> action) : this(action, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null ? true : _predicate((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
