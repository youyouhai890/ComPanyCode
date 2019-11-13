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
                    string uid = MyInfo<object>.GetInstance.UID;   //获取登陆后的UID

                    //接口,方法,参数
                    AbsInterfaces<ProdDataModel>.AppInfFun(HttpInterf.ShangPinLieBiao,
                        ButtLogic<ProductsListWin>.ShowGoodsListSuccLogic ,
                    "", "1011", "1", "1", "15", MyInfo<object>.GetInstance.UID);


                };
                Wind.Dispatcher.BeginInvoke(action1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //产品列表显示逻辑
        public static void LoadLocDataLogic(object win)
        {
            try
            {
                ProductsListWin Wind = (ProductsListWin)win;
                Action action1 = () =>      //匿名方法
                {
                    //获取本地数据
                    Wind.AddListView.ItemsSource = MyInfo<object>.GetInstance.GenfaList;  //跟发列表显示
                    Wind.WClistView.ItemsSource = MyInfo<object>.GetInstance.WeChatList;  //微信群列表显示

                };
                Wind.Dispatcher.BeginInvoke(action1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        
        //模版配置
        public static void ReadTemplLogic(object obj) {

            ProductsListWin pwin = (ProductsListWin)obj;
            try
            {
                Action action1 = () =>      //匿名方法
                {
                    //获取配置文件路径
                    string PathConfig = PathTools.DebugConf;
                    string TextCont1 = string.Empty; //存储读取后的内容
                    string TempPath1 = PathConfig + "Template_1.txt";
                    //获取模板内容,如果模板不存在则重新创建
                    TextCont1 = IOTools.WRTemp(TempPath1, TextCont1);
                    //往模板里1显示内容
                    //ShowTools.ShowRichTextBox(pwin.PrichTextBox, TextCont1);
                    ShowTools.ShowTextBox(pwin.StextBox2, TextCont1);        //显示到TexBox



                    string TextCont2 = string.Empty; //存储读取后的内容
                    string TempPath2 = PathConfig + "Template_2.txt";
                    //获取模板内容,如果模板不存在则重新创建
                    TextCont2 = IOTools.WRTemp(TempPath2, TextCont2);
                    //往模板2里显示内容
                    //ShowTools.ShowRichTextBox(pwin.PrichTextBox2, TextCont2);
                    ShowTools.ShowTextBox(pwin.StextBox3, TextCont2);        //显示到TexBox
                };
                pwin.Dispatcher.BeginInvoke(action1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }







    }
}
