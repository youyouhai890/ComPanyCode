using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfPro.HttpJsons;
using static WpfPro.HttpJsons.AllInterfaceCls;
using static WpfPro.ManageAllCls.ManaEnumCls;

namespace WpfPro.TestCode
{
    class TestCls
    {
        static string RetCont = String.Empty;

        //flag表示是否测试,false表示不测试 , TestContent表示要转换的接口字符串
        //参数#为替换符号, //pm[i]1代表电话 , #2代表密码 , #3代表替换的新密码 , #4代表账号(uid) 
        //, #5代表groupName , #6代表验证码 , #7代表pid(推广位,授权得到) ，#8 id(删除群的唯一标识) , #9 预留 ,#10 授权地址
        //
        //public static string FlagTest(string flag , string TestContent,
        //                            string pm[i]1, string pm[i]2, string pm[i]3, string pm[i]4, string pm[i]5, 
        //                            string pm[i]6, string pm[i]7, string pm[i]8, string pm[i]9, string pm[i]10,
        //                            string pm[i]11, string pm[i]12, string pm[i]13, string pm[i]14, string pm[i]15,
        //                            string pm[i]16, string pm[i]17, string pm[i]18, string pm[i]19, string pm[i]20,
        //                            string pm[i]21
        public static string FlagTest(string flag, HttpInterf inf, params string[] pram) //params可变参数
        {
           string RetCont = string.Empty;
            //获取对应接口的内容
            switch (inf)
            {

                case    HttpInterf.YanZhengMa:
                                         RetCont = HttpJsons.AllInterfaceCls.YanZhengMa;
                                   break;
                case    HttpInterf.ZhuCe:
                                         RetCont = HttpJsons.AllInterfaceCls.ZhuCe;
                                   break;
                case    HttpInterf.DengLu:
                                         RetCont = HttpJsons.AllInterfaceCls.DengLu;
                                   break;
                case    HttpInterf.HuoQuPID:
                                         RetCont = HttpJsons.AllInterfaceCls.HuoQuPID;
                                   break;
                case    HttpInterf.MiMaChongZhi:
                                         RetCont = HttpJsons.AllInterfaceCls.MiMaChongZhi;
                                   break;
                case    HttpInterf.ZengJiaWeiXinQQqun:
                                         RetCont = HttpJsons.AllInterfaceCls.ZengJiaWeiXinQQqun;
                                   break;
                case    HttpInterf.QunLieBiao:
                                          RetCont = HttpJsons.AllInterfaceCls.QunLieBiao;
                                   break;
                case    HttpInterf.ShanChuQunLieBiao:
                                          RetCont = HttpJsons.AllInterfaceCls.ShanChuQunLieBiao;
                                   break;
                case    HttpInterf.SheZhiPid:
                                        RetCont = HttpJsons.AllInterfaceCls.SheZhiPid;
                                   break;
                case    HttpInterf.ShangPinLieBiao:
                                         RetCont = HttpJsons.AllInterfaceCls.ShangPinLieBiao;
                                   break;
                case    HttpInterf.ZhuanLianJieKou:
                                       RetCont = HttpJsons.AllInterfaceCls.ZhuanLianJieKou;
                                   break;
                case    HttpInterf.SouQuan:
                                       RetCont = HttpJsons.AllInterfaceCls.SouQuan;
                                    break;
                default:    
                          MessageBox.Show("接口找不到匹配项...");
                                    break;
            }


            if (flag.Trim() == "true" || flag.Trim() == "TRUE")     //测试的时候
            {
                RetCont= TestInterf(RetCont);   //测试接口
            }
            else                  //正规运行的接口
            {

                RetCont= XunHuanPram(inf,  RetCont, pram);  //非测试接口

            }

            //MessageBox.Show("接口内容为-------"+ RetCont);

            return RetCont;
        }


