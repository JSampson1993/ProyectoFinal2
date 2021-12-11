using EasyNote.ViewModels;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyNote.Views
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        string email;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new Login();
          
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                ////SecureStorage.RemoveAll();
                var oauthToken = await SecureStorage.GetAsync("Usuario");
                tEmail.Text = oauthToken;

            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
       

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await SecureStorage.SetAsync("Usuario", tEmail.Text);
        }
        //  await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
    

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegistroPage());
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Logout());
        }
    }
}