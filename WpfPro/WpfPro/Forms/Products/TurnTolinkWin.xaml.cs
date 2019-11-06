using System;
using System.Collections.Generic;
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

namespace WpfPro.Forms.Products
{
    /// <summary>
    /// AuthoWin.xaml 的交互逻辑
    /// </summary>
    public partial class TurnTolinkWin : Window
    {
        public TurnTolinkWin()
        {
            InitializeComponent();
        }

        //转链界面复制按钮

        private void Tbutton_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<TurnTolinkWin>.ButtAllClickEve(sender);

        }


        //添加跟发按钮
        private void Tbutton2_Click(object sender, RoutedEventArgs e)
        {
            AllTrigEvent<TurnTolinkWin>.ButtAllClickEve(sender);

        }
    }
}
