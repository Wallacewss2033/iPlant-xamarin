using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Principal : ContentPage
	{
		public Principal()
		{
			InitializeComponent();

			var iniciarCultura_tap = new TapGestureRecognizer();
			iniciarCultura_tap.Tapped += iniciarCultura_Tapped;
			img_cadastrar_cultura.GestureRecognizers.Add(iniciarCultura_tap);

			var MinhasCulturas_tap = new TapGestureRecognizer();
			MinhasCulturas_tap.Tapped += MinhasCulturas_Tapped;
			img_minhas_culturas.GestureRecognizers.Add(MinhasCulturas_tap);

			//	Esconde a barra de navegação		NavigationPage.SetHasNavigationBar(this,false);

			NavigationPage.SetHasBackButton(this, false);

		}

		private void MinhasCulturas_Tapped(object sender, EventArgs e)
		{
		 Navigation.PushAsync(new MinhasCulturas(), false);
		}

		private void iniciarCultura_Tapped(object sender, EventArgs e)
		{
		  Navigation.PushAsync(new Page2(), false);
		}

		private void sobre_Clicked(object sender, EventArgs e)
		{

		}

		private void Button_Clicked_1(object sender, EventArgs e)
		{

		}
	}
}