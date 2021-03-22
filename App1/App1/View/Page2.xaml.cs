using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Voltar à 1");
        }

        private async void pimentao_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.CadastrarCultura(),false);

        }
    }
}