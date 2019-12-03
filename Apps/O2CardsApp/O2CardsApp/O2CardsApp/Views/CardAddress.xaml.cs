using O2CardsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace O2CardsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardAddress : ContentPage
    {
        public CardAddress(BusinessCard card)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            btnNext.Clicked += async (sender, args) =>
            {
                card.AddressLine1 = txtAddressLine1.Text;
                card.AddressLine2 = txtAddressLine2.Text;
                card.City = txtCity.Text;
                card.State = txtState.Text;
                card.Country = txtCountry.Text;
                card.ZipCode = txtZip.Text;
                await Navigation.PushAsync(new CompanySocialMedia(card), false);
            };
        }


    }
}