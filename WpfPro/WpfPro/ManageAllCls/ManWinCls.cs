using Gecko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfPro;

//管理所有窗体
namespace WpfPro.ManageAllCls
{
    class ManWinCls<T> where T : Window ,new()
    {
        //static List<Window> WinList = new List<Window>();

       // public static IDictionary<string, IntPtr> WinHandle = new Dictionary<string, IntPtr>();


        //键值对
        static IDictionary<string, Window> WinIDictt = new Dictionary<string, Window>();

   #region MyRegion 单例
        //private static ManWinCls<T> _instance = null;
        //private static readonly object SynObject = new object();


        //public static ManWinCls<T> GetInstance
        //{
        //    get
        //    {
        //        // Syn operation.
        //        lock (SynObject)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new ManWinCls<T>();
        //            }

        //            return _instance;
        //        }
        //    }
        //}
   #endregion



        //把类型当作名称
        public static void AddWin(string name , T win)
        {
           
            WinIDictt.Add(name, win);
        }

        //删除窗口
        public static void RemovWin(string WinName)
        {
            //删除姓名为Tang的学生
            //for (int i = 0; i < WinIDictt.Count; i++)
            //{
            //    if (WinIDictt[i].Name == WinName)
            //        WinIDictt.Remove(WinIDictt[i]);
            //}

            WinIDictt.Remove(WinName.Trim());

        }

        //获取元素
        public static T GetWin(string WinName)
        {
            //检测是否包含窗口
            bool bol = WinIDictt.ContainsKey(WinName.Trim());
            T win;
            if (bol)
            {
                win = (T)WinIDictt[WinName.Trim()];
            }
            else
            {
              //  MessageBox.Show("没有找到要获取的窗口");
                win = null;
            }

            return win;
        }

        //关闭窗口时触发的函数
        public void WinClose(object o, System.ComponentModel.CancelEventArgs e)
        {
            //  Window obj = (Window)o;
            // System.Environment.Exit(0);

                Application.Current.Shutdown();
      
           // Application.Current.Shutdown();
        }

        public static void HideWin(T obj)
        {
            T win = obj as T;
            //if (win.Visibility == Visibility.Visible)
            //{
                win.Visibility = Visibility.Hidden; //隐藏
            //}
            //else
            //{
            //    win.Hide();         //隐藏窗口
            //}
        }

        //关闭窗口,实际是重写方法隐藏窗口
        public static void CloseWin(object obj, CancelEventArgs e)
        {
            T win = obj as T;
            e.Cancel = true;  // cancels the window close    
            win.Hide();      // Programmatically hides the window
        }

        //模态启动
        public static void ShowDialogWin(T obj)
        {
            T win = obj as T;
            win.ShowDialog();         //模态启动
            win.Topmost = true;         //窗口显示到最前
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen; //显示到屏幕中间

        }

        public static void OpenOrCreatWin(object obj)
        {
            string WinName = obj as string;
            T rw = ManWinCls<T>.GetWin(WinName);
            if (rw == null)
            {
                T NewRgw = new T();       //创建第一次启动的对象
                ManWinCls<T>.ShowWin(NewRgw);
            }
            else
            {
                ManWinCls<T>.ShowWin(rw);
            }
        }

        //非模态启动
        public static void ShowWin(T obj)
        {
            T win = obj as T;
            //if (win.Visibility==Visibility.Hidden)
            //{
            //    win.Visibility = Visibility.Visible;    //显示
            //    win.Topmost = true;         //窗口显示到最前
            //    win.WindowStartupLocation = WindowStartupLocation.CenterScreen; //显示到屏幕中间
            //}
            //else
           // {
                win.Visibility = Visibility.Visible;    //显示
                win.Topmost = true;         //窗口显示到最前
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen; //显示到屏幕中间
                //win.Show();         //显示窗口
                //win.Topmost = true;         //窗口显示到最前
                //win.WindowStartupLocation = WindowStartupLocation.CenterScreen; //显示到屏幕中间
           // }

        }

        public static void AppShowRun(T obj)   
        {
            T win = obj as T;
            Application app = new Application();
            //设置关闭模式为只有在调用Application对象的Shutdown()方法时,应用程序才会关闭,必须写在app.Run()方法之前
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            app.Run(win);
        }



        //火狐内核启动
       public  static  GeckoWebBrowser geckoWebBrowser;
        public static void FixWebPage(object obj)
        {
            string URL = obj as string;

            System.Windows.Forms.Form fm1 = new System.Windows.Forms.Form();
            Xpcom.Initialize("Firefox");
            geckoWebBrowser = new GeckoWebBrowser { Dock = System.Windows.Forms.DockStyle.Fill };
            fm1.Controls.Add(geckoWebBrowser);
            geckoWebBrowser.Navigate(URL);

            fm1.Width = 888;
            fm1.Height = 600;
            fm1.TopMost = true; //窗口显示在最前
            fm1.ShowDialog();   //模态显示

        }


    }
}
