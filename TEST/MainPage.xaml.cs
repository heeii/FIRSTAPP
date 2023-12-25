using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace TEST
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        string login = " ";
        string password = " ";
        public MainPage()
        {
            InitializeComponent();
        }

        private void login_Button_Clicked(object sender, EventArgs e)
        {
            login = Login_Place.Text;
            password = Password_Place.Text;
            send_form(login, password);

        }
        private void registration_Button_Clicked(object sender, EventArgs e)
        {

        }
        private static async void send_form(string login , string password)
        {
            string data = $"{{\"name\":\"{login}\",\"email\":\"{password}\"}}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            var client = new HttpClient();
            var response = await client.PostAsync("http://192.168.45.104:5000/api/login", new StringContent(JsonConvert.SerializeObject(new { param1 = login, param2 = password }), Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            string url = "http://192.168.45.104:5000/api/login";
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
