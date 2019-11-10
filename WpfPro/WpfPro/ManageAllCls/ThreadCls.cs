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
        public static void ThreadFunStart (Action<object> fFun,object sender){

            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(fFun));
                thread.Name = "WPF线程---" + Thint++;
                // MessageBox.Show("线程-------" + thread.Name);
                thread.IsBackground = true;//设置为后台线程，当主线程结束后，后台线程自动退出，否则不会退出程序不能结束
                thread.Start(sender);//启动新线程
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Action<object> Fun = fFun;


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



        public static void SyncFun(SendOrPostCallback fFun, object pram)
        {
            ProductsListWin.sync.Post(fFun, pram);
        }

        


    }
}
