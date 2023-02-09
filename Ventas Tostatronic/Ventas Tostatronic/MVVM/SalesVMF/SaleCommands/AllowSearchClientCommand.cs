using System;
using System.Windows.Input;

namespace Ventas_Tostatronic.MVVM.SalesVMF.SaleCommands
{
	public class AllowSearchClientCommand:ICommand
	{
        SaleVM VM;
        public AllowSearchClientCommand(SaleVM vm)
		{
            VM = vm;
		}

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.SearClientVisibility = true;
            VM.SearClientButton = false;
        }
    }
}

