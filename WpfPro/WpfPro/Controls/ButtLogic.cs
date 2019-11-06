using Gecko;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfPro.Forms.LoginDir;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;
using WpfPro.ToolsCls;
using static WpfPro.ManageAllCls.ManaEnumCls;
using static WpfPro.ToolsCls.WinHandle;

namespace WpfPro.Controls
{

    /// <summary>
    /// 这个类只处理逻辑
    /// </summary>
    class ButtLogic<T> : HttpJsons.AbsInterfaces<T>
    {

        public static void InputContLogic(object obj, MouseButtonEventArgs e)
        {
            TextBox CurrObj = (TextBox)obj;

            if (CurrObj.Name.Trim() == "LUID" || CurrObj.Name.Trim() == "LPASS"
                || CurrObj.Name.Trim() == "RUID" || CurrObj.Name.Trim() == "RUPASS"
                 || CurrObj.Name.Trim() == "RVEFTCHATXT" || CurrObj.Name.Trim() == "FPHONO"
                  || CurrObj.Name.Trim() == "FNEWPASS" || CurrObj.Name.Trim() == "FVEfCODE")
            {

                CurrObj.Text = "";

            }
        }



        //登录页立即注册按钮逻辑(只是用于进入注册页面)
        public static void RegistLogic(object but)  //LoginInputWin
        {

            Button Butt = (Button)but;
            Action action1 = () =>      //匿名方法
            {
                //创建或显示窗口
                ManWinCls<RegWin>.OpenOrCreatWin("RegWinForm");
            };
            Butt.Dispatcher.BeginInvoke(action1);

        }


        //注册页获取验证码按钮的逻辑
        public static void RegtVefLogic(RegWin rgw)
        {

            // 1.窗体句柄(编号)：
            // IntPtr hwnd = new WindowInteropHelper(窗体实例化的对象).Handle;
            //2.控件句柄(编号)：
            //IntPtr hwnd2 = ((HwndSource)PresentationSource.FromVisual(ManWinCls<window>.GetWin("RegWin"))).Handle;

            //RegWin rgw = (RegWin)ManWinCls<window>.GetWin("RegWin");
            string PhoneId = rgw.RUID.Text.Trim();

            //MessageBox.Show(rgw.ToString());
            if (!(RegularTools.IsAccountPhone(PhoneId)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("电话号码输入不正确..");
                return;
            }

            // string str = HttpJsons.AllInterfaceCls.YanZhengMa;
            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, HttpInterf.YanZhengMa, PhoneId);
            //字符串反序列化
            MLoginJson<string> model = SerializationTools<MLoginJson<string>>.RevJsonObj(result);

            if (model.code == 200)
            {
                MessageBox.Show(model.message);
            }
            else
            {
                MessageBox.Show(model.message);

            }

            return;

        }



        //忘记密码页面验证码按钮逻辑
        public static void FRgetvef(ForgetWin fw) {     //在参数里直接类型转换

            string PhoneId = fw.FPHONO.Text.Trim();

            //MessageBox.Show(rgw.ToString());

            if (!(RegularTools.IsAccountPhone(PhoneId)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("电话号码输入不正确..");
                return;
            }

            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.YanZhengMa, PhoneId);

            //字符串反序列化
            MLoginJson<string> model = SerializationTools<MLoginJson<string>>.RevJsonObj(result);

            if (model.code == 200)
            {
                MessageBox.Show(model.message);

            }
            else
            {
                MessageBox.Show(model.message);
            }

        }

        //忘记密码返回登陆按钮
        public static void FreTurnLogic(object obj)     //注册的逻辑
        {
            Button but = obj as Button;
            Window targetWindow = Window.GetWindow(but);
            ForgetWin fg = (ForgetWin)targetWindow;

            if (fg.Visibility == Visibility.Visible)
            {
                fg.Visibility = Visibility.Hidden;
            }

        }


