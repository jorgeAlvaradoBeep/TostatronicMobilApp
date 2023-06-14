using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ventas_Tostatronic.MVVM.SalesVMF;
using Ventas_Tostatronic.Services;
using Ventas_Tostatronic.Views.SalesVF;

namespace Ventas_Tostatronic.MVVM.Menu.MenuCommand
{
    public class ViewSelectionCommand : ICommand
    {
        MenuVM VM;
        public ViewSelectionCommand(MenuVM vm)
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
            string option = (string)parameter;
            if (option.Equals("Ventas"))
                await VM.pageService.PushAsync(new SaleV(new PageService()));
        }
    }
}
