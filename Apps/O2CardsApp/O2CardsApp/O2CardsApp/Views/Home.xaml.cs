using O2CardsApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace O2CardsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();

            BindingContext = new MenuItemViewModel();
        }


        private async void Menu_Tapped(object sender, TappedEventArgs e)
        {
            //var content = e.Item as MenuItemViewModel;
            var param = (sender as ListView).SelectedItem;
            string name = ((O2CardsApp.Models.MenuItem)param).ImageName;
            switch (name)
            {
                case "CreateCard":
                    await Navigation.PushAsync(new CreateCard());
                    break;
                case "QRCode":
                    await Navigation.PushAsync(new QRCodeScanner());
                    break;
                case "ShareCard":
                case "SearchCard":
                    break;
                case "ScanCard":
                    await Navigation.PushAsync(new ScanCard());
                    break;
            }

        }
    }

}