        //忘记密码页面的确定按钮逻辑
        public static void FSourButt(ForgetWin Fwin)
        {
            ForgetWin fw = Fwin;
            string VerifCode = fw.FVEfCODE.Text.Trim();   //验证码
            string PhoneId = fw.FPHONO.Text.Trim();       //注册页的手机号
            string NewPass = fw.FNEWPASS.Text.Trim();  //要设置的新密码


            //MessageBox.Show(rgw.ToString());


            if ((VerifCode == string.Empty || VerifCode == null) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("请输入验证码");
                return;
            }

            if (!(RegularTools.IsAccountPhone(PhoneId)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("电话号码输入不正确..");
                return;
            }


            if (!(RegularTools.IsPassword(NewPass)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("密码不匹配,请输入6到15位的英文字符或数字");
                return;
            }


            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.MiMaChongZhi,
                PhoneId, NewPass, VerifCode);


            //字符串反序列化
            MLoginJson<string> model = SerializationTools<MLoginJson<string>>.RevJsonObj(result);

            if (model.code == 200)
            {
                MessageBox.Show(model.message);
            }
            else
            {
                MessageBox.Show(model.message);
            }


        }


        public static void RReg_ButtLogic(RegWin rgw)     //注册的逻辑
        {
            string PhoneId = rgw.RUID.Text.Trim();       //注册页的手机号
            string Pwd = rgw.RUPASS.Text.Trim();     //注册页的密码
            string YanZheng = rgw.RVEFTCHATXT.Text.Trim();     //注册页的密码

            //MessageBox.Show(rgw.ToString());

            if (!(RegularTools.IsAccountPhone(PhoneId)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("电话号码输入不正确..");
                return;
            }

            if (!(RegularTools.IsPassword(Pwd)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("密码不匹配,请输入6到15位的英文字符或数字");
                return;
            }

            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.ZhuCe, PhoneId, Pwd, YanZheng);

            //字符串反序列化
            MLoginJson<string> model = SerializationTools<MLoginJson<string>>.RevJsonObj(result);

            if (model.code == 200)  //200为成功
            {

                MessageBox.Show(model.message);
            }
            else
            {
                MessageBox.Show(model.message);
            }

        }

        //登录页忘记密码按钮
        public static void LForgetPassLogic(object but)
        {

            Button Butt = (Button)but;
            Action action1 = () =>      //匿名方法
            {
                //  ForgetWin frw = new ForgetWin();

                ManWinCls<ForgetWin>.OpenOrCreatWin("ForWinForm");

            };
            Butt.Dispatcher.BeginInvoke(action1);	//再次开启异步委托的线程

        }



        //登陆页立即登陆按钮逻辑
        public static void Log_ButtLogic(Button CurrBut)
        {


            Window targetWindow = Window.GetWindow(CurrBut);
            LoginInputWin lgw = (LoginInputWin)targetWindow;

            //LoginInputWin lgw = Lwin;
            string PhoneId = lgw.LUID.Text.Trim();       //登录页账号
            string Pwd = lgw.LPASS.Text.Trim();     //登陆页密码


            if (!(RegularTools.IsAccountPhone(PhoneId)) && (AllInterfaceCls.TestFlag.Trim() != "true"))//不是测试的时候执行
            {
                MessageBox.Show("电话号码输入不正确..");
                return;
            }

            if (!(RegularTools.IsPassword(Pwd)) && (AllInterfaceCls.TestFlag.Trim() != "true"))
            {
                MessageBox.Show("密码不匹配,请输入6到15位的英文字符或数字");
                return;
            }


            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.DengLu, PhoneId, Pwd);



            //字符串反序列化
            MLoginJson<string> model = SerializationTools<MLoginJson<string>>.RevJsonObj(result);

            if (model.code == 200)   //200代表登陆成功
            {

                //保存个人账号
                MyInfo.GetInstance.PHONE = PhoneId;

                ManWinCls<LoginInputWin>.HideWin(lgw);
                MyInfo.GetInstance.UID = model.data;  //保存UID

                string uid = ManageAllCls.MyInfo.GetInstance.UID;

                //查询PID
                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                string result2 = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.HuoQuPID, uid);

                //字符串反序列化
                MLoginJson<string> model2 = SerializationTools<MLoginJson<string>>.RevJsonObj(result2);


                if (model2.code == 200)      //成功
                {
                    MyInfo.GetInstance.PID = model2.data;  //保存PID
                }
                else
                {
                    MessageBox.Show(model2.message);
                }
                //显示对应窗口
                ProductsListWin plw = new ProductsListWin();
                plw.StextBox.Text = MyInfo.GetInstance.PID;
                ManWinCls<ProductsListWin>.AppShowRun(plw);    //用app方式打开窗口,主窗口启动

                return;


                // rgw.Show();   //非模态
                //plw.ShowDialog();   //模态方式显示 , 必须相应当前窗口
            }
            else
            {
                MessageBox.Show(model.message.Trim());
            }
            return;
        }



        //授权逻辑
        public static void ShouQuan()
        {

            string pid = MyInfo.GetInstance.PID;

            string AuthoPid = RegularTools.IsPid(pid);  //截取PID的第一个字符

            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.SouQuan, AuthoPid + "@" + AuthoPid);

            // 显示授权网页
            ManageAllCls.ManWinCls<ProductsListWin>.FixWebPage(result);

        }




