using System;
using System.Collections.Generic;
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
using WpfPro.HttpJsons;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;

namespace WpfPro.Forms.Products
{


    public partial class ProductsListWin : Window
    {
        //异步更新,线程通信
        public  static SynchronizationContext sync;
        public ProductsListWin()
        {

            InitializeComponent();

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

        
        //单击ListView(商品列表)某行内容获取数据
        private void PlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count>0 && MyInfo.GetInstance.GoodsFlg) // 这个逻辑可以防止第一次加载页面时触发SelectionChanged事件
            {

               AllTrigEvent<ProductsListWin>.SelectContentAllClickEve(sender);
            }

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
    }
}
