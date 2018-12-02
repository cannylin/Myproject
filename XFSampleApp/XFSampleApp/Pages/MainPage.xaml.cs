using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using XFSampleApp.Models;

namespace XFSampleApp.Pages
{


    public class My_BIND_DATA
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsMischievous { get; set; }
    }
    public partial class MainPage : ContentPage

    {
        ObservableCollection <string> schoolData2; //宣告變數 SCHOOLDATA 的抽屜放STRING
        ObservableCollection<SchoolINFO> schoolData; //宣告變數 SCHOOLDATA 的抽屜放STRING


        public MainPage()
        {
            InitializeComponent();

            BuildSchoolData();
            SchoolListView.ItemsSource = schoolData;
            


            // xaml Ui 元件本身 一定會有 "繫結"
        }

        private  void BuildSchoolData()
        {
            schoolData = new ObservableCollection<SchoolINFO>()
          {
           new SchoolINFO() { Name = "NAME_AAAAA", LOGO = "auo",ADDRESS="ADD111111",TEL="111111",Email="Email_11111"},
           new SchoolINFO() { Name = "NAME_BBBBB", LOGO = "auo",ADDRESS="ADD2222222",TEL="22222",Email="Email_11111"},
           new SchoolINFO() { Name = "NAME_ccccc", LOGO = "googlelogo",ADDRESS="ADD3333",TEL="3333",Email="Email_11111"},
           new SchoolINFO() { Name = "NAME_dddd", LOGO = "googlelogo",ADDRESS="ADD4444",TEL="44444",Email="Email_11111" },
           };
        }
      //  private async void Button_Clicked(object sender, EventArgs e)
       // {
      //     await Navigation.PushAsync(new DetailPage());
       // }

        private async void ListView_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(5000); //轉圈圈 5 sec for wait io user see na
            SchoolListView.IsRefreshing = false; //一種寫法
           //同上功能  SchoolListView.EndRefresh();
        }

        private async void SchoolListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var SchoolINFO = SchoolListView.SelectedItem as SchoolINFO;
            await Navigation.PushAsync(new DetailPage(SchoolINFO));
            SchoolListView.SelectedItem = null; //點選反回無標記
        }

        private void BT_click()
        {

            //var page = new DetailsPage();
            //page.BindingContext = monkey;
           // await Navigation.PushAsync(page);

        }

        private void CALL_BT_CLICK(object sender, EventArgs e)
        {
            var call_BT = sender as Button;
            var Scahoolcall_ID = call_BT.BindingContext as SchoolINFO;
            var call_TEL = Scahoolcall_ID.TEL;
              Device.OpenUri(new Uri($"tel:{call_TEL}"));
           // Device.OpenUri(new Uri("tel:86664235"));
            // sender 告我們誰引發此程式碼執行
            //   Device.OpenUri(new Uri("tel:86664235"));

            // mailto
            // https://
            // sms 
            //app link行為 正確的URI格式需正確
        }
    }
}
