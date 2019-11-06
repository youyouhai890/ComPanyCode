using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfPro.Forms.LoginDir;
using WpfPro.Controls;
using WpfPro.ManageAllCls;
using System.Threading;
using System.Windows.Threading;
using WpfPro.Forms.Products;
using System.Windows.Interop;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.HttpJsons;
using System.ComponentModel;

namespace WpfPro.Controls
{
    /// <summary>
    /// 所有的事件处理类,不处理逻辑
    /// </summary>
    class AllTrigEvent<T> where T : Window
    {

        //鼠标点击textbox的事件
        public static void TexInputEve(object sender,MouseButtonEventArgs e)
        {

            ButtLogic<T>.InputContLogic( sender,e);    //实际处理的逻辑

            //LoginInputWin current = (LoginInputWin)App.Current.Windows[0];//获取当前窗口的控制权
        }


        //键盘按钮事件
        public static void KeyboardEve(object sender, KeyEventArgs e)
        {
            TextBox kb = sender as TextBox;

            if (kb.Name == "LUID")
            {
                Window targetWindow = Window.GetWindow(kb); //通过控件找窗体
                LoginInputWin lgw = (LoginInputWin)targetWindow;		//窗口类型转换
                lgw.LPASS.Text = "";
            }
            else if(kb.Name == "RUID")
            {
                Window targetWindow = Window.GetWindow(kb); //通过控件找窗体
                RegWin rw = (RegWin)targetWindow;		//窗口类型转换
                rw.RUPASS.Text = "";
            }
            else if (kb.Name == "RUPASS")
            {
                Window targetWindow = Window.GetWindow(kb); //通过控件找窗体
                RegWin rw = (RegWin)targetWindow;		//窗口类型转换
                rw.RVEFTCHATXT.Text = "";
            }
            else if (kb.Name == "FPHONO")
            {
                Window targetWindow = Window.GetWindow(kb); //通过控件找窗体
                ForgetWin fw = (ForgetWin)targetWindow;		//窗口类型转换
                fw.FVEfCODE.Text = "";
            }
            else if (kb.Name == "FVEfCODE")
            {
                Window targetWindow = Window.GetWindow(kb); //通过控件找窗体
                ForgetWin fw = (ForgetWin)targetWindow;		//窗口类型转换
                fw.FNEWPASS.Text = "";
            }
        }


