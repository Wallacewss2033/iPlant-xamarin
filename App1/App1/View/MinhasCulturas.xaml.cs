using App1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MinhasCulturas : ContentPage
    {
        private ObservableCollection<Cultivo> cultivo { get; set; }

       

        

        public MinhasCulturas()
        {
           
            InitializeComponent();


            cultivo = new ObservableCollection<Cultivo>
            {
                new Cultivo{nome = "Tomate", img = "tomate.png"},
                new Cultivo{nome = "Pimentão", img = "pimentao.png"},
                new Cultivo{nome = "Couve", img = "couve.png"},
                new Cultivo{nome = "Coentro", img = "coentro.png"}
            };

            Lv_Culturas.ItemsSource = cultivo;
			

		}

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Page1());
        }

		private void Lv_Culturas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

		}
	}
}