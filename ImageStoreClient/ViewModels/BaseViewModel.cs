using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
