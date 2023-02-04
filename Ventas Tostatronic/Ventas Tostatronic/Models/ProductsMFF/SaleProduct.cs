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
        public int cantidadComprada { get; set; }
        public double precioAlMomento { get; set; }
        public int descuento { get; set; }
        public double DisplayPrice { get; set; }
        public double Subtotal { get; set; }
    }
}

