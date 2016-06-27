using Easy.Logger;
using ImageClient.Command;
using ImageClient.Infrastructure;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageClient.ViewModels
{
    class SendImageModel : BaseViewModel
    {
        private ILogger _logger = Log4NetService.Instance.GetLogger<SendImageModel>();
        public SendImageModel()
        {
            SendImageCommand = new AsyncCommand(this.SendImage, x => this.CanExecute);
            BrowseFileCommand = new SyncCommand(this.Browse, x => true);
            LogoutCommand = new AsyncCommand(this.Logout, x => true);

        }


        private bool _canExecute = true;
        public bool CanExecute
        {
            get
            { return _canExecute; }

            set
            {
                _canExecute = value;
                NotifyPropertyChanged("CanExecute");
            }
        }

        public ICommand BrowseFileCommand { get; private set; }
        public IAsyncCommand SendImageCommand { get; private set; }
        public IAsyncCommand LogoutCommand { get; private set; }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; NotifyPropertyChanged("ImagePath"); }
        }

        private bool _isStore;
        public bool IsStore
        {
            get { return _isStore; }
            set { _isStore = value; NotifyPropertyChanged("IsStore"); }
        }

        private string _message;
        public string Message { get { return _message; } set { _message = value; NotifyPropertyChanged("Message"); } }

        private bool? _displayMsg = false;
        public bool? DisplayMsg { get { return _displayMsg; } set { _displayMsg = value; NotifyPropertyChanged("DisplayMsg"); } }



        private void Browse(object sender)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            var result = dlg.ShowDialog();
            if (result == true)
            {
                ImagePath = dlg.FileName;
            }
        }


        public async Task SendImage(object sender)
        {
            try
            {
                CanExecute = false;
                DisplayMsg = false;

                if (String.IsNullOrEmpty(ImagePath))
                {
                    DisplayMsg = true;
                    Message = "Image path is requried";
                    return;
                }

                ApiConsumer consumer = new ApiConsumer();
                var result = await consumer.SendImageAsync(ImagePath, IsStore);

                DisplayMsg = true;
                Message = "";
                foreach (var cur in result.JContent)
                {
                    Message += cur.Value.Value<string>() + " ";
                }
                Message = Message.TrimEnd(' ');

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show(Message + " the token is expired, please re-login");

                    AppContext.Current.App.CurPageViewModel = new LoginViewModel();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                DisplayMsg = true;
                Message = ex.Message;
            }
            finally
            {
                CanExecute = true;
            }
        }

        public async Task Logout(object sender)
        {
            try
            {
                ApiConsumer consumer = new ApiConsumer();
                var result = await consumer.LogoutSync();

                DisplayMsg = true;
                Message = "";
                foreach (var cur in result.JContent)
                {
                    Message += cur.Value.Value<string>() + " ";
                }
                Message = Message.TrimEnd(' ');

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    AppContext.Current.App.CurPageViewModel = new LoginViewModel();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                DisplayMsg = true;
                Message = ex.Message;
            }
            finally
            {
                CanExecute = true;
            }
        }

    }

}
