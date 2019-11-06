using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPro
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application currApp = Application.Current;

            currApp.StartupUri = new Uri("LoginInputWin.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
