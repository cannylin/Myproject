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





        private void DisConnect_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Connect_Click(object sender, EventArgs e)
        {
           
        }
    }
}