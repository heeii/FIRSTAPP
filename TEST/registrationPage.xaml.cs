using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TEST;

public partial class registrationPage : ContentPage
{

    string login = " ";
    string password = " ";
    string check_Password=" ";
    string email = " ";
    string str_alert = " ";
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
    private void login_Button_Clicked(object sender, EventArgs e)
    {
        login = Login_Place.Text;
        password = Password_Place.Text;
        check_Password= Password_Check_Place.Text;
        email = email_Place.Text;
        if (password == check_Password){
            char ch = '@';
            bool isContain = IsContainChar(email, ch);
            if (isContain){
                send_form(login, password);
            }
            else { 
                str_alert = "email don`t correct";
                Alert(str_alert);
            }
            
        }
        else {
            str_alert = "password don`t correct";
            Alert(str_alert); 
        }
        
        

    }
    private void Alert(string str_alert);
    private static async void send_form(string login, string password)
    {
        string url = "http://192.168.45.104:5000/api/registration";
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
    private void registration_btn_Clicked(object sender, EventArgs e)
    {

    }
}