        #region 所有按钮事件路由
        //处理按钮单击事件
        public static void ButtAllClickEve(object sender)
        {
            Button CurrObj = sender as Button;

            try
            {
                if (CurrObj.Name.Trim() == "RegButt")    //登录窗口的立即注册按钮
                {

                    //启动线程,参数为带参数的方法和,当前控件, 方法的参数必须为object
                    ThreadCls<T>.ThreadFunStart(ButtLogic<T>.RegistLogic, CurrObj);

                    //同步时处理的逻辑
                    //ButtLogic<T>.RegistLogic((LoginInputWin)ManWinCls<LoginInputWin>.GetWin("LoginInputWin"));    //处理的逻辑

                }
                else if (CurrObj.Name.Trim() == "RGETVEF") //注册窗口的获取验证码按钮
                {
                    ButtLogic<T>.RegtVefLogic((RegWin)ManWinCls<RegWin>.GetWin("RegWin"));
                }
                else if (CurrObj.Name.Trim() == "RREG_BUTT") //注册窗口的立即注册按钮
                {

                    ButtLogic<T>.RReg_ButtLogic((RegWin)ManWinCls<RegWin>.GetWin("RegWin"));
                }
                else if (CurrObj.Name.Trim() == "LFORGETPASS") //登陆页忘记密码按钮
                {
                    //启动线程,参数为带参数的方法和,当前控件, 方法的参数必须为object
                    ThreadCls<T>.ThreadFunStart(ButtLogic<T>.LForgetPassLogic, CurrObj);

                    //ButtLogic<T>.LForgetPassLogic((LoginInputWin)ManWinCls<LoginInputWin>.GetWin("LoginInputWin"));
                }
                else if (CurrObj.Name.Trim() == "LOG_BUTT") //登陆页立即登陆按钮
                {
                    //ButtLogic<T>.Log_ButtLogic((LoginInputWin)ManWinCls<LoginInputWin>.GetWin("LoginInputWin"));

                    ButtLogic<T>.Log_ButtLogic(CurrObj);
                }
                else if (CurrObj.Name.Trim() == "FRGETVEF") //忘记密码页面的验证码按钮逻辑
                {
                    ButtLogic<T>.FRgetvef((ForgetWin)ManWinCls<ForgetWin>.GetWin("ForgetWin"));
                }
                else if (CurrObj.Name.Trim() == "FSOURBUTT") //忘记密码页面的确定按钮逻辑
                {
                    ButtLogic<T>.FSourButt((ForgetWin)ManWinCls<ForgetWin>.GetWin("ForgetWin"));
                }
                else if (CurrObj.Name.Trim() == "FRETURN") //忘记密码页面的确定按钮逻辑
                {
                    ButtLogic<T>.FreTurnLogic(CurrObj);
                }
                else if (CurrObj.Name.Trim() == "Sbutton1") //软件设置界面 确定按钮(PID设置,推广位)
                {
                    ButtLogic<T>.SbuttPidLogic(CurrObj);
                }
                else if (CurrObj.Name.Trim() == "Sbutton2") //
                {
                    ButtLogic<T>.ShouQuan();        //授权逻辑
                }
                else if (CurrObj.Name.Trim() == "Tbutton") //转链界面按钮,复制转链结果按钮
                {
                    ButtLogic<T>.CopyResultLogic(CurrObj);       
                }
                else if (CurrObj.Name.Trim() == "Tbutton2") //转链界面的,添加到跟发按钮
                {
                    ButtLogic<T>.AddToWithLinkLogic(CurrObj);

                }
                else if (CurrObj.Name.Trim() == "Abutton2") //默认模板1
                {
                    ButtLogic<T>.TemplDefineLogic(CurrObj,"1");
                }
                else if (CurrObj.Name.Trim() == "Abutton4") //默认模板2
                {
                    ButtLogic<T>.TemplDefineLogic(CurrObj, "2");
                }
                else if (CurrObj.Name.Trim() == "Abutton3") //第一个模板的保存按钮
                {
                    ButtLogic<T>.SaveTemplLogic(CurrObj, "1");
                }
                else if (CurrObj.Name.Trim() == "Abutton5") //第二个模板的保存按钮
                {
                    ButtLogic<T>.SaveTemplLogic(CurrObj, "2");
                }
                else if (CurrObj.Name.Trim() == "Pbutton") //商品列表搜索你牛
                {
                    ButtLogic<T>.SearchGoodsLogic(CurrObj);
                }
                else if (CurrObj.Name.Trim() == "LButton1") //商品列表,刷新功能
                {
                    ButtLogic<T>.VListRefreshLogic(CurrObj);
                }



                else
                {
                    MessageBox.Show("找不到匹配项....");
                }



                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion


 #region 处理所有列表里的子项按钮
        public static void MenuItemAllClickEve(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem CurrObj = sender as MenuItem;

                    if (CurrObj.Name.Trim() == "AddGenFa") //右键添加跟发
                    {
                        ThreadCls<ProductsListWin>.ThreadFunStart(ButtLogic<T>.AddGenFaLogic, CurrObj);
                         // ThreadCls<ProductsListWin>.SyncFun(ButtLogic<T>.AddGenFaLogic, CurrObj);
                        //ButtLogic<T>.AddGenFaLogic(CurrObj);
                    }
                    else if (CurrObj.Name.Trim() == "FlagYiFa") //右键标记已发
                    {
                        ButtLogic<T>.FlagYiFaLogic(CurrObj);
                    }

                    else
                    {
                        MessageBox.Show("找不到匹配项....");
                    }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

#endregion



#region 处理所有鼠标路由
        public static void MouseAllClickEve(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) //如果是双击
            {
                //双击商品列表里的某行
                ButtLogic<T>.ListViewLogic(sender);    //显示双击信息
            }
            else
            {

                ButtLogic<T>.GetCurrObjLogic(sender,e);    //鼠标任意键获取对象

            }

        }

#endregion



        //窗口加载时的路由
        public static void LoadedWinEve(object sender)
        {
            T win = sender as T;
            try
            {
                if (win.Name == "ProdWinForm")
                {
                    //启动线程,商品列表显示,当前控件 , 方法的参数必须为object
                    ThreadCls<T>.ThreadFunStart(WinLoadedLogic<T>.ProLiewViewLogic, sender);
                    WinLoadedLogic<T>.ReadTemplLogic(sender); //读取模版的处理逻辑
                }
                else
                {
                    MessageBox.Show("找不到匹配的窗口.....");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString());
            }


        }

        //窗口关闭事件
        public static void WinCloseEve(object sender, CancelEventArgs e)
        {
            T win = sender as T;
            try
            {
                if (win.Name == "RegWinForm" )   //注册窗口
                {
                   ManWinCls<RegWin>.CloseWin(win, e);
                    return;
                }
                else if (win.Name == "ForWinForm")   //注册窗口
                {
                    ManWinCls<ForgetWin>.CloseWin(win, e);
                    return;
                }
                else if (win.Name == "LoginInputWinForm")   //注册窗口
                {
                    ManWinCls<LoginInputWin>.CloseWin(win, e);
                    return;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }



    
}
