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
    /// RegWin.xaml 的交互逻辑
    /// </summary>
    public partial class RegWin : Window
    {
        public RegWin()
        {
            InitializeComponent();
            ManWinCls<RegWin>.AddWin(this.Name, this);   //添加管理的窗口

            //关闭当前窗口,在属性窗口那里貌似找不到这事件，但是可以手动注册
           // this.Closing += new ManWinCls<RegWin>().WinClose;

        }

        //获取验证码按钮
        private void RGETVEF_Click(object sender, RoutedEventArgs e)
        {          
            AllTrigEvent<RegWin>.ButtAllClickEve(sender); //处理BUTTON按钮类事件
        }

        private void RUID_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<RegWin>.TexInputEve(sender, e);    //处理类似TexBox控件事件
        }

        private void RUPASS_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<RegWin>.TexInputEve(sender, e);

        }

        private void RVEFTCHATXT_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<RegWin>.TexInputEve(sender, e);
        }

        //注册页面的立即注册按钮
        private void RREG_BUTT_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<RegWin>.ButtAllClickEve(sender);    //处理BUTTON按钮类事件
        }

        private void RUID_KeyDown(object sender, KeyEventArgs e)
        {
            AllTrigEvent<LoginInputWin>.KeyboardEve(sender, e);

        }

        private void RUPASS_KeyDown(object sender, KeyEventArgs e)
        {
            AllTrigEvent<LoginInputWin>.KeyboardEve(sender, e);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            AllTrigEvent<RegWin>.WinCloseEve(this,e);
        }

    }
}
