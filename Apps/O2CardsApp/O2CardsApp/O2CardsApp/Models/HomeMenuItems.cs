using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace O2CardsApp.Models
{

    public class MenuItemViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MenuItem> items;
        public ObservableCollection<MenuItem> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }

        public MenuItemViewModel()
        {
            Items = new ObservableCollection<MenuItem>() {
                new MenuItem { ImageName="CreateCard", Title = "Create Business Card", SubTitle = "Design your new business card" },
                new MenuItem { ImageName="ShareCard", Title = "Share Your Card", SubTitle = "Share your business card" },
                new MenuItem { ImageName="SearchCard", Title = "Search Business Cards", SubTitle = "Search previously scanned business card" },
                new MenuItem { ImageName="ScanCard", Title = "Scan New Business Cards", SubTitle = "Scan new business card" },
                new MenuItem { ImageName="QRCode", Title = "Scan QR Code", SubTitle = "Scan new QR Code" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)items).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)items).PropertyChanged -= value;
            }
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageName { get; set; }
    }
}
