using EasyNote.Services;
using EasyNote.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyNote
{
    public partial class App : Application
    {
        string email;
        string password;
        private LoginPage _mainview;
        public App()
        {
            BindingContext = new Login();
            InitializeComponent();
           
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Aquamarine);
        }

        protected override void OnStart()
        {
           // BindingContext = new Login();
        }

        protected override void OnSleep()
        {
           // BindingContext = new Login();
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
          //  BindingContext = new Login();
        }
    }
}
