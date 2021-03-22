using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnTake.Clicked += BtnTake_Clicked;
            btnUpload.Clicked += BtnUpload_Clicked;
            btnSemImg.Clicked += BtnSemImg_Clicked;
        }

        byte[] img = null;

        public byte []ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0 ,buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }


        }

        private async void BtnUpload_Clicked(Object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Indisponível","Recurso Indisponível","Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                });

                if(file == null)
                {
                    return;
                }

                img = ReadFully(file.GetStream());
                imgFoto.Source = ImageSource.FromStream(() => file.GetStream());
            }
            catch ( Exception ex)
            {
                await this.DisplayAlert("", "Dificuldade ao Executar ação", "Ok");

            }
        }

        private async void BtnTake_Clicked(Object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Sem Camera", "Camera Indisponível", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    SaveToAlbum = true,
                    Name = "Foto.jpg",
                    DefaultCamera = CameraDevice.Front,

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

        private void BtnSemImg_Clicked(Object sender, EventArgs e)
        {
            img = null;
            imgFoto.Source = null;
        }

    }
}
