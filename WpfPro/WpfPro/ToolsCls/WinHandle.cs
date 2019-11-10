using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPro.ToolsCls
{

    //窗口句柄的控制
    class WinHandle
    {

        #region 窗口句柄
        ////声明 API 函数
        //[DllImport("User32.dll", EntryPoint = "SendMessage")]
        //private static extern IntPtr SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);


        //[DllImport("User32.dll", EntryPoint = "FindWindow")]
        ////其中第一个参数为该窗体的类名，其实一般来说都设置为null
        ////第二个参数为窗体的标题名(一般第二个参数经常使用)
        //private static extern int FindWindow(string lpClassName, string lpWindowName);

        ////定义消息常数
        //public const int CUSTOM_MESSAGE = 0X400 + 2;//自定义消息


        ////向窗体发送消息的函数
        //public void SendMsgToMainForm(int MSG)
        //{
        //    int WINDOW_HANDLER = FindWindow(null, "A_엄마");
        //    if (WINDOW_HANDLER == 0)
        //    {
        //        throw new Exception("Could not find Main window!");
        //    }

        //    long result = SendMessage(WINDOW_HANDLER, CUSTOM_MESSAGE, new IntPtr(14), IntPtr.Zero).ToInt64();

        //}


        ////获取窗口句柄
        //public static void GetHandle(string Str)
        //{
        //    string Content = Str;

        //    IntPtr ParenthWnd = new IntPtr(0);
        //    ParenthWnd = (IntPtr)FindWindow(null, Content);
        //    //判断这个窗体是否有效
        //    if (ParenthWnd != IntPtr.Zero)
        //    {
        //        MessageBox.Show(ParenthWnd.ToString());
        //    }
        //    else
        //        MessageBox.Show("没有找到窗口");

        //}

        #endregion


  #region  遍历所有窗口(获取所有窗口)     


        //1.首先需要声明一个委托函数用于 Win32 API - EnumWindows 的回调函数：
        //private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam); //IntPtr hWnd用int也可以
        //2.然后利用 C# 中的平台调用声明从 USER32.DLL 库中调用 API - EnumWindows，具体参数请参考 MSDN - Win32 API。
        //private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        //3.最后实例化委托，调用 EnumWindows。
        //EnumWindows(delegate(IntPtr hWnd, int lParam) {……},0);

        //1.首先需要声明一个委托函数用于 Win32 API - EnumWindows 的回调函数：
        private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam); //IntPtr hWnd用int也可以
        //用来遍历所有窗口 
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);

        //获取窗口Text 
        [DllImport("user32.dll")]
        private static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        //获取窗口类名 
        [DllImport("user32.dll")]
        private static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        //自定义一个类，用来保存句柄信息，在遍历的时候，随便也用空上句柄来获取些信息，呵呵 
        public struct WindowInfo
        {
            public IntPtr hWnd;
            public string szWindowName;     //存储窗口名称
            public string szClassName;
        }

        //获取指定窗口名称,参数为要查找的窗口名
        public static string AssignWinHwnd(string WinName)
        {
            IDictionary<IntPtr, string> WinHwnd = null;

            //获取所有窗口句柄
            WinHwnd = GetAllDesktopWindows();

            //跟名称对应的窗口句柄
            string hwndName = string.Empty; 
            foreach (KeyValuePair<IntPtr, string> di in WinHwnd)     //遍历类型转换
            {
                if (di.Value.ToString() == WinName)
                {
                    hwndName = di.Key.ToString();
                    //MessageBox.Show((string)WinHwnd[va]);
                    //MessageBox.Show(di.Key.ToString());
                    break;
                }
            }

            if (hwndName == "" || hwndName == null)
            {
                MessageBox.Show("没有获取到匹配的窗口...");
            }

            return hwndName;
        }

        //获取所有窗口句柄
        //public static List<WindowInfo> GetAllDesktopWindows()
        public static IDictionary<IntPtr, string> GetAllDesktopWindows()
        {
            //用来保存窗口对象 列表
            IDictionary<IntPtr,string> WinHwnd = new Dictionary<IntPtr, string>();


            //enum all desktop windows 
            EnumWindows(delegate (IntPtr hWnd, int lParam)          ////用来遍历所有窗口 ,应该是调用API
            {


                WindowInfo wnd = new WindowInfo();  //定义上面的结构体
                StringBuilder sb = new StringBuilder(256);  //可变字符串

                //get hwnd 
                wnd.hWnd = hWnd;    //应该是句柄

                //get window name  
                GetWindowTextW(hWnd, sb, sb.Capacity);      //获取窗口Text,调用API 
                wnd.szWindowName = sb.ToString();

                WinHwnd.Add(hWnd,sb.ToString()); //存储句柄和名字
                //get window class 
                GetClassNameW(hWnd, sb, sb.Capacity);        //获取窗口类名,调用API
                wnd.szClassName = sb.ToString();

                //add it into list 
                return true;
            } ,  0);

           // return wndList.ToArray();
            return WinHwnd;
        }

#endregion




#region 往微信默认打开的主窗口发送消息

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String ClassName, String WindwosName);  //用于查找窗口

        [DllImport("user32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);



        //第一个参数为内容 , 第二个为窗口句柄
        public static void WeChatMainWinMsgSend(string Content,string WinName)
        {
            //参数为窗口类 , 窗口名
            // IntPtr win = FindWindow(null, WinName);    //"WeChatMainWndForPC" 为微信默认打开的主窗口
            IntPtr win = GetStringToIntptr(WinName);
            SetForegroundWindow(win);
            System.Windows.Forms.SendKeys.SendWait(Content);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
        }




 #endregion

         //string转换为窗口句柄类型
        public static IntPtr GetStringToIntptr(string str)
        {
            //如果直接转换成字符串会有问题,所以先转int类型
            IntPtr inpt = (IntPtr)Convert.ToInt64(str);
            return inpt;
        }

        public static string GetIntPtrToString(IntPtr inpt)
        {
            string str = Marshal.PtrToStringAnsi(inpt);
            return str;
        }

        //发送微信消息 , 参数为内容 , 次数 ,句柄
        public static void WinSendMsg(string ContTxt, int CiShu ,string HwndStr)
        {
            int j = 0;
            while (j++ < CiShu)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(50));    //应该是间隔时间
                WeChatMainWinMsgSend(ContTxt, HwndStr);
            }
        }



    }
}
