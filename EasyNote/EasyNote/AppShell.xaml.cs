using EasyNote.ViewModels;
using EasyNote.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EasyNote
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        string email;
        string password;
        public AppShell()
            
        {
            
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
           

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                String oauthToken = await SecureStorage.GetAsync("Usuario");
                
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}
