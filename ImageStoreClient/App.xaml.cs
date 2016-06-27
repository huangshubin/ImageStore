using ImageClient.Infrastructure;
using ImageClient.ViewModels;
using ImageClient.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ImageClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationView app = new ApplicationView();

            ApplicationViewModel model = new ApplicationViewModel();
            model.CurPageViewModel = new LoginViewModel();

            app.DataContext = model;

            AppContext.Current.App = model;

            app.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            var consumer = new ApiConsumer();
            await consumer.LogoutSync();
        }

    
    }
}
