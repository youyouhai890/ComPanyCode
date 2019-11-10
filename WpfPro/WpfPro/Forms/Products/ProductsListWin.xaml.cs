using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfPro.Controls;
using WpfPro.HttpJsons;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;

namespace WpfPro.Forms.Products
{


    public partial class ProductsListWin : Window
    {



        public static bool RightButt = false;
        //异步更新,线程通信
        public  static SynchronizationContext sync;
        public ProductsListWin()
        {

            InitializeComponent();
            //为指定的路由事件添加路由事件处理程序，并将该处理程序添加到当前元素的处理程序集合中。
            //将 handledEventsToo 指定为 true 时，可为已标记为由其他元素在事件路由过程中处理的路由事件调用所提供的处理程序。

            //必须得是主线程才有值,并且必须在InitializeComponent后面
            sync = SynchronizationContext.Current;

            ManWinCls<ProductsListWin>.AddWin(this.Name, this);   //添加管理的窗口

            //关闭当前窗口,在属性窗口那里貌似找不到这事件，但是可以手动注册
            this.Closing += new ManWinCls<ProductsListWin>().WinClose;

        }


        //加载窗口时触发的函数
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //路由所有窗口加载的事件
            AllTrigEvent<ProductsListWin>.LoadedWinEve(sender);
        }
        //确定按钮 , PID 设置(推广位)
        private void Sbutton1_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);
        }


        //软件设置 , 授权按按钮
        private void Sbutton2_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);

        }

        

        private void Abutton2_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);
        }

        private void Abutton4_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);

        }

        private void Abutton3_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);

        }

        private void Abutton5_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);
        }

        //商品列表搜索
        private void Pbutton_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);
        }

        private void LButton1_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);

        }

        //爆款鼠标任意键单击时只获取当前对象
        private void PlistView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MouseAllClickEve(sender,e);
        }

        //爆款双击商品列表里的某行获取信息
        private void PlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MouseAllClickEve(sender, e);
        }

        //右键爆款菜单里的添加根发按钮
        private void AddGenFa_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("添加到跟发");
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);
        }
        //右键菜单里的标记已发按钮
        private void FlagYiFa_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("标记已发");
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender,e);
        }


        //群发列表里的右键
        private void AddListView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MouseAllClickEve(sender, e);

        }

        //群发右键置顶
        private void ZhiDing_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);

        }
        //群发右键上移
        private void ShangYi_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);

        }
        //群发右键下移
        private void XiaYi_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);

        }
        //群发右键查看跟发内容(双击)
        private void ChaKanNeiRong_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);

        }
        //群发右键删除跟发内容
        private void ShanChuGenFa_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);

        }
        //群发右键发送选择内容
        private void FaSongNeiRong_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.MenuItemAllClickEve(sender, e);

        }

        //微信群输入框
        private void MATextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.TexInputEve(sender, e);
        }

        //添加微信群按钮
        private void MAButton_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);
        }

        //群发助手,开始发送
        private void MAButt1_Click(object sender, RoutedEventArgs e)
        {

            AllTrigEvent<ProductsListWin>.ButtAllClickEve(sender);

        }
    }
}
