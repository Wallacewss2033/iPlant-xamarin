using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {


        public Login()
        {
            InitializeComponent();



        }


        const string url = "http://localhost/";

        private async Task btnEntrar_Clicked()
        {
            var usuario = Entry_usuario.Text;
            var senha = Entry_senha.Text;

            try
            {
                if (string.IsNullOrEmpty(Entry_usuario.Text) || string.IsNullOrEmpty(Entry_senha.Text))
                {

                    if (string.IsNullOrEmpty(Entry_usuario.Text))
                    {
                        await DisplayAlert("Erro", "Digite um usuario válido", " Aceitar");
                        Entry_usuario.Focus();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Entry_senha.Text))
                        {
                            await DisplayAlert("Erro", "Digite uma senha válida", " Aceitar");
                            Entry_senha.Focus();
                        }
                    }
                }
                else
                {


                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("usuario", usuario));
                    postData.Add(new KeyValuePair<string, string>("senha", senha));

                    var content = new FormUrlEncodedContent(postData);

                    HttpClient client = new HttpClient();
                    Uri BaseAddress = new Uri("http://192.168.1.114/");

                    var response = await client.PostAsync("http://192.168.1.114/webservice2.php", content);

                    var result = response.Content.ReadAsStringAsync().Result;

                    string res = result.ToString();
                    res = res.Replace("\r\n", string.Empty);

                    if (res == "Success")
                    {
                        await Navigation.PushAsync(new View.Principal());
                        Entry_usuario.Text = null;
                        Entry_senha.Text = null;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Usuário ou senha incorreto(s)", "Ok");
                        Entry_usuario.Text = null;
                        Entry_senha.Text = null;
                    }

                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
                return;
            }

        }
    }

}


