using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfPro.Forms.LoginDir;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.Local.LocalModel;
using WpfPro.ManageAllCls;
using WpfPro.TestCode;
using WpfPro.ToolsCls;
using WpfPro.HttpJsons.WeChat;
using static WpfPro.ManageAllCls.ManaEnumCls;


namespace WpfPro.Controls
{

    /// <summary>
    /// 这个类只处理逻辑
    /// </summary>
    class ButtLogic<T> where T: class
    {
        private static readonly object ButLockObj = new object();


        public static void InputContLogic(object obj, MouseButtonEventArgs e)
        {
            TextBox CurrObj = (TextBox)obj;

            if (CurrObj.Name.Trim() == "LUID" || CurrObj.Name.Trim() == "LPASS"
                || CurrObj.Name.Trim() == "RUID" || CurrObj.Name.Trim() == "RUPASS"
                 || CurrObj.Name.Trim() == "RVEFTCHATXT" || CurrObj.Name.Trim() == "FPHONO"
                  || CurrObj.Name.Trim() == "FNEWPASS" || CurrObj.Name.Trim() == "FVEfCODE"
                  || CurrObj.Name.Trim() == "MATextBox")
            {

                CurrObj.Text = "";

            }
        }



        //登录页立即注册按钮逻辑(只是用于进入注册页面)
        public static void RegistLogic(object but)  //LoginInputWin
        {

            Button Butt = (Button)but;
            //托管
            ThreadCls<Button>.DelegateBIVKFun(Butt, ManWinCls<RegWin>.OpenOrCreatWin, "RegWinForm");

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

        //注册的逻辑
        public static void RReg_ButtLogic(RegWin rgw)     
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
                MyInfo<object>.GetInstance.PHONE = PhoneId;

                ManWinCls<LoginInputWin>.HideWin("LoginInputWinForm");  //隐藏窗口

                MyInfo<object>.GetInstance.UID = model.data;  //保存UID

                string uid = MyInfo<object>.GetInstance.UID;

                //查询PID
                //参数1 是否测试 , 参数2 接口名,从第三个开始参数
                string result2 = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.HuoQuPID, uid);

                //字符串反序列化
                MLoginJson<string> model2 = SerializationTools<MLoginJson<string>>.RevJsonObj(result2);


                if (model2.code == 200)      //成功
                {
                    MyInfo<object>.GetInstance.PID = model2.data;  //保存PID
                }
                else
                {
                    MessageBox.Show(model2.message);
                }

                
                //显示对应窗口
                ProductsListWin plw = new ProductsListWin();
                plw.StextBox.Text = MyInfo<object>.GetInstance.PID;
                ManWinCls<ProductsListWin>.ShowDialogWin(plw);  //模态启动
                //ManWinCls<ProductsListWin>.ShowWin(plw);

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

            string pid = MyInfo<object>.GetInstance.PID;

            string AuthoPid = RegularTools.IsPid(pid);  //截取PID的第一个字符

            //参数1 是否测试 , 参数2 接口名,从第三个开始参数
            string result = TestCls.FlagTest(AllInterfaceCls.TestFlag, ManaEnumCls.HttpInterf.SouQuan, AuthoPid + "@" + AuthoPid);

            // 显示授权网页
            ManWinCls<ProductsListWin>.FixWebPage(result);

        }




