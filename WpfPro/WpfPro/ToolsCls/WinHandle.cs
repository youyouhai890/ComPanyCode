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
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfPro.Forms.Products;
using WpfPro.ManageAllCls;

namespace WpfPro.ToolsCls
{

    //窗口句柄的控制,窗口句柄不能用泛型
    class WinHandle
    {
        private static readonly object WHlockSync = new object();

 #region Windows API
        ////声明 API 函数
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);


        //其中第一个参数为该窗体的类名，其实一般来说都设置为null
        //第二个参数为窗体的标题名(一般第二个参数经常使用)
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        //定义消息常数
        //public const int CUSTOM_MESSAGE = 0X400 + 2;//自定义消息

        //   2、对窗口进行最小化
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int _value);
        //WinHandle.ShowWindow(handle, 2);  //最小化

        //窗口大小模版
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }


        [DllImport("user32.dll")]
        //获取窗口大小
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        //配合RECT结构体一起用

        //窗口设置(包含置顶)
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        //配合RECT结构体一起用


        //1.首先需要声明一个委托函数用于 Win32 API - EnumWindows 的回调函数：
        //private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam); //IntPtr hWnd用int也可以
        //2.然后利用 C# 中的平台调用声明从 USER32.DLL 库中调用 API - EnumWindows，具体参数请参考 MSDN - Win32 API。
        //private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        //3.最后实例化委托，调用 EnumWindows。
        //EnumWindows(delegate(IntPtr hWnd, int lParam) {……},0);

        //1.首先需要声明一个委托函数用于 Win32 API - EnumWindows 的回调函数：
        public delegate bool WNDENUMPROC(IntPtr hWnd, int lParam); //IntPtr hWnd用int也可以
        //用来遍历所有窗口 
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);

        //获取窗口Text 
        [DllImport("user32.dll")]
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);
        //GetWindowTextW(hWnd, StringBuilder, StringBuilder.Capacity);      //实例

        //获取窗口类名 
        [DllImport("user32.dll")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        //获取窗口焦点,激活窗口
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        //SetForegroundWindow(win);       //实例


 #endregion



 #region  遍历所有窗口(获取所有窗口)     

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
                    break;
                }
            }

            return hwndName;
        }

        //获取所有窗口句柄
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



 #region 句柄类型操作
        //string转换为窗口句柄类型
        public static IntPtr GetStringToIntptr(string str)
        {
            //如果直接转换成字符串会有问题,所以先转int类型
            IntPtr inpt = (IntPtr)Convert.ToInt64(str);
            return inpt;
        }
        //句柄转换字符串
        public static string GetIntPtrToString(IntPtr inpt)
        {
            string str = Marshal.PtrToStringAnsi(inpt);
            return str;
        }

        #endregion

#region 窗口设置(获取窗口大小,置顶之类的操作 

        //获取窗口大小
        public static RECT SetSizeWinOpt(IntPtr win)
        {
            //IntPtr win = WinHandle.GetStringToIntptr(Hwnd);   //字符串句柄转换为intptr

            RECT rect = new RECT();
            WinHandle.GetWindowRect(win, ref rect);//h为窗口句柄

            int width = rect.Right - rect.Left;                        //窗口的宽度
            int height = rect.Bottom - rect.Top;                   //窗口的高度
            int x = rect.Left;
            int y = rect.Top;

            return rect;
        }

        //窗口置顶和激活操作
        //public static void SetStickWinOpt(IntPtr win)
        public static void SetStickWinOpt(object obj)
        {
            IntPtr win = (IntPtr)obj;
            RECT rect = SetSizeWinOpt(win);  //获取窗口大小

            IntPtr ZhiDing = (IntPtr)1;
            //设置窗口 , 第二个参数为置顶
            SetWindowPos(win, ZhiDing, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, 1);

            SetForegroundWindow(win);        //激活窗口,移动焦点
        }

        //窗口最小化
        public static void SetMixWin(IntPtr Hwnd)
        {
            WinHandle.ShowWindow(Hwnd, 2);  //最小化窗口
        }

        //激活窗口
        public static bool ActiveWin(object Hwnd)
        {
            IntPtr win = (IntPtr)Hwnd;

          return  SetForegroundWindow(win);       
        }

        //循环激活
        public static void ActiveWinWhi(object Hwnd)
        {
            while (ManageAllStateClscs<object>.GetInstance.ThreadState)
            {
                IntPtr win = (IntPtr)Hwnd;
                SetForegroundWindow(win);        
            }
        }
        #endregion

    }
}
