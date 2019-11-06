using Common.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfPro.ToolsCls;

namespace WpfPro.HttpJsons
{
    class AllInterfaceCls
    {
        public static string TestFlag = "";                //测试标志

        public static string YanZhengMa = "";           
        public static string ZhuCe = "";                
        public static string DengLu = "";
        public static string HuoQuPID = "";
        public static string MiMaChongZhi = "";         
        public static string ZengJiaWeiXinQQqun = "";   
        public static string QunLieBiao = "";                       
        public static string ShanChuQunLieBiao = "";
        public static string SheZhiPid = "";
        public static string ShangPinLieBiao = "";
        public static string ZhuanLianJieKou = "";
        public static string SouQuan = "";

        //转链
        public static string auctionId = "";
        public static string activityId = "";
        public static string couponAmount = "";
        public static string pictUrl = "";
        public static string title = "";
        public static string zkPrice = "";

        //-----------------------------



        public static void InitInfs(string path)
        {



            CfgHelper config = new CfgHelper(path); //获取文件信息
            TestFlag = config.GetField("TestFlag"); //测试标志

            YanZhengMa = config.GetField("YanZhengMa");
            ZhuCe = config.GetField("ZhuCe");
            DengLu = config.GetField("DengLu");
            HuoQuPID = config.GetField("HuoQuPID");
            MiMaChongZhi = config.GetField("MiMaChongZhi");
            ZengJiaWeiXinQQqun = config.GetField("ZengJiaWeiXinQQqun");
            QunLieBiao = config.GetField("QunLieBiao");
            ShanChuQunLieBiao = config.GetField("ShanChuQunLieBiao");

            SheZhiPid = config.GetField("SheZhiPid");
            ShangPinLieBiao = config.GetField("ShangPinLieBiao");
            ZhuanLianJieKou = config.GetField("ZhuanLianJieKou");
            SouQuan = config.GetField("SouQuan");
        }





    }
}
