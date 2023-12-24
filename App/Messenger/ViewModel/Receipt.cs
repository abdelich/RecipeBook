using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.ViewModel
{
    public partial class Receipt : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Receipt> receipts;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photo_url")]
        public string Photo_URL { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ingredients")]
        public Dictionary<string, string> Ingredients { get; set; }

        [JsonProperty("cooking")]
        public string Cooking { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        public Receipt()
        {

        }
        public Receipt(string name, string photo_URL, string description, Dictionary<string, string> ingredients, string cooking, string author)
        {
            Name = name;
            Photo_URL = photo_URL;
            Description = description;
            Ingredients = ingredients;
            Cooking = cooking;
            Author = author;
        }
        [RelayCommand]
        void Add()
        {
            //var receipt = new Receipt(Name,Photo_URL,Description,Ingredients,Cooking,Author);
            Shell.Current.GoToAsync(nameof(ReceiptAdd));
            //Receipts.Add(receipt);
        }
        [RelayCommand]
        async Task Check_Details(Receipt rs)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object> { { "receipt", rs } });
        }
    }
}
