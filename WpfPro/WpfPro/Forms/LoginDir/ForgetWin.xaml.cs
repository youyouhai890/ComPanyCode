using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfPro.Controls;
using WpfPro.ManageAllCls;

namespace WpfPro.Forms.LoginDir
{
    /// <summary>
    /// ForgetWin.xaml 的交互逻辑
    /// </summary>
    public partial class ForgetWin : Window
    {
        public ForgetWin()
        {
            InitializeComponent();
            ManWinCls<ForgetWin>.AddWin(this.Name , this);   //添加管理的窗口

            //关闭当前窗口,在属性窗口那里貌似找不到这事件，但是可以手动注册
            //this.Closing += new ManWinCls<ForgetWin>().WinClose;
        }

        //忘记密码页面的验证码按钮
        private void RGETVEF_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ForgetWin>.ButtAllClickEve(sender);
        }


        //忘记页面的输入框
        private void FPHONO_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ForgetWin>.TexInputEve(sender,e);
        }

        //忘记页面的设置新密码输入框
        private void FNEWPASS_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ForgetWin>.TexInputEve(sender,e);
        }

        private void FVEfCODE_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ForgetWin>.TexInputEve(sender,e);
        }

        //忘记页面的确定按钮
        private void FSOURBUTT_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ForgetWin>.ButtAllClickEve(sender);
        }

        private void FPHONO_KeyDown(object sender, KeyEventArgs e)
        {
            AllTrigEvent<LoginInputWin>.KeyboardEve(sender, e);
        }

        private void FVEfCODE_KeyDown(object sender, KeyEventArgs e)
        {
            AllTrigEvent<LoginInputWin>.KeyboardEve(sender, e);

        }

        private void FRETURN_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ForgetWin>.ButtAllClickEve(sender);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            AllTrigEvent<ForgetWin>.WinCloseEve(this, e);
        }


    }
}
