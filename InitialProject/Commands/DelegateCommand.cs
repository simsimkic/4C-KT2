using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _executed;
        private readonly Action<object> _executed2;

        private readonly Func<bool> _canExecute;

        public DelegateCommand(Action execute) : this(execute, null)
        {
        }

        public DelegateCommand(Func<bool> canExecute)
        {
            _canExecute = canExecute;
        }

        public DelegateCommand(Action<object> executed2)
        {
            _executed2 = executed2;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public DelegateCommand(Func<bool> executed, Func<bool> canExecute)
        {
            _executed = executed ?? throw new ArgumentNullException(nameof(executed));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
            else if (_executed2 != null)
            {
                _executed2(parameter);
            }
        }

        public void Executed(object parameter)
        {
            _executed();
        }
    }

}
