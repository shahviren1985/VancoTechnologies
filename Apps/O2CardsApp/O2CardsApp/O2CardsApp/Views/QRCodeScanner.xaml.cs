using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace O2CardsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCodeScanner : ContentPage
    {
        public QRCodeScanner()
        {
            InitializeComponent();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert("No Camera", "No camera avaialble.", "OK");
                return;
            }

            takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", "No camera avaialble.", "OK");
                    return;
                }
                try
                {
                    var optionsDefault = new MobileBarcodeScanningOptions();
                    var optionsCustom = new MobileBarcodeScanningOptions();
                    var scanner = new MobileBarcodeScanner()
                    {
                        TopText = "Scan the QR Code",
                        BottomText = "Please Wait",
                    };

                    var scanResult = await scanner.Scan(optionsCustom);

                    if (scanResult != null)
                    {
                        await DisplayAlert("QR Code", scanResult.Text, "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Xamarin.Insights.Report(ex);
                    // await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
                }
            };

           
        }
    }
}