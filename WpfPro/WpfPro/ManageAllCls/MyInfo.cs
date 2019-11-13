using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WpfPro.Forms.Products;
using WpfPro.HttpJsons;
using WpfPro.HttpJsons.JsonModel;
using WpfPro.Local.LocalModel;
using WpfPro.ToolsCls;


namespace WpfPro.ManageAllCls
{
    //多线程单例
    class MyInfo<T> where T :  class 
    {
        private static MyInfo<T> _instance = null;
        private static readonly object SynObject = new object();

        private static string _phone;
        private static string _uid;
        private static string _pid;
        private static string _mytemplate;     //模板
        private static List<ProdListModel> _prollistview;   //用于商品列表显示
        public bool GoodsFlg = false;      //商品列表的标志 , true时才能访问,因为加载列表时莫名的自动点击所以加了这个

        private int _currid=1;
        private TurnListModel _pmobj;       //爆款当前点击的列表项对象
        private GenFaMoel _gfobj;       //群发当前点击的列表项对象
        public List<GenFaMoel> GenfaList = new List<GenFaMoel>();       //群发助手里跟发列表
        public List<WeChatModel> WeChatList = new List<WeChatModel>();      



        MyInfo()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static MyInfo<T> GetInstance
        {
            get
            {
                // Syn operation.
                lock (SynObject)
                {
                    if (_instance ==null)
                    {
                        _instance = new MyInfo<T>();
                    }

                    return _instance;
                }
            }
        }


        public  int CurrId
        {
            set
            {
                _currid = value;
            }
            get
            {
                return _currid++;
            }
        }

        public  TurnListModel CurrPmObj
        {
            set
            {
                _pmobj = value;
            }
            get
            {
                return  _pmobj;
            }
        }

        public GenFaMoel CurrGfObj
        {
            set
            {
                _gfobj = value;
            }
            get
            {
                return _gfobj;
            }
        }


        //用户id,登陆时获取
        public string PHONE
        {
            set
            {
                _phone = value;
            }
            get
            {
                return _phone;
            }
        }

        //用户id,登陆时获取
        public  string UID
        {
            set
            {
                _uid = value;
            }
            get
            {
                return _uid;
            }
        }
        public string PID
        {
            set
            {
                _pid = value;
            }
            get
            {
                return _pid;
            }
        }

        public List<ProdListModel> PRODLISTLIEW
        {
            set
            {
                _prollistview = value;
            }
            get
            {
                return _prollistview;
            }
        }

        
        public string MYTEMPLATE
        {
            set
            {
                _mytemplate = value;
            }
            get
            {
                return _mytemplate;
            }
        }

        //删除微信群数据
        public static void  LocalDelWeChatData(int Did)
        {
            List<WeChatModel> wlis = MyInfo<object>.GetInstance.WeChatList;
            //根据ID删除
            wlis.Remove(wlis[Did-1]);   //删除的时候实际索引为 id-1

            for (int i = 0; i < wlis.Count; i++)
            {
                wlis[i].id = i + 1;     //重新遍历编号 , 要不然删除时报错
            }

            ProductsListWin pw = ManWinCls<ProductsListWin>.GetWin("ProdWinForm");

            string enstr = SerializationTools<List<WeChatModel>>.EnpJsonObj(wlis);

            string TextFile = PathTools.LocalDataWeChatFile; //获取文件路径
            IOTools.WriteFile(TextFile, enstr); //在本地写入内容

            pw.WClistView.Items.Refresh(); //刷新数据
        }

    }
}
