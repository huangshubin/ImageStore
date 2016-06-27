using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageClient.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {

        private IPageViewModel _curPageViewModel;

        public ApplicationViewModel()
        {

            CurPageViewModel = new LoginViewModel();
        }


        public IPageViewModel CurPageViewModel
        {
            get
            {
                return _curPageViewModel;
            }
            set
            {
                if (_curPageViewModel != value)
                {
                    _curPageViewModel = value;
                    NotifyPropertyChanged("CurPageViewModel");
                }
            }
        }



    }
}
