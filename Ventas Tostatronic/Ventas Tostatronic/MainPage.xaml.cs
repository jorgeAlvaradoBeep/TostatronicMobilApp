using Ventas_Tostatronic.MVVM.Menu;
using Ventas_Tostatronic.Services;

namespace Ventas_Tostatronic;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		PageService ps = new PageService();
		BindingContext = new MenuVM(ps);
	}

}


