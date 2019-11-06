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

namespace WpfPro.ManageAllCls
{
    class ThreadCls<T>
    {

       // public delegate void NewTaskDelegate(object obj);

        public static int Thint = 1;
        public static void ThreadFunStart (Action<object> fFun,object sender){

            Action<object> Fun = fFun;

            Thread thread = new Thread(new ParameterizedThreadStart(fFun));
            thread.Name = "WPF线程---" + Thint++;
           // MessageBox.Show("线程-------" + thread.Name);
            thread.IsBackground = true;//设置为后台线程，当主线程结束后，后台线程自动退出，否则不会退出程序不能结束
            thread.Start(sender);//启动新线程
        }




    }
}
