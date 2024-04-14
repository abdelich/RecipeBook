using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Messenger;

public partial class MainPage : ContentPage
{
    bool isRegistering = false;
    string url = "http://192.168.1.156:5000";


	public MainPage()
	{
		InitializeComponent();

        var deviceType = DeviceInfo.Platform;
        if (deviceType == DevicePlatform.Android)
            url = "http://10.0.2.2:5000";
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            using (HttpClient client = new HttpClient())
            {
                var login = LoginEntry.Text;
                var password = PasswordEntry.Text;

                //создать запрос на создание пользователя
                if (isRegistering)
                {
                    UserRegister(login, password);
                    UserLogin(login, password);
                }
                //войти в существующий аккаунт
                else
                {
                    UserLogin(login, password);
                }
            }
        }
    }
    async void UserLogin(string user_login, string user_password) 
    {
        using (HttpClient client = new HttpClient())
        {
            var post = url + $"/api/user_login/{user_login}/{user_password}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, post);
            var r = await client.SendAsync(message);

            if (r.IsSuccessStatusCode)
            {
                string jsonResult = await r.Content.ReadAsStringAsync();
                Dictionary<string, object> userInfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);

                string login = userInfo["login"].ToString();
                string password = userInfo["pass"].ToString();
                string name = userInfo["nickname"].ToString();
                string id = userInfo["id"].ToString();

                User.Login = login;
                User.Password = password;
                User.Name = name;
                User.Id = id;

                await Shell.Current.GoToAsync(nameof(Feed), false);
            }
            else
            {
                if(!isRegistering)
                    await Shell.Current.DisplayAlert("Wrong data", "Incorrect password or login", "Close");
            }
        }
    }

    async void UserRegister(string user_login, string user_password)
    {
        using (HttpClient client = new HttpClient())
        {
            var post = url + $"/api/user_create/{user_login}/{user_password}";

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, post);
            var r = await client.SendAsync(message);

            if (r.IsSuccessStatusCode)
            {
                string jsonResult = await r.Content.ReadAsStringAsync();
                Dictionary<string, object> userInfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);

                string login = userInfo["login"].ToString();
                string password = userInfo["pass"].ToString();
                string name = userInfo["nickname"].ToString();
                string id = userInfo["id"].ToString();

                User.Login = login;
                User.Password = password;
                User.Name = name;
                User.Id = id;
            }
            else
            {
                //вывести код ошибки
            }
        }
    }

    async void GetUserData(string user_id)
    {
        using (HttpClient client = new HttpClient())
        {
            var post = url + $"/api/user_info/{user_id}";

            //HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, post);
            var r = await client.GetAsync(post);

            if (r.IsSuccessStatusCode)
            {
                string jsonResult = await r.Content.ReadAsStringAsync();
                Dictionary<string, object> userInfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResult);

                string login = userInfo["login"].ToString();
                string password = userInfo["pass"].ToString();
                string name = userInfo["nickname"].ToString();
                string id = userInfo["id"].ToString();

                User.Login = login;
                User.Password = password;
                User.Name = name;
                User.Id = id;
            }
            else
            {
                //вывести код ошибки
            }
        }
    }

    private void RegisterButton_Click(object sender, EventArgs e)
    {
        LoginLabel.Text = "Register or log in";
        isRegistering = true;

        LoginButton.Text = "Register";
        RegisterButton.IsVisible = false;

        LoginEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
    }

    private void ShowPasswordButton_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
    }
}

