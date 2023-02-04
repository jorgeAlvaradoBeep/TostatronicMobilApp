namespace Ventas_Tostatronic.Views.SalesVF;

using Ventas_Tostatronic.Models.Clients;
using Ventas_Tostatronic.MVVM.SalesVMF;
using Ventas_Tostatronic.Services;

public partial class SaleV : ContentPage
{
    SaleVM vm;
    public SaleV(PageService pageService)
	{
		InitializeComponent();
        vm = new SaleVM(pageService);
        BindingContext = vm;

    }

    void txtAutoComplete_SuggestionItemSelected(System.Object sender, Telerik.Maui.Controls.AutoComplete.SuggestionItemSelectedEventArgs e)
    {
        ClientComplete ex = (ClientComplete)e.DataItem;
        if(ex!=null)
        {
            new Action(async () => await vm.getProducts())();
            vm.CompleteSale.ClientSale = ex;
        }
            
    }
}
