using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.ViewModel
{
    public partial class ReceiptsViewModel : ObservableObject
    {
        string url = "http://192.168.1.156:5000";
        string post = "/api/receipt";

        [ObservableProperty]
        ObservableCollection<Receipt> receipts;

        [ObservableProperty]
        string receipt_name;

        [ObservableProperty]
        string receipt_description;

        [ObservableProperty]
        string receipt_author;

        [ObservableProperty]
        string receipt_cooking;

        [ObservableProperty]
        string receipt_photo;

        [ObservableProperty]
        Dictionary<string, string> recepient_ingredients;

        public ReceiptsViewModel()
        {
            Receipts = new ObservableCollection<Receipt>();
        }

        [RelayCommand]
        void Add()
        {
            Shell.Current.GoToAsync(nameof(ReceiptAdd));
        }

        [RelayCommand]
        async Task Check_Details(Receipt rs)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object> { { "receipt", rs } });
        }
    }
}