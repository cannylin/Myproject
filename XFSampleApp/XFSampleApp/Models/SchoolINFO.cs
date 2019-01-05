using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XFSampleApp.Models
{
    public class SchoolINFO : INotifyPropertyChanged  // 如果與畫面相關需變更須做~Infotifyproperchange
    {

        //  public string Name { get; set; }
        //  public string LOGO { get; set; }
        //  public string ADDRESS { get; set; }
        //  public string TEL { get; set; }
        //  public string Email { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanged();
                    _name = value;
                }
            }
        }

        private string _logo;
        public string LOGO
        {
            get { return _logo; }
            set
            {
                if (_logo != value)
                {
                    NotifyPropertyChanged();
                    _logo = value;
                }
            }
        }

        private string _address;
        public string ADDRESS
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    NotifyPropertyChanged();
                    _address = value;
                }
            }
        }

        public string _tel;
        public string TEL
        {
            get { return _tel; }
            set
            {
                if (_tel != value)
                {
                    NotifyPropertyChanged();
                    _tel = value;
                }
            }
        }

        public string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    NotifyPropertyChanged();
                    _email = value;
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
