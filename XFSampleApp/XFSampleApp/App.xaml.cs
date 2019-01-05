using DataBindingDemos;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSampleApp.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFSampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AccountPage());
         //  MainPage = new NavigationPage(new DecimalKeypadPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
