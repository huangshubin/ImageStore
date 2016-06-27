using Easy.Logger;
using ImageClient.Command;
using ImageClient.Http;
using ImageClient.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImageClient.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private ILogger _logger = Log4NetService.Instance.GetLogger<LoginViewModel>();

        public LoginViewModel()
        {
            LoginCommand = new AsyncCommand(this.Login, x => this.CanExecute);
        }



        private string _userName;
        public string UserName { get { return _userName; } set { _userName = value; NotifyPropertyChanged("UserName"); } }

        private string _password;
        public string Password { get { return _password; } set { _password = value; } }

        private bool? _failure = true;
        public bool? Failure { get { return _failure; } set { _failure = value; NotifyPropertyChanged("Failure"); } }


        private string _failureReason;
        public string FailureReason { get { return _failureReason; } set { _failureReason = value; NotifyPropertyChanged("FailureReason"); } }

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

        public IAsyncCommand LoginCommand { get; private set; }

        public async Task Login(object parameter)
        {
            try
            {
                var passwordCtrl = parameter as PasswordBox;

                if (passwordCtrl == null) throw new Exception("can not find the password control");

                Password = passwordCtrl.Password;

                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    Failure = true;
                    FailureReason = "user name and password is requried";
                    return;
                }

                CanExecute = false;

                ApiConsumer consumer = new ApiConsumer();
                var result = await consumer.LoginAsync(UserName, Password);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    Failure = false;
                    FailureReason = "";

                    var token = new AuthToken()
                    {
                        Key = result.JContent["access_token"].Value<string>(),
                        ExpiresIn = result.JContent["expires_in"].Value<int>(),
                        TokenType = result.JContent["token_type"].Value<string>()
                    };

                    AppContext.Current.AuthToken = token;
                    AppContext.Current.App.CurPageViewModel = new SendImageModel();

                }
                else
                {
                    Failure = true;
                    FailureReason = "";
                    foreach (var cur in result.JContent)
                    {
                        FailureReason += cur.Value.Value<string>() + " ";
                    }
                    FailureReason = FailureReason.TrimEnd(' ');
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                Failure = true;
                FailureReason = ex.Message;
            }
            finally
            {
                CanExecute = true;
            }
        }


    }

}
