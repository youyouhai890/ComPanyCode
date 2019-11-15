using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using WpfPro.Forms.Products;
using WpfPro.ManageAllCls;
using static WpfPro.ToolsCls.WinHandle;

namespace WpfPro.ToolsCls
{
    
    class ClipboardOptCls<T>
    {
        private static readonly object CliSynObj = new object();
        //发送剪贴板里的内容
        public static void ClipboardCoptToPaste(object parm)
        {

            ParmObj poj = parm as ParmObj;
            Button but = poj.ParmArray[0] as Button;
            Action action1 = () =>      //匿名方法
            {

                int NumOf = (int)poj.ParmArray[1];
                int time = (int)poj.ParmArray[2];
                string HwndStr = poj.ParmArray[3] as string;
                string ContTxt = poj.ParmArray[4].ToString().Trim() as string;
                BitmapImage pic = poj.ParmArray[5] as BitmapImage;


                if (HwndStr == null || HwndStr == "")
                {
                    MessageBox.Show("没有找到窗口");
                    return;
                }

                //字符串转换为真正句柄
                IntPtr win = GetStringToIntptr(HwndStr);

                    //单独开线程窗口置顶,和激活
                    //ThreadCls<object>.ThreadPoolFun(SetStickWinOpt, win);
                    //激活窗口
                    //ThreadCls<object>.ThreadFunStart(ActiveWin, win));

                    while (!ActiveWin(win))     //激活时继续往下执行
                    {
                        ;
                    }


                 //   pw.WindowState = WindowState.Minimized; //设置WPF的窗体最小化


                    //单线程单元模式
                    poj.ParmArray[0] = ContTxt;
                    poj.ParmArray[1] = pic;
                    poj.ParmArray[2] = win;

                    //执行两个
                   // ThreadCls<object>.STAThread(SendContentObj, poj);



                    ThreadCls<object>.STAThread(SendStrCont, poj);

                    //ThreadCls<object>.STAThread(SendBigMap, poj);

             };
             but.Dispatcher.BeginInvoke(action1);

        }

        public static void SendContentObj(object obj)
        {
            ManageAllStateClscs<object>.GetInstance.ThreadState = true;     //状态设置

            DataObject dataobj1 = new DataObject();
            DataObject dataobj2 = new DataObject();



            ParmObj poj = obj as ParmObj;
            Button but = poj.ParmArray[0] as Button;
            string ContTxt = (string)poj.ParmArray[0];
            BitmapImage pic = (BitmapImage)poj.ParmArray[1];


            //清空剪贴板,发送内容部分
           // Clipboard.Clear();
            dataobj1.SetData(DataFormats.Text, ContTxt);
            Clipboard.SetDataObject(dataobj1);       //数据放到剪贴板
            System.Windows.Forms.SendKeys.SendWait(dataobj1+ "^{v}{ENTER}");// CTRL+V
            //System.Windows.Forms.SendKeys.SendWait("{ENTER}");


            //清空剪贴板,发送内容部分
           // Clipboard.Clear();
            dataobj2.SetData(DataFormats.Bitmap, pic);
            Clipboard.SetDataObject(dataobj2);       //数据放到剪贴板
            System.Windows.Forms.SendKeys.SendWait(dataobj2+ "^{v}{ENTER}");// CTRL+V
           // System.Windows.Forms.SendKeys.SendWait("{ENTER}");

            //Thread.Sleep(TimeSpan.FromMilliseconds(1000));  //当前线程停顿一会

            ManageAllStateClscs<object>.GetInstance.ThreadState = false;     //状态设置
        }

        public static void SendStrCont(object  obj)
        {
            lock (CliSynObj)
            {
              //  Thread.Sleep(TimeSpan.FromMilliseconds(2000));  //当前线程停顿一会

                ParmObj poj = obj as ParmObj;
                string ContTxt = (string)poj.ParmArray[0];
                DataObject dataobj1 = new DataObject();

                //清空剪贴板,发送内容部分
                dataobj1.SetData(DataFormats.Text, ContTxt);

                Clipboard.Clear();
                Clipboard.SetDataObject(dataobj1);       //数据放到剪贴板
                System.Windows.Forms.SendKeys.SendWait("^{v}{ENTER}");// CTRL+V
                //Thread.Sleep(TimeSpan.FromMilliseconds(1000));  //当前线程停顿一会
            }


        }

        public static void SendBigMap(object obj)
        {
            lock (CliSynObj)
            {

                ParmObj poj = obj as ParmObj;
                BitmapImage pic = (BitmapImage)poj.ParmArray[1];

                DataObject dataobj2 = new DataObject();
                //清空剪贴板,发送内容部分
                dataobj2.SetData(DataFormats.Bitmap, pic);

                Clipboard.Clear();
                Clipboard.SetDataObject(dataobj2);       //数据放到剪贴板
                System.Windows.Forms.SendKeys.SendWait("^{v}{ENTER}");// CTRL+V
               //  Thread.Sleep(TimeSpan.FromMilliseconds(1000));  //当前线程停顿一会
            }

        }
    }
}

