using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Ventas_Tostatronic.MVVM.SalesVMF.SaleCommands;
using Ventas_Tostatronic.Services;
using Ventas_Tostatronic.Models.Clients;
using Ventas_Tostatronic.Models.SalesMF;
using Ventas_Tostatronic.Models.ProductsMFF;

namespace Ventas_Tostatronic.MVVM.SalesVMF
{
    public class SaleVM: BaseNotifyPropertyChanged
    {
        IPageService pageService;
        #region Properties
        bool gettingData;
        public bool GettingData
        {
            get
            {
                return gettingData;
            }
            set
            {
                SetValue(ref gettingData, value);
            }
        }
        bool searClientVisibility;
        public bool SearClientVisibility
        {
            get
            {
                return searClientVisibility;
            }
            set
            {
                SetValue(ref searClientVisibility, value);
            }
        }
        bool searClientButton;
        public bool SearClientButton
        {
            get
            {
                return searClientButton;
            }
            set
            {
                SetValue(ref searClientButton, value);
            }
        }
        bool seeSaleControls;
        public bool SeeSaleControls
        {
            get
            {
                return seeSaleControls;
            }
            set
            {
                SetValue(ref seeSaleControls, value);
            }
        }
        private ObservableCollection<ClientComplete> clientes;

        public ObservableCollection<ClientComplete> Clients
        {
            get { return clientes; }
            set { SetValue(ref clientes, value); }
        }
        public CompleteSaleM CompleteSale { get; set; }


        #endregion

        #region PropertyCommands
        public AllowSearchClientCommand AllowSearchClientCommand { get; set; }
        #endregion
        public SaleVM(PageService ps)
        {
            pageService = ps;
            GettingData = false;
            SearClientVisibility = false;
            SearClientButton = true;
            AllowSearchClientCommand = new AllowSearchClientCommand(this);
            CompleteSale = new CompleteSaleM();
            CompleteSale.PriceType = 2;
            
            new Action(async () => await getClients())();
            SeeSaleControls = false;
        }

        async Task getClients()
        {
            GettingData = true;
            Response rmp = await WebService.GetDataNode(URLData.getClients, "");
            if (rmp.succes)
            {
                Clients = JsonConvert.DeserializeObject<ObservableCollection<ClientComplete>>(rmp.data.ToString());
            }
            else
            {
                Clients = new ObservableCollection<ClientComplete>();
                await pageService.DisplayAlert("Error al extraer datos", $"Error al traer clientes: {rmp.message}", "Ok");
                Console.WriteLine(rmp.message);
            }
            SeeSaleControls = true;
            GettingData = false;
        }

        public async Task getProducts()
        {
            GettingData = true;
            Response rmp = await WebService.GetDataNode(URLData.getProducts, "");
            if (rmp.succes)
            {
                CompleteSale.SearchedProducts = JsonConvert.DeserializeObject<List<SaleProduct>>(rmp.data.ToString());
            }
            else
            {
                CompleteSale.SearchedProducts = new List<SaleProduct>();
                await pageService.DisplayAlert("Error al extraer datos", $"Error al traer productos: {rmp.message}", "Ok");
                Console.WriteLine(rmp.message);
            }
            SeeSaleControls = true;
            GettingData = false;
        }
    }
}

