using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Maui.Controls;


namespace TEST
{
    public partial class loginPage : ContentPage
    {
        int count = 0;
        string login = " ";
        string password = " ";
        public loginPage()
        {
            InitializeComponent();
        }

        private void login_Button_Clicked(object sender, EventArgs e)
        {
            login = Login_Place.Text;
            password = Password_Place.Text;
            send_form(login, password);

        }
        private async void registration_Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("registrationPage");
        }


        private static async void send_form(string login , string password)
        {
            string url = "http://192.168.45.104:5000/api/login";
            string data = $"{{\"name\":\"{login}\",\"email\":\"{password}\"}}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            var client = new HttpClient();
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(new { log = login, pas = password }), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = dataBytes.Length;
            if (result["message"] == "ok")
            {
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }
            }
        }

    }

}
