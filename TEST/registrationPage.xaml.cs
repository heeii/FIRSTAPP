using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.Maui.Controls;
using System.Net.Sockets;

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
    private async void registration_btn_Clicked(object sender, EventArgs e)
    {

        
        login = Login_Place.Text;
        password = Password_Place.Text;
        check_Password= Password_Check_Place.Text;
        email = email_Place.Text;
        if (password == check_Password & password !=""){
            char ch = '@';
            bool isContain = IsContainChar(email, ch);
            if (isContain){
                ;
                string get_sand= await send_form(login, password, email);
                Alert(get_sand);
            }
            else { 
                s_aler = "invalid email address was entered";
                Alert(s_aler);
            }
            
        }
        else {
            s_aler = "password contains invalid characters";
            Alert(s_aler); 
        }
        
        

    }
    private async void Alert(string s_alert)
    {
        await DisplayAlert("Sorry!", s_alert, "OK");
    }
    private async Task<string>  send_form(string login, string password, string email)
    {
        string url = "http://192.168.45.104:5000/api/registration";
        var client = new HttpClient();
        var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(new { log = login, pas = password, e_mail = email }), Encoding.UTF8, "application/json"));
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
        if (result["message"] == "ok")
        {
            return result["data"];
        }
        // ïîëó÷åíèå äàííûõ ñ ñåðâåðà
        return "fail";
    }
    
    
}
