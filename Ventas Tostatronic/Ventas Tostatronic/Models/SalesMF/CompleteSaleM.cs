using System;
using System.Collections.ObjectModel;
using Ventas_Tostatronic.Models.Clients;
using Ventas_Tostatronic.Models.ProductsMFF;

namespace Ventas_Tostatronic.Models.SalesMF
{
    public class CompleteSaleM : Services.BaseNotifyPropertyChanged
    {
        public int IDSale { get; set; }
        private ClientComplete clientSale;

        public ClientComplete ClientSale
        {
            get { return clientSale; }
            set
            {
                SetValue(ref clientSale, value);
                switch(ClientSale.IdTipoCliente)
                {
                    case 1:
                        PriceType = 1;
                        break;
                    case 2:
                        PriceType = 0;
                        break;
                }
            }
        }
        private int priceType;

        public int PriceType
        {
            get { return priceType; }
            set
            {
                if (value == PriceType)
                    return;
                SetValue(ref priceType, value);
                if(SearchedProducts!=null)
                {
                    if (SearchedProducts.Count > 0)
                    {
                        switch (PriceType)
                        {
                            case 0:
                                foreach (SaleProduct product in SearchedProducts)
                                {
                                    product.DisplayPrice = product.precioPublico;
                                }
                                break;
                            case 1:
                                foreach (SaleProduct product in SearchedProducts)
                                {
                                    product.DisplayPrice = product.precioDistribuidor;
                                }
                                break;
                            case 2:
                                foreach (SaleProduct product in SearchedProducts)
                                {
                                    product.DisplayPrice = product.precioMinimo;
                                }
                                break;
                        }
                    }
                }
                if(SaledProducts!=null)
                {
                    if (SaledProducts.Count > 0)
                    {
                        switch (PriceType)
                        {
                            case 0:
                                foreach (SaleProduct product in SaledProducts)
                                {
                                    product.DisplayPrice = product.precioPublico;
                                }
                                break;
                            case 1:
                                foreach (SaleProduct product in SaledProducts)
                                {
                                    product.DisplayPrice = product.precioDistribuidor;
                                }
                                break;
                            case 2:
                                foreach (SaleProduct product in SaledProducts)
                                {
                                    product.DisplayPrice = product.precioMinimo;
                                }
                                break;
                        }
                    }
                    GetSubtotal();
                }
                
            }
        }

        private List<SaleProduct> searchedProducts;

        public List<SaleProduct> SearchedProducts
        {
            get { return searchedProducts; }
            set { SetValue(ref searchedProducts, value); }
        }

        private ObservableCollection<SaleProduct> saleSearches;

        public ObservableCollection<SaleProduct> SaledProducts
        {
            get { return saleSearches; }
            set { SetValue(ref saleSearches, value); }
        }

        private double subTotal;

        public double SubTotal
        {
            get { return subTotal; }
            set
            {
                SetValue(ref subTotal, value);
                if (NeedFactura)
                    IVA = SubTotal * .16;
                else
                    IVA = 0;
                Total = SubTotal + IVA;
            }
        }

        private double iva;

        public double IVA
        {
            get { return iva; }
            set { SetValue(ref iva, value); }
        }

        private double total;

        public double Total
        {
            get { return total; }
            set { SetValue(ref total, value); }
        }

        public void GetSubtotal()
        {
            double sub = 0;
            foreach (SaleProduct p in SaledProducts)
            {
                p.Subtotal = p.DisplayPrice * p.cantidadComprada;
                sub += p.Subtotal;
            }
            SubTotal = sub;
        }

        private bool needFactura;

        public bool NeedFactura
        {
            get { return needFactura; }
            set
            {
                SetValue(ref needFactura, value);
                GetSubtotal();
            }
        }

        private int salerID;

        public int SalerID
        {
            get { return salerID; }
            set { SetValue(ref salerID, value); }
        }

        //public PaymentM Payment { get; set; }

        public bool SetIVAPrices()
        {
            if (SaledProducts.Count > 0)
            {
                string message = "";
                foreach (SaleProduct product in SaledProducts)
                {
                    if ((product.DisplayPrice / 1.16f) >= (product.precioMinimo / 1.16f))
                        product.DisplayPrice /= 1.16f;
                    else
                        message += $"El precio actual ({product.DisplayPrice}) del producto {product.nombre} ya esta en su minimo permitido{Environment.NewLine}";
                }
                GetSubtotal();
                NeedFactura = true;
            }
            return true;
        }

        public bool UndoIVAPrices()
        {
            if (SaledProducts.Count > 0)
            {
                foreach (SaleProduct product in SaledProducts)
                {
                    product.DisplayPrice *= 1.16f;
                }
                GetSubtotal();
                NeedFactura = false;
            }
            return true;
        }

    }
}

