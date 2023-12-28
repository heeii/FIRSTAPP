using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.Maui.Controls;

namespace TEST;

public partial class registrationPage : ContentPage
{

    string login = " ";
    string password = " ";
    string check_Password=" ";
    string email = " ";
    string s_aler = " ";
    public registrationPage()
    {
        InitializeComponent();
    }

    static bool IsContainChar(string str, char ch)
    {
        foreach (char c in str)
        {
            if (c == ch)
            {
                return true;
            }
        }

        return false;
    }
    private void registration_btn_Clicked(object sender, EventArgs e)
    {
        login = Login_Place.Text;
        password = Password_Place.Text;
        check_Password= Password_Check_Place.Text;
        email = email_Place.Text;
        if (password == check_Password & password !=""){
            char ch = '@';
            bool isContain = IsContainChar(email, ch);
            if (isContain){
                send_form(login, password,email);
            }
            else { 
                s_aler = "email don`t correct";
                Alert(s_aler);
            }
            
        }
        else {
            s_aler = "password don`t correct";
            Alert(s_aler); 
        }
        
        

    }
    private async void Alert(string s_alert)
    {
        await DisplayAlert("Sorry!", s_alert, "OK");
    }
    private static async void send_form(string login, string password, string email)
    {
        string url = "http://192.168.45.104:5000/api/registration";
        string data = $"{{\"name\":\"{login}\",\"email\":\"{password}\"}}";
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        var client = new HttpClient();
        var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(new { log = login, pas = password, e_mail = email  }), Encoding.UTF8, "application/json"));
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