using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageClient.Command
{
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, Task> _command;
        private Predicate<object> _canExecute;

        public AsyncCommand(Func<object, Task> command, Predicate<object> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }
        public AsyncCommand(Func<object, Task> command)
        {
            _command = command;
            _canExecute = x => true;
        }
        public override bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command(parameter);
        }
    }
}
