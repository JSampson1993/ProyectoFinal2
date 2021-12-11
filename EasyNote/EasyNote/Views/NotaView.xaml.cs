using EasyNote.Controller;
using EasyNote.Models;
using Firebase.Database;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotaView : ContentPage
    {
        FirebaseClient firebase = new FirebaseClient("https://apis-movil-2-default-rtdb.firebaseio.com/Notas");

        Controller.FirebaseHelper services;
        ENotas u;
        public string Vnotaid = "";
        public string Vdescrip = "";
        public string Vimagen = "";

        public NotaView()
        {
            
            InitializeComponent();
            u = new ENotas();
            services = new FirebaseHelper();
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("imagen"))
            {
                image1.Source = Xamarin.Forms.Application.Current.Properties["imagen"].ToString();

                
            }
        }

        private void Editor_Completed(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Error", "No se soporta el cargar fotos", "ok");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "mifoto.jpg"
            });

            if (file == null)
            {

                return;
            }
            //await DisplayAlert("El path es:", file.Path, "ok");
            image1.Source = file.Path;

            //Console.WriteLine(file.Path);
            Xamarin.Forms.Application.Current.Properties["imagen"] = file.Path;


        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Error", "No se soporta el cargar fotos", "ok");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
            {
                return;
            }

            

            image1.Source = file.Path;
            //Console.WriteLine(file.Path);
            Application.Current.Properties["imagen"] = file.Path;
        }

       
        private async void ToolbarItem_Clicked_3(object sender, EventArgs e)
        {

            //services.DeleteNotas(u.notasId, u.notas_Descrip, u.notas_Image);
            //Navigation.PushAsync(new UbicacionesPage());
            //await Navigation.PopAsync(new ItemsPage());
            // await NavigationPage.Pop

            Vnotaid = txtidnotas.Text;
            Vdescrip = txtdescri.Text;
            Vimagen = txtimagen.Text;
            
           
            
            //Vimagen = Convert.ToString(item.notas_Image);

            var mensaje = await DisplayAlert("Eliminar", "Desea Eliminar la nota", "Si", "No");
            if (mensaje)
            {
                //await services.DeleteNotas(Vnotaid, Vdescrip, Vimagen);
                await services.DeleteNotas(txtidnotas.Text, txtdescri.Text, txtimagen.Text);
                await DisplayAlert("Alerta", "Nota Eliminada Correctamente!!", "OK");
                //vaciar variables
                Vnotaid = "";
                Vdescrip = "";
                Vimagen = "";
                await Navigation.PopAsync();
               // await Navigation.PushModalAsync(new UbicacionesPage());
            }

        }



        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}