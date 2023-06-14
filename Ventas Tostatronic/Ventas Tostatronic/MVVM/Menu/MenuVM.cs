using System;
using Ventas_Tostatronic.Models.MenuMFF;
using Ventas_Tostatronic.MVVM.Menu.MenuCommand;
using Ventas_Tostatronic.Services;
using Ventas_Tostatronic.Views.SalesVF;

namespace Ventas_Tostatronic.MVVM.Menu
{
	public class MenuVM: BaseNotifyPropertyChanged
    {
        public IPageService pageService;

        #region Properties
        private List<MenuM> menus;
		public List<MenuM> Menus
		{
			get { return menus; }
			set
			{
				SetValue(ref menus, value);
			}
		}
        private MenuM selectedOption;
        public MenuM SelectedOption
        {
            get { return selectedOption; }
            set
            {
                SetValue(ref selectedOption, value);
                if(SelectedOption!= null)
                {
                    if(SelectedOption.Name.Equals("Ventas"))
                        new Action(async () => await pageService.PushAsync(new SaleV(new PageService())))();
                }
            }
        }

        public ViewSelectionCommand ViewSelectionCommand { get; set; }
        #endregion

        public MenuVM(PageService pageService)
		{
			this.pageService = pageService;
			Menus = setMenus();
            ViewSelectionCommand = new ViewSelectionCommand(this);

        }

        List<MenuM> setMenus()
		{
			List<MenuM> aux = new List<MenuM>();
			aux.Add(
				new MenuM()
				{
					Name = "Ventas",
					ImageUrl = new Image() { Source= "https://lh3.googleusercontent.com/pw/AMWts8ACJd8mKgU93fQn2jE3ZJCYx6SEiELA16T-6tCcR1TpWrnjAcYjdmMYb89OZnqWwic15noq-75I4DAAosckbSYmOVXEVnETRFTqagGptL7tnTJzsE593WzEnzEjl-IXhktqSJgKQsZh7CCDl9EtCjHV=s512-no?authuser=0" }
				});
            aux.Add(
                new MenuM()
                {
                    Name = "Cotizaciones",
                    ImageUrl = new Image() { Source = "https://lh3.googleusercontent.com/pw/AMWts8AdfHya0TqszAA41wiDcb39AtQ_vFeutDNFcLlxigglc0PDGSH4HcgRIM3mxD7IdyZbaY9-qva5ySE16FpD3WN4o-2jE3aRfxOPBg6QLl1KtN6rjgPxLFYXkR97zajDwIt12QAmFqBoJ2OWmDUlu3BF=s512-no?authuser=0" }
                });

            return aux;
        }

        private async Task DisplayMessage(string title,string message)
        {
            await pageService.DisplayAlert(title, message, "Ok");
        }
    }
}