        //非测试接口
        public static string XunHuanPram(HttpInterf IntInf, string content , string[] pm)
        {

            switch (IntInf)
            {
                //注意接口必须按照顺序写入参数
                case HttpInterf.YanZhengMa:
                            if (pm.Length==1)   //参数数量
                            {
                                  content = content.Replace("#pho", pm[0]);  //#15 页码
                            }
                            else
                            {
                                MessageBox.Show("接口参数不正确");
                            }
                    break;
                case HttpInterf.ZhuCe:
                            if (pm.Length == 3)
                            {
                                content = content.Replace("#pho", pm[0]);  //电话
                                content = content.Replace("#pwd", pm[1]);  //密码
                                content = content.Replace("#vfCode", pm[2]);  //检验码
                            }
                            else
                            {
                                MessageBox.Show("接口参数不正确");
                            }
                    break;
                case HttpInterf.DengLu:
                                    if (pm.Length == 2)
                                    {
                                        content = content.Replace("#pho", pm[0]);  //账号,电话
                                        content = content.Replace("#pwd", pm[1]);  //密码
                                    }
                                    else
                                    {
                                        MessageBox.Show("接口参数不正确");
                                    }
                    break;
                case HttpInterf.HuoQuPID:
                                if (pm.Length == 1)
                                {
                                    content = content.Replace("#uid", pm[0]);  //uid
                                }
                                else
                                {
                                    MessageBox.Show("接口参数不正确");
                                }
                    break;
                case HttpInterf.MiMaChongZhi:
                            if (pm.Length == 3)
                            {
                                content = content.Replace("#pho", pm[0]);  //电话
                                content = content.Replace("#newpwd", pm[1]);  //重新设置的密码
                                content = content.Replace("#vfCode", pm[2]);  //检验码
                            }
                            else
                            {
                                MessageBox.Show("接口参数不正确");
                            }
                    break;
                case HttpInterf.ZengJiaWeiXinQQqun:
                                if (pm.Length == 2)
                                {
                                    content = content.Replace("#uid", pm[0]);  //uid
                                    content = content.Replace("#grName", pm[1]);  //groupName
                                 }
                                else
                                {
                                    MessageBox.Show("接口参数不正确");
                                }
                    break;
                case HttpInterf.QunLieBiao:
                                 RetCont = HttpJsons.AllInterfaceCls.QunLieBiao;
                    break;
                case HttpInterf.ShanChuQunLieBiao:
                                if (pm.Length == 1)
                                {
                                    content = content.Replace("#id", pm[0]);  //id
                                }
                                else
                                {
                                    MessageBox.Show("接口参数不正确");
                                }

                                break;
                case HttpInterf.SheZhiPid:
                            if (pm.Length == 2)
                            {
                                content = content.Replace("#uid", pm[0]);  //uid
                                content = content.Replace("#pid", pm[1]);  //pid
                            }
                            else
                            {
                                MessageBox.Show("接口参数不正确");
                            }
                    break;
                case HttpInterf.ShangPinLieBiao:
                            if (pm.Length == 6)
                            {
                                content = content.Replace("#keyword", pm[0]);  //keyword
                                content = content.Replace("#indexTypeValue", pm[1]);  //indexTypeValue
                                content = content.Replace("#sortType", pm[2]);  //sortType
                                content = content.Replace("$page", pm[3]);  //page
                                content = content.Replace("#pageSize", pm[4]);  //pageSize
                                content = content.Replace("#uid", pm[5]);  //uid
                            }
                            else
                            {
                                MessageBox.Show("接口参数不正确");
                            }
                    break;
                case HttpInterf.ZhuanLianJieKou:
                                if (pm.Length == 7)
                                {
                                    content = content.Replace("#uid", pm[0]);  //uid
                                    content = content.Replace("#auctionId", pm[1]);  //auctionId
                                    content = content.Replace("#activityId", pm[2]);  //auctionId
                                    content = content.Replace("#couponAmount", pm[3]);  //couponAmount
                                    content = content.Replace("#pictUrl", pm[4]);  //pictUrl
                                    content = content.Replace("#title", pm[5]);  //title
                                    content = content.Replace("#zkPrice", pm[6]);  //zkPrice
                                }
                                else
                                {
                                    MessageBox.Show("接口参数不正确");
                                }
                    break;
                case HttpInterf.SouQuan:
                            if (pm.Length == 1)
                            {
                                content = content.Replace("{state}", pm[0]);  //PID截取的第一段
                            }
                            else
                            {
                                MessageBox.Show("接口参数不正确");
                            }
                    break;
                default:
                    MessageBox.Show("接口找不到匹配项...");
                    break;
            }

            #region  循环查找

       //     for (int i = 0; i < pm.Length; i++)
          //  {
                // content = content.Replace("#pho", pm[i]);  //3 电话
                // content = content.Replace("#pwd", pm[i]);  //4 密码
                // content = content.Replace("#newpwd", pm[i]);  //要替换的密码
                // content = content.Replace("#uid", pm[i]);  //5 uid账号
                // content = content.Replace("#grName", pm[i]);  //6 groupName
                // content = content.Replace("#vfCode", pm[i]);  //7 verfiyCode
                // content = content.Replace("#pid", pm[i]);  //8 PID(推广位),授权获取
                // content = content.Replace("#id", pm[i]);  //9 id(删除群的唯一标识)
                // content = content.Replace("#预留", pm[i]);  //#10  预留
                //  content = content.Replace("{state}", pm[i]);  //#11 授权

                // //商品列表参数
                //content = content.Replace("#keyword", pm[i]);  //#12 查询关键字
                //content = content.Replace("#indexTypeValue", pm[i]);  //13   实时爆款单
                //content = content.Replace("#sortType", pm[i]);  //#14 默认排序
                //content = content.Replace("$page", pm[i]);  //#15 页码
                //content = content.Replace("#pageSize", pm[i]);  //#16 页数

                // //转链用的参数
                // content = content.Replace("#auctionId", pm[i]);  //#17 用于转联 , 商品id
                // content = content.Replace("#activityId", pm[i]);  //#18 用于转联
                // content = content.Replace("#couponAmount", "10");  //#19 用于转联 , 优惠劵金额
                // content = content.Replace("#pictUrl", pm[i]);  //#20 用于转联
                // content = content.Replace("#title", pm[i]);  //#21 用于转联
                // content = content.Replace("#zkPrice", pm[i]);  //#22 用于转联 , 原价
           // }
            #endregion

            return content;

        }


