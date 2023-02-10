using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ventas_Tostatronic.Models.ProductsMFF;
using Ventas_Tostatronic.Models.SalesMF;
using Ventas_Tostatronic.Services;

namespace Ventas_Tostatronic.MVVM.SalesVMF.SaleCommands
{
    public class SaveSaleCommand : ICommand
    {
        SaleVM VM;
        public SaveSaleCommand(SaleVM vm)
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
            if(VM.CompleteSale.ClientSale==null)
            {
                await VM.pageService.DisplayAlert("Error", "Debe seleccionar un cliente", "Ok");
                return;
            }
            if(VM.CompleteSale.SaledProducts==null) 
            {
                await VM.pageService.DisplayAlert("Error", "Debe agregar al menos un producto a la venta", "Ok");
                return;
            }
            if (VM.CompleteSale.SaledProducts.Count==0)
            {
                await VM.pageService.DisplayAlert("Error", "Debe agregar al menos un producto a la venta", "Ok");
                return;
            }

            VM.GettingData = true;
            Response rmp = await WebService.InsertData(VM.CompleteSale,URLData.ventas);
            if (rmp.succes)
            {
                await VM.pageService.DisplayAlert("Exito", $"Venta #{rmp.message} agregada correctamente", "Ok");
                VM.SearClientVisibility = false;
                VM.SeeSaleControls = false;
                VM.ActivateSaleControls = false;
                VM.SearchResultListDisplay = false;
                VM.SearClientButton = true;
                VM.CompleteSale.SaledProducts.Clear();
                VM.CompleteSale = new CompleteSaleM();
                VM.CompleteSale.FechaDeVenta = DateTime.Now.ToString();
                VM.CompleteSale.SalerID = 1;
                VM.CompleteSale.PriceType = 2;
                VM.CompleteSale.SaledProducts = new ObservableCollection<SaleProduct>();
                VM.CompleteSale.SearchedProducts = new List<SaleProduct>();
                await VM.getProducts();
                VM.NeedFactura = false;
            }
            else
            {
                await VM.pageService.DisplayAlert("Error", $"Error al insertar:" +
                    $"{Environment.NewLine}{rmp.message}", "Ok");
                VM.GettingData = false;
                return;
            }
            VM.GettingData = false; 
        }
    }
}
