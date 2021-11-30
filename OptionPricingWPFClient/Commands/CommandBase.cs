using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OptionPricingWPFClient.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        // tells if the command execute
        // if returns false => button should be disabled
        // if its return value changes => should raise CanExecuteChanged and the UI will execute this method to check if can execute
        public virtual bool CanExecute(object parameter)
        {
            return true; // by default
        }

        // what's gonna be execute when we click the button
        public abstract void Execute(object parameter);

        // helper method to raise the event CanExecuteChanged
        protected void OnCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
       
    }
}
