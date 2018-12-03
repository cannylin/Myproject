using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using XFSampleApp.Models;
using System.Net;         //cannylin
using System.Net.Sockets;  //cannylin

namespace XFSampleApp.Pages
{



    
    public partial class MainPage : ContentPage

    {
        ObservableCollection <string> schoolData2; //宣告變數 SCHOOLDATA 的抽屜放STRING
        ObservableCollection<SchoolINFO> schoolData; //宣告變數 SCHOOLDATA 的抽屜放STRING
         int PLC_connect_ststus { get;  set; }
        String REC_PLC { get;  set; }


        public MainPage()
        {
            InitializeComponent();

            BuildSchoolData();
            SchoolListView.ItemsSource = schoolData;
            
            // xaml Ui 元件本身 一定會有 "繫結"
        }
        private void BuildSchoolData()
        {
            schoolData = new ObservableCollection<SchoolINFO>()
          {
           new SchoolINFO() { Name = "NAME_AAAAA", LOGO = "auo",ADDRESS="ADD111111",TEL="111111",Email="Email_11111@gmail.com"},
           new SchoolINFO() { Name = "NAME_BBBBB", LOGO = "auo",ADDRESS="ADD2222222",TEL="22222",Email="Email222222@gmail.com"},
           new SchoolINFO() { Name = "NAME_ccccc", LOGO = "googlelogo",ADDRESS="ADD3333",TEL="3333",Email="Email333333@gmail.com"},
           new SchoolINFO() { Name = "NAME_dddd", LOGO = "googlelogo",ADDRESS="ADD4444",TEL="44444",Email="Emai44444@gmail.com" },
           };
            
            schoolData.Add(new SchoolINFO
            {
                Name = "ADDName555555",
                LOGO = "auo",
                ADDRESS = "ADD66666666",
                TEL = "7777777777777",
                Email = "Email_88888@gmail.com"
            }
              
            );


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

        private void Mail_BT_CLICK(object sender, EventArgs e)
        {
            var Mail_BT = sender as Button;
            var Mail_ID = Mail_BT.BindingContext as SchoolINFO;
            var Mail_To = Mail_ID.Email;
            Device.OpenUri(new Uri($"mailto:{Mail_To}"));

           // Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));

        }

        public void PLC_Connect()
        {


            /*建立以TCP為協定的Socket*/
            Socket m_SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //   Console.Write("請輸入遠端IP位置 : ");
            //   string m_RemoteIP = Console.ReadLine();
            string m_RemoteIP = "192.168.31.31";

            IPAddress m_RemoteAddress;



            while (true)
            {
                /*離開程式*/
                // if (PLC_connect_ststus == 0)
                //    return;
                /*將IP字串轉換成IPAddress物件*/
                if (!IPAddress.TryParse(m_RemoteIP, out m_RemoteAddress))
                {

                    DisplayAlert("IP位置格式錯誤", "檢查IP", "再試試");

                }
                else
                {
                    break;
                }
            }
            /*初始化IPEndPoint物件,表示要連結的遠端IP位置和Port號4999/PLC_1378*/
            IPEndPoint m_IPPoint = new IPEndPoint(m_RemoteAddress, 4999);
            try
            {
                /*連結遠端Socket*/
                m_SocketClient.Connect(m_IPPoint);
                /*接收資料的緩衝區*/
                byte[] m_RecvBuffer = new byte[8192];
                /*送出資料的緩衝區*/
                byte[] m_SendBuffer;
                int m_RecvLen;
                string m_SendMessage;
                /*接收伺服器login歡迎訊息*/
                //     m_RecvLen = m_SocketClient.Receive(m_RecvBuffer, SocketFlags.None);
                //     ShowMessage(m_RecvBuffer, m_RecvLen);


                // Console.WriteLine("A333333333333___PLC_connect_ststus="+ PLC_connect_ststus);

                //     while (PLC_connect_ststus ==1)
                //    {
                //   Console.Write("輸入訊息 : ");
                /*輸入運算的資訊*/
                //  m_SendMessage = Console.ReadLine();
                m_SendMessage = "01FF000A4420000000000500";
                /*編碼要傳出的訊息*/
                m_SendBuffer = Encoding.Default.GetBytes(m_SendMessage);
                /*送出訊息到伺服器*/
                m_SocketClient.Send(m_SendBuffer, m_SendBuffer.Length, SocketFlags.None);
                /*如果送出的訊息是exit則跳出*/
                //   if (m_SendMessage == "exit")
                //       break;
                /*接收伺服器回傳的訊息*/


                m_RecvLen = m_SocketClient.Receive(m_RecvBuffer, SocketFlags.None);




                ShowMessage(m_RecvBuffer, m_RecvLen);
                // }
                /*關閉連接,確保已連接通訊端上的所有資料在關閉連接前都會加以傳送和接收*/
                m_SocketClient.Shutdown(SocketShutdown.Both);
                /*關閉 Socket 連接並釋放所有相關資源*/
                m_SocketClient.Close();
                //  Console.ReadLine();
            }
            catch (Exception ex)
            {
                m_SocketClient.Close();
                Console.WriteLine(string.Format("錯誤發生 Exception ex : {0}", ex.Message));
                Console.ReadLine();
            }

            /*解碼並顯示伺服器傳回的訊息*/
            void ShowMessage(byte[] buffer, int len)
            {

                //  DisplayAlert("接收字串=", Encoding.Default.GetString(buffer, 0, len), "");

                REC_PLC = Encoding.Default.GetString(buffer, 0, len);


                DisplayAlert("接收字串=====", REC_PLC, "ok");

                Console.WriteLine("伺服器訊息111111111 : {0}", REC_PLC);

                Console.WriteLine("伺服器訊息 : {0}", Encoding.Default.GetString(buffer, 0, len));


            }


        }

    }
}
