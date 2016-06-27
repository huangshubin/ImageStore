using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageClient.Command
{
    public class SyncCommand : ICommand
    {
        private readonly Action<object> _command;
        private Predicate<object> _canExecute;


        public SyncCommand(Action<object> command, Predicate<object> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _command(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
