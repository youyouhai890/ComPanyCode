using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfPro.Forms.LoginDir;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.Local.LocalModel;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;
using WpfPro.ToolsCls;
using WpfPro.HttpJsons.WeChat;
using static WpfPro.ManageAllCls.ManaEnumCls;


namespace WpfPro.Forms.TestForm
{
    /// <summary>
    /// TestWin.xaml 的交互逻辑
    /// </summary>
    public partial class TestWin : Window
    {
        public TestWin()
        {
            InitializeComponent();
        }

        private void TestButt_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;

            Action action1 = () =>      //匿名方法
            {
               string HwndStr = WinHandle.AssignWinHwnd("A_엄마");

                //当前线程间隔时间,已经是在线程里面
                for (int i = 0; i < 1; i++)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(300));
                    //字符串转换成句柄
                    IntPtr win = WinHandle.GetStringToIntptr(HwndStr);
                    WinHandle.SetForegroundWindow(win);       //激活窗口,获取焦点

                    Thread.Sleep(TimeSpan.FromMilliseconds(300));
                    System.Windows.Forms.SendKeys.SendWait("1111");//发送内容
                    Thread.Sleep(TimeSpan.FromMilliseconds(300));
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");

                    //Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    //System.Windows.Forms.SendKeys.SendWait(pic);//发送内容
                    //Thread.Sleep(TimeSpan.FromMilliseconds(300));
                    //System.Windows.Forms.SendKeys.SendWait("{ENTER}");

                }

            };
            but.Dispatcher.BeginInvoke(action1);

        }
    }
}
