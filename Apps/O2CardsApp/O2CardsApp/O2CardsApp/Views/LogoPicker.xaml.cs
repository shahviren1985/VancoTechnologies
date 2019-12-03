using O2CardsApp.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace O2CardsApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoPicker : ContentPage
	{
		public LogoPicker(BusinessCard card)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);

            btnNext.Clicked += async (sender, args) =>
            {
                card.LogoImage = "";
                await Navigation.PushAsync(new MarketingMaterial(card), false);
            };

            pickLogo.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                    return;
                }
                try
                {
                    Stream stream = null;
                    var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);


                    if (file == null)
                        return;

                    stream = file.GetStream();
                    file.Dispose();

                    logo.Source = ImageSource.FromStream(() => stream);

                }
                catch (Exception ex)
                {
                    // Xamarin.Insights.Report(ex);
                    // await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
                }
            };
        }

        public List<string> GetImagePaths(string email)
        {
            string domainName = email.Split('@')[1];
            // Get list of logo images from the website
            return null;
        }
	}
}