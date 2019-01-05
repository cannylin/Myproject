using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;

public static class Extensions
{
    public static StringContent AsJson(this object o)
     => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
}


namespace XFSampleApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage : ContentPage
	{
		public AccountPage ()
		{
			InitializeComponent ();
            AccountEntry.Text = "nchu";
            PasswordEntry.Text = "123456";

            label.BindingContext = slider;
            label.SetBinding(Label.RotationProperty, "Value");
        }

        private async Task <bool> LoginAuthAsync(string account,string password)
        {
            var httpClient = new System.Net.Http.HttpClient();
            var urlStr = "http://xamarinclassdemo.azurewebsites.net/api/login";

             httpClient.DefaultRequestHeaders.Add("username",account);
             httpClient.DefaultRequestHeaders.Add("password", password);

            var authContent = new System.Net.Http.StringContent("");

           // var stringContent = new StringContent(Sb_json, UnicodeEncoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(urlStr, authContent);
            var resultStr = await result.Content.ReadAsStringAsync();

            // await Task.Delay(500);
            // return true; // app直接進入BYPASS

                   var returnValue = false;
                   try
                   {
                      returnValue = bool.Parse(resultStr);
                  }
                  catch (FormatException ftEx) {
                await DisplayAlert("Err", ftEx.Message, "OK");
                   }
            return returnValue; // app直接進入BYPASS
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(AccountEntry.Text))
            {
                await DisplayAlert("call","is Null","ok");
                return; //輸入空值




            }

            if (String.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("call", "is Null", "ok");
                return; //輸入空值

            }

            //實務上應是 WebAPI 呼叫驗證
            // if (AccountEntry.Text.Equals("1", StringComparison.CurrentCultureIgnoreCase) && 
            //     PasswordEntry.Text == "1")

            // if(await LoginAuthAsync("nchu","123456"))

            var accountStr = AccountEntry.Text.ToLower();
            var passwordStr = PasswordEntry.Text.ToLower();

            if (await LoginAuthAsync(accountStr, passwordStr))
            {
                await DisplayAlert("通知", "登入OK", "GO");
                // recoder 登入

                saveAuthData();


                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("通知", "帳號密碼驗證有誤，帳號: nchu 密碼: 123456", "Ok");
                AccountEntry.Text = "";
                PasswordEntry.Text = "";
            }


          
           // var urlStr = "https://cannyapptest.azurewebsites.net/api/values";
             //var urlStr = "http://xamarinclassdemo.azurewebsites.net/api/getschoolinfos";
        

        }


        private void saveAuthData()
        {
            var cacheDir = Xamarin.Essentials.FileSystem.CacheDirectory;
            //var mainDir = Xamarin.Essentials.FileSystem.AooDataDirectory;

            System.Diagnostics.Debug.WriteLine(cacheDir);
            //System.Diagnostics.Debug.WriteLinemailDir);

            var fullPath = cacheDir + "auth.log";
            //var fullPath = mainDir + "auth.log";
            File.WriteAllText(fullPath, $"Be Authorized,{DateTime.Now.Ticks}");//時間戳
           var result= File.ReadAllText(fullPath);
            System.Diagnostics.Debug.WriteLine(result);//check Write data
            System.Diagnostics.Debug.WriteLine("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");//check Write data
        }








        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            AccountEntry.Text = "";
            PasswordEntry.Text = "";
        }
    }






}