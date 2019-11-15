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
using System.Diagnostics;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Text;

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

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(5000);
            //timer.Tick += EventSend;  //你的事件,注意参数必须是固定的类型
            //timer.IsEnabled = true;
            //timer.Start();

            try
            {
                EventSend();

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }



        //注意参数必须是固定的类型
        // public  void EventSend(object sender, EventArgs e)
        public  void EventSend()
        {
            //获取当前WPF窗口句柄 
            IntPtr handle = new WindowInteropHelper(this).Handle;
            WinHandle.ShowWindow(handle, 2);  //最小化

            string HwndStr = WinHandle.AssignWinHwnd("A_엄마");
            if (HwndStr == null)
            {
                return;
            }
            //string HwndStr = WinHandle.AssignWinHwnd("A_아빠");
            //获取窗口大小
            IntPtr win = WinHandle.GetStringToIntptr(HwndStr);

            WinHandle.RECT rect = new WinHandle.RECT();
            WinHandle.GetWindowRect(win, ref rect);//h为窗口句柄
            int width = rect.Right - rect.Left;                        //窗口的宽度
            int height = rect.Bottom - rect.Top;                   //窗口的高度
            int x = rect.Left;
            int y = rect.Top;
            IntPtr itp= (IntPtr)1;
            //设置窗口 , 第二个参数为置顶
            WinHandle.SetWindowPos(win, itp, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, 1);
            // Button but = sender as Button;

            //Action action1 = () =>      //匿名方法
            //{

            string str = @"黑芝麻核桃黑豆粉 现磨熟黑芝麻糊即食三桑葚粉 代餐营养早餐食品-
                       ★原*价29.9元 ★劵*后价9.9元 ★劵*面额20 ★卖*点【60天无理由退换货，吃了拆了也能退，
                        好东西就是这么自信】百补黑为首，食补先补黑！五谷新鲜研磨，香醇可口，代餐食补，
                        好气色好身体养出来!★二合一链接￥zSnjYFSO2YN￥";

            string str2 = "https://img.alicdn.com/imgextra/i1/721690846/O1CN017w8YYv1I7Vb8R1DdI_!!721690846.jpg";
            BitmapImage img = new BitmapImage(new Uri(str2));   //获取图片


            Thread.Sleep(TimeSpan.FromMilliseconds(1000));
            Clipboard.Clear();  //清空剪贴板
            Clipboard.SetData(DataFormats.Text, "1111111111111111111111");   //剪贴板保存数据
            WinHandle.SetForegroundWindow(win); //激活
            System.Windows.Forms.SendKeys.SendWait("^{v}");// CTRL+V
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");

            

            //当前线程间隔时间,已经是在线程里面
            for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(2000));
                    //字符串转换成句柄
                    //WinHandle.SetForegroundWindow(win);       //激活窗口,获取焦点



                //String img = (ttw.Timage.Source as BitmapImage).UriSource.OriginalString;//获取图片URI地址
                //dataobj.SetData(DataFormats.Text, str);
                //拷贝到剪贴板
                //Clipboard.SetDataObject(TextContent, true);
                //   Clipboard.SetDataObject(dataobj);
                             //清空剪贴板


                IDataObject dataobj = new DataObject();

                //清空剪贴板
                Clipboard.Clear();
                dataobj.SetData(DataFormats.Text, str);
                Clipboard.SetDataObject(dataobj);       //数据放到剪贴板
                WinHandle.SetForegroundWindow(win);       //激活窗口,获取焦点
                System.Windows.Forms.SendKeys.SendWait("^{v}");// CTRL+V
                WinHandle.SetForegroundWindow(win);       //激活窗口,获取焦点
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");

               // Thread.Sleep(TimeSpan.FromMilliseconds(2000));

                //清空剪贴板
                Clipboard.Clear();
                dataobj.SetData(DataFormats.Bitmap, img);
                Clipboard.SetDataObject(dataobj);       //数据放到剪贴板
                WinHandle.SetForegroundWindow(win);       //激活窗口,获取焦点
                System.Windows.Forms.SendKeys.SendWait("^{v}");// CTRL+V
                WinHandle.SetForegroundWindow(win);       //激活窗口,获取焦点
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");


            }

            //};
            //but.Dispatcher.BeginInvoke(action1);
        }





        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);//退出全部线程,关闭整个进程  
            Process pos =  Process.GetCurrentProcess();
            pos.Kill();
        }
    }
}
