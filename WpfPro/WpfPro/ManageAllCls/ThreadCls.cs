using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfPro.Forms.LoginDir;
using WpfPro.Forms.Products;

namespace WpfPro.ManageAllCls
{
    class ThreadCls<T> //where T : Button  
    {
        private static readonly object BvkObject = new object();

        //public delegate void NewTaskDelegate(object obj);

        public static int Thint = 1;

        //有参数
        public static void ThreadFunStart (Action<object> fFun,object parm){

            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(fFun));
                thread.Name = "WPF线程---" + Thint++;
                // MessageBox.Show("线程-------" + thread.Name);
                thread.IsBackground = true;//设置为后台线程，当主线程结束后，后台线程自动退出，否则不会退出程序不能结束
                thread.Start(parm);//启动新线程
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //无参数
        public static void ThreadFunStart(Action fFun)
        {

            try
            {
                Thread thread = new Thread(new ThreadStart(fFun));
                thread.Name = "WPF线程---" + Thint++;
                // MessageBox.Show("线程-------" + thread.Name);
                thread.IsBackground = true;//设置为后台线程，当主线程结束后，后台线程自动退出，否则不会退出程序不能结束
                thread.Start();//启动新线程
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //有参数,通过将线程的 ApartmentState 属性设置为单线程单元(STA)
        public static void STAThread(Action<object> fFun, object parm)
        {

            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(fFun));
                thread.Name = "WPF线程---" + Thint++;
                // MessageBox.Show("线程-------" + thread.Name);
                thread.IsBackground = true;//设置为后台线程，当主线程结束后，后台线程自动退出，否则不会退出程序不能结束
                thread.TrySetApartmentState(ApartmentState.STA);    //属性设置为单线程单元(STA)
                thread.Start(parm);//启动新线程
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        //开启托管的线程
        private void ThreadDelegateFun(Action<object> fun, object parm)
        {
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                fun(parm);
            }));
            // 将当前线程设置为单个线程单元(STA)模式方可进行 OLE 调用。
          //  objThread.TrySetApartmentState(ApartmentState.STA);// ~~~~~~~~~~~~~~~~~~~~加上这句话就可以解决问题了
            // 设置为后台线程
            objThread.IsBackground = true;
            // 开启线程
            objThread.Start();
        }


        //UI线程托管按钮
        public static void DelegateBIVKFun(Button but,Action<object> Fun , params object[] str)
        {
            Action action1 = () =>      //匿名方法
            {
                //创建或显示窗口
                //ManWinCls<RegWin>.OpenOrCreatWin((string)str[0]);
                Fun((string)str[0]);
            };
            but.Dispatcher.BeginInvoke(action1);
        }


        //重载委托方法 ,线程托管menuItem
        public static void ItemDelegateBIVKFun(MenuItem mi, Action<object> Fun)
        {
            lock (BvkObject)
            {
                    Action action1 = () =>      //匿名方法
                    {
                    //创建或显示窗口
                    //ManWinCls<RegWin>.OpenOrCreatWin((string)str[0]);
                          Fun(mi);
                     };
                    mi.Dispatcher.BeginInvoke(action1);

            }
        }


        //同步的委托方法 ,线程托管menuItem
        public static void ThreadIvkFun(object parm, Action<object> Fun)
        {

            ParmObj poj = parm as ParmObj;
            Button but = poj.ParmArray[0] as Button;

            but.Dispatcher.Invoke(new Action(() =>
            {
                  Fun(parm);
            }));
        }




        //跨线程更新UI
        public static void SyncFun(SendOrPostCallback fFun, object pram)
        {
            ProductsListWin.sync.Post(fFun, pram);
        }


        //线程池,有参数
       //public const int cycleNum = 10;
         public  static void ThreadPoolFun(Action<object> Fun ,object parm)
        {
            ThreadPool.SetMinThreads(1, 2); //创建的线程最少数量
            ThreadPool.SetMaxThreads(10, 5); //设置线程池的最大可用辅助线程数目，最大异步线程数目
            ThreadPool.QueueUserWorkItem(new WaitCallback(Fun), parm);

        }
        //线程池,无参数
        //public const int cycleNum = 10;
        public static void ThreadPoolFun(Action<object> Fun)
        {
            ThreadPool.SetMinThreads(1, 2); //创建的线程最少数量
            ThreadPool.SetMaxThreads(2, 5); //活动状态的线程数量
            ThreadPool.QueueUserWorkItem(new WaitCallback(Fun));

        }

    }
}
