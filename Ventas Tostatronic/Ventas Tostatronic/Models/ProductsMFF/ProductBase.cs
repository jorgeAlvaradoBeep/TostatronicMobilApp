using System;
namespace Ventas_Tostatronic.Models.ProductsMFF
{
	public class ProductBase: Services.BaseNotifyPropertyChanged
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int existencia { get; set; }
        public int cantidadMinima { get; set; }
        public double precioCompra { get; set; }
        public double precioPublico { get; set; }
        public double precioDistribuidor { get; set; }
        public double precioMinimo { get; set; }
        public bool eliminado { get; set; }
        public string imagen { get; set; }
        public string SearchProperty
        {
            get
            {
                return $"{codigo} - {nombre}";
            }
        }
    }
}

