using EasyNote.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EasyNote.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}