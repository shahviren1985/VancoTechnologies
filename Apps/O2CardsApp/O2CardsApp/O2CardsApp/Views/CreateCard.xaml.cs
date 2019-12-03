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
    public partial class CreateCard : ContentPage
    {
        public CreateCard()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            btnNext.Clicked += async (sender, args) =>
            {

                BusinessCard card = new BusinessCard();
                card.Name = txtName.Text;
                card.EmailAddress = txtEmail.Text;
                card.Designation = txtDesignation.Text;
                card.CompanyName = txtCompanyName.Text;
                card.MobileNumber = txtMobile.Text;
                card.Website = txtWebsite.Text;
                await Navigation.PushAsync(new CardAddress(card), false);
            };
        }
    }
}