        //测试接口
        public static string TestInterf(string Resu)
        {
            Resu = Resu.Replace("#pho", "15950910338");  //3 电话
            Resu = Resu.Replace("#pwd", "123456");  //4 密码
            Resu = Resu.Replace("#newpwd", "abcdef");  //要替换的密码
            Resu = Resu.Replace("#uid", "8a2efddc6db97f25016dcd6d6611054c");  //5 uid账号
            Resu = Resu.Replace("#grName", "SHLQun");  //6 groupName
            Resu = Resu.Replace("#vfCode", "299824");  //7 verfiyCode
            Resu = Resu.Replace("#pid", "mm_628070189_977900267_109621300070");  //8 PID(推广位),授权获取
            Resu = Resu.Replace("#id", "8a2efddc6db97f25016dcd6d6611054c");  //9 id(删除群的唯一标识)
            Resu = Resu.Replace("#预留", "299824");  //#10  预留
            Resu = Resu.Replace("{state}", "628070189@628070189");  //#11 授权

           //商品表参参数
            Resu = Resu.Replace("#keyword", "");  //#12 查询关键字
            Resu = Resu.Replace("#indexTypeValue", "1011");  //13   实时爆款单
            Resu = Resu.Replace("#sortType", "1");  //#14 默认排序
            Resu = Resu.Replace("$page", "1");  //#15 页码
            Resu = Resu.Replace("#pageSize", "10");  //#16 页数


            //转链参数
            Resu = Resu.Replace("#auctionId", "571725923176");  //#17 用于转联 , 商品id
            Resu = Resu.Replace("#activityId", "5b929637570b4a85b581785548f00c15");  //#18 用于转联
            Resu = Resu.Replace("#couponAmount","10");  //#19 用于转联 , 优惠劵金额
            Resu = Resu.Replace("#pictUrl", "https://gd4.alicdn.com/imgextra/i4/2972508842/O1CN01G91Fcr2FBgXGtsU4A_!!2972508842.jpg");  //#20 用于转联
            Resu = Resu.Replace("#title", "青蛙吃豆子玩具网红爆款亲子互动桌游儿童益智大号趣味创意解压");  //#21 用于转联
            Resu = Resu.Replace("#zkPrice", "32");  //#22 用于转联 , 原价

            //  MessageBox.Show("测试接口内容为---" + RetCont);
            return Resu;
        }



    }
}
