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
        bool activateSaleControls;
        public bool ActivateSaleControls
        {
            get
            {
                return activateSaleControls;
            }
            set
            {
                SetValue(ref activateSaleControls, value);
            }
        }

        bool searchResultListDisplay;
        public bool SearchResultListDisplay
        {
            get
            {
                return searchResultListDisplay;
            }
            set
            {
                SetValue(ref searchResultListDisplay, value);
            }
        }

        private ObservableCollection<ClientComplete> clientes;

        public ObservableCollection<ClientComplete> Clients
        {
            get { return clientes; }
            set { SetValue(ref clientes, value); }
        }
        public CompleteSaleM CompleteSale { get; set; }

        string searchPredictionText;
        public string SearchPredictionText
        {
            get
            {
                return searchPredictionText;
            }
            set
            {
                SetValue(ref searchPredictionText, value);
                if(!string.IsNullOrEmpty(SearchPredictionText))
                {
                    SearchResultListDisplay = true;
                    getSearchedCoincidences();
                }
                else
                {
                    SearchResultList = new List<SaleProduct>();
                    SearchResultListDisplay = false;
                }
            }
        }

        List<SaleProduct> searchResultList;
        public List<SaleProduct> SearchResultList
        {
            get
            {
                return searchResultList;
            }
            set
            {
                SetValue(ref searchResultList, value);
            }
        }
        SaleProduct selectedProduct;
        public SaleProduct SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                SetValue(ref selectedProduct, value);
                if(SelectedProduct!= null)
                {
                    if (string.IsNullOrEmpty(SelectedProduct.nombre))
                        return;
                    CompleteSale.SaledProducts.Add(SelectedProduct);
                    //SelectedProduct = null;
                    SearchPredictionText = string.Empty;
                }
            }
        }
        #endregion

        #region PropertyCommands
        public AllowSearchClientCommand AllowSearchClientCommand { get; set; }
        public UpdateProductsInfoCommand UpdateProductsInfoCommand { get; set; }
        #endregion
        public SaleVM(PageService ps)
        {
            pageService = ps;
            GettingData = false;
            SearClientVisibility = false;
            SeeSaleControls = false;
            ActivateSaleControls = false;
            SearchResultListDisplay = false;
            SearClientButton = true;
            AllowSearchClientCommand = new AllowSearchClientCommand(this);
            UpdateProductsInfoCommand = new UpdateProductsInfoCommand(this);
            CompleteSale = new CompleteSaleM();
            SearchResultList = new List<SaleProduct>();
            CompleteSale.PriceType = 2;
            CompleteSale.SaledProducts = new ObservableCollection<SaleProduct>();
            CompleteSale.SearchedProducts = new List<SaleProduct>();
            new Action(async () => await getClients())();
            new Action(async () => await getProducts())();
            
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

        void getSearchedCoincidences()
        {
            List<SaleProduct> aux = new List<SaleProduct>();
            if (CompleteSale.SearchedProducts!=null)
            {
                if(CompleteSale.SearchedProducts.Count>0)
                {
                    aux = CompleteSale.SearchedProducts.Where(a => a.nombre.ToLower().Contains(SearchPredictionText.ToLower()) || a.codigo.ToLower().Contains(SearchPredictionText.ToLower())).ToList();
                    SearchResultList = aux;
                }
            }
        }
    }
}

