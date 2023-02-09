using System;
namespace Ventas_Tostatronic.Models.ProductsMFF
{
	public class SaleProduct: ProductBase
    {
		public SaleProduct()
		{
		}
        public string idProducto { get; set; }
        public int idVentaPV { get; set; }
        int cantComprada;
        public int cantidadComprada
        {
            get
            {
                return cantComprada;
            }
            set
            {
                SetValue(ref cantComprada, value);
                Subtotal = DisplayPrice * cantidadComprada;
            }
        }
        public double precioAlMomento { get; set; }
        public int descuento { get; set; }
        double displayPrice;
        public double DisplayPrice
        {
            get
            {
                return displayPrice;
            }
            set
            {
                SetValue(ref displayPrice, value);
                precioAlMomento = DisplayPrice;
                Subtotal = DisplayPrice * cantidadComprada;
            }
        }
        double subtotal;
        public double Subtotal
        {
            get
            {
                return subtotal;
            }
            set
            {
                SetValue(ref subtotal, value);
            }
        }
    }
}

