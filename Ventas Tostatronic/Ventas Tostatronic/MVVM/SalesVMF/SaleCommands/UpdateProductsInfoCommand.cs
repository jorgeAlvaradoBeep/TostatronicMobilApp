using System;
using System.Windows.Input;

namespace Ventas_Tostatronic.MVVM.SalesVMF.SaleCommands
{
    public class UpdateProductsInfoCommand : ICommand
    {
        SaleVM VM;
        public UpdateProductsInfoCommand(SaleVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await VM.getProducts();
        }
    }
}

