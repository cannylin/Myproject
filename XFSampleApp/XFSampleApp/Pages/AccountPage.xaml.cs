using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFSampleApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage : ContentPage
	{
		public AccountPage ()
		{
			InitializeComponent ();
            AccountEntry.Text = "1";
            PasswordEntry.Text = "1";

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //實務上應是 WebAPI 呼叫驗證
            if(AccountEntry.Text.Equals("1", StringComparison.CurrentCultureIgnoreCase) && 
                PasswordEntry.Text == "1")
            { 
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("通知", "帳號密碼驗證有誤，帳號: 1 密碼: 1", "Ok");
                AccountEntry.Text = "";
                PasswordEntry.Text = "";
            }
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            AccountEntry.Text = "";
            PasswordEntry.Text = "";
        }
    }
}