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
    public partial class InicioPage : ContentPage
    {
        public InicioPage()
        {
            InitializeComponent();
        }

        private void RegistroNav_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegistroPage());
        }
    

        private void LoginNav_Clicked(object sender, EventArgs e)
        {
                 Navigation.PushModalAsync(new LoginPage());
        }
    }
}