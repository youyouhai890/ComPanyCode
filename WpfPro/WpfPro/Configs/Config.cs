using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using System.Data.SQLite;
using System.Windows;
using WpfPro.DataBase;
using System.Windows.Controls;
using System.IO;
using WpfPro.ManageAllCls;
using WpfPro.ToolsCls;
using WpfPro.HttpJsons;
using WpfPro.Local.LocalModel;

namespace WpfPro.Configs
{
    class Config
    {
        public static string StartPath = "";                //程序启动路径
        public static string UserName = "";                 //登录用户名
        public static string DBHost = "";                   //本地数据库主机名

        public static string HttpInfConfPath = "";                   

        #region 单例模式

        //private static Config _instance = null;
        //private static readonly object SynObject = new object();

        //Config() { }

        ///// <summary>
        ///// Gets the instance.
        ///// </summary>
        //public static Config GetInstance
        //{
        //    get
        //    {
        //        // Syn operation.
        //        lock (SynObject)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new Config();
        //            }

        //            return _instance;
        //        }
        //    }
        //}

        #endregion

        public static void AllEnvirConfInit()
        {
            GetDeFineTempl(); //默认模板
            HttpInfConfPath = PathTools.HttpInfPath; //把接口文件拷贝到cofnig里
            AllInterfaceCls.InitInfs(HttpInfConfPath);   //初始化接口

            //读取本地数据
            CofLocalData();
        }






        public static void GetDeFineTempl()
        {
            //获取配置文件路径
            string PathConfig = PathTools.DebugConf;
            //默认模板文件路径
            string DfiFilePath = PathConfig + "DefineTemplate.txt";

            //检测默认模版DefineTemplate是否存在,没有重新创建并指定第一个模板为默认模板
            if (!File.Exists(DfiFilePath))
            {
                //读取内容
                ToolsCls.IOTools.WriteFile(DfiFilePath, "1");
                MyInfo.GetInstance.MYTEMPLATE = ToolsCls.IOTools.ReadFile(DfiFilePath); //保存默认模板信息到我的数据里
            }
            else
            {
                MyInfo.GetInstance.MYTEMPLATE = ToolsCls.IOTools.ReadFile(DfiFilePath); //如果已存在直接读取
            }

        }


        //初始化时的默认模版
        public static string SetTemplateText_1()
        {
            string TempText = string.Empty;

            //Windows能够显示的换行必须由两个字符组成："\r\n",要确保让换行效果在各种平台上都能够正常的显示用Environment.NewLine
            TempText = @"    {title}" + Environment.NewLine + Environment.NewLine +        //换行
                       @" ---------------------" + Environment.NewLine +
                       @" ★原*价{zkPrice}元" + Environment.NewLine +               //ProdListModel
                       @" ★劵*后价{afterCouponPrice}元" + Environment.NewLine +     //ProdListModel
                       @" ★劵*面额{couponAmount}" + Environment.NewLine +          //TurnDataModel
                       @" ★卖*点{extendDesc}" + Environment.NewLine +             //ProdListModel
                       @" ★二合一链接{tkl}" + Environment.NewLine +          //TurnDataModel
                       @" ★短链接{shortLink}" + Environment.NewLine;       //TurnDataModel

            return TempText;
        }

        //读取本地数据
        public static void CofLocalData()
        {
            string gf = string.Empty;
                gf=IOTools.WRLoc(PathTools.LocalDataGenFaFile);    //跟发
            if (gf==null || gf=="")
            {
                MyInfo.GetInstance.GenfaList = new List<GenFaMoel>();//如果没有数据初始化
            }
            else
            {
                 MyInfo.GetInstance.GenfaList = SerializationTools<List<GenFaMoel>>.LocRevJsonObj(gf); //读取跟发列表
            }


            string wx = string.Empty;
            wx = IOTools.WRLoc(PathTools.LocalDataWeChatFile);   //微信群
            if (wx==null || wx=="")     
            {
                MyInfo.GetInstance.WeChatList = new List<WeChatModel>();    //如果没有数据初始化
            }
            else
            {
                MyInfo.GetInstance.WeChatList = SerializationTools<List<WeChatModel>>.LocRevJsonObj(wx); //读取微信群列表
            }

        }



    }
}
