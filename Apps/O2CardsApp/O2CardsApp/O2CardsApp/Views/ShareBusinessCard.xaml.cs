using O2CardsApp.Models;
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
    public partial class ShareBusinessCard : ContentPage
    {
        public ShareBusinessCard()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string[] files = Directory.GetFiles(documentsPath, "*.vcard");

            foreach (string file in files)
            {
              //Share(file);
            }
        }

        
    }
}