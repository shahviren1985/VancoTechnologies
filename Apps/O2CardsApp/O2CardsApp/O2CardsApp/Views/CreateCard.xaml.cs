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
		public CreateCard ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);

            //ToolbarItems.Add(new ToolbarItem("Close", "close.png", () =>
            //{

            //}));
            //ToolbarItems.Add(new ToolbarItem("Save", "search.png", () =>
            //{
                
            //}));
        }
	}
}