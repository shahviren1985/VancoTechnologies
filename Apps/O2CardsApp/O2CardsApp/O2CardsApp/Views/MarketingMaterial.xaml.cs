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
	public partial class MarketingMaterial : ContentPage
	{
		public MarketingMaterial(BusinessCard card)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);

            btnSave.Clicked += async (sender, args) =>
            {
                card.MarketingAttachment = txtBrochure.Text;
                card.OtherAttachment = txtOtherAttachments.Text;
                card.PresentationLink = txtPresentation.Text;

                if (!string.IsNullOrEmpty(txtTags.Text))
                {
                    card.Tags = txtTags.Text.Split(',').ToList();
                }

                await Navigation.PushAsync(new TemplatePicker(card), false);
            };
        }
	}
}