using System;
namespace Ventas_Tostatronic.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task DisplayAlert(string titulo, string mensaje, string ok);
        Task<bool> DisplayAlert(string titulo, string mensaje, string ok, string cancel);
    }
}

