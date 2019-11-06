using System;
using System.Collections.Generic;
using System.Windows;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;
using WpfPro.ToolsCls;
using static WpfPro.ManageAllCls.ManaEnumCls;

namespace WpfPro.Controls
{
    class WinLoadedLogic<T> where T : Window
    {

        //产品列表显示逻辑
        public static void ProLiewViewLogic(object win)
        {
            try
            {
                ProductsListWin Wind = (ProductsListWin)win;
                Action action1 = () =>      //匿名方法
                {
                    string uid = MyInfo.GetInstance.UID;   //获取登陆后的UID

                    //接口,方法,参数
                    AbsInterfaces<ProdDataModel>.AppInfFun(HttpInterf.ShangPinLieBiao,
                        ButtLogic<ProductsListWin>.ShowGoodsListSuccLogic ,
                    "", "1011", "1", "2", "15", MyInfo.GetInstance.UID);

                    //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                    //string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.ShangPinLieBiao,
                    //   keyword, "1011", "1", "2", "15", uid);

                    //字符串反序列化,泛型为Data类型
                    //  ProdJsonObj<ProdDataModel> model = SerializationTools<ProdJsonObj<ProdDataModel>>.RevJsonObj(result);
                    //MLoginJson<ProdDataModel> model = SerializationTools<MLoginJson<ProdDataModel>>.RevJsonObj(result);

                    //if (model.code == 200)  //200为成功
                    //{
                    //   // MessageBox.Show(model.message);

                    //    MyInfo.GetInstance.PRODLISTLIEW = model.data.list;      //保存商品列表

                    //    List<ProdListModel> viewList = model.data.list;

                    //    Wind.PlistView.ItemsSource = viewList;       //显示到ListView

                    //}
                    //else if (model.code == 500) //设置PID
                    //{

                    //    MessageBox.Show("自动跳转到设置PID的页面.");
                    //    Wind.TabItem2.IsSelected = true;    //选择第二个子菜单
                    //}
                    //else
                    //{
                    //    MessageBox.Show(model.message);
                    //    return;
                    //}
                };
                Wind.Dispatcher.BeginInvoke(action1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

      

        public static void ReadTemplLogic(object obj) {

            //获取配置文件路径
            string PathConfig = PathTools.DebugConf;

            ProductsListWin pwin = obj as ProductsListWin;

            string TextCont1 = string.Empty; //存储读取后的内容
            //模板路径
            string TempPath1 = PathConfig + "Template_1.txt";
            //获取模板内容,如果模板不存在则重新创建
            TextCont1 = IOTools.WRTemp(TempPath1, TextCont1);
            //往模板里1显示内容
            //ShowTools.ShowRichTextBox(pwin.PrichTextBox, TextCont1);
            ShowTools.ShowTextBox(pwin.StextBox2,TextCont1);        //显示到TexBox

            string TextCont2 = string.Empty; //存储读取后的内容
            //模板路径
            string TempPath2 = PathConfig + "Template_2.txt";
            //获取模板内容,如果模板不存在则重新创建
            TextCont2 = IOTools.WRTemp(TempPath2, TextCont2);

            //往模板2里显示内容
            //ShowTools.ShowRichTextBox(pwin.PrichTextBox2, TextCont2);
            ShowTools.ShowTextBox(pwin.StextBox3, TextCont2);        //显示到TexBox


        }







    }
}