        //软件设置里面设置PID的逻辑
        public static void SbuttPidLogic(Button CurrButt)
        {
            if (CurrButt != null)
            {

                ProductsListWin pwin = (ProductsListWin)ManWinCls<ProductsListWin>.GetWin("ProductsListWin");

                //  MessageBox.Show(VisualTreeHelper.GetParent(CurrButt).ToString()); //获取父对象
                //string uid = "8a2efddc6db8c7cb016db9627fed07ec";
                string uid = MyInfo<object>.GetInstance.UID;

                string pid = pwin.StextBox.Text.Trim();

                if (pid == null || pid == "")
                {
                    MessageBox.Show("请输入PID...");
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



        //双击商品列表(ListView)上某行,成功调用转链接口 ,  参数为控件 , 执行的方法 , 方法的参数
        //public static void ListViewLogic(object sender, Action<ListView , ProdListModel, T> fun)
        public static void ListViewLogic(Action<object[]> fun )
        {
            //TurnOrWeChatFun = Fun;  //要托管的方法
          

            // ListView lv = sender as ListView;
           //  ProdListModel emp = lv.SelectedItem as ProdListModel;   //获取选中对象
            TurnListModel emp = MyInfo<object>.GetInstance.CurrPmObj;        //获取选中对象

            string uid = MyInfo<object>.GetInstance.UID;

            //接口参数
            ParmObj poj = new ParmObj(7);
            poj.ParmStrArray[0] = uid;
            poj.ParmStrArray[1] = emp.auctionId;
            poj.ParmStrArray[2] = emp.activityId;
            poj.ParmStrArray[3] = emp.couponAmount.ToString().Trim();
            poj.ParmStrArray[4] = emp.pictUrl;
            poj.ParmStrArray[5] = emp.title;
            poj.ParmStrArray[6] = emp.zkPrice;
            

            //接口,方法,参数 , 返回从接口获取到的数据模型
            MLoginJson<TurnListModel> model = AbsInterfaces<TurnListModel>.AppInfFun2(HttpInterf.ZhuanLianJieKou,
                                                    poj.ParmStrArray);

            if (emp != null && emp is ProdListModel)
                {
                    if (model.code == 200)       //授权成功状态
                    {

                         //合并数据
                        TurnListModel tlm =  GetOrSetTools<TurnListModel>.ReflectMergeData(model.data, emp);
                        //参数
                        ParmObj pj1 = new ParmObj(1);
                        pj1.ParmArray[0] = tlm;      //TurnListModel对象
                        fun(pj1.ParmArray);
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


        //单击爆款列表时获取当前信息
        // public static void GetCurrObjLogic(object sender, MouseButtonEventArgs e)
        public static void GetCurrObjLogic(object pobjs)
        {
          //  lock (ButLockObj)
          //  {

                 ParmObj pj = pobjs as ParmObj;
                 ListView lv = pj.ParmArray[0] as ListView;
                Action action1 = () =>      //匿名方法
                {
                    MouseButtonEventArgs e = pj.ParmArray[1] as MouseButtonEventArgs;
                    //获取当前控件的窗体
                    Window targetWindow = Window.GetWindow(lv); //通过控件找窗体
                    ProductsListWin plw = targetWindow as ProductsListWin;      //窗体类型转换

                    TurnListModel emp = lv.SelectedItem as TurnListModel;   //获取选中对象(原型数据)
                    MyInfo<object>.GetInstance.CurrPmObj = emp;     //保存当前对象

                    // MessageBox.Show(MyInfo<object>.GetInstance.CurrPmObj.title);   //显示当前对象

                    //屏蔽事件
                    if (e.ChangedButton == MouseButton.Right)   //如果是点击右键
                    {
                        plw.BContMenu.IsOpen = true;            //显示右键菜单
                    }
                    //MessageBox.Show(MyInfo<object>.GetInstance.CurrPmObj.title.ToString());
                };
                lv.Dispatcher.BeginInvoke(action1);

          //  }

        }


        //群发单击鼠标左键或右键时获取当前信息
        public static void ShowRightMenuLogic(object sender, MouseButtonEventArgs e)
        {
            ListView lv = sender as ListView;
            //获取当前控件的窗体
            Window targetWindow = Window.GetWindow(lv); //通过控件找窗体
            ProductsListWin plw = targetWindow as ProductsListWin;		//窗体类型转换

            GenFaMoel gfm = lv.SelectedItem as GenFaMoel;   //获取选中对象
            MyInfo<object>.GetInstance.CurrGfObj = gfm;     //保存当前对象


            //屏蔽事件
            if (e.ChangedButton == MouseButton.Right)   //如果是点击右键
            {
                plw.QContMenu.IsOpen = true;            //显示右键菜单
            }
            //MessageBox.Show(ProductsListWin.sync.ToString());
            //MessageBox.Show(MyInfo<object>.GetInstance.CurrPmObj.title.ToString());
        }


        

        //转链后的界面的功能
        //public static void TurnToCopyLogic(ListView sender, ProdListModel obj, TurnListModel tdm)
        public static void TurnToCopyLogic(params object[] parm)
        {
           // ProdListModel plm = obj;     //商品列表点击的对象
            TurnListModel tdo = parm[0] as TurnListModel;     //转链后的对象


            TurnTolinkWin ttw = new TurnTolinkWin();//初始化窗口

            ttw.Timage.Source = new BitmapImage(new Uri(tdo.pictUrl));    //加载图片

            //获取默认模版内容
           // string TempCont = GetOrSetTools<ProductsListWin>.GetDefTemp(sender).Trim();
            string TempCont = GetOrSetTools<ProductsListWin>.GetDefTemp().Trim();

            //处理转链后的数据(替换占位符) , 第一个为替换的模版内容 ,  参数2替换的对象
            TempCont = GetOrSetTools<TurnListModel>.GetAssocaObj(TempCont, tdo);


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
            Button but = obj as Button;
            Action action1 = () =>      //匿名方法
            {
                //创建或显示窗口
                //GenFaLogic();
               ButtLogic<Button>.ListViewLogic(GenFaLogic);  //添加转链后的数据
            };
            but.Dispatcher.BeginInvoke(action1);

        }




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
            MyInfo<object>.GetInstance.MYTEMPLATE = IOTools.ReadFile(DfiFilePath); 

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
            IOTools.WriteFile(TempPath, TextContent);

        }


        //刷新
        public static void SearchGoodsLogic(object obj)
        {
            //获取当前控件的窗体
            ProductsListWin pw =  GetOrSetTools<ProductsListWin>.GetParentWin(obj);

            string TexStr = pw.PtextBox.Text;
            if (TexStr != null)
            {
                //调用抽象接口 
                AbsInterfaces<ProdDataModel>.AppInfFun(HttpInterf.ShangPinLieBiao, ShowGoodsListSuccLogic,
                    TexStr, "1011", "1", "2", "15", MyInfo<object>.GetInstance.UID);
            }

        }


        //商品列表刷新按钮
        public static void VListRefreshLogic(object obj)
        {
                //调用抽象接口 
             AbsInterfaces<ProdDataModel>.AppInfFun(HttpInterf.ShangPinLieBiao, ShowGoodsListSuccLogic,
                    "", "1011", "1", "2", "15", MyInfo<object>.GetInstance.UID);
        }

      
        //商品列表显示
        public static void ShowGoodsListSuccLogic(MLoginJson<ProdDataModel> model)
        {
            MyInfo<object>.GetInstance.GoodsFlg = false;
            ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm");  //获取窗口


            List<TurnListModel> viewList = model.data.list; //获取显示的数据(数据存于基类)

            for (int i = 0; i < viewList.Count; i++)
            {
                viewList[i].BianHao = i + 1;
            }

            if (pw.PlistView.ItemsSource ==null)
            {
                pw.PlistView.ItemsSource = viewList;       //显示到ListView
            }
            else
            {
                pw.PlistView.Items.Refresh();   //刷新
            }
 

            MyInfo<object>.GetInstance.GoodsFlg = true;

        }

        //微信群添加功能
        public static void AddWeChatLogic(object obj)
        {
                Button but = obj as Button;

            Action action1 = () =>      //匿名方法
            {
                Window targetWindow = Window.GetWindow(but); //通过控件找窗体
                ProductsListWin pw = targetWindow as ProductsListWin;		//窗口类型转换
                string WCName = pw.MATextBox.Text;  //获取输入框里输入的微信群名称

                if (WCName =="" || WCName==null)
                {
                    MessageBox.Show("没有群名称");
                    return;
                }

                new WeChatModel(WCName);    //创建对象时关联
                List<WeChatModel> wl = MyInfo<object>.GetInstance.WeChatList;   //微信群
                string enstr = SerializationTools<List<WeChatModel>>.EnpJsonObj(wl);

                string TextFile = PathTools.LocalDataWeChatFile; //获取文件路径
                IOTools.WriteFile(TextFile, enstr); //在本地写入内容

                //因为前面已经加载了,所以这里只要刷新就可以
                pw.WClistView.Items.Refresh(); //刷新数据

             };
             but.Dispatcher.BeginInvoke(action1);
            // MessageBox.Show("添加微信群");
        }


        //群发助手,开始发送按钮逻辑
        public static void SendLinkImgLogic(object obj)
        {
            Button but = obj as Button;
            //ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm");

            List<WeChatModel> lst = MyInfo<object>.GetInstance.WeChatList;  //微信群列表
            List<GenFaMoel> gfl = MyInfo<object>.GetInstance.GenfaList;   //跟发列表

            string piurl = gfl[0].pictUrl; //图片地址,存到基类属性上面
            //BitmapImage Bmg = new BitmapImage(new Uri(gfl[0].pictUrl));    //加载图片
            //获取默认模版内容
            string TempCont = GetOrSetTools<ProductsListWin>.GetDefTemp().Trim();
            //匹配模板(替换占位符) , 第一个为替换的模版内容 ,  参数2替换的对象
            TempCont = GetOrSetTools<GenFaMoel>.GetAssocaObj(TempCont, gfl[0]);


         //   string ss = new WeChatInf().add_material();   


            int NumOf = 2;
            int time = 50;
            List<object> ParmList = new List<object>();
            ParmList.Add(but);
            ParmList.Add(NumOf);
            ParmList.Add(time);

            string HwndStr = string.Empty;


            for (int i = 0; i < lst.Count; i++)
            {
                //每次循环最好暂停一下
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                //查找指定窗口句柄
                HwndStr = WinHandle.AssignWinHwnd(lst[i].QunMingCheng);
                if (HwndStr == null || HwndStr == "")
                {
                    continue;
                }
                else
                {
                    // MessageBox.Show(HwndStr); //句柄显示
                   // poj.ParmArray[3] = HwndStr;
                    ParmList.Add(HwndStr);
                    ParmList.Add(TempCont);
                    ParmList.Add(piurl);

                    ParmObj poj = new ParmObj();
                    poj.ParmArray = ParmList.ToArray();
                    ThreadCls<object>.ThreadPoolFun(WinHandle.WeChatMainWinMsgSend, poj); //开启线程池
                }

            }

        }

        //微信群删除逻辑
        public static void DelWechatLogic(object obj)
        {
            Button but = obj as Button;

            Action action1 = () =>      //匿名方法
            {
                //获取当前控件绑定的参数
                int id = Convert.ToInt32(but.CommandParameter);
                MyInfo<WeChatModel>.LocalDelWeChatData(id); //删除微信群逻辑
            };
            but.Dispatcher.BeginInvoke(action1);
        }
        





 #region 右键子项功能



        //添加到跟发
        public static void AddGenFaLogic(object obj)
        {
            MenuItem mi = obj as MenuItem;
            Action action1 = () =>      //匿名方法
            {
                //创建或显示窗口
                //GenFaLogic();
               ButtLogic<MenuItem>.ListViewLogic(GenFaLogic);  //添加转链后的数据
            };
            mi.Dispatcher.BeginInvoke(action1);

            //params object[] parm

        }
        //添加到跟发
        public static void GenFaLogic(object[] obj)
        {

            //获取窗口
            ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm"); ;      
            //ProdListModel emp = MyInfo<object>.GetInstance.CurrPmObj;   //获取选中对象
           // ProdListModel emp = obj[0] as ProdListModel;   //获取选中对象
            TurnListModel tdo = obj[0] as TurnListModel;

            //构造函数自动关联对象,存储全部跟发信息

             new GenFaMoel(tdo);

            // MessageBox.Show( tdo.title);

            List<GenFaMoel> gf = MyInfo<object>.GetInstance.GenfaList;  //跟发群
            string enstr = SerializationTools<List<GenFaMoel>>.EnpJsonObj(gf);  //序列化

            string TextFile = PathTools.LocalDataGenFaFile; //获取跟发文件路径
            IOTools.WriteFile(TextFile, enstr); //在本地写入内容


            //显示到listview里
            pw.AddListView.ItemsSource = null;      //先清空
            pw.AddListView.ItemsSource = MyInfo<object>.GetInstance.GenfaList;

        }

        //标记已发
        public static void FlagYiFaLogic(object obj)
        {
            //获取当前控件的窗体
            ProductsListWin pw = GetOrSetTools<ProductsListWin>.GetItemParentWin(obj);
            //pw.AddGenFa = obj;
            //ListView lv = sender as ListView;
            //ProdListModel emp = lv.SelectedItem as ProdListModel;   //获取选中对象
            MessageBox.Show("标记已发");
        }

        //置顶
        public static void ZhiDingLogic(object obj)
        {
            MessageBox.Show("置顶");
        }

        public static void ShangYiLogic(object obj)
        {
            MessageBox.Show("上移");
        }       
        public static void XiaYiFaLogic(object obj)
        {
            MessageBox.Show("下移");
        }       
        public static void ChaKanNeiRongFaLogic(object obj)
        {
            MessageBox.Show("查看内容");
        }        
        public static void ShanChuGenFaFaLogic(object obj)
        {
            MessageBox.Show("删除跟发");
        }        
        public static void FaSongNeiRongFaLogic(object obj)
        {
            MessageBox.Show("发送内容");
        }
  #endregion


        

    }
}
