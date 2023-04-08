namespace Ventas_Tostatronic.Views.SalesVF;

using Telerik.Maui.Controls;
using Ventas_Tostatronic.Models.Clients;
using Ventas_Tostatronic.Models.ProductsMFF;
using Ventas_Tostatronic.MVVM.SalesVMF;
using Ventas_Tostatronic.Services;

public partial class SaleV : ContentPage
{
    SaleVM vm;
    IPageService ps;
    public SaleV(PageService pageService)
	{
        
		InitializeComponent();
        vm = new SaleVM(pageService);
        ps = pageService;
        BindingContext = vm;

    }

    void txtAutoComplete_SuggestionItemSelected(System.Object sender, Telerik.Maui.Controls.AutoComplete.SuggestionItemSelectedEventArgs e)
    {
        ClientComplete ex = (ClientComplete)e.DataItem;
        if(ex!=null)
        {
            vm.CompleteSale.ClientSale = ex;
            vm.ActivateSaleControls = true;
        }
            
    }


    //Esta sección es del control de autocomplete text box. Se modifica por una versión con un despliegue de
    //sugerencias con mejor visualización que inluya nombre, código y precio actual.
    /*
    async void txtProducts_SuggestionItemSelected(System.Object sender, Telerik.Maui.Controls.AutoComplete.SuggestionItemSelectedEventArgs e)
    {
        SaleProduct sP = (SaleProduct)e.DataItem;
        if(sP!=null)
        {
            if (sP.DisplayPrice == 0)
                vm.CompleteSale.PriceType = vm.CompleteSale.PriceType;
            if (vm.CompleteSale.SaledProducts.Contains(sP))
                await ps.DisplayAlert("Error", $"Producto: {sP.nombre} ya se encuentra agregado.", "OK");
            else
            {
                sP.cantidadComprada = 1;
                vm.CompleteSale.SaledProducts.Add(sP);
                vm.CompleteSale.GetSubtotal();
            }
                
        }
    }*/

    void txtCantidad_ValueChanged(System.Object sender, Telerik.Maui.Controls.NumericInput.ValueChangedEventArgs e)
    {
        vm.CompleteSale.GetSubtotal();
    }
    /*async void lstSearchResults_ItemTapped(System.Object sender, Telerik.Maui.Controls.Compatibility.DataControls.ListView.ItemTapEventArgs e)
    {
        SaleProduct sP = (SaleProduct)e.Item;
        if(sP!=null)
        {
            if (vm.CompleteSale.SaledProducts.Contains(sP))
                await ps.DisplayAlert("Error", $"Producto: {sP.nombre} ya se encuentra agregado.", "OK");
            else
            {
                sP.cantidadComprada = 1;
                vm.CompleteSale.SaledProducts.Add(sP);
                vm.CompleteSale.GetSubtotal();
                vm.SearchPredictionText = string.Empty;
            }
        }
    }*/
}
