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
	public partial class CompanySocialMedia : ContentPage
	{
		public CompanySocialMedia(BusinessCard card)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);

            btnNext.Clicked += async (sender, args) =>
            {
                card.FacebookLink = txtFacebook.Text;
                card.YoutubeLink = txtYoutube.Text;
                card.TwitterLink = txtTwitter.Text;
                card.InstagramLink = txtInstagram.Text;
                card.PinterestLink = txtPinterest.Text;
                card.LinkedInLink = txtLinkedIn.Text;

                await Navigation.PushAsync(new LogoPicker(card), false);
            };
        }
	}
}