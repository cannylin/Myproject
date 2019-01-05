using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSampleApp.Models;

namespace XFSampleApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Detail_EditPage : ContentPage
	{
        private SchoolINFO school_info; //TR DATA AFTER CAN SAVE
        private SchoolINFO Org_Schoolinfo; 

        public Detail_EditPage ()
		{
			//InitializeComponent ();
		}
        public Detail_EditPage(SchoolINFO school_info) : this()
        {
            CloneSchoolinfo(school_info);
            BindingContext = this.school_info = school_info; 
            //C#建構子 CONTRTER COPY 記憶體 SAVE 參考變數 ;school_info記錄用
            //
        }


        private void CloneSchoolinfo(SchoolINFO school_info)
        {
            Org_Schoolinfo = new SchoolINFO
            {
                Name = school_info.Name,
                TEL = school_info.TEL,
                ADDRESS = school_info.ADDRESS,
                Email = school_info.Email,
                LOGO = school_info.LOGO,
            };
        }
        private async void FinishBT_clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private async void Cancel_BT(object sender, EventArgs e)
        {
            var index = MainPage.schoolData.IndexOf(school_info);
            MainPage.schoolData.Remove(school_info);        
            //取消On MainPage Ovser...Collection link 踢掉原本schoolinfo
            MainPage.schoolData.Insert(index,Org_Schoolinfo);
            //將Org_Schoolinfo 依 index 插回原本data
            await Navigation.PopModalAsync(true);
        }
    }
}