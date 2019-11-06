using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfPro.Configs;
using WpfPro.Forms.LoginDir;
using WpfPro.ManageAllCls;

namespace WpfPro
{

    class Program : Window
    {
        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;



        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Process instance = RunningInstance();
            if (instance == null)   //检测是否为第一次启动
            {
                //初始化环境信息,包含链接信息
                //Thread thread = new Thread(new ThreadStart(initEnv));
                //thread.Start();
                EnvirInit();   //启动环境

                // Application app = new Application();        //创建application对象
                LoginInputWin LogInp = new LoginInputWin();        //作为主窗口启动
                LogInp.Title = "Login";        //窗口标题
                //ManWinCls<LoginInputWin>.OpenOrCreatWin("LoginInputWinForm");
                ManWinCls<LoginInputWin>.ShowDialogWin(LogInp);

               // WpfPro.UI.DrawForm.LonginInputDraw();               
               // app.Run(LogInp);

            }
            else
            {
                //显示已运行的程序
                HandleRunningInstance(instance);
            }
        }



        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            //GetCurrentProcess创建新的Process类实例并将其当前活动的进程资源关联            
            Process current = Process.GetCurrentProcess();  //
            //创建新的 Process 组件的数组，并将它们与本地计算机上共享指定的进程名称的所有进程资源关联。
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception);
            //throw new NotImplementedException();
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端 
        }

        private static void initEnv()
        {

            Config.AllEnvirConfInit(); //管理所有环境

            //下面这个比较耗时
            System.Environment.SetEnvironmentVariable("TYH_HOME", AppDomain.CurrentDomain.BaseDirectory, EnvironmentVariableTarget.User);
        }



        static void EnvirInit()
        {
           Config.AllEnvirConfInit(); //管理所有环境
        }

    }
}
