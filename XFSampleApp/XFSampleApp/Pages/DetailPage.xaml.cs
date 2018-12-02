using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;         //cannylin
using System.Net.Sockets;  //cannylin
using XFSampleApp.Models;

namespace XFSampleApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        public int PLC_connect_ststus { get; private set; }
        public String  REC_PLC { get; private set; }

        public DetailPage ()
		{
			InitializeComponent ();
            PreviewGridCell(this.Content as Grid , Color.Pink);
           // PLC_Connect();
        }

        public DetailPage(SchoolINFO schoolINFO):this()
       {
            BindingContext = schoolINFO;

        }
 

        private void PreviewGridCell(Grid grid, Color cellBackgroundColor)
        {
            var columnCount = grid.ColumnDefinitions.Count;
            var rowCount = grid.RowDefinitions.Count;

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    var boxView = new BoxView()
                    {
                        BackgroundColor = cellBackgroundColor
                    };
                    Grid.SetColumn(boxView, column);
                    Grid.SetRow(boxView, row);
                    grid.Children.Add(boxView);
                }
            }

            var allBuildPreviewCellCount = columnCount * rowCount;

            for (int start = 0; start < grid.Children.Count - allBuildPreviewCellCount; start++)
            {
                var targetCellView = grid.Children[0];
                for (int raiseCount = 0; raiseCount < grid.Children.Count - 1; raiseCount++)
                {
                    grid.RaiseChild(targetCellView);
                }
            }
        }

         private void PLC_Connect()
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
                m_SendMessage ="01FF000A4420000000000500";
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




        private void DisConnect_Click(object sender, EventArgs e)
        {
            PLC_connect_ststus = 0;
            
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            PLC_connect_ststus = 1;
            PLC_Connect();
        }
    }
}