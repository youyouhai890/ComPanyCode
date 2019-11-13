using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfPro.Controls;
using WpfPro.ManageAllCls;

namespace WpfPro.Forms.LoginDir
{
    /// <summary>
    /// LoginInput.xaml 的交互逻辑
    /// </summary>
    public partial class LoginInputWin : Window
    {
        public LoginInputWin()
        {
            InitializeComponent();
           
            ManWinCls<LoginInputWin>.AddWin(this.Name, this);   //添加管理的窗口

            //关闭当前窗口,在属性窗口那里貌似找不到这事件，但是可以手动注册
            this.Closing += new ManWinCls<LoginInputWin>().WinClose;

        }


        //鼠标单击TexBox
        private void LUID_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           AllTrigEvent<LoginInputWin>.TexInputEve(sender,e);

        }

        private void LPASS_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           AllTrigEvent<LoginInputWin>.TexInputEve(sender,e);

        }

        //登陆页忘记密码按钮
        private void FORGETPASS_Click(object sender, RoutedEventArgs e)
        {
           AllTrigEvent<LoginInputWin>.ButtAllClickEve(sender);

        }


        public delegate void NewTaskDelegate(object sender);
        //登陆页面的立即注册按钮
        private void RegButt_Click(object sender, RoutedEventArgs e)
        {
              AllTrigEvent<LoginInputWin>.ButtAllClickEve(sender);
        }

        //登陆页面的立即登陆按钮
        private void LOG_BUTT_Click(object sender, RoutedEventArgs e)
        {
           AllTrigEvent<LoginInputWin>.ButtAllClickEve(sender);
        }

        private void LUID_KeyDown(object sender, KeyEventArgs e)
        {

            AllTrigEvent<LoginInputWin>.KeyboardEve(sender, e);

        }

        //protected override void OnClosing(object sender,CancelEventArgs e)
        //{
        //    AllTrigEvent<LoginInputWin>.WinCloseEve(this, e);
        //}

        private void LoginInputWinForm_Closing(object sender, CancelEventArgs e)
        {
            AllTrigEvent<LoginInputWin>.WinCloseEve(this, e);
        }
    }
}
