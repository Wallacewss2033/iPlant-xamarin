using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarCultura : ContentPage
    {
        private MediaFile _mediaFile;

        public CadastrarCultura()
        {
            InitializeComponent();

            // Criando Recuperadores de TapGesture  
            var tapImage = new TapGestureRecognizer();
            // eventos vinculativos 
            tapImage.Tapped += tapImage_Tapped;
            // Associando eventos de toque aos botões de imagem   
            imagen.GestureRecognizers.Add(tapImage);

            btnGaleria.Clicked += BtnUpload_Clicked;
            btnSemImg.Clicked += BtnEnviar_Clicked;

        }

        byte[] img = null;



        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }


        }

        async void tapImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                await imagePick();
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Erro", ex + "Dificuldade ao Executar ação", "Ok");

            }

        }

        private async void BtnUpload_Clicked(Object sender, EventArgs e)
        {
            try
            {


                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Indisponível", "Recurso Indisponível", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                });

                if (file == null)
                {
                    return;
                }

                img = ReadFully(file.GetStream());
                imgFoto.Source = ImageSource.FromStream(() => file.GetStream());
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("", "Dificuldade ao Executar ação", "Ok");

            }
        }

        private async void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            if (_mediaFile == null || imgFoto.Source == null)
            {
                await DisplayAlert("Erro", "Sem imagem", "OK");

            }
            else
            {


                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(_mediaFile.GetStream()), "\"imageupload\"", $"\"{_mediaFile.Path}\"");

                var httpclient = new HttpClient();


                var request = await httpclient.PostAsync("http://iplantweb.online/webservice/upload.php", content);
                var nome = await request.Content.ReadAsStringAsync();
                labelStatus.Text = nome;

                await Navigation.PopAsync();

            }
        }
        private async Task imagePick()
        {

            var media = await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Erro", "Tirar foto não é suportado!", "OK");
            }

            var opcoescamera = new StoreCameraMediaOptions
            {
                AllowCropping = true,
                SaveToAlbum = true,
                DefaultCamera = CameraDevice.Rear,
                Directory = "teste"

            };

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(opcoescamera);

            imgFoto.Source = _mediaFile.Path;
        }

        private void BtnSemImg_Clicked(Object sender, EventArgs e)
        {

            img = null;
            imgFoto.Source = null;
        }
    }
}