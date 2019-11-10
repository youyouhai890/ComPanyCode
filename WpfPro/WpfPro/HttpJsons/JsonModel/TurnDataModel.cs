using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static WpfPro.ManageAllCls.ManaEnumCls;

namespace WpfPro.HttpJsons.JsonModel
{

    //调用转链接口后用于接受的模型
    [DataContract]
      public  class TurnDataModel
    {
        public TurnDataModel() { }


        [DataMember]
        public string tkl { get;  set; }="";                 //淘口令,二合一链接
        [DataMember]
        public string shortLink { get;  set; }="";           //短链接
        [DataMember]
        public string longLink { get;  set; }="";            //长链接
        [DataMember]
        public string res1 { get;  set; }="";
        [DataMember]
        public string tklTips { get;  set; }="";             
        [DataMember]
        public object couponAmount { get; set; } = "";       //劵金额,劵面额
        [DataMember]
        public string clickUrl { get;  set; }="";            //应该是点击的url图片地址
        [DataMember]
        public string maxCommissionRate { get;  set; }="";      //佣金比例

        /// ///////////////////不是从接口里获取的/////////////////////

        [DataMember]
        public string createTime { get; set; } = "";      //
        [DataMember]
        public string title { get; set; } = "";      //标题



        public static string GetTrunLab(ETurnObj ETObj)
        {
                    string cont = string.Empty;
                    // 商品标题, 原价, 劵后价, 劵面额, 卖点, 二合一链接淘口令, 蘑菇街uid, 短链接,长连接
                    switch (ETObj)
                    {
                        case ETurnObj.商品标题:
                            cont =  "空";  
                            break;
                        case ETurnObj.原价:
                            cont =    "空";  
                            break;
                        case ETurnObj.劵后价:
                            cont =    "空";  
                            break;
                        case ETurnObj.劵面额:
                            cont =   "couponAmount";
                            break;
                        case ETurnObj.卖点:
                            cont =    "空";  
                            break;
                        case ETurnObj.淘口令:
                            cont =   "tkl";
                            break;
                        case ETurnObj.蘑菇街uid:
                            cont =    "空";  
                            break;
                        case ETurnObj.短链接:
                            cont =   "shortLink";
                            break;
                        case ETurnObj.长连接:
                            cont =   "longLink";
                            break;

                        default:
                            MessageBox.Show("找不到匹配项...");
                            break;
                    }

                
              return cont;


        }

    }



}
