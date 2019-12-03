using Newtonsoft.Json;
using O2CardsApp.Models;
//using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace O2CardsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemplatePicker : ContentPage
    {
        public TemplatePicker(BusinessCard card)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            btnSave.Clicked += async (sender, args) =>
            {
                card.TemplateName = "o2card-horizontal.html";

                btnSave.Text = "Saving...";
                btnSave.IsEnabled = false;

                // Call API to pass on the object and generate image/html/business card
                string response = await SaveBusinessCard(card);
                await DisplayAlert("Notification", "Business Card Created Successfully, downloading files.", "Ok");
                
                List<string> links = JsonConvert.DeserializeObject<List<string>>(response);

                await DownloadFile(links[1], links[0], ".html");
                await DownloadFile(links[2], links[0], ".vcard");

                Task<Page> p = null;
                p = Navigation.PopAsync(false);
                p = Navigation.PopAsync(false);
                p = Navigation.PopAsync(false);
                p = Navigation.PopAsync(false);
                p = Navigation.PopAsync(false);
                p = Navigation.PopAsync(false);
            };
        }

        public async Task<string> SaveBusinessCard(BusinessCard card)
        {
            var httpClient = new HttpClient();
            
            var content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PostAsync("http://myo2cards.com/app/api/BusinessCard/Post", content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DownloadFile(string url, string fileName, string extension)
        {
            var httpClient = new HttpClient();

            try
            {
                byte[] file = await httpClient.GetByteArrayAsync(url);

                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string name = System.IO.Path.Combine(documentsPath, fileName + extension);
                File.WriteAllBytes(name, file);
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Error occurred while downloading " + extension + " file.", "Ok");
            }

            return fileName;
        }
    }
}