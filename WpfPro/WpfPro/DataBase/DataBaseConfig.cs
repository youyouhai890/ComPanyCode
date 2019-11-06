using Common.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.DataBase
{
    class DataBaseConfig
    {
        public static string StartPath = "";                //程序启动路径
        public static string UserName = "";                 //登录用户名
        public static string DBHost = "";                   //本地数据库主机名
        public static string DBName = "";                   //本地数据库名
        public static string DBUser = "";                   //本地数据库账号
        public static string DBPswd = "";                   //本地数据库密码
        public static string CodeUrl = "";
        public static string DataUrl = "";                  //服务器域名
        public static string LogPath = "";                  //日志文件路径
        public static string DataPath = "";                 //导出文件路径
        public static string PestAddr = "";
        public static string SqlConnect = "";               //数据库连接字符串
        public static string OrganCode = "";


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





        public static void ReadConfig()
        {
            string path = StartPath + "\\Configs\\Config.ini";
            CfgHelper config = new CfgHelper(path);
            DBHost = config.GetField("DBHost");
            DBName = config.GetField("DBName");
            DBUser = config.GetField("DBUser");
            DBPswd = config.GetField("DBPswd");
            CodeUrl = config.GetField("CodeUrl");
            DataUrl = config.GetField("DataUrl");
            PestAddr = config.GetField("PestAddr");

        }

        public static void LoginInit()
        {
            SQLiteConnection conn = new SQLiteConnection(getSQLiteConn());
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS tbl_token_look_up(tq_id varchar(70) primary key , tq_token varchar(20),created_time datetime);"
                + "CREATE TABLE IF NOT EXISTS tbl_config_keys(config_key varchar(80) , config_value varchar(2048),created_time datetime);"
                + "CREATE TABLE IF NOT EXISTS tbl_goods_hub(tq_id varchar(128) primary key , tp_title varchar(512), zkPrice varchar(20),couponAmount varchar(20),couponStartFee varchar(20),maxRate varchar(20),campaignId varchar(20),extendDesc varchar(2048),originalMsg varchar(2048),imgPath varchar(1024),afterCouponPrice varchar(20),userType varchar(20),auctionId varchar(48),shopTitle varchar(512),pictUrl varchar(1024),biz30Day varchar(20),auctionUrl varchar(1024),couponUrl varchar(1024),created_time datetime);";
            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //获取程序启动路径
        public static String getSQLiteConn()
        {
            //AppDomain.CurrentDomain.BaseDirectory获取程序启动路径
            return "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\db_data.db3;Pooling=true;FailIfMissing=false";  //获取绝对路径，看好了，别搞错了
            //return "Data Source=db_data.db3;Pooling=true;FailIfMissing=false";  //获取绝对路径，看好了，别搞错了
        }



    }
}
