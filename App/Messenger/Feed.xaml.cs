using Messenger.ViewModel;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json.Bson;
using System.Xml.Linq;

namespace Messenger;

public partial class Feed : ContentPage
{
    string url = "http://127.0.0.1:5000";
    string post = "/api/receipts";

    List<Receipt> receipts = new List<Receipt>();

    ReceiptsViewModel ref_rvm;

    public Feed(ReceiptsViewModel rvm)
    {
        InitializeComponent();
        BindingContext = rvm;
        ref_rvm = rvm;

        LoadReceiptsFromDB(rvm);
    }
    async void LoadReceiptsFromDB(ReceiptsViewModel rvm)
    {
        using (HttpClient client = new HttpClient())
        {
            var deviceType = DeviceInfo.Platform;
            if (deviceType == DevicePlatform.Android)
                url = "http://10.0.2.2:5000";
            string request = url + post;

            HttpResponseMessage response = await client.GetAsync(request);

            if(response.IsSuccessStatusCode)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                var resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResult);

                rvm.Receipts.Clear();

                foreach (var resultDict in resultList)
                {
                    Dictionary<string, string> ingreients_dict = new Dictionary<string, string>();

                    var name = resultDict["name"].ToString();
                    var desc = resultDict["description"].ToString();
                    var photo = resultDict["photo_url"].ToString();
                    var cooking = resultDict["cooking"].ToString();
                    var author = resultDict["author"].ToString();
                    var ingredients = JObject.FromObject(resultDict["ingredients"]).ToObject<Dictionary<string, string>>();

                    var new_receipt = new Receipt(name,photo,desc, ingredients, cooking,author);
                    //receipts.Add(new_receipt);
                    rvm.Receipts.Add(new_receipt);
                }
            }
        }
    }

    private void Reload_Receipts(object sender, EventArgs e)
    {
        LoadReceiptsFromDB(ref_rvm);
    }

    private async void Add_Receipt(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ReceiptAdd));
    }
}