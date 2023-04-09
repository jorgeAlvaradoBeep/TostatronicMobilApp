using System;
namespace Ventas_Tostatronic.Services
{
    public class URLData
    {
        //URL Para API de .net
        //public const string baseURLNET = "http://192.168.3.15:5249/api/";
        //public const string baseURLNET = "http://192.168.3.48:5249/api/";
        public const string baseURLNET = "http://143.198.173.21:5249/api/";

        public static string getClients = baseURLNET + "Clientes";
        public static string getProducts = baseURLNET + "Products/";
        public static string ventas= baseURLNET + "Ventas/";

        public static string quoteNET = baseURLNET + "Cotizacion/";


    }
}

