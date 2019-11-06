using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPro.ManageAllCls
{
   public  class ManaEnumCls
    {


        //[Display(Name = "接口")]
        public enum HttpInterf
        {
            //[Display(Name = "验证码")]
            YanZhengMa,
            ZhuCe, DengLu, HuoQuPID, MiMaChongZhi, ZengJiaWeiXinQQqun, QunLieBiao,
            ShanChuQunLieBiao, SheZhiPid, ShangPinLieBiao, ZhuanLianJieKou, SouQuan
        };




        //用于接口
        public  enum ETurnObj {
           商品标题, 原价, 劵后价, 劵面额, 卖点, 淘口令, 蘑菇街uid, 短链接,长连接
        }



    }

}
