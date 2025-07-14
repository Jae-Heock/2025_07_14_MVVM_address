using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _20250714_4
{
    internal class Command : ICommand
    {
        Action<object> ExecuteMethod;
        Func<Object, bool> CanExeCuteMethod;

        public Command(Action<object> execute, Func<Object, bool> canexecute)
        {
            this.ExecuteMethod = execute;
            this.CanExeCuteMethod = canexecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