        //软件设置里面设置PID的逻辑
        public static void SbuttPidLogic(Button CurrButt)
        {
            if (CurrButt != null)
            {

                ProductsListWin pwin = (ProductsListWin)ManWinCls<ProductsListWin>.GetWin("ProductsListWin");

                //  MessageBox.Show(VisualTreeHelper.GetParent(CurrButt).ToString()); //获取父对象
                //string uid = "8a2efddc6db8c7cb016db9627fed07ec";
                string uid = MyInfo.GetInstance.UID;

                string pid = pwin.StextBox.Text.Trim();

                if (pid == null || pid == "")
                {
                    System.Windows.MessageBox.Show("请输入PID...");
                    return;
                }


                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.SheZhiPid, uid, pid);


                //字符串反序列化
                MLoginJson<string> model = SerializationTools<MLoginJson<string>>.RevJsonObj(result);

                if (model.code == 200)  //200为成功
                {

                    System.Windows.Forms.MessageBox.Show(model.message);
                }
                else
                {
                    MessageBox.Show(model.message);

                }


            }
            else
            {
                MessageBox.Show("获取不到当前控件信息");
            }


        }


        //点击商品列表(ListView)上获取某行值的逻辑,成功调用转链接口
        public static void ListViewLogic(object sender)
        {
            ListView lv = sender as ListView;
            ProdListModel emp = lv.SelectedItem as ProdListModel;

            string uid = MyInfo.GetInstance.UID;
            if (emp != null && emp is ProdListModel)
            {

                //转链接口,参数1 是否测试 , 参数2 接口名,从第三个开始参数
                string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.ZhuanLianJieKou,
                uid, emp.auctionId, emp.activityId, emp.couponAmount.ToString().Trim(), emp.pictUrl, emp.title, emp.zkPrice);

                //字符串反序列化,获取TurnDataModel
                MLoginJson<TurnDataModel> model = SerializationTools<MLoginJson<TurnDataModel>>.RevJsonObj(result);


                if (model.code == 200)       //授权成功状态
                {
                    //MessageBox.Show("成功授权"),第一个为视图显示的列,第二个为转链后获取的对象
                    TurnToCopyLogic(lv, emp, model.data); //成功授权才能,转链窗口
                }
                else if (model.code == 3023)        //授权过期
                {
                    MessageBox.Show(model.message.Trim());
                    ShouQuan(); //授权
                }
                else
                {
                    MessageBox.Show(model.message.Trim());
                }

            }



        }

        //转链界面的功能
        public static void TurnToCopyLogic(ListView sender, ProdListModel obj, TurnDataModel tdm)
        {
            ProdListModel plm = obj;     //商品列表点击的对象
            TurnTolinkWin ttw = new TurnTolinkWin();

            ttw.Timage.Source = new BitmapImage(new Uri(plm.pictUrl));    //加载图片

            //获取默认模版内容
            string TempCont = GetOrSetTools<ProductsListWin>.GetDefTemp(sender).Trim();

            //处理转链后的数据(替换占位符) , 第一个为替换的模版内容 ,  参数2替换的对象
            TempCont = GetOrSetTools<ProductsListWin>.GetAssocaObj(TempCont, tdm);
            //重载方法,在遍历ProdListModel对象
            TempCont = GetOrSetTools<ProductsListWin>.GetAssocaObj(TempCont, plm);

            //显示信息到转链的面板上
            ShowTools.ShowTextBox(ttw.TtextBox, TempCont);

            ManWinCls<TurnTolinkWin>.ShowDialogWin(ttw);   //显示窗口

        }




        //转链界面的复制按钮逻辑
        public static void CopyResultLogic(object obj)
        {

            //获取当前控件的窗体
            TurnTolinkWin ttw = GetOrSetTools<TurnTolinkWin>.GetParentWin(obj);

            IDataObject dataobj = new DataObject();

            //String img = (ttw.Timage.Source as BitmapImage).UriSource.OriginalString;//获取图片URI地址
            BitmapImage img = (BitmapImage)ttw.Timage.Source;   //获取图片
            string TextContent = ttw.TtextBox.Text;

            dataobj.SetData(DataFormats.Bitmap, img);
            dataobj.SetData(DataFormats.Text, TextContent);
            //清空剪贴板
            //Clipboard.Clear();
            //拷贝到剪贴板
            //Clipboard.SetDataObject(TextContent, true);
            Clipboard.SetDataObject(dataobj);

        }



        //转链界面的添加到跟发按钮逻辑
        public static void AddToWithLinkLogic(object obj)
        {
            //获取当前控件的窗体
            TurnTolinkWin ttw = GetOrSetTools<TurnTolinkWin>.GetParentWin(obj);

            Hashtable WinHwnd = new Hashtable();



            //获取窗口句柄
            //WinHandle.GetHandle("A_엄마");
            // WindowInfo[] winNam =  WinHandle.GetAllDesktopWindows();
            WinHwnd = GetAllDesktopWindows();

            // MessageBox.Show(WinHwnd.ContainsValue("微信").ToString());

            string va = string.Empty;
            string hwnd = string.Empty;
            foreach (DictionaryEntry di in WinHwnd)     //遍历类型转换
            {

                if (di.Value.ToString() == "A_엄마")
                {
                    hwnd = di.Key.ToString();
                    // MessageBox.Show((string)WinHwnd[va]);
                    MessageBox.Show(di.Key.ToString());
                    break;
                }
            }



            int j = 0;
            while (j++<20)
            {

                    Thread.Sleep(TimeSpan.FromMilliseconds(50));    //应该是间隔时间
                WeChatMainWinMsgSend("2222222222", hwnd);

               
            }


        }





        //    public static void SendKey(string name, string l)
        //{
        //   // var win =Program.FindWindow(null, name);
        //    IntPtr win = Marshal.StringToHGlobalAnsi(name);

        //        Program.keybd_event(0x01, 0, 0, 0);//激活TIM
        //        Program.PostMessage(win, 0x0302, 0, 0);
        //        //    PostMessage(win, 0x0101, new Random().Next(65,128),0);//发送字符                                              //下面是发送回车
        //        Program.PostMessage(win, 0x0100, 13, 0);
        //        Program.PostMessage(win, 0x0101, 13, 0);
        //        Program.keybd_event(0x11, 0, 0x0002, 0);

        //}





        //第一个默认模板按钮
        public static void TemplDefineLogic(object obj,object TemplId)
        {
            string DefineId = TemplId as string;

            //获取配置文件路径
            string PathConfig = PathTools.DebugConf;

            if (!Directory.Exists(PathConfig)) //查询目录是否存在
            {
                Directory.CreateDirectory(PathConfig);      //创建目录
            }

            //默认模板文件路径
            string DfiFilePath = PathConfig + "DefineTemplate.txt";
            //写入内容,指定默认模版 1
            IOTools.WriteFile(DfiFilePath, DefineId);
            //保存默认模板信息到我的数据里
            MyInfo.GetInstance.MYTEMPLATE = IOTools.ReadFile(DfiFilePath); 

        }

        //模版保存按钮
        public static void SaveTemplLogic(object obj , object Tex)
        {

            //获取当前控件的窗体
            ProductsListWin lgw = GetOrSetTools<ProductsListWin>.GetParentWin(obj);

            //Button button = obj as Button;
            //Window targetWindow = Window.GetWindow(button); //通过控件找窗体
            //ProductsListWin lgw = (ProductsListWin)targetWindow;		//窗口类型转换

            string TempId = Tex as string;

            //获取配置文件路径
            string PathConfig = PathTools.DebugConf;

            if (!Directory.Exists(PathConfig)) //查询目录是否存在
            {
                Directory.CreateDirectory(PathConfig);      //创建目录
            }

            string TextContent = string.Empty;  //要存储的内容
            string TempPath = string.Empty;  //要存储的路径
            if (TempId == "1")
            {
                 TempPath = PathConfig + "Template_1.txt";  //要保存模板路径
                //读取模版内容
               TextContent= lgw.StextBox2.Text;

                //TextContent = textRange.Text;
            }
            else if (TempId == "2")
            {
                 TempPath = PathConfig + "Template_2.txt";  //要保存模板路径
                //读取模版内容
               TextContent= lgw.StextBox3.Text;
               
            }
            else
            {
                MessageBox.Show("找不到要保存的匹配模版...");
            }

             //要写入的内容
            ToolsCls.IOTools.WriteFile(TempPath, TextContent);

        }



        public static void SearchGoodsLogic(object obj)
        {
            //获取当前控件的窗体
            ProductsListWin pw =  GetOrSetTools<ProductsListWin>.GetParentWin(obj);

            string TexStr = pw.PtextBox.Text;
            if (TexStr != null)
            {
                //调用抽象接口 
                AbsInterfaces<ProdDataModel>.AppInfFun(HttpInterf.ShangPinLieBiao, ShowGoodsListSuccLogic,
                    TexStr, "1011", "1", "2", "15", MyInfo.GetInstance.UID);
            }

        }


        //商品列表刷新按钮
        public static void VListRefreshLogic(object obj)
        {
                //调用抽象接口 
                AbsInterfaces<ProdDataModel>.AppInfFun(HttpInterf.ShangPinLieBiao, ShowGoodsListSuccLogic,
                    "", "1011", "1", "2", "15", MyInfo.GetInstance.UID);
           
        }

       

        //商品列表显示
        public static void ShowGoodsListSuccLogic(MLoginJson<ProdDataModel> model)
        {
            MyInfo.GetInstance.GoodsFlg = false;
            ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm");

            List<ProdListModel> viewList = model.data.list;

            pw.PlistView.ItemsSource = viewList;       //显示到ListView

            MyInfo.GetInstance.GoodsFlg = true;


        }


    }
